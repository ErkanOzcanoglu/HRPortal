using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Company : BaseModel {

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the company mail.
        /// </summary>
        /// <value>
        /// The company mail.
        /// </value>
        public string CompanyMail { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        /// <value>
        /// The logo.
        /// </value>
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the company phone.
        /// </summary>
        /// <value>
        /// The company phone.
        /// </value>
        public string CompanyPhone { get; set; }

        /// <summary>
        /// Gets or sets the company address.
        /// </summary>
        /// <value>
        /// The company address.
        /// </value>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is premium.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsPremium { get; set; }

        // Relationship

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ICollection<Employee> Users { get; set; }

        /// <summary>
        /// Gets or sets the credit cards.
        /// </summary>
        /// <value>
        /// The credit cards.
        /// </value>
        public CreditCard CreditCards { get; set; }

        /// <summary>
        /// Gets or sets the company workers.
        /// </summary>
        /// <value>
        /// The company workers.
        /// </value>
        public ICollection<CompanyWorkers> CompanyWorkers { get; set; }
    }
}
