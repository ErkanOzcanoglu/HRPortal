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
    public class CompanyWorkersController : GenericController<CompanyWorkers, CompanyWorkersDto, CreationDtoFormCompanyWorkers, UpdateDtoForCompanyWorkers> {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<CompanyWorkers, CompanyWorkersDto, CreationDtoFormCompanyWorkers, UpdateDtoForCompanyWorkers> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<CompanyWorkers> _dbSet;

        public CompanyWorkersController(HRPortalContext context, IMapper mapper) : base(context, mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<CompanyWorkers>();
            _unitOfWork = new UnitOfWork<CompanyWorkers, CompanyWorkersDto, CreationDtoFormCompanyWorkers, UpdateDtoForCompanyWorkers>(_context, mapper);
        }

        [HttpPost("Office")]
        public IActionResult Office(CreationDtoFormCompanyWorkers creationDtoFormCompanyWorkers) {
            var companyWorkers = _mapper.Map<CompanyWorkers>(creationDtoFormCompanyWorkers);
            _dbSet.Add(companyWorkers);
            _context.SaveChanges();
            return Ok(companyWorkers);
        }

        [HttpPost("{companyId}/{employeeId}")]
        public IActionResult Creates(Guid companyId, Guid employeeId, CreationDtoFormCompanyWorkers creationDtoFormCompanyWorkers) {
            var companyWorkers = _mapper.Map<CompanyWorkers>(creationDtoFormCompanyWorkers);
            companyWorkers.CompanyId = companyId;
            companyWorkers.EmployeeId = employeeId;
            _dbSet.Add(companyWorkers);
            _context.SaveChanges();
            return Ok(companyWorkers);
        }

        // get by companyID
        [HttpGet("GetByCompanyId/{id}")]
        public IActionResult GetByCompanyId(Guid id) {
            var companyWorkers = _dbSet.Include(x => x.Company).Include(x => x.Employee).Where(x => x.CompanyId == id).ToList();
            if (companyWorkers == null) {
                return NotFound();
            }
            return Ok(companyWorkers);
        }
    }
}
