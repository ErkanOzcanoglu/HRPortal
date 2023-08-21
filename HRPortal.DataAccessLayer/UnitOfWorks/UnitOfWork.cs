using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.Repositories;
using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using HRPortal.Entities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.UnitOfWorks {
    public class UnitOfWork<T,TDto,TCreation> : IUnitOfWork<T,TDto,TCreation> where T: BaseModel {
        private readonly HRPortalContext _context;

        public UnitOfWork(HRPortalContext context, IMapper mapper) {
            _context = context;
            Repository = new Repository<T, TDto, TCreation>(_context, mapper);
        }

        public IRepository<T, TDto, TCreation> Repository { get; }

        public int SaveChanges() {
            return _context.SaveChanges();
        }
    }
}
