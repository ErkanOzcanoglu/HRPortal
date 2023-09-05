using HRPortal.Entities.Dto.OutComing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Services.Service {
    public interface IEmailService {
        void SendEmail(EmailDto request);
    }
}
