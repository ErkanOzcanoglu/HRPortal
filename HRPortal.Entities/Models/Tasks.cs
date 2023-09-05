using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Tasks: BaseModel {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskPriority { get; set; }
        public string TaskType { get; set; }
        public string TaskStartDate { get; set; }
        public string TaskEndDate { get; set; }
        public string TaskDuration { get; set; }


        // Relationships
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid TaskOwnerId { get; set; }
        public User Owner { get; set; }
    }
}
