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
        
        /// <summary>
        /// ID if a product
        /// </summary>
        public Guid productID { get; private set; }
        
        /// <summary>
        /// Value Object which represents details of product
        /// </summary>
        public ProductData productData { get; set; }

        private Product() { }
        
        private Product(ProductData productData)
        {
            this.productID = Guid.NewGuid();
            this.productData = productData;
        }

        /// <summary>
        /// Static method that creates an instance of Product class. Since DDD patern is used
        /// instantiation, modification and validation of objects takes place inside class.
        /// </summary>
        /// <param name="productData">Value Object that contains details of a product</param>
        /// <returns>New instance of Product Class</returns>
        public static Product createNewProduct(ProductData productData) 
        {
            return new Product(productData);
        }
        
        /// <summary>
        /// Static method that creates an instance of Product class. 
        /// </summary>
        /// <param name="Name">Name of product</param>
        /// <param name="Price">Price of a product</param>
        /// <returns>New instance of Product Class</returns>
        public static Product createNewProduct(string Name, Double Price) 
        {
            ProductData productData = ProductData.createProductData(Name, Price);
            return new Product(productData);
        }
        
        /// <summary>
        /// Method that updates the details of an instance of Product class. 
        /// </summary>
        /// <param name="productData">Value Object that contains details of a product</param>
        public void updateProductData(ProductData productData) 
        {
            this.productData.UpdateProductData(productData);
        }
    }
}