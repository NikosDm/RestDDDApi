using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Infrastructure.Domain.Customers
{
    /// <summary>
    /// Comfiguration of Customer class and classes Order and OrderItem,
    /// which are linked to Customer class for Database. 
    /// </summary>
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) 
        {
            builder.ToTable("Customers");

            builder.HasKey(b => b.customerID).HasName("CustomerID");

            builder.OwnsOne<CustomerFullName>("fullName", fn =>
            {
                fn.Property(p => p.FirstName).HasColumnName("FirstName");
                fn.Property(p => p.LastName).HasColumnName("LastName");
            });

            builder.OwnsOne<CustomerAddress>("address", add =>
            {
                add.Property(p => p.Street).HasColumnName("Street");
                add.Property(p => p.PostalCode).HasColumnName("PostalCode");
            });
            
            builder.OwnsMany<Order>("orders", x =>
            {
                x.WithOwner().HasForeignKey("CustomerID");

                x.ToTable("Orders");
                
                x.HasKey(o => o.orderID).HasName("OrderID");

                x.OwnsOne<OrderData>("orderData", da =>
                {
                    da.Property(p => p.OrderDate).HasColumnName("OrderDate");
                    da.Property(p => p.TotalPrice).HasColumnName("TotalPrice");
                });

                x.OwnsMany<OrderItem>("orderItems", y =>
                {
                    y.ToTable("OrderItems");

                    y.HasKey(o => o.orderItemID).HasName("OrderItemID");

                    y.OwnsOne<OrderProductData>("productData", op => 
                    {
                        op.Property(p => p.productID).HasColumnName("ProductID");
                        op.Property(p => p.Quantity).HasColumnName("Quantity");
                    });
                });
            });
        }
    }
}