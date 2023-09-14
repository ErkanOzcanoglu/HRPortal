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
    public class CompanyWorkersController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<CompanyWorkers, CompanyWorkersDto, CreationDtoFormCompanyWorkers, UpdateDtoForCompanyWorkers> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<CompanyWorkers> _dbSet;
        private readonly ICacheService _cacheService;

        public CompanyWorkersController(HRPortalContext context, IMapper mapper, ICacheService cacheService) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<CompanyWorkers>();
            _cacheService = cacheService;
            _unitOfWork = new UnitOfWork<CompanyWorkers, CompanyWorkersDto, CreationDtoFormCompanyWorkers, UpdateDtoForCompanyWorkers>(_context, mapper);
        }

        [HttpGet("companyWorkers")]
        public async Task<IEnumerable<CompanyWorkersDto>> GetAllAsync() {
            //return await _unitOfWork.Repository.GetAllAsync();
            var cacheData = _cacheService.GetData<IEnumerable<CompanyWorkersDto>>("companyWorkers");
            if (cacheData != null && cacheData.Count() > 0) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.GetAllAsync();

            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<CompanyWorkersDto>>("companyWorkers", cacheData, expiryTime);
            return cacheData;
        }

        [HttpGet("companyWorker/{id}")]
        public async Task<ActionResult<CompanyWorkersDto>> GetAsync(Guid id) {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) {
                return NotFound();
            }
            var dto = _mapper.Map<CompanyWorkersDto>(entity);
            return Ok(dto);
        }

        [HttpPost("postCompanyWorker")]
        public async Task<ActionResult<CompanyWorkersDto>> CreateAsync(CreationDtoFormCompanyWorkers creationDto) {
            try {
                var cacheData = _cacheService.GetData<ActionResult<CompanyWorkersDto>>("postCompanyWorker");
                if (cacheData != null) {
                    return cacheData;
                }

                await _unitOfWork.Repository.Create(creationDto);
                _unitOfWork.SaveChanges();
                var expiryTime = DateTime.Now.AddSeconds(1);
                _cacheService.SetData<ActionResult<CompanyWorkersDto>>("string", cacheData, expiryTime);
                return cacheData;
            } catch (Exception ex) {
                return Ok("Creation Incomplete");
            }
        }

        [HttpPut("updateCompanyWorker/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateDtoForCompanyWorkers updateDto) {
            var result = await _unitOfWork.Repository.Update(id, updateDto);
            if (result == null) {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("deleteCompanyWorker/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            var result = await _unitOfWork.Repository.Delete(id);
            if (result == null) {
                return BadRequest();
            }
            return Ok(result);
        }

        // get company workers by company id
        [HttpGet("companyWorkerByCompanyId/{id}")]
        public async Task<ActionResult<CompanyWorkersDto>> GetCompanyWorkersByCompanyId(Guid id) {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.CompanyId == id);
            if (entity == null) {
                return NotFound();
            }
            var dto = _mapper.Map<CompanyWorkersDto>(entity);
            return Ok(dto);
        }

        // get company workers by worker id
        [HttpGet("companyWorkerByEmployeeId/{id}")]
        public async Task<ActionResult<CompanyWorkersDto>> GetCompanyWorkersByWorkerId(Guid id) {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (entity == null) {
                return NotFound();
            }
            var dto = _mapper.Map<CompanyWorkersDto>(entity);
            return Ok(dto);
        }

        [HttpGet("getManyEmployeeByCompanyId/{id}")]
        public async Task<ActionResult<CompanyWorkersDto>> GetManyEmployeeByCompanyId(Guid id) {
            var entity = await _dbSet.Where(x => x.CompanyId == id).ToListAsync();
            if (entity == null) {
                return NotFound();
            }
            
            return Ok(entity);
        }

    }
}
