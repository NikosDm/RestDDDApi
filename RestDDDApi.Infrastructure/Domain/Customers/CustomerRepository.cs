using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Interfaces;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Infrastructure.Database;

namespace RestDDDApi.Infrastructure.Domain.Customers
{
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

        public async Task<Order> AddNewOrderForCustomer(CustomerID customerID, OrderData orderData, IEnumerable<OrderProductData> productDatas)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            Order order = customer.placeNewOrder(orderData, productDatas);
            _context.Customers.Update(customer);

            return order;
        }

        public async Task<OrderItem> AddNewOrderItemOnOrder(CustomerID customerID, OrderID orderID, OrderProductData productData)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            OrderItem orderItem = customer.addNewOrderItemOnCustomerOrder(orderID, productData);
            return orderItem;
        }

        public async Task DeleteCustomer(CustomerID customerID)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            _context.Customers.Remove(customer);
        }

        public async Task DeleteOrderFromCustomer(CustomerID customerID, OrderID orderID)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            customer.deleteOrder(orderID);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.Include(x => x.orders).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetCustomerOrdersByCustomerID(CustomerID customerID)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            return customer.orders;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsPerOrder(CustomerID customerID, OrderID orderID)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            return customer.orders.Where(x => x.orderID == orderID).First().orderItems;
        }

        public async Task<Customer> UpdateCustomerDetails(CustomerID customerID, CustomerAddress address, CustomerFullName fullName)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            customer.UpdateCustomerDetails(fullName, address);
            _context.Customers.Update(customer);

            return customer;
        }

        public async Task<Order> UpdateCustomerOrder(CustomerID customerID, OrderID orderID,  OrderData orderData, IEnumerable<OrderProductData> productDatas)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            var updatedOrder = customer.UpdateCustomerOrder(orderID, orderData, productDatas);
            _context.Customers.Update(customer);
            
            return updatedOrder;
        }

        public async Task<OrderItem> UpdateOrderItemOnCustomerOrder(CustomerID customerID, OrderID orderID, OrderItemID orderItemID, OrderProductData productData)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            var updatedItem = customer.UpdateOrderItemOnSelectedOrder(orderID, orderItemID, productData);
            _context.Customers.Update(customer);

            return updatedItem;
        }

    }
}