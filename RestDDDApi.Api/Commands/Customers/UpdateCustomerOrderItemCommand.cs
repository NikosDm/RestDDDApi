using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;

namespace RestDDDApi.Api.Commands.Customers
{
    public class UpdateCustomerOrderItemCommand
    {
        public Guid CustomerID { get; set; }
        public Guid OrderID { get; set; }
        public Guid OrderItemID { get; set; }
        public Guid ProductID { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

        public UpdateCustomerOrderItemCommand() { }

        public UpdateCustomerOrderItemCommand(CustomerOrderItemDTO customerOrderItemDTO) 
        {
            this.CustomerID = customerOrderItemDTO.customerID;
            this.OrderID = customerOrderItemDTO.orderID;
            this.ProductID = customerOrderItemDTO.ProductID;
            this.Quantity = customerOrderItemDTO.Quantity;
            this.ProductPrice = customerOrderItemDTO.ProductPrice;
        }
    }
}