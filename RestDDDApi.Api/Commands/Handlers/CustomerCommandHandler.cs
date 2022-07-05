using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.Commands.Customers;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Domain.Interfaces;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Api.Commands.Handlers;

/**/
public class CustomerCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerCommandHandler(IUnitOfWork unitOfWork) 
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<CustomerDetailsDTO> Handle(CreateNewCustomerCommand createNewCustomerCommand) 
    {
        var customerFullName = CustomerFullName.createNewCustomerFullName(createNewCustomerCommand.FirstName, createNewCustomerCommand.LastName);
        var customerAddress = CustomerAddress.createNewCustomerAddress(createNewCustomerCommand.Street, createNewCustomerCommand.PostalCode);
        var customer = await _unitOfWork.customerRepository.AddNewCustomer(customerAddress, customerFullName);

        if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

        return this.GetCustomerDetailsDTO(customer);
    }

    public async Task<CustomerOrderDTO> Handle(CreateNewOrderCommand createNewOrderCommand)
    {
        var customer = await GetCustomerById(createNewOrderCommand.CustomerID);

        if (customer == null) throw new Exception("Customer does not exist");

        var orderData = OrderData.createOrderData(createNewOrderCommand.OrderDate);

        var order = customer.placeNewOrder(orderData, createNewOrderCommand.OrderItems);

        await UpdateCustomer(customer);

        return new CustomerOrderDTO 
        {
            customerID = customer.customerID,
            orderID = order.orderID,
            orderData = order.orderData,
            productDatas = order.orderItems.Select(x => x.productData)
        };
    }
    
    public async Task<CustomerOrderItemDTO> Handle(CreateNewOrderItemCommand createNewOrderItemCommand)
    {
        var customer = await GetCustomerById(createNewOrderItemCommand.CustomerID);

        var products = await GetProductsAsync();

        if (!products.Any(x => x.productID == createNewOrderItemCommand.ProductID))
            throw new Exception("Product cannot be add on an Order since it does not exist");
        else {
            createNewOrderItemCommand.ProductPrice = products.FirstOrDefault(x => x.productID == createNewOrderItemCommand.ProductID).productData.Price;
        }

        var orderProductData = OrderProductData.createNewOrderProductData(createNewOrderItemCommand.ProductID, createNewOrderItemCommand.Quantity, createNewOrderItemCommand.ProductPrice);
        var orderItem = customer.addNewOrderItemOnCustomerOrder(createNewOrderItemCommand.OrderID, orderProductData);

        await UpdateCustomer(customer);

        return GetOrderItemDTO(orderItem, customer, createNewOrderItemCommand.OrderID);
    }
    
    public async Task<bool> Handle(DeleteCustomerCommand deleteCustomerCommand)
    {
        await _unitOfWork.customerRepository.DeleteCustomer(deleteCustomerCommand.CustomerID);

        if (_unitOfWork.HasChanges()) return await _unitOfWork.Complete();

        return false;
    }

    public async Task<CustomerDetailsDTO> Handle(UpdateCustomerCommand updateCustomerCommand)
    {
        var customer = await GetCustomerById(updateCustomerCommand.CustomerID);
        customer.UpdateCustomerDetails(CustomerFullName.createNewCustomerFullName(updateCustomerCommand.FirstName, updateCustomerCommand.LastName), CustomerAddress.createNewCustomerAddress(updateCustomerCommand.Street, updateCustomerCommand.PostalCode));

        await UpdateCustomer(customer);

        return this.GetCustomerDetailsDTO(customer);
    }

    public async Task<bool> Handle(DeleteCustomerOrderCommand deleteCustomerOrderCommand)
    {
        var customer = await GetCustomerById(deleteCustomerOrderCommand.CustomerID);

        customer.deleteOrder(deleteCustomerOrderCommand.OrderID);

        await _unitOfWork.customerRepository.UpdateCustomerOrder(customer);

        if (_unitOfWork.HasChanges()) return await _unitOfWork.Complete();

        return false;
    }

    public async Task<CustomerOrderItemDTO> Handle(UpdateCustomerOrderItemCommand updateCustomerOrderItemCommand)
    {
        var customer = await GetCustomerById(updateCustomerOrderItemCommand.CustomerID);
        var products = await GetProductsAsync();

        if (!products.Any(x => x.productID == updateCustomerOrderItemCommand.ProductID))
            throw new Exception("Product cannot be add on an Order since it does not exist");
        else {
            updateCustomerOrderItemCommand.ProductPrice = products.FirstOrDefault(x => x.productID == updateCustomerOrderItemCommand.ProductID).productData.Price;
        }

        var productData = OrderProductData.createNewOrderProductData(updateCustomerOrderItemCommand.ProductID, updateCustomerOrderItemCommand.Quantity, updateCustomerOrderItemCommand.ProductPrice);
        var orderItem = customer.UpdateOrderItemOnSelectedOrder(updateCustomerOrderItemCommand.OrderID, updateCustomerOrderItemCommand.OrderItemID, productData);

        await UpdateCustomer(customer);

        return GetOrderItemDTO(orderItem, customer, updateCustomerOrderItemCommand.OrderID);
    }

    private async Task<Customer> GetCustomerById(Guid customerId)
    {
        var customer = await _unitOfWork.customerRepository.GetCustomerByID(customerId);

        if (customer == null) throw new Exception("Customer does not exist");

        return customer;
    }

    private async Task UpdateCustomer(Customer customer) 
    {
        await _unitOfWork.customerRepository.UpdateCustomerOrder(customer);

        if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();
    }

    private CustomerDetailsDTO GetCustomerDetailsDTO(Customer customer) 
    {
        return new CustomerDetailsDTO 
        {
            CustomerId = customer.customerID,
            fullName = customer.fullName,
            address = customer.address
        };
    }

    private CustomerOrderItemDTO GetOrderItemDTO(OrderItem orderItem, Customer customer, Guid OrderID) 
    {    
        return new CustomerOrderItemDTO 
        {
            orderItemID = orderItem.orderItemID,
            customerID = customer.customerID,
            orderID = OrderID,
            ProductID = orderItem.productData.productID,
            ProductPrice = orderItem.productData.ProductPrice,
            Quantity = orderItem.productData.Quantity
        };
    }

    private async Task<IEnumerable<Product>> GetProductsAsync() 
    {
        return await _unitOfWork.productRepository.GetAllProducts();
    }
}
