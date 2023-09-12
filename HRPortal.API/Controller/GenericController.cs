using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using HRPortal.Services.CacheServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HRPortal.API.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TDto, TCreate, TUpdate> : ControllerBase, IRepository<T, TDto, TCreate, TUpdate> where T: BaseModel{
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<T, TDto, TCreate, TUpdate> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<T> _dbSet;
        private readonly ICacheService _cacheService;

        public GenericController(HRPortalContext context, IMapper mapper, ICacheService cacheService) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
            _cacheService = cacheService;
            _unitOfWork = new UnitOfWork<T, TDto, TCreate, TUpdate>(_context, mapper);
        }

        [HttpPost]
        public async Task<ActionResult<TDto>> Create(TCreate entity) {
            try {
                var cacheData = _cacheService.GetData<ActionResult<TDto>>("string");
                if (cacheData != null) {
                    return cacheData;
                }

                await _unitOfWork.Repository.Create(entity);
                _unitOfWork.SaveChanges();
                var expiryTime = DateTime.Now.AddSeconds(1);
                _cacheService.SetData<ActionResult<TDto>>("string", cacheData, expiryTime);
                return cacheData;
                //await _unitOfWork.Repository.Create(entity);
                //_unitOfWork.SaveChanges();
                //return Ok("Creation Complete");
            } catch (Exception ex) {
                return Ok("Creation Incomplete");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            var cacheData = _cacheService.GetData<IEnumerable<TDto>>("TDto");
            if (cacheData != null && cacheData.Count() > 0) {
                return Ok(cacheData);
            }
            await _unitOfWork.Repository.Delete(id);
            _unitOfWork.SaveChanges();

            var expiryTime = DateTime.Now.AddSeconds(1);
            _cacheService.SetData<IEnumerable<TDto>>("TDto", cacheData, expiryTime);
            return Ok(cacheData);
        }

        [HttpGet]
        public async Task<IEnumerable<TDto>> GetAllAsync() {
            //return await _unitOfWork.Repository.GetAllAsync();
            var cacheData = _cacheService.GetData<IEnumerable<TDto>>("TDto");
            if(cacheData != null && cacheData.Count() > 0) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.GetAllAsync();

            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<TDto>>("TDto", cacheData, expiryTime);
            return cacheData;
        }

        [HttpGet("{id}")]
        public async Task<T> GetAsync(Guid id) {
            var cacheData = _cacheService.GetData<T>("T");
            if (cacheData != null) {
                return cacheData;
            }

            cacheData = await _unitOfWork.Repository.GetAsync(id);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<T>("T", cacheData, expiryTime);
            return cacheData;
            //return await _unitOfWork.Repository.GetAsync(id);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(Guid id, TUpdate entity) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("string");
            if (cacheData != null) {
                return cacheData;
            }

            await _unitOfWork.Repository.Update(id, entity);
            _unitOfWork.SaveChanges();
            var expiryTime = DateTime.Now.AddSeconds(1);
            _cacheService.SetData<ActionResult<string>>("string", cacheData, expiryTime);
            return cacheData;
            //await _unitOfWork.Repository.Update(id, entity);
            //_unitOfWork.SaveChanges();
            //return Ok("Update Complete");
        }
    }
}
