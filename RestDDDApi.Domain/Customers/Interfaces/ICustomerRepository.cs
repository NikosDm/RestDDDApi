using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Domain.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> AddNewCustomer(CustomerAddress address, CustomerFullName fullName);
        Task<Customer> UpdateCustomerDetails(Guid customerID, CustomerAddress address, CustomerFullName fullName);
        Task DeleteCustomer(Guid customerID);
        Task<IEnumerable<Order>> GetCustomerOrdersByCustomerID(Guid customerID);
        Task<Order> AddNewOrderForCustomer(Guid customerID, OrderData orderData, IEnumerable<OrderProductData> productDatas);
        Task<Order> UpdateCustomerOrder(Guid customerID, Guid orderID,  OrderData orderData, IEnumerable<OrderProductData> productDatas);
        Task DeleteOrderFromCustomer(Guid customerID, Guid orderID);
        Task<IEnumerable<OrderItem>> GetOrderItemsPerOrder(Guid customerID, Guid orderID);
        Task<OrderItem> AddNewOrderItemOnOrder(Guid customerID, Guid orderID, OrderProductData productData);
        Task<OrderItem> UpdateOrderItemOnCustomerOrder(Guid customerID, Guid orderID, Guid orderItemID, OrderProductData productData);
    }
}