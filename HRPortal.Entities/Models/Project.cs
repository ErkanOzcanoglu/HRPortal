using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Project : BaseModel{

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the project manager.
        /// </summary>
        /// <value>
        /// The project manager.
        /// </value>
        public string ProjectManager { get; set; }

        /// <summary>
        /// Gets or sets the type of the project.
        /// </summary>
        /// <value>
        /// The type of the project.
        /// </value>
        public string ProjectType { get; set; }

        /// <summary>
        /// Gets or sets the project status.
        /// </summary>
        /// <value>
        /// The project status.
        /// </value>
        public string ProjectStatus { get; set; }

        /// <summary>
        /// Gets or sets the project start date.
        /// </summary>
        /// <value>
        /// The project start date.
        /// </value>
        public string ProjectStartDate { get; set; }

        /// <summary>
        /// Gets or sets the project end date.
        /// </summary>
        /// <value>
        /// The project end date.
        /// </value>
        public string ProjectEndDate { get; set; }

        // Relationship
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public User Owner { get; set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public ICollection<Tasks> Tasks { get; set; }
    }
}
