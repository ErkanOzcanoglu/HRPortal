using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Event : BaseModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relationships
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Budget> Budgets { get; set; }
    }
}
