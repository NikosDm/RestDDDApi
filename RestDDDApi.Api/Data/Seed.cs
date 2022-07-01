using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Domain.Products;
using RestDDDApi.Domain.Products.ValueObjects;
using RestDDDApi.Infrastructure.Database;

namespace RestDDDApi.Api.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext dataContext) 
        {
            var listOfProducts = new List<Product>() 
            {
                Product.createNewProduct(new ProductData { Name = "Product One", Price = 50.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Two", Price = 50.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Three", Price = 50.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Four", Price = 50.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Five", Price = 20.00 }) 
            };
            
            var customers = new List<Customer>() {
                Customer.createNewCustomer(new CustomerFullName { FirstName = "Cus1", LastName = "Tomer1" }, new CustomerAddress { Street = "Str1", PostalCode = "POSTA1" }),
                Customer.createNewCustomer(new CustomerFullName { FirstName = "Cus2", LastName = "Tomer2" }, new CustomerAddress { Street = "Str2", PostalCode = "POSTA2" }),
                Customer.createNewCustomer(new CustomerFullName { FirstName = "Cus3", LastName = "Tomer3" }, new CustomerAddress { Street = "Str3", PostalCode = "POSTA3" })
            };

            foreach (var customer in customers)
                customer.placeNewOrder(new OrderData { OrderDate = DateTime.Now, TotalPrice = 200.00 }, new List<OrderProductData>() { 
                    new OrderProductData { productID = listOfProducts[0].productID, Quantity = 2 },
                    new OrderProductData { productID = listOfProducts[2].productID, Quantity = 2 } });

            await dataContext.Products.AddRangeAsync(listOfProducts);
            await dataContext.Customers.AddRangeAsync(customers);

            await dataContext.SaveChangesAsync();
        }
    }
}