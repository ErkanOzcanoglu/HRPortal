using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.UnitOfWorks {
    public interface IUnitOfWork<T,TDto,TCreate> where T: BaseModel  {
        IRepository<T,TDto,TCreate> Repository { get; }
        int SaveChanges();
    }
}
