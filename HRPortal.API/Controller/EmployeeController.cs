using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : GenericController<User, UserDto, CreationDtoForEmployee, UpdateDtoForUser> {

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork<User, UserDto, CreationDtoForEmployee, UpdateDtoForUser> _unitOfWork;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<User> _dbSet;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public EmployeeController(HRPortalContext context, IMapper mapper) : base(context, mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<User>();
            _unitOfWork = new UnitOfWork<User, UserDto, CreationDtoForEmployee, UpdateDtoForUser>(_context, mapper);
        }

        [HttpPost("EmployeeCreate")]
        public async Task<ActionResult<string>> Create(CreationDtoForEmployee entity) {
            var employeeMail = _dbSet.FirstOrDefault(x => x.Mail == entity.Mail);
            if (employeeMail != null) {
                return Ok("There is already a user with the same mail");
            }

            var companyPhone = _dbSet.FirstOrDefault(x => x.Phone == entity.Phone);
            if (companyPhone != null) {
                return Ok("There is already a company with the same phone");
            }

            await _unitOfWork.Repository.Create(entity);
            _unitOfWork.SaveChanges();
            return Ok("Creation Complete");
        }

        [HttpGet("Employee/{id}")]
        public async Task<IEnumerable<UserDto>> GetManyAsync(Guid id) { 
            return await _unitOfWork.Repository.GetManyAsync(id);
        }
    }
}