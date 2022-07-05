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
    /// <summary>
    /// Customer Unit Tests
    /// </summary>
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
            
        /// <summary>
        /// For the inserted customer orders
        /// Verify that each Orders Total Price is equal to the sum of Products 
        /// that are included on the order 
        /// </summary>
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
        
        /// <summary>
        /// Check that new orders are stored correctly on database
        /// </summary>
        [Test, Order(2)]
        public async Task PlaceNewOrderForEachCustomer()
        {
            var customers = await customerRepository.GetAllCustomers();
            var products = await productRepository.GetAllProducts();
            int customerOrders = 0;
            foreach (var customer in customers)
            {
                customerOrders = customer.orders.Count;

                var order = await customerRepository.AddNewOrderForCustomer(customer.customerID, OrderData.createOrderData(DateTime.Now), new List<OrderProductData>() { 
                    OrderProductData.createNewOrderProductData(products.ToList()[3].productID, 2, products.ToList()[3].productData.Price)}
                );

                Assert.AreEqual(customerOrders + 1, customer.orders.Count);
                Assert.That(order.orderID, Is.Not.Empty);
            }
        }
        
        /// <summary>
        /// Check that order is deleted successfully
        /// </summary>
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

            context.Products.AddRange(listOfProducts);
            context.Customers.AddRange(customers);

            context.SaveChanges();
        }
    }
}