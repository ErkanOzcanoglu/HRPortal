using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.DtoForCompany;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using HRPortal.Entities.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : GenericController<Company, CompanyDto, CompanyCreationDto> {
        private readonly HRPortalContext _context;
        private readonly IUnitOfWork<Company, CompanyDto, CompanyCreationDto> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<Company> _dbSet;

        public CompanyController(HRPortalContext context, IMapper mapper) : base(context, mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<Company>();
            _unitOfWork = new UnitOfWork<Company, CompanyDto, CompanyCreationDto>(_context, mapper);
        }
    }
}
