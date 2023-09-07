using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
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

        public GenericController(HRPortalContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
            _unitOfWork = new UnitOfWork<T, TDto, TCreate, TUpdate>(_context, mapper);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(TCreate entity) {
            try {
                await _unitOfWork.Repository.Create(entity);
                _unitOfWork.SaveChanges();
                return Ok("Creation Complete"); 
            } catch (Exception ex) {
                return Ok("Creation Incomplete");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            await _unitOfWork.Repository.Delete(id);
            _unitOfWork.SaveChanges();
            return Ok("Deletion Complete");
        }

        [HttpGet]
        public async Task<IEnumerable<TDto>> GetAllAsync() {
            return await _unitOfWork.Repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<T> GetAsync(Guid id) {
            return await _unitOfWork.Repository.GetAsync(id);
        }

        [HttpGet("asd")]
        public Task<IEnumerable<EmployeeDto>> GetManyAsync(Guid id) {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(Guid id, TUpdate entity) {
            await _unitOfWork.Repository.Update(id, entity);
            _unitOfWork.SaveChanges();
            return Ok("Update Complete");
        }
    }
}
