using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.DtoForUserCompanyInfo {
    public class UserCompanyCreationDto {
        public string Title { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public double TotalDaysOff { get; set; }
    }
}
