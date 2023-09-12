using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using HRPortal.Services.CacheServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controller {
    public class CreditCardController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<CreditCard, CreditCardDto, CreationDtoForCreditCard, UpdateDtoForCreditCard> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<CreditCard> _dbSet;
        private readonly ICacheService _cacheService;

        public CreditCardController(HRPortalContext context, IMapper mapper, ICacheService cacheService) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<CreditCard>();
            _cacheService = cacheService;
            _unitOfWork = new UnitOfWork<CreditCard, CreditCardDto, CreationDtoForCreditCard, UpdateDtoForCreditCard>(_context, mapper);
        }

        [HttpGet("creditCard")]
        public async Task<IEnumerable<CreditCardDto>> GetAllAsync() {
            //return await _unitOfWork.Repository.GetAllAsync();
            var cacheData = _cacheService.GetData<IEnumerable<CreditCardDto>>("creditCard");
            if (cacheData != null && cacheData.Count() > 0) {
                return cacheData;
            }
            cacheData = await _unitOfWork.Repository.GetAllAsync();

            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<IEnumerable<CreditCardDto>>("creditCard", cacheData, expiryTime);
            return cacheData;
        }

        [HttpGet("creditCard/{id}")]
        public async Task<CreditCard> GetAsync(Guid id) {
            var cacheData = _cacheService.GetData<CreditCard>("creditCard");
            if (cacheData != null) {
                return cacheData;
            }

            cacheData = await _unitOfWork.Repository.GetAsync(id);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<CreditCard>("creditCard", cacheData, expiryTime);
            return cacheData;
        }

        [HttpPost("postCreditCard")]
        public async Task<IActionResult> CreateAsync(CreationDtoForCreditCard creationDto) {
            var result = await _unitOfWork.Repository.Create(creationDto);
            if (result == null) {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("updateCreditCard/{id}")]
        public async Task<ActionResult<string>> Update(Guid id, UpdateDtoForCreditCard entity) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("updateCreditCard");
            if (cacheData != null) {
                return cacheData;
            }

            cacheData = await _unitOfWork.Repository.Update(id, entity);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<string>>("updateCreditCard", cacheData, expiryTime);
            return cacheData;
        }

        [HttpDelete("deleteCreditCard/{id}")]
        public async Task<ActionResult<string>> Delete(Guid id) {
            var cacheData = _cacheService.GetData<ActionResult<string>>("deleteCreditCard");
            if (cacheData != null) {
                return cacheData;
            }

            cacheData = await _unitOfWork.Repository.Delete(id);
            var expiryTime = DateTime.Now.AddSeconds(30);
            _cacheService.SetData<ActionResult<string>>("deleteCreditCard", cacheData, expiryTime);
            return cacheData;
        }
    }
}