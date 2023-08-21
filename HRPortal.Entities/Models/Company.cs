using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Company : BaseModel {
        // Company Info
        public string Name { get; set; }
        public string Address { get; set; }
        public string TaxId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }

        // Credit Card Info
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardType { get; set; }
        public string CardSecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }

        // Relationship
        public UserPersonalInfo UserPersonalInfo { get; set; }
        public Guid UserId { get; set; }
    }
}
