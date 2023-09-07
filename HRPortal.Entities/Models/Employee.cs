using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Employee : BaseModel {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string VerificationToken { get; set; }
        public string Phone { get; set; }
        public string TC { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiresAt { get; set; }

        // Relationships
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }

        public EmployeeCompanyInformation EmployeeCompanyInformation { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<CompanyWorkers> CompanyWorkers { get; set; }
    }
}
