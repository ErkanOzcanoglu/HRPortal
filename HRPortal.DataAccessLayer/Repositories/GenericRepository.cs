using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Repositories {
    public class GenericRepository<T, TDto, TCreate, TUpdate> : IRepository<T, TDto, TCreate, TUpdate> where T : BaseModel {
        private readonly HRPortalContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public GenericRepository(HRPortalContext context, IMapper mapper) {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }
        public async Task<ActionResult<string>> Create(TCreate entity) {
            var entityToCreate = _mapper.Map<T>(entity);
            await _dbSet.AddAsync(entityToCreate);
            return new OkObjectResult(entityToCreate);
        }
        public async Task<ActionResult<string>> Delete(Guid id) {
            var entityToDelete = await _dbSet.FindAsync(id);
            entityToDelete.Status = 0;
            return new OkObjectResult(entityToDelete);
        }
        public async Task<IEnumerable<TDto>> GetAllAsync() {
            var entitiesToGet = await _dbSet.Where(x => x.Status == 1).ToListAsync();
            var entitiesToReturn = _mapper.Map<IEnumerable<TDto>>(entitiesToGet);
            return entitiesToReturn;
        }
        public async Task<T> GetAsync(Guid id) {
            var entityToGet = await _dbSet.FindAsync(id);
            return entityToGet;
        }
        public async Task<ActionResult<string>> Update(Guid id, TUpdate entity) {
            var existingEntity = await _dbSet.FindAsync(id);

            if (existingEntity == null) {
                return new NotFoundObjectResult("Entity not found");
            }

            _mapper.Map(entity, existingEntity);
            await _context.SaveChangesAsync();
            return new OkObjectResult(existingEntity);
        }

    }
}
