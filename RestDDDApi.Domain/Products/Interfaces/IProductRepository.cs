using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Domain.Products.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductsById(Guid productID);
    Task<Product> AddNewProduct(ProductData productData);
    Task<Product> UpdateProduct(Guid productID, ProductData productData);
    Task DeleteProduct(Guid productID);
}
