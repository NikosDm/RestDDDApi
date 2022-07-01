using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Domain.Products
{
    public class Product
    {
        public ProductID productID { get; private set; }
        public ProductData productData { get; set; }
        private Product() { }
        private Product(ProductData productData)
        {
            this.productID = new ProductID(Guid.NewGuid());
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