using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Company : BaseModel {
        public string CompanyName { get; set; }
        public string CompanyMail { get; set; }
        public string Logo { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string Website { get; set; }
        public Boolean IsPremium { get; set; }   

        // Relationship
        public ICollection<User> Users { get; set; }
        public CreditCard CreditCards { get; set; }
    }
}
