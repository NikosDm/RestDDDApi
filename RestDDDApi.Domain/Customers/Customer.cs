using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Domain.Customers
{
    /// <summary>
    /// Aggregate root object
    /// Represents Customer who makes new orders
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Property Key
        /// </summary>
        public Guid customerID { get; private set; }
        
        /// <summary>
        /// Value Object which represents full name of customer
        /// </summary>
        public CustomerFullName fullName { get; private set; }
        
        /// <summary>
        /// Value Object which represents address of customer
        /// </summary>
        public CustomerAddress address { get; private set; }
        
        /// <summary>
        /// List orders of a customer
        /// </summary>
        public List<Order> orders { get; private set; }

        /// <summary>
        /// Default private constructor
        /// </summary>
        private Customer() { }
        
        private Customer(CustomerFullName fullName, CustomerAddress address)
        {
            this.customerID = Guid.NewGuid();
            this.fullName = fullName;
            this.address = address;
            this.orders = new List<Order>();
        }
        
        private Customer(string FirstName, string LastName, string Street, string PostalCode)
        {
            this.customerID = Guid.NewGuid();
            this.fullName = CustomerFullName.createNewCustomerFullName(FirstName, LastName);
            this.address = CustomerAddress.createNewCustomerAddress(Street, PostalCode);
            this.orders = new List<Order>();
        }

        /// <summary>
        /// Static method that creates an instance of Customer class. Since DDD patern is used
        /// instantiation, modification and validation of objects takes place inside class.
        /// </summary>
        /// <param name="fullName">Value Object that contains full name details of customer</param>
        /// <param name="address">Value Object that contains address details of customer</param>
        /// <returns>New instance of Customer Class</returns>
        public static Customer createNewCustomer(CustomerFullName fullName, CustomerAddress address) 
        {
            return new Customer(fullName, address);
        }

        /// <summary>
        /// Static method that creates an instance of Customer class. 
        /// </summary>
        /// <param name="FirstName">Customer First Name</param>
        /// <param name="LastName">Customer Last Name</param>
        /// <param name="Street">Customer Street Name of residency</param>
        /// <param name="PostalCode">Customer Postal code of residency </param>
        /// <returns>New instance of Customer Class</returns>
        public static Customer createNewCustomer(string FirstName, string LastName, string Street, string PostalCode) 
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) 
                throw new Exception("First and Last name are required");
                
            return new Customer(FirstName, LastName, Street, PostalCode);
        }

        /// <summary>
        /// Static method that creates an instance of Order class. 
        /// </summary>
        /// <param name="orderData">Value object that contains details of Order</param>
        /// <param name="productDatas">List of Value object that contains details regarding items in an order</param>
        /// <returns>New instance of Order Class</returns>
        public Order placeNewOrder(OrderData orderData, IEnumerable<OrderProductData> productDatas) 
        {
            var order = Order.createNewOrder(orderData, productDatas);
            this.orders.Add(order);
            return order; 
        }

        /// <summary>
        /// Updates details of Customer
        /// </summary>
        /// <param name="fullName">Value Object that contains full name details of customer</param>
        /// <param name="address">Value Object that contains address details of customer</param>
        public void UpdateCustomerDetails(CustomerFullName fullName, CustomerAddress address) 
        {
            this.fullName.UpdateFullName(fullName);
            this.address.UpdateAddress(address);
        }

        /// <summary>
        /// Adds a new order item on a specific order of a customer
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <param name="productData">Value object that contains details regarding new item</param>
        /// <returns>New instance of Order Item Class</returns>
        public OrderItem addNewOrderItemOnCustomerOrder(Guid orderID, OrderProductData productData) 
        {
            OrderItem item = null;
            foreach (var order in this.orders.Where(x => x.orderID == orderID))
                item = order.addNewOrderItem(productData);

            return item; 
        }

        /// <summary>
        /// Updates a specific order item on a specific order of the customer
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <param name="orderItemID">Order Item ID</param>
        /// <param name="productData">Value object that contains details regarding existing item</param>
        /// <returns>Returns updated instance of Order Item Class</returns>
        public OrderItem UpdateOrderItemOnSelectedOrder(Guid orderID, Guid orderItemID, OrderProductData productData) 
        {
            OrderItem item = null;
            foreach (var order in this.orders.Where(x => x.orderID == orderID))
                item = order.updateOrderItem(orderItemID, productData);

            return item; 
        }

        /// <summary>
        /// Deletes an order
        /// </summary>
        public void deleteOrder(Guid orderID)
        {
            this.orders = orders.Where(x => x.orderID != orderID).ToList();
        }
    }
}