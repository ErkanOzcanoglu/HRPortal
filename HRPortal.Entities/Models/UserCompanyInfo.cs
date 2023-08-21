using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class UserCompanyInfo : BaseModel {
        public string Title { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public double TotalDaysOff { get; set; }

        // Relationships
        public Guid UserPersonalId { get; set; }
        public UserPersonalInfo UserPersonalInfo { get; set; }
    }
}
