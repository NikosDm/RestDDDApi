using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class OrderItem
    {
        public OrderItemID orderItemID { get; set; }
        
        public OrderID orderID { get; set; }
        
        public OrderProductData productData { get; set; }
        private OrderItem() { }
        private OrderItem(OrderProductData productData, OrderID orderID)
        {
            this.orderItemID = new OrderItemID(Guid.NewGuid());
            this.orderID = orderID;
            this.productData = productData;
        }

        public static OrderItem createNewOrderItem(OrderProductData productData, OrderID orderID)  
        {
            return new OrderItem(productData, orderID);
        }

        public void UpdateOrderItemData(OrderProductData productData) 
        {
            this.productData.UpdateOrderProductData(productData);
        }
    }
}