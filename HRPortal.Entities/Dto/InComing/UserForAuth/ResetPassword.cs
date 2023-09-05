using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.UserForAuth {
    public class ResetPassword {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
