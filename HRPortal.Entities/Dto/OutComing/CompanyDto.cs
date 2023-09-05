using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.OutComing {
    public class CompanyDto : BaseDto {
        public string CompanyName { get; set; }
        public string CompanyMail { get; set; }
        public string Logo { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string Website { get; set; }
    }
}
