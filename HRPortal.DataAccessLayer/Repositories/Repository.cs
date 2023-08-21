using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Repositories {
    public class Repository<T, TDto, TCreation> : IRepository<T, TDto, TCreation> where T : BaseModel {
        private readonly HRPortalContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public Repository(HRPortalContext context, IMapper mapper) {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public async Task Create(TCreation entity) {
            var entityToCreate = _mapper.Map<T>(entity);
            _dbSet.Add(entityToCreate);
        }

        public async Task Delete(Guid id) {
            var entityToDelete = await _dbSet.FindAsync(id);
            entityToDelete.Status = 0;
        }

        public async Task<IEnumerable<TDto>> GetAll() {
            var entitiesToGet = await _dbSet.Where(x => x.Status == 1).ToListAsync();
            var entitiesToReturn = _mapper.Map<IEnumerable<TDto>>(entitiesToGet);
            return entitiesToReturn;
        }

        public Task<T> GetById(Guid id) {
            var entityToGet = _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return entityToGet;

        }

        public async Task Update(Guid id, TCreation entity) {
            _dbSet.Update(_mapper.Map<T>(entity));
        }
    }
}
