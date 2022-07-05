using RestDDDApi.Api.DTOs;

namespace RestDDDApi.Api.Commands.Customers;

public class CreateNewOrderItemCommand
{
    public Guid CustomerID { get; set; }
    public Guid OrderID { get; set; }
    public Guid ProductID { get; set; }
    public int Quantity  { get; set; }
    public double ProductPrice { get; set; }

    public CreateNewOrderItemCommand() { }

    public CreateNewOrderItemCommand(NewCustomerOrderItemDTO customerOrderItemDTO) 
    { 
        this.CustomerID = customerOrderItemDTO.customerID;
        this.OrderID = customerOrderItemDTO.orderID;
        this.ProductID = customerOrderItemDTO.ProductID;
        this.Quantity = customerOrderItemDTO.Quantity;
        this.ProductPrice = customerOrderItemDTO.ProductPrice;
    }

}
