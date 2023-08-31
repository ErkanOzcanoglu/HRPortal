using HRPortal.Entities.IRepositories;
using HRPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.IUnitOfWorks {
    public interface IUnitOfWork<T, TDto, TCreate> where T: BaseModel {

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        IRepository<T, TDto, TCreate> Repository { get; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
