using Core.Entities.Concrete;
using Core.Extensions;
using DataAccess.Concrete.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class SompoContext : DbContext
    {
        private readonly string _connectionString;
        public SompoContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            _connectionString = configuration.GetConnectionString("DbConnection");
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.ApplySoftDeletePatternGlobalFilters(this);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Proposal> Proposals { get; set; }
    }
}
