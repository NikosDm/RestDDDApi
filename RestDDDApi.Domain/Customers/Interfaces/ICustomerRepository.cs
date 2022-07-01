using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Domain.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> AddNewCustomer(CustomerAddress address, CustomerFullName fullName);
        Task<Customer> UpdateCustomerDetails(CustomerID customerID, CustomerAddress address, CustomerFullName fullName);
        Task DeleteCustomer(CustomerID customerID);
        Task<IEnumerable<Order>> GetCustomerOrdersByCustomerID(CustomerID customerID);
        Task<Order> AddNewOrderForCustomer(CustomerID customerID, OrderData orderData, IEnumerable<OrderProductData> productDatas);
        Task<Order> UpdateCustomerOrder(CustomerID customerID, OrderID orderID,  OrderData orderData, IEnumerable<OrderProductData> productDatas);
        Task DeleteOrderFromCustomer(CustomerID customerID, OrderID orderID);
        Task<IEnumerable<OrderItem>> GetOrderItemsPerOrder(CustomerID customerID, OrderID orderID);
        Task<OrderItem> AddNewOrderItemOnOrder(CustomerID customerID, OrderID orderID, OrderProductData productData);
        Task<OrderItem> UpdateOrderItemOnCustomerOrder(CustomerID customerID, OrderID orderID, OrderItemID orderItemID, OrderProductData productData);
    }
}