using HRPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.IRepositories {
    public interface IRepository<T, TDto, TCreation> where T: BaseModel {
        Task<IEnumerable<TDto>> GetAll();
        Task<T> GetById(Guid id);
        Task Create(TCreation entity);
        Task Update(Guid id, TCreation entity);
        Task Delete(Guid id);
    }
}
