using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Products.ValueObjects
{
    public class ProductData
    {
        public string Name { get; set; }
        public Double Price { get; set; }
        public void UpdateProductData(ProductData productData)
        {
            this.Name = productData.Name;
            this.Price = productData.Price;
        }
    }
}