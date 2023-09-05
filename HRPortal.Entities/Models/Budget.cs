using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Models {
    public class Budget : BaseModel {
        public string BudgetDescription { get; set; }
        public string BudgetAmount { get; set; }

        // Relationnship
        public ICollection<Project> Projects { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
