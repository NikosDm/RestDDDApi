using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Domain.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductsById(ProductID productID);
        Task<Product> AddNewProduct(ProductData productData);
        Task<Product> UpdateProduct(ProductData productData);
        Task DeleteProduct(ProductID productID);
    }
}