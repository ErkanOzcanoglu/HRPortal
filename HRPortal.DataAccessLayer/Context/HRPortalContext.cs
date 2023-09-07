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

            var employeeConfiguration = new EmployeeConfiguration();
            employeeConfiguration.Configure(modelBuilder.Entity<Employee>());

            var employeeCompanyInformationConfiguration = new EmployeeCompanyInformationConfiguration();
            employeeCompanyInformationConfiguration.Configure(modelBuilder.Entity<EmployeeCompanyInformation>());

            var companyWorkersConfiguration = new CompanyWorkersConfiguration();
            companyWorkersConfiguration.Configure(modelBuilder.Entity<CompanyWorkers>());
        }



        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<CompanyWorkers> CompanyWorkers { get; set; }
        public DbSet<EmployeeCompanyInformation> EmployeeCompanyInformations { get; set; }
    }
}
