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
        
        [Test, Order(1)]
        public async Task CheckNumberOfProducts()
        {
            var result = await productRepository.GetAllProducts();
            result = result.ToList();

            Assert.That(result.Count, Is.EqualTo(5));
        }

        [Test, Order(2)]
        public async Task CheckAddingNewProduct()
        {
            var productData = new ProductData { Name = "New Product", Price = 15.99 };
            var result = await productRepository.AddNewProduct(productData);

            Assert.That(result.productID, Is.Not.Empty);
        }

        [Test, Order(3)]
        public async Task CheckForNegativePrices()
        {
            var result = await productRepository.GetAllProducts();

            Assert.That(result.Where(x => x.productData.Price <= 0), Is.Empty);
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
                Product.createNewProduct(new ProductData { Name = "Product One", Price = 1.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Two", Price = 5.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Three", Price = 1.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Four", Price = 10.00 }), 
                Product.createNewProduct(new ProductData { Name = "Product Five", Price = 20.00 }) 
            };

            context.Products.AddRange(listOfProducts);

            context.SaveChanges();
        }
    }
}