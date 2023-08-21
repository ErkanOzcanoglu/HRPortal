using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.DtoForCompany {
    public class CreditCardCreationDto {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public string CardSecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
