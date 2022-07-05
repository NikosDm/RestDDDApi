using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;

namespace RestDDDApi.Api.Queries.Customers
{
    public class GetOrderItemsPerOrderQuery
    {
        public Guid customerID { get; set; }
        public Guid orderID { get; set; }

        public GetOrderItemsPerOrderQuery() { }

        public GetOrderItemsPerOrderQuery(CustomerOrderDTO customerOrderDTO) 
        { 
            this.customerID = customerOrderDTO.customerID;
            this.orderID = customerOrderDTO.orderID;
        }
    }
}