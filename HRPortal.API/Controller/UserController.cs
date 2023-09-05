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

namespace HRPortal.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<User, UserDto, CreationDtoForUser, UpdateDtoForUser> {

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork<User, UserDto, CreationDtoForUser, UpdateDtoForUser> _unitOfWork;

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
        public UserController(HRPortalContext context, IMapper mapper) : base(context, mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<User>();
            _unitOfWork = new UnitOfWork<User, UserDto, CreationDtoForUser, UpdateDtoForUser>(_context, mapper);
        }

        // getuserbymail
        [HttpGet("GetUserByMail")]
        public IActionResult GetUserByMail(string mail) {
            var user = _dbSet.FirstOrDefault(x => x.Mail == mail);
            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        // getuser
        [HttpGet("users")]
        public IActionResult GetUsers() {
            var users = _dbSet.ToList();
            if (users == null) {
                return NotFound();
            }
            return Ok(users);
        }
    }
}
