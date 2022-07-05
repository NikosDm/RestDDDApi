using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace RestDDDApi.Domain.Customers.Orders;

/// <summary>
/// Entity Order
/// </summary>
public class Order
{
    public Guid orderID { get; private set; }
    
    public OrderData orderData { get; private set; }
    
    public List<OrderItem> orderItems { get; set; }
    
    private Order() { }

    private Order(OrderData orderData, IEnumerable<OrderProductData> productDatas)
    {
        this.orderID = Guid.NewGuid();
        
        this.orderData = orderData;
        this.orderItems = new List<OrderItem>();

        foreach (var item in productDatas)
            this.orderItems.Add(OrderItem.createNewOrderItem(item, this.orderID));
    }

    private Order(DateTime orderDate, IEnumerable<OrderProductData> productDatas)
    {
        double totalAmount = 0;
        this.orderID = Guid.NewGuid();
        
        this.orderData = OrderData.createOrderData(orderDate);
        this.orderItems = new List<OrderItem>();

        foreach (var item in productDatas){
            this.orderItems.Add(OrderItem.createNewOrderItem(item, this.orderID));
            totalAmount += item.GetTotalOrderItemAmount();
        }

        this.orderData.setTotalPrice(totalAmount);
    }

    /// <summary>
    /// Static method that creates an instance of Order class. 
    /// </summary>
    /// <param name="orderData">Value object that contains Order details</param>
    /// <param name="productDatas">List of value objects that contain details of Order Item</param>
    /// <returns>New instance of Order Class</returns>
    public static Order createNewOrder(OrderData orderData, IEnumerable<OrderProductData> productDatas) 
    {
        return new Order(orderData, productDatas);
    }

    /// <summary>
    /// Adds a new product - item to the order 
    /// </summary>
    /// <param name="productData">Value object that contains Order Item details details</param>
    /// <returns>New instance of Order Item Class</returns>
    public OrderItem addNewOrderItem(OrderProductData productData) 
    {
        var orderItem = OrderItem.createNewOrderItem(productData, this.orderID);
        this.orderItems.Add(orderItem);
        return orderItem;
    }

    /// <summary>
    /// Updates an order
    /// </summary>
    /// <param name="orderData">Value object that contains updated Order details</param>
    /// <param name="productDatas">List of value objects that contain updated details of Order Item</param>
    public void UpdateOrderDetails(OrderData orderData, IEnumerable<OrderProductData> productDatas) 
    {
        this.orderData.UpdateOrderData(orderData.OrderDate, orderData.TotalPrice);

        foreach (var productData in productDatas) {
            this.orderItems.Add(OrderItem.createNewOrderItem(productData, orderID));
        }
    }

    /// <summary>
    /// Updates a new product - item to the order 
    /// </summary>
    /// <param name="orderItemID">Id of order item</param>
    /// <param name="productData">Value object that contains updated Order Item details details</param>
    /// <returns>Updated instance of Order Item Class</returns>
    public OrderItem updateOrderItem(Guid orderItemID, OrderProductData productData) 
    {
        OrderItem item = null;
        foreach (var orderItem in this.orderItems.Where(x => x.orderItemID == orderItemID)){
            orderItem.UpdateOrderItemData(productData);
            item = orderItem;
        }

        this.orderData.setTotalPrice(this.orderItems.Sum(x => x.productData.ProductPrice * x.productData.Quantity));

        return item;
    }
}
