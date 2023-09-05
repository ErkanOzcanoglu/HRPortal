using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.CustomUpdatesDto;
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
    public class CompanyController : GenericController<Company, CompanyDto, CreationDtoForCompany,  UpdateDtoForCompany> {

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork<Company, CompanyDto, CreationDtoForCompany, UpdateDtoForCompany> _unitOfWork;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<Company> _dbSet;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public CompanyController(HRPortalContext context, IMapper mapper) : base(context, mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<Company>();
            _unitOfWork = new UnitOfWork<Company, CompanyDto, CreationDtoForCompany, UpdateDtoForCompany>(_context, mapper);
        }

        // getcompanybymail
        [HttpGet("GetCompanyByMail")]
        public IActionResult GetCompanyByMail(string mail) {
            var company = _dbSet.FirstOrDefault(x => x.CompanyMail == mail);
            if (company == null) {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpGet("GetCompanyByName")]
        public IActionResult GetCompanyByName(string name) {
            var company = _dbSet.FirstOrDefault(x => x.CompanyName == name);
            if (company == null) {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost("company")]
        public async Task<ActionResult<string>> Create(CreationDtoForCompany entity) {
            // check if there is any company with the same mail
            var companyMail = _dbSet.FirstOrDefault(x => x.CompanyMail == entity.CompanyMail);
            if (companyMail != null) {
                return Ok("There is already a company with the same mail");
            }

            var companyName = _dbSet.FirstOrDefault(x => x.CompanyName == entity.CompanyName);
            if (companyName != null) {
                return Ok("There is already a company with the same name");
            }

            var companyPhone = _dbSet.FirstOrDefault(x => x.CompanyPhone == entity.CompanyPhone);
            if (companyPhone != null) {
                return Ok("There is already a company with the same phone");
            }

            await _unitOfWork.Repository.Create(entity);
            _unitOfWork.SaveChanges();
            return Ok("Creation Complete");

        }

        [HttpPut("premiumUpdate/{id}")]
        public async Task<ActionResult<string>> UpdatePremium(Guid id) {
            var company = _dbSet.FirstOrDefault(x => x.Id == id);
            if (company == null) {
                return NotFound();
            }

            company.IsPremium = true;
            _dbSet.Update(company);
            await _context.SaveChangesAsync();
            return Ok("Update Complete");
        }
    }
}
