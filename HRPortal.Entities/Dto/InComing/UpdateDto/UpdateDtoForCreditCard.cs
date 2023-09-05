using HRPortal.Entities.Dto.OutComing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.UpdateDto {
    public class UpdateDtoForCreditCard : BaseDto
    {
        ///// <summary>
        ///// Gets or sets the card number.
        ///// </summary>
        ///// <value>
        ///// The card number.
        ///// </value>
        //public string CardNumber { get; set; }

        ///// <summary>
        ///// Gets or sets the name of the card holder.
        ///// </summary>
        ///// <value>
        ///// The name of the card holder.
        ///// </value>
        //public string CardHolderName { get; set; }

        ///// <summary>
        ///// Gets or sets the type of the card.
        ///// </summary>
        ///// <value>
        ///// The type of the card.
        ///// </value>
        //public string CardType { get; set; }

        ///// <summary>
        ///// Gets or sets the card security code.
        ///// </summary>
        ///// <value>
        ///// The card security code.
        ///// </value>
        //public string CardSecurityCode { get; set; }

        ///// <summary>
        ///// Gets or sets the expiration date.
        ///// </summary>
        ///// <value>
        ///// The expiration date.
        ///// </value>
        //public string ExpirationDate { get; set; }
        public Guid CompanyId { get; set; }
    }
}
