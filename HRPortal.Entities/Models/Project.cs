using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Project : BaseModel{
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectManager { get; set; }
        public string ProjectType { get; set; }
        public string ProjectStatus { get; set; }
        public string ProjectStartDate { get; set; }
        public string ProjectEndDate { get; set; }

        // Relationship
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}
