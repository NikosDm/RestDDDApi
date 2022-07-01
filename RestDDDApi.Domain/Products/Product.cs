using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Domain.Products
{
    /// <summary>
    /// Aggregate root object
    /// Represents Item or Product of an order
    /// </summary>
    public class Product
    {
        public Guid productID { get; private set; }
        public ProductData productData { get; set; }
        private Product() { }
        private Product(ProductData productData)
        {
            this.productID = Guid.NewGuid();
            this.productData = productData;
        }

        public static Product createNewProduct(ProductData productData) 
        {
            return new Product(productData);
        }

        public void updateProductData(ProductData productData) 
        {
            this.productData.UpdateProductData(productData);
        }
    }
}