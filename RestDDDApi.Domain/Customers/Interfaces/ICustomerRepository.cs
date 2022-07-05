using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Domain.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerByID(Guid customerID);
        Task<Customer> AddNewCustomer(CustomerAddress address, CustomerFullName fullName);
        Task<IEnumerable<Order>> GetCustomerOrdersBySpecificDate(DateTime orderDate); 
        Task DeleteCustomer(Guid customerID);
        Task<Order> AddNewOrderForCustomer(Guid customerID, OrderData orderData, IEnumerable<OrderProductData> productDatas);
        Task<Customer> UpdateCustomerOrder(Customer customer);
        Task DeleteOrderFromCustomer(Guid customerID, Guid orderID);
        Task<IEnumerable<OrderItem>> GetOrderItemsPerOrder(Guid customerID, Guid orderID);
    }
}