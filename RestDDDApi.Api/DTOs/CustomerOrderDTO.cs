using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Api.DTOs
{
    public class CustomerOrderDTO
    {
        public Guid customerID { get; set; }
        public Guid orderID { get; set; }
        public OrderData orderData { get; set; }
        public IEnumerable<OrderProductData> productDatas { get; set; }
    }
}