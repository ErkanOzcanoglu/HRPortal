using AutoMapper;
using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.UnitOfWorks;
using HRPortal.Entities.Dto.InComing;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.IUnitOfWorks;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPortal.API.Controller {
    public class CreditCardController : GenericController<CreditCard, CreditCardDto, CreationDtoForCreditCard> {

        /// <summary>
        /// The context
        /// </summary>
        private readonly HRPortalContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork<CreditCard, CreditCardDto, CreationDtoForCreditCard> _unitOfWork;

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
            _unitOfWork = new UnitOfWork<CreditCard, CreditCardDto, CreationDtoForCreditCard>(_context, mapper);
        }
    }
}
