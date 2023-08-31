using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class EventConfiguration {
        public void Configure(EntityTypeBuilder<Event> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Owner).WithMany(b => b.Events).HasForeignKey(b => b.OwnerId);
        }
    }
}
