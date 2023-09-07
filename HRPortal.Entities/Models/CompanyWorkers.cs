using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class CompanyWorkers : BaseModel {
        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }

        public Employee Employee { get; set; }
        public Company Company { get; set; }

    }
}
