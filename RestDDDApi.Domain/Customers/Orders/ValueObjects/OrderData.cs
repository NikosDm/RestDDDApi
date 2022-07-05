using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers.Orders;

/// <summary>
/// Value object
/// Represents Order details 
/// More specifically Date of order and Total price of order
/// </summary>
public class OrderData
{
    public DateTime OrderDate { get; private set; }
    public Double TotalPrice { get; private set; }
    
    private OrderData() { }

    private OrderData(DateTime OrderDate) 
    { 
        this.OrderDate = OrderDate;
        this.TotalPrice = 0;
    }

    /// <summary>
    /// Method used for updating Order data details
    /// More specifically order date and total price.
    /// </summary>
    /// <param name="orderDate">Order date</param>
    /// <param name="totalPrice">Total price of order</param>
    public void UpdateOrderData(DateTime orderDate, Double totalPrice)
    {
        this.OrderDate = orderDate;
        this.TotalPrice = totalPrice;
    }

    /// <summary>
    /// Static method that creates an instance of OrderData class. 
    /// </summary>
    /// <param name="OrderDate">Date of order</param>
    /// <returns>New instance of OrderData Class</returns>
    public static OrderData createOrderData(DateTime OrderDate) 
    {
        return new OrderData(OrderDate);
    }

    /// <summary>
    /// Sets the total price to Order
    /// </summary>
    /// <param name="TotalPrice">Total price of order</param>
    public void setTotalPrice(Double TotalPrice) 
    {
        this.TotalPrice = TotalPrice;
    }
}
