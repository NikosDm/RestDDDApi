using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Api.DTOs
{
    public class ProductDetailsDTO
    {
        public Guid ProductID { get; set; }
        public ProductData ProductData { get; set; }
    }
}