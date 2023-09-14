using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Employee : BaseModel {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>
        /// The mail.
        /// </value>
        public string Mail { get; set; }

        /// <summary>
        /// Gets or sets the verification token.
        /// </summary>
        /// <value>
        /// The verification token.
        /// </value>
        public string VerificationToken { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the tc.
        /// </summary>
        /// <value>
        /// The tc.
        /// </value>
        public string TC { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>
        /// The password salt.
        /// </value>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the verified at.
        /// </summary>
        /// <value>
        /// The verified at.
        /// </value>
        public DateTime? VerifiedAt { get; set; }
        /// <summary>
        /// Gets or sets the password reset token.
        /// </summary>
        /// <value>
        /// The password reset token.
        /// </value>
        public string PasswordResetToken { get; set; }
        /// <summary>
        /// Gets or sets the password reset token expires at.
        /// </summary>
        /// <value>
        /// The password reset token expires at.
        /// </value>
        public DateTime? PasswordResetTokenExpiresAt { get; set; }

        // Relationships

        /// <summary>
        /// Gets or sets the employee company information.
        /// </summary>
        /// <value>
        /// The employee company information.
        /// </value>
        public EmployeeCompanyInformation EmployeeCompanyInformation { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public ICollection<Tasks> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public ICollection<Event> Events { get; set; }

        /// <summary>
        /// Gets or sets the budgets.
        /// </summary>
        /// <value>
        /// The budgets.
        /// </value>
        public ICollection<Budget> Budgets { get; set; }

        /// <summary>
        /// Gets or sets the company workers.
        /// </summary>
        /// <value>
        /// The company workers.
        /// </value>
        public ICollection<CompanyWorkers> CompanyWorkers { get; set; }
    }
}
