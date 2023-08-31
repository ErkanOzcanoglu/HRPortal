using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class BudgetConfiguration {
        public void Configure(EntityTypeBuilder<Budget> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Event).WithMany(e => e.Budgets).HasForeignKey(b => b.EventId);
        }
    }
}
