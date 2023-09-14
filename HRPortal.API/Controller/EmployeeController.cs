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
using System.ComponentModel.Design;

namespace HRPortal.API.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<Employee, EmployeeDto, CreationDtoForEmployee, UpdateDtoForEmployee> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<Employee> _dbSet;
        private readonly ICacheService _cacheService;

        public EmployeeController(HRPortalContext context, IMapper mapper, ICacheService cacheService) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<Employee>();
            _cacheService = cacheService;
            _unitOfWork = new UnitOfWork<Employee, EmployeeDto, CreationDtoForEmployee, UpdateDtoForEmployee>(_context, mapper);
        }

        [HttpGet("employees")]
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync() {
            //return await _unitOfWork.Repository.GetAllAsync();
            var cacheData = _cacheService.GetData<IEnumerable<EmployeeDto>>("employees");
            if (cacheData != null && cacheData.Count() > 0) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.GetAllAsync();

            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<EmployeeDto>>("employees", cacheData, expiryTime);
            return cacheData;
        }

        [HttpGet("employee/{id}")]
        public async Task<Employee> GetAsync(Guid id) {
            //var cacheData = _cacheService.GetData<Employee>("employee");
            //if (cacheData != null) {
            //    return cacheData;
            //}

            var cacheData = await _unitOfWork.Repository.GetAsync(id);
            //var expiryTime = DateTime.Now.AddSeconds(30);
            //_cacheService.SetData<Employee>("employee", cacheData, expiryTime);
            return cacheData;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateAsync(CreationDtoForEmployee creationDto) {
            try {
                await _unitOfWork.Repository.Create(creationDto);
                _unitOfWork.SaveChanges();
                return Ok("Creation Complete");
            } catch (Exception ex) {
                return Ok("Creation Incomplete");
            }
        }

        [HttpPut("employee/{id}")]
        public async Task<ActionResult<string>> Update(Guid id, UpdateDtoForEmployee updateDtoForEmployee) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("updateEmployee");
            if (cacheData != null) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.Update(id, updateDtoForEmployee);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<string>>("updateEmployee", cacheData, expiryTime);
            return cacheData;
        }

        [HttpDelete("employee/{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("deleteEmployee");
            if (cacheData != null) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.Delete(id);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<string>>("deleteEmployee", cacheData, expiryTime);
            return cacheData;
        }
    }
}