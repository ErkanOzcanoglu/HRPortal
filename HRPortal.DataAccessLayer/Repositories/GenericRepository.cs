using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Repositories {
    public class GenericRepository<T, TDto, TCreate> : IRepository<T, TDto, TCreate> where T : BaseModel {
        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public GenericRepository(HRPortalContext context, IMapper mapper) {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task Create(TCreate entity) {
            var entityToCreate = _mapper.Map<T>(entity);
            await _dbSet.AddAsync(entityToCreate);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task Delete(Guid id) {
            var entityToDelete = await _dbSet.FindAsync(id);
            entityToDelete.Status = 0;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TDto>> GetAllAsync() {
            var entitiesToGet = await _dbSet.Where(x => x.Status == 1).ToListAsync();
            var entitiesToReturn = _mapper.Map<IEnumerable<TDto>>(entitiesToGet);
            return entitiesToReturn;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetAsync(Guid id) {
            var entityToGet = await _dbSet.FindAsync(id);
            return entityToGet;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        public async Task Update(Guid id, TDto entity) {
            var entityToUpdate = await _dbSet.FindAsync(id);
            entityToUpdate = _mapper.Map<T>(entity);
        }
    }
}
