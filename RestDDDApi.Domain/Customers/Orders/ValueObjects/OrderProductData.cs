using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class OrderProductData
    {
        public Guid productID { get; set; }
        public int Quantity  { get; set; }
        public void UpdateOrderProductData(OrderProductData productData) 
        {
            this.productID = productData.productID;
            this.Quantity = productData.Quantity;
        }
    }
}