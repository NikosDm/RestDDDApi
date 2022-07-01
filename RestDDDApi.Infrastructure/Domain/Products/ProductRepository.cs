using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestDDDApi.Domain.Products;
using RestDDDApi.Domain.Products.Interfaces;
using RestDDDApi.Domain.Products.ValueObjects;
using RestDDDApi.Infrastructure.Database;

namespace RestDDDApi.Infrastructure.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        
        public async Task<Product> AddNewProduct(ProductData productData)
        {
            Product customer = Product.createNewProduct(productData);
            await _context.Products.AddAsync(customer);
            return customer;
        }

        public async Task DeleteProduct(Guid productID)
        {
            var product = await _context.Products.FindAsync(productID);
            _context.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductsById(Guid productID)
        {
            return await _context.Products.FindAsync(productID);
        }

        public async Task<Product> UpdateProduct(ProductData productData)
        {
            throw new NotImplementedException();
        }
    }
}