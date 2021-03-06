// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestDDDApi.Infrastructure.Database;

#nullable disable

namespace RestDDDApi.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220701024800_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RestDDDApi.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("customerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("customerID")
                        .HasName("CustomerID");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("RestDDDApi.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("productID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("productID")
                        .HasName("ProductID");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("RestDDDApi.Domain.Customers.Customer", b =>
                {
                    b.OwnsMany("RestDDDApi.Domain.Customers.Orders.Order", "orders", b1 =>
                        {
                            b1.Property<Guid>("orderID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("CustomerID")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("orderID")
                                .HasName("OrderID");

                            b1.HasIndex("CustomerID");

                            b1.ToTable("Orders", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CustomerID");

                            b1.OwnsOne("RestDDDApi.Domain.Customers.Orders.OrderData", "orderData", b2 =>
                                {
                                    b2.Property<Guid>("orderID")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<DateTime>("OrderDate")
                                        .HasColumnType("datetime2")
                                        .HasColumnName("OrderDate");

                                    b2.Property<double>("TotalPrice")
                                        .HasColumnType("float")
                                        .HasColumnName("TotalPrice");

                                    b2.HasKey("orderID");

                                    b2.ToTable("Orders");

                                    b2.WithOwner()
                                        .HasForeignKey("orderID");
                                });

                            b1.OwnsMany("RestDDDApi.Domain.Customers.Orders.OrderItem", "orderItems", b2 =>
                                {
                                    b2.Property<Guid>("orderItemID")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("orderID")
                                        .HasColumnType("uniqueidentifier");

                                    b2.HasKey("orderItemID")
                                        .HasName("OrderItemID");

                                    b2.HasIndex("orderID");

                                    b2.ToTable("OrderItems", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("orderID");

                                    b2.OwnsOne("RestDDDApi.Domain.Customers.Orders.OrderProductData", "productData", b3 =>
                                        {
                                            b3.Property<Guid>("orderItemID")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<int>("Quantity")
                                                .HasColumnType("int")
                                                .HasColumnName("Quantity");

                                            b3.Property<Guid>("productID")
                                                .HasColumnType("uniqueidentifier")
                                                .HasColumnName("ProductID");

                                            b3.HasKey("orderItemID");

                                            b3.ToTable("OrderItems");

                                            b3.WithOwner()
                                                .HasForeignKey("orderItemID");
                                        });

                                    b2.Navigation("productData")
                                        .IsRequired();
                                });

                            b1.Navigation("orderData")
                                .IsRequired();

                            b1.Navigation("orderItems");
                        });

                    b.OwnsOne("RestDDDApi.Domain.Customers.CustomerAddress", "address", b1 =>
                        {
                            b1.Property<Guid>("customerID")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PostalCode");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.HasKey("customerID");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("customerID");
                        });

                    b.OwnsOne("RestDDDApi.Domain.Customers.CustomerFullName", "fullName", b1 =>
                        {
                            b1.Property<Guid>("customerID")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("LastName");

                            b1.HasKey("customerID");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("customerID");
                        });

                    b.Navigation("address")
                        .IsRequired();

                    b.Navigation("fullName")
                        .IsRequired();

                    b.Navigation("orders");
                });

            modelBuilder.Entity("RestDDDApi.Domain.Products.Product", b =>
                {
                    b.OwnsOne("RestDDDApi.Domain.Products.ValueObjects.ProductData", "productData", b1 =>
                        {
                            b1.Property<Guid>("productID")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.Property<double>("Price")
                                .HasColumnType("float")
                                .HasColumnName("Price");

                            b1.HasKey("productID");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("productID");
                        });

                    b.Navigation("productData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
