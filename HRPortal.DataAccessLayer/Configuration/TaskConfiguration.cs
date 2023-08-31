using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class TaskConfiguration {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Tasks> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Project).WithMany(b => b.Tasks).HasForeignKey(b => b.ProjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Owner).WithMany(b => b.Tasks).HasForeignKey(b => b.TaskOwnerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
