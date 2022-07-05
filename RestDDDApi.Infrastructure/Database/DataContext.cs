using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Domain.Products;
using RestDDDApi.Infrastructure.Domain.Customers;
using RestDDDApi.Infrastructure.Domain.Products;

namespace RestDDDApi.Infrastructure.Database
{
    /// <summary>
    /// Comfiguration of Database context class 
    /// for mapping Aggregated Root classes with their respective table on database
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options): base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }
    }
}