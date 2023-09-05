using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controller
{
    public class CreditCardController : GenericController<CreditCard, CreditCardDto, CreationDtoForCreditCard, UpdateDtoForCreditCard> {

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork<CreditCard, CreditCardDto, CreationDtoForCreditCard, UpdateDtoForCreditCard> _unitOfWork;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The database set
        /// </summary>
        private readonly DbSet<CreditCard> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        public CreditCardController(HRPortalContext context, IMapper mapper) : base(context, mapper) {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<CreditCard>();
            _unitOfWork = new UnitOfWork<CreditCard, CreditCardDto, CreationDtoForCreditCard, UpdateDtoForCreditCard>(_context, mapper);
        }

        // create credit card with company id
        [HttpPost("{id}")]
        public IActionResult CreateCreditCard(Guid id, [FromBody] CreationDtoForCreditCard creationDtoForCreditCard) {
            var creditCard = _mapper.Map<CreditCard>(creationDtoForCreditCard);
            creditCard.CompanyId = id;
            _dbSet.Add(creditCard);
            _context.SaveChanges();
            return Ok();
        }
    }
}
