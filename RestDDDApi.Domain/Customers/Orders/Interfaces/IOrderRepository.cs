using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers.Orders.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetAllOrdersByCustomerID(CustomerID customerID);
        Task<Order> AddNewOrder(Order order);
        Task<bool> DeleteOrder(OrderID orderID);
    }
}