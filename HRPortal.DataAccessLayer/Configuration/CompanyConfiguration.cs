using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class CompanyConfiguration {
        public void Configure(EntityTypeBuilder<Company> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
