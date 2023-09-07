using HRPortal.Entities.Dto.OutComing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.CreationDto {
    public class CreationDtoForEmployeeCompanyInformation : BaseDto {
        public string Title { get; set; }
        public string Department { get; set; }
        public string CompanyMail { get; set; }
        public float Salary { get; set; }
        public float LeaveDay { get; set; }
        public bool IsAdmin { get; set; }
    }
}
