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
    public class UnitOfWork<T, TDto, TCreate, TUpdate> : IUnitOfWork<T, TDto, TCreate, TUpdate> where T: BaseModel {
        private readonly HRPortalContext _context;
        public UnitOfWork(HRPortalContext context, IMapper mapper) {
            _context = context;
            Repository = new GenericRepository<T, TDto, TCreate, TUpdate>(_context, mapper);
        }

        public IRepository<T, TDto, TCreate, TUpdate> Repository { get; private set; }

        public int SaveChanges() {
            return _context.SaveChanges();
        }
    }
}
