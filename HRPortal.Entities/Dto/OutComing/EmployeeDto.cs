using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.OutComing {
    public class EmployeeDto : BaseDto {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public bool IsVerificated { get; set; }
        public string Phone { get; set; }
        public string TC { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
    }
}
