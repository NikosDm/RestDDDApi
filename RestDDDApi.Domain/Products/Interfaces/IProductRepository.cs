using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<ProductID> ids);
        Task<Product> AddNewProduct();
        Task<Product> UpdateProduct();
        Task<bool> DeleteProduct(ProductID productID);
    }
}