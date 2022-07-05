using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Domain.Customers.Orders;

/// <summary>
/// Entity Order Item
/// </summary>
public class OrderItem
{
    public Guid orderItemID { get; private set; }
    public Guid orderID { get; private set; }
    public OrderProductData productData { get; private set; }

    private OrderItem() { }
    private OrderItem(OrderProductData productData, Guid orderID)
    {
        this.orderItemID = Guid.NewGuid();
        this.orderID = orderID;
        this.productData = productData;
    }
    
    /// <summary>
    /// Static method that creates an instance of Order Item class for a specified OrderID. 
    /// </summary>
    /// <param name="productData">Value object that contains Order details</param>
    /// <param name="orderID">Order ID with which the order item will be linked to</param>
    /// <returns>New instance of Order Item Class</returns>
    public static OrderItem createNewOrderItem(OrderProductData productData, Guid orderID)  
    {
        var orderProductData = OrderProductData.createNewOrderProductData(productData.productID, productData.Quantity, productData.ProductPrice);
        return new OrderItem(orderProductData, orderID);
    }
    
    /// <summary>
    /// Updates the details of an Order Item
    /// </summary>
    /// <param name="productData">Value object that contains Order Item details</param>
    public void UpdateOrderItemData(OrderProductData productData) 
    {
        this.productData.UpdateOrderProductData(productData);
    }
}
