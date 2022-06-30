using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Products
{
    public class Product
    {
        public ProductID ProductID { get; set; }
        public string Name { get; set; }
        public ProductPrice Price { get; set; }
    }
}