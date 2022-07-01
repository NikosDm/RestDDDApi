using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Interfaces;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Domain.Products;
using RestDDDApi.Domain.Products.Interfaces;
using RestDDDApi.Domain.Products.ValueObjects;
using RestDDDApi.Infrastructure.Database;
using RestDDDApi.Infrastructure.Domain.Customers;
using RestDDDApi.Infrastructure.Domain.Products;

namespace Tests
{
    public class CustomerUnitTest
    {
       private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "RestDDDApiTest")
            .Options;

        DataContext context;
        ICustomerRepository customerRepository;
        IProductRepository productRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            productRepository = new ProductRepository(context);
            customerRepository = new CustomerRepository(context);

            SeedDatabase();
        }
        
        [Test, Order(1)]
        public async Task CheckOrdersOfCustomers()
        {
            var customers = await customerRepository.GetAllCustomers();
            var products = await productRepository.GetAllProducts();
            foreach (var customer in customers)
            {
                foreach (var order in customer.orders) 
                {
                    Assert.AreEqual(order.orderData.TotalPrice, order.orderItems.Sum(x => x.productData.Quantity * products.ToList().Find(y => y.productID == x.productData.productID).productData.Price));
                    Assert.Greater(order.orderData.TotalPrice, 0);
                    Assert.Greater(order.orderItems.Sum(x => x.productData.Quantity * products.ToList().Find(y => y.productID == x.productData.productID).productData.Price), 0);
                }
            }
        }
        
        [Test, Order(2)]
        public async Task PlaceNewOrderForEachCustomer()
        {
            var customers = await customerRepository.GetAllCustomers();
            var products = await productRepository.GetAllProducts();
            int customerOrders = 0;
            foreach (var customer in customers)
            {
                customerOrders = customer.orders.Count;

                var order = await customerRepository.AddNewOrderForCustomer(customer.customerID, new OrderData { OrderDate = DateTime.Now, TotalPrice = 100.00 }, new List<OrderProductData>() { 
                    new OrderProductData { productID = products.ToList()[3].productID, Quantity = 2 } });

                Assert.AreEqual(customerOrders + 1, customer.orders.Count);
            }
        }

        [Test, Order(3)]
        public async Task DeleteLastOrder()
        {
            var customers = await customerRepository.GetAllCustomers();
            var products = await productRepository.GetAllProducts();
            foreach (var customer in customers)
            {
                Assert.DoesNotThrowAsync(async () => {
                    await customerRepository.DeleteOrderFromCustomer(customer.customerID, customer.orders.Last().orderID);
                });
            }
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
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

            context.Products.AddRange(listOfProducts);
            context.Customers.AddRange(customers);

            context.SaveChanges();
        }
    }
}