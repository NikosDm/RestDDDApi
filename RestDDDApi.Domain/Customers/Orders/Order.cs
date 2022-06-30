using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class Order
    {
        public OrderID orderID { get; set; }
        public OrderData orderData { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
    }
}