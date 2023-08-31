using HRPortal.DataAccessLayer.Configuration;
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

            var budgetConfiguration = new BudgetConfiguration();
            budgetConfiguration.Configure(modelBuilder.Entity<Budget>());

            var companyConfiguration = new CompanyConfiguration();
            companyConfiguration.Configure(modelBuilder.Entity<Company>());

            var creditCardConfiguration = new CreditCardConfiguration();
            creditCardConfiguration.Configure(modelBuilder.Entity<CreditCard>());

            var eventConfiguration = new EventConfiguration();
            eventConfiguration.Configure(modelBuilder.Entity<Event>());

            var projectConfiguration = new ProjectConfiguration();
            projectConfiguration.Configure(modelBuilder.Entity<Project>());

            var taskConfiguration = new TaskConfiguration();
            taskConfiguration.Configure(modelBuilder.Entity<Tasks>());

            var userConfiguration = new UserConfiguration();
            userConfiguration.Configure(modelBuilder.Entity<User>());
        }



        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
