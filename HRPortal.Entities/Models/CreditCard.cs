using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class CreditCard : BaseModel {

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public string CardSecurityCode { get; set; }
        public string ExpirationDate { get; set; }

        // Relationships
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
