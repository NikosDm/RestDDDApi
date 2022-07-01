using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class OrderItem
    {
        public Guid orderItemID { get; set; }
        
        public Guid orderID { get; set; }
        
        public OrderProductData productData { get; set; }
        private OrderItem() { }
        private OrderItem(OrderProductData productData, Guid orderID)
        {
            this.orderItemID = Guid.NewGuid();
            this.orderID = orderID;
            this.productData = productData;
        }

        public static OrderItem createNewOrderItem(OrderProductData productData, Guid orderID)  
        {
            return new OrderItem(productData, orderID);
        }

        public void UpdateOrderItemData(OrderProductData productData) 
        {
            this.productData.UpdateOrderProductData(productData);
        }
    }
}