

using HRPortal.Entities.Dto.OutComing;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace HRPortal.Services.Service {
    public class EmailService : IEmailService {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config) {
            _config = config;
        }

        public void SendEmail(EmailDto request) {
            string fromMail = _config.GetSection("EmailUsername").Value;
            string fromPassword = _config.GetSection("EmailPassword").Value;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = request.Subject;
            message.To.Add(new MailAddress(request.To));
            message.Body = request.Body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com") {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
    }
}
