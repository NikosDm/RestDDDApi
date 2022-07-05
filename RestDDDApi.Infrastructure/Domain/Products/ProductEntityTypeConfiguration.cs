using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestDDDApi.Domain.Products;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Infrastructure.Domain.Products
{
    /// <summary>
    /// Comfiguration of Product class for Database. 
    /// </summary>
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) 
        {
            builder.ToTable("Products");
            
            builder.HasKey(b => b.productID).HasName("ProductID");

            builder.OwnsOne<ProductData>("productData", add =>
            {
                add.Property(p => p.Name).HasColumnName("Name");
                add.Property(p => p.Price).HasColumnName("Price");
            });
        }
    }
}