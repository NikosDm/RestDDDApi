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
        /*The application is filled with data for testing purposes*/
        public static async Task SeedData(DataContext dataContext) 
        {
            var listOfProducts = new List<Product>() 
            {
                Product.createNewProduct(ProductData.createProductData("Product One", 50.00 )), 
                Product.createNewProduct(ProductData.createProductData("Product Two", 50.00 )), 
                Product.createNewProduct(ProductData.createProductData("Product Three", 50.00 )), 
                Product.createNewProduct(ProductData.createProductData("Product Four", 50.00 )), 
                Product.createNewProduct(ProductData.createProductData("Product Five", 50.00 )), 
            };

            var customers = new List<Customer>() {
                Customer.createNewCustomer(CustomerFullName.createNewCustomerFullName("Cus1", "Tomer1"), CustomerAddress.createNewCustomerAddress("Str1", "POSTA1")),
                Customer.createNewCustomer(CustomerFullName.createNewCustomerFullName("Cus2", "Tomer2"), CustomerAddress.createNewCustomerAddress("Str2", "POSTA2")),
                Customer.createNewCustomer(CustomerFullName.createNewCustomerFullName("Cus3", "Tomer3"), CustomerAddress.createNewCustomerAddress("Str3", "POSTA3"))
            };

            foreach (var customer in customers)
                customer.placeNewOrder(OrderData.createOrderData(DateTime.Now), new List<OrderProductData>() { 
                    OrderProductData.createNewOrderProductData(listOfProducts[0].productID, 2, listOfProducts[0].productData.Price),
                    OrderProductData.createNewOrderProductData(listOfProducts[0].productID, 2, listOfProducts[0].productData.Price) });

            await dataContext.Products.AddRangeAsync(listOfProducts);
            await dataContext.Customers.AddRangeAsync(customers);

            await dataContext.SaveChangesAsync();
        }
    }
}