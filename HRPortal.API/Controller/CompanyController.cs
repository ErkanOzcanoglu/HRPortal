using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.CustomUpdatesDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using HRPortal.Services.CacheServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRPortal.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<Company, CompanyDto, CreationDtoForCompany, UpdateDtoForCompany> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<Company> _dbSet;
        private readonly ICacheService _cacheService;

        public CompanyController(HRPortalContext context, IMapper mapper, ICacheService cacheService) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<Company>();
            _cacheService = cacheService;
            _unitOfWork = new UnitOfWork<Company, CompanyDto, CreationDtoForCompany, UpdateDtoForCompany>(_context, mapper);
        }

        [HttpGet("companies")]
        public async Task<IEnumerable<CompanyDto>> GetAllAsync() {
            //return await _unitOfWork.Repository.GetAllAsync();
            var cacheData = _cacheService.GetData<IEnumerable<CompanyDto>>("companies");
            if (cacheData != null && cacheData.Count() > 0) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.GetAllAsync();

            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<CompanyDto>>("companies", cacheData, expiryTime);
            return cacheData;
        }

        [HttpGet("company/{id}")]
        public async Task<ActionResult<Company>> GetAsync(Guid id) {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) {
                return NotFound();
            }
            var dto = _mapper.Map<CompanyDto>(entity);
            return Ok(dto);
        }

        [HttpPost("postCompany")]
        // post and return company
        public async Task<ActionResult<CompanyDto>> PostAsync(CreationDtoForCompany creationDtoForCompany) {
            var cacheData = _cacheService.GetData<ActionResult<CompanyDto>>("postCompany");
            if (cacheData != null) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.Create(creationDtoForCompany);
            _unitOfWork.SaveChanges();
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<CompanyDto>>("postCompany", cacheData, expiryTime);
            return cacheData;
        }

        [HttpPut("updateCompany/{id}")]
        public async Task<ActionResult<string>> Update(Guid id, UpdateDtoForCompany updateDtoForEmployee) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("updateCompany");
            if (cacheData != null) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.Update(id, updateDtoForEmployee);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<string>>("updateCompany", cacheData, expiryTime);
            return cacheData;
        }

        [HttpDelete("deleteCompany/{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("deleteCompany");
            if (cacheData != null) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.Delete(id);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<string>>("deleteCompany", cacheData, expiryTime);
            return cacheData;
        }

        [HttpPut("premiumUpdate/{id}")]
        public async Task<IActionResult> PremiumUpdate(Guid id) {
            _dbSet.Where(x => x.Id == id).FirstOrDefault().IsPremium = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}