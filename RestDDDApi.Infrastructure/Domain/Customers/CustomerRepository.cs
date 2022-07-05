using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Interfaces;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Infrastructure.Database;

namespace RestDDDApi.Infrastructure.Domain.Customers;

/// <summary>
/// Customer Repository
/// Includes all the CRUD operation which have to do with either Customer or its linked class (Order and OrderItem)
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;
    public CustomerRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Customer> AddNewCustomer(CustomerAddress address, CustomerFullName fullName)
    {
        Customer customer = Customer.createNewCustomer(fullName, address);
        await _context.Customers.AddAsync(customer);
        return customer;
    }

    public async Task<Order> AddNewOrderForCustomer(Guid customerID, OrderData orderData, IEnumerable<OrderProductData> productDatas)
    {
        var customer = await _context.Customers.FindAsync(customerID);
        Order order = customer.placeNewOrder(orderData, productDatas);
        _context.Customers.Update(customer);

        return order;
    }

    public async Task DeleteCustomer(Guid customerID)
    {
        var customer = await _context.Customers.FindAsync(customerID);
        _context.Customers.Remove(customer);
    }

    public async Task DeleteOrderFromCustomer(Guid customerID, Guid orderID)
    {
        var customer = await _context.Customers.FindAsync(customerID);
        customer.deleteOrder(orderID);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _context.Customers.Include(x => x.orders).ToListAsync();
    }

    public async Task<Customer> GetCustomerByID(Guid customerID)
    {
        return await _context.Customers.Include(x => x.orders).FirstOrDefaultAsync(c => c.customerID == customerID);
    }

    public async Task<IEnumerable<Order>> GetCustomerOrdersBySpecificDate(DateTime orderDate)
    {
        return await _context.Customers.AsNoTracking().Include(x => x.orders).SelectMany(x => x.orders).Where(x => x.orderData.OrderDate.Date == orderDate.Date).ToListAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsPerOrder(Guid customerID, Guid orderID)
    {
        var customer = await _context.Customers.FindAsync(customerID);
        return customer.orders.Where(x => x.orderID == orderID).First().orderItems;
    }

    public async Task<Customer> UpdateCustomerOrder(Customer customer)
    {
        _context.Customers.Update(customer);

        return await Task.FromResult(customer);
    }
}
