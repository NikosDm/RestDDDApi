using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestDDDApi.Domain.Products;
using RestDDDApi.Domain.Products.Interfaces;
using RestDDDApi.Domain.Products.ValueObjects;
using RestDDDApi.Infrastructure.Database;
using RestDDDApi.Infrastructure.Domain.Products;

namespace Tests
{    
    /// <summary>
    /// Product Unit Tests
    /// </summary>
    public class ProductUnitTest
    {
        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "RestDDDApiTest")
            .Options;

        DataContext context;
        IProductRepository productRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new DataContext(dbContextOptions);
            context.Database.EnsureCreated();
            productRepository = new ProductRepository(context);

            SeedDatabase();
        }
        
        /// <summary>
        /// Check that the initial insert of products is retrieved from ORM
        /// </summary>
        [Test, Order(1)]
        public async Task CheckNumberOfProducts()
        {
            var result = await productRepository.GetAllProducts();
            result = result.ToList();

            Assert.That(result.Count, Is.EqualTo(5));
        }

        /// <summary>
        /// Check that a product is added successfully to the database
        /// </summary>
        [Test, Order(2)]
        public async Task CheckAddingNewProduct()
        {
            var productData = ProductData.createProductData("New Product", 15.99 );
            var result = await productRepository.AddNewProduct(productData);

            Assert.That(result.productID, Is.Not.Empty);
        }

        /// <summary>
        /// Check that all Products have positive value price
        /// </summary>
        [Test, Order(3)]
        public async Task CheckForNegativePrices()
        {
            var result = await productRepository.GetAllProducts();

            Assert.That(result.Where(x => x.productData.Price <= 0), Is.Empty);
        }

        /// <summary>
        /// Validation checks regarding Product class.
        /// More specifically regarding Product Name and price
        /// </summary>
        /// <remarks>
        /// Product name should not be empty or null 
        /// Product price should contain a positive value
        /// </remarks>
        [Test, Order(4)]
        public void ValidationOfProductData()
        {
            Assert.That(() => Product.createNewProduct("", 0), Throws.Exception);
            Assert.That(() => Product.createNewProduct(null, 0), Throws.Exception);
            Assert.That(() => Product.createNewProduct("", -45.00), Throws.Exception);
            Assert.That(() => Product.createNewProduct(string.Empty, 0), Throws.Exception);
            Assert.That(() => Product.createNewProduct(string.Empty, -45.00), Throws.Exception);
            Assert.That(() => Product.createNewProduct(null, -45.00), Throws.Exception);
            Assert.That(() => Product.createNewProduct("Dummy Name", -45.00), Throws.Exception);
            Assert.That(() => Product.createNewProduct("Dummy Name", 0), Throws.Exception); 
            Assert.That(() => Product.createNewProduct("", 9.95), Throws.Exception); 
            Assert.That(() => Product.createNewProduct(null, 15.55), Throws.Exception); 

            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData(null, 0));
            }, Throws.Exception); 
            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData("", 0));
            }, Throws.Exception); 
            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData(null, -15.5));
            }, Throws.Exception); 
            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData("", -15.2));
            }, Throws.Exception); 
            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData("DummyName2", -15.5));
            }, Throws.Exception); 
            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData("", 9.95));
            }, Throws.Exception); 

            Assert.That(() => Product.createNewProduct("Dummy Name", 15.55), Throws.Nothing); 
            Assert.That(() => {
                Product product = Product.createNewProduct("DummyName", 15.55);
                product.updateProductData(ProductData.createProductData("DummyName2", 9.95));
            }, Throws.Nothing); 
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

            context.Products.AddRange(listOfProducts);

            context.SaveChanges();
        }
    }
}