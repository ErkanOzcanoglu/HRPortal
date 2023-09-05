using AutoMapper;
using HRPortal.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.UserForAuth {
    public class RegisterDto : BaseModel {
        public string Title { get; set; }
        public string TC { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }
        public Guid? CompanyGuid { get; set; }
    }
}
