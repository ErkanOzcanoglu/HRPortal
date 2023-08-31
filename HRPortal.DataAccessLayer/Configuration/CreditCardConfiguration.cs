using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class CreditCardConfiguration {
        public void Configure(EntityTypeBuilder<CreditCard> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Company).WithOne(b => b.CreditCards).HasForeignKey<CreditCard>(b => b.CompanyId);
        }
    }
}
