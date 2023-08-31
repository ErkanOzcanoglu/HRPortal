using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class UserConfiguration {

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Company).WithMany(b => b.Users).HasForeignKey(b => b.CompanyId);


        }
    }
}
