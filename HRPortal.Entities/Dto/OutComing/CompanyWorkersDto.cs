using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.OutComing {
    public class CompanyWorkersDto : BaseDto {
        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
