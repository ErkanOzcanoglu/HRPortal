using HRPortal.DataAccessLayer.Mapper;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Context {
    public class HRPortalContext : DbContext {
        public HRPortalContext(DbContextOptions<HRPortalContext> options) : base(options) { }
            public HRPortalContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            var companyMapper = new CompanyMapper();
            companyMapper.Configure(modelBuilder.Entity<Company>());

            var userCompanyMapper = new UserCompanyMapper();
            userCompanyMapper.Configure(modelBuilder.Entity<UserCompanyInfo>());

            var userPersonalMapper = new UserPersonalMapper();
            userPersonalMapper.Configure(modelBuilder.Entity<UserPersonalInfo>());
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<UserCompanyInfo> UserCompanyInfo { get; set; }
        public DbSet<UserPersonalInfo> UserPersonalInfo { get; set; }

    }
}
