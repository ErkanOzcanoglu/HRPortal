using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using HRPortal.Entities.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T,TDto,TCreation> : ControllerBase , IRepository<T,TDto,TCreation> where T: BaseModel {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<T, TDto, TCreation> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<T> _dbSet;

        public GenericController(HRPortalContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
            _unitOfWork = new UnitOfWork<T, TDto, TCreation>(_context, mapper);
        }

        [HttpGet]
        public async Task<IEnumerable<TDto>> GetAll() {
           return await _unitOfWork.Repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<T> GetById(Guid id) {
            return await _unitOfWork.Repository.GetById(id);
        }

        [HttpPost]
        public async Task Create(TCreation entity) {
            await _unitOfWork.Repository.Create(entity);
             _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public async Task Update(Guid id, TCreation entity) {
            await _unitOfWork.Repository.Update(id, entity);
             _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id) {
            await _unitOfWork.Repository.Delete(id);
             _context.SaveChanges();
        }
    }
}
