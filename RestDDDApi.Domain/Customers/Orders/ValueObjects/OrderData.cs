using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class OrderData
    {
        public DateTime OrderDate { get; set; }
        public Double TotalPrice { get; set; }
        public void UpdateOrderData(DateTime orderDate, Double totalPrice)
        {
            this.OrderDate = orderDate;
            this.TotalPrice = totalPrice;
        } 
    }
}