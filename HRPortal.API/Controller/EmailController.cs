using HRPortal.DataAccessLayer.Context;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using HRPortal.Services.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace HRPortal.API.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly DbSet<Employee> _dbSet;
        private readonly IEmailService _emailService;

        public EmailController(HRPortalContext context, IEmailService emailService) {
            _context = context;
            _dbSet = _context.Set<Employee>();
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto request) 
        {
            _emailService.SendEmail(request);
            return Ok(request);
        }
    }
}
