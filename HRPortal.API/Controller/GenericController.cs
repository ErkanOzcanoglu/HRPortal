using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
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
    public class GenericController<T, TDto, TCreate> : ControllerBase, IRepository<T, TDto, TCreate> where T: BaseModel{

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork<T, TDto, TCreate> _unitOfWork;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericController{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public GenericController(HRPortalContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
            _unitOfWork = new UnitOfWork<T, TDto, TCreate>(_context, mapper);
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [HttpPost]
        public async Task Create(TCreate entity) {
            await _unitOfWork.Repository.Create(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public async Task Delete(Guid id) {
            await _unitOfWork.Repository.Delete(id);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<TDto>> GetAllAsync() {
            return await _unitOfWork.Repository.GetAllAsync();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<T> GetAsync(Guid id) {
            return await _unitOfWork.Repository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task Update(Guid id, TDto entity) {
            await _unitOfWork.Repository.Update(id, entity);
            _context.SaveChanges();
        }
    }
}
