using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Budget : BaseModel {

        /// <summary>
        /// Gets or sets the budget description.
        /// </summary>
        /// <value>
        /// The budget description.
        /// </value>
        public string BudgetDescription { get; set; }

        /// <summary>
        /// Gets or sets the budget amount.
        /// </summary>
        /// <value>
        /// The budget amount.
        /// </value>
        public string BudgetAmount { get; set; }

        // Relationnship

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public Guid EventId { get; set; }

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public Event Event { get; set; }
    }
}
