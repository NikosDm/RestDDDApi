using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Api.Commands.Customers;

public class CreateNewOrderCommand
{
    public Guid CustomerID { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderProductData> OrderItems { get; set; }

    public CreateNewOrderCommand() { }
    public CreateNewOrderCommand(NewCustomerOrderDTO customerOrderDTO) 
    { 
        this.CustomerID = customerOrderDTO.customerID;
        this.OrderDate = customerOrderDTO.orderData.OrderDate;
        this.OrderItems = customerOrderDTO.productDatas.ToList();
    }
}
