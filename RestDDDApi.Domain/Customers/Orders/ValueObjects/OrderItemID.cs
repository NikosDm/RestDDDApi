using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class OrderItemID
    {
        public Guid orderItemID { get; set; }
        
        public OrderItemID() { }

        public OrderItemID(Guid guid) 
        {
            this.orderItemID = guid;
        }
    }
}