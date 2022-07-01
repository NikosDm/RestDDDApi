using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Domain.Customers
{
    public class Customer
    {
        public CustomerID customerID { get; private set; }
        private CustomerFullName fullName { get; set; }
        private CustomerAddress address { get; set; }
        public List<Order> orders { get; private set; }
        private Customer() { }
        private Customer(CustomerFullName fullName, CustomerAddress address)
        {
            this.customerID = new CustomerID(Guid.NewGuid());
            this.fullName = fullName;
            this.address = address;
            this.orders = new List<Order>();
        }

        public static Customer createNewCustomer(CustomerFullName fullName, CustomerAddress address) 
        {
            return new Customer(fullName, address);
        }

        public Order placeNewOrder(OrderData orderData, IEnumerable<OrderProductData> productDatas) 
        {
            var order = Order.createNewOrder(orderData, productDatas);
            this.orders.Add(order);
            return order; 
        }

        public void UpdateCustomerDetails(CustomerFullName fullName, CustomerAddress address) 
        {
            this.fullName.UpdateFullName(fullName);
            this.address.UpdateAddress(address);
        }

        public OrderItem addNewOrderItemOnCustomerOrder(OrderID orderID, OrderProductData productData) 
        {
            OrderItem item = null;
            foreach (var order in this.orders.Where(x => x.orderID == orderID))
                item = order.addNewOrderItem(productData);

            return item; 
        }

        public Order UpdateCustomerOrder(OrderID orderID,  OrderData orderData, IEnumerable<OrderProductData> productDatas) 
        {
            Order item = null;
            foreach (var order in this.orders.Where(x => x.orderID == orderID))
                order.UpdateOrderDetails(orderData, productDatas);

            return item; 
        }

        public OrderItem UpdateOrderItemOnSelectedOrder(OrderID orderID, OrderItemID orderItemID, OrderProductData productData) 
        {
            OrderItem item = null;
            foreach (var order in this.orders.Where(x => x.orderID == orderID))
                item = order.updateOrderItem(orderItemID, productData);

            return item; 
        }

        public void deleteOrder(OrderID orderID)
        {
            this.orders = orders.Where(x => x.orderID != orderID).ToList();
        }
    }
}