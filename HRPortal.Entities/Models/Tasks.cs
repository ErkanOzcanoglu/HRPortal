﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Tasks: BaseModel {

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        /// <value>
        /// The name of the task.
        /// </value>
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>
        /// The task description.
        /// </value>
        public string TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the task priority.
        /// </summary>
        /// <value>
        /// The task priority.
        /// </value>
        public string TaskPriority { get; set; }

        /// <summary>
        /// Gets or sets the type of the task.
        /// </summary>
        /// <value>
        /// The type of the task.
        /// </value>
        public string TaskType { get; set; }

        /// <summary>
        /// Gets or sets the task start date.
        /// </summary>
        /// <value>
        /// The task start date.
        /// </value>
        public string TaskStartDate { get; set; }

        /// <summary>
        /// Gets or sets the task end date.
        /// </summary>
        /// <value>
        /// The task end date.
        /// </value>
        public string TaskEndDate { get; set; }

        /// <summary>
        /// Gets or sets the duration of the task.
        /// </summary>
        /// <value>
        /// The duration of the task.
        /// </value>
        public string TaskDuration { get; set; }


        // Relationships

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project Project { get; set; }

        /// <summary>
        /// Gets or sets the task owner identifier.
        /// </summary>
        /// <value>
        /// The task owner identifier.
        /// </value>
        public Guid TaskOwnerId { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Employee Owner { get; set; }
    }
}
