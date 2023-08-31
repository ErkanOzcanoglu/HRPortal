using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.Repositories;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.UnitOfWorks {
    public class UnitOfWork<T, TDto, TCreate> : IUnitOfWork<T, TDto, TCreate> where T: BaseModel {

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{T, TDto}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public UnitOfWork(HRPortalContext context, IMapper mapper) {
            _context = context;
            Repository = new GenericRepository<T, TDto, TCreate>(_context, mapper);
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public IRepository<T, TDto, TCreate> Repository { get; }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose() {
            _context.Dispose();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() {
            return _context.SaveChanges();
        }
    }
}
