using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<IEnumerable<Customer>> GetCustomersByIds(IEnumerable<CustomerID> customerIDs);
        Task<Customer> AddNewCustomer(Customer customer);
        Task<Customer> UpdateCustomerDetails(Customer customer);
        Task<bool> DeleteCustomer(CustomerID customerID);
    }
}