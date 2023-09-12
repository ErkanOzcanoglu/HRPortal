using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using HRPortal.Services.CacheServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCompanyInformationController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<EmployeeCompanyInformation, EmployeeCompanyInformationDto, CreationDtoForEmployeeCompanyInformation, UpdateForEmployeeCompanyInformation> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<Employee> _dbSet;
        private readonly ICacheService _cacheService;

        public EmployeeCompanyInformationController(HRPortalContext context, IMapper mapper, ICacheService cacheService) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<Employee>();
            _cacheService = cacheService;
            _unitOfWork = new UnitOfWork<EmployeeCompanyInformation, EmployeeCompanyInformationDto, CreationDtoForEmployeeCompanyInformation, UpdateForEmployeeCompanyInformation>(_context, mapper);
        }

        [HttpGet("employeeInformations")]
        public async Task<IEnumerable<EmployeeCompanyInformationDto>> GetAllAsync() {
            //return await _unitOfWork.Repository.GetAllAsync();
            var cacheData = _cacheService.GetData<IEnumerable<EmployeeCompanyInformationDto>>("employeeInformations");
            if (cacheData != null && cacheData.Count() > 0) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.GetAllAsync();

            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<EmployeeCompanyInformationDto>>("employeeInformation", cacheData, expiryTime);
            return cacheData;
        }

        [HttpGet("employeeInformation/{id}")]
        public async Task<EmployeeCompanyInformation> GetAsync(Guid id) {
            var cacheData = _cacheService.GetData<EmployeeCompanyInformation>("employeeInformation");
            if (cacheData != null) {
                return cacheData;
            }

            cacheData = await _unitOfWork.Repository.GetAsync(id);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<EmployeeCompanyInformation>("employeeInformation", cacheData, expiryTime);
            return cacheData;
        }

        [HttpPost("postEmployeeInformation")]
        public async Task<ActionResult<EmployeeCompanyInformationDto>> Create(CreationDtoForEmployeeCompanyInformation entity) {
            var cacheData = _cacheService.GetData<ActionResult<EmployeeCompanyInformationDto>>("postEmployeeInformation");
            if (cacheData != null) {
                return cacheData;
            }

            cacheData = await _unitOfWork.Repository.Create(entity);
            _unitOfWork.SaveChanges();
            var expiryTime = DateTime.Now.AddSeconds(1);
            _cacheService.SetData<ActionResult<EmployeeCompanyInformationDto>>("postEmployeeInformation", cacheData, expiryTime);
            return cacheData;
        }

        [HttpPut("updateEmployeeInformation/{id}")]
        public async Task<ActionResult<string>> Update(Guid id, UpdateForEmployeeCompanyInformation entity) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("updateEmployeeInformation");
            if (cacheData != null) {
                return cacheData;
            }

            await _unitOfWork.Repository.Update(id, entity);
            _unitOfWork.SaveChanges();
            var expiryTime = DateTime.Now.AddSeconds(1);
            _cacheService.SetData<ActionResult<string>>("updateEmployeeInformation", cacheData, expiryTime);
            return cacheData;
        }

        [HttpDelete("deleteEmployeeInformation/{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("deleteEmployeeInformation");
            if (cacheData != null) {
                return cacheData;
            }
            await _unitOfWork.Repository.Delete(id);
            _unitOfWork.SaveChanges();

            var expiryTime = DateTime.Now.AddSeconds(1);
            _cacheService.SetData<ActionResult<string>>("deleteEmployeeInformation", cacheData, expiryTime);
            return cacheData;
        }
    }
}
