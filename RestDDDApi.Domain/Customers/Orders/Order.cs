using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace RestDDDApi.Domain.Customers.Orders
{
    public class Order
    {
        public Guid orderID { get; private set; }
        private OrderData orderData { get; set; }
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

        public static Order createNewOrder(OrderData orderData, IEnumerable<OrderProductData> productDatas) 
        {
            return new Order(orderData, productDatas);
        }

        public OrderItem addNewOrderItem(OrderProductData productData) 
        {
            var orderItem = OrderItem.createNewOrderItem(productData, this.orderID);
            this.orderItems.Add(orderItem);
            return orderItem;
        }

        public void UpdateOrderDetails(OrderData orderData, IEnumerable<OrderProductData> productDatas) 
        {
            this.orderData.UpdateOrderData(orderData.OrderDate, orderData.TotalPrice);

            foreach (var productData in productDatas) {
                this.orderItems.Add(OrderItem.createNewOrderItem(productData, orderID));
            }
        }

        public OrderItem updateOrderItem(Guid orderItemID, OrderProductData productData) 
        {
            OrderItem item = null;
            foreach (var orderItem in this.orderItems.Where(x => x.orderItemID == orderItemID)){
                orderItem.UpdateOrderItemData(productData);
                item = orderItem;
            }

            return item;
        }
    }
}