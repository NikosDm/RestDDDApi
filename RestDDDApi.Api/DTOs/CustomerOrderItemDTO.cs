using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Api.DTOs
{
    public class CustomerOrderItemDTO
    {
        public Guid customerID { get; set; }
        public Guid orderID { get; set; }
        public Guid orderItemID { get; set; }
        public OrderProductData orderProductData { get; set; }
    }
}