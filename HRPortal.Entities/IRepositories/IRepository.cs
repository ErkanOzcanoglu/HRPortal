using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.IRepositories {
    public interface IRepository<T, TDto, TCreate, TUpdate> where T: BaseModel {

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetAllAsync();

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<ActionResult<string>> Create(TCreate entity);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<ActionResult<string>> Update(Guid id, TUpdate entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<ActionResult<string>> Delete(Guid id);
    }
}
