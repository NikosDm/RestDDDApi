using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Api.Queries.Customers;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Customers.Orders;
using RestDDDApi.Domain.Interfaces;

namespace RestDDDApi.Api.Queries.Handlers;

public class CustomerQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerQueryHandler(IUnitOfWork unitOfWork) 
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CustomerDetailsDTO>> Handle(GetAllCustomersQuery query)
    {
        var customers =  await this._unitOfWork.customerRepository.GetAllCustomers();

        return customers.ToList().Select(c => {
            return new CustomerDetailsDTO 
            {
                CustomerId = c.customerID,
                fullName = c.fullName,
                address = c.address,
                orderDTOs = c.orders.Select(o => {
                    return new CustomerOrderDTO 
                    {
                        orderID = o.orderID ,
                        customerID = c.customerID,
                        orderData = o.orderData,
                        productDatas = o.orderItems.Select( x => {
                            return OrderProductData.createNewOrderProductData(x.productData.productID, x.productData.Quantity);
                        })
                    };
                })
            };
        });
    }

    public async Task<CustomerDetailsDTO> Handle(GetCustomerDetailsQuery query)
    {
        var customer =  await GetCustomer(query.customerID);

        return new CustomerDetailsDTO 
        {
            CustomerId = customer.customerID,
            fullName = customer.fullName,
            address = customer.address,
            orderDTOs = customer.orders.Select(o => {
                return new CustomerOrderDTO 
                {
                    orderID = o.orderID ,
                    customerID = customer.customerID,
                    orderData = o.orderData,
                    productDatas = o.orderItems.Select(x => {
                        return OrderProductData.createNewOrderProductData(x.productData.productID, x.productData.Quantity);
                    })
                };
            })
        };
    }

    public async Task<IEnumerable<CustomerOrderDTO>> Handle(GetOrdersByDateQuery query) 
    {
        var orders =  await this._unitOfWork.customerRepository.GetCustomerOrdersBySpecificDate(query.dateTime);

        return orders.Select(o => {
            return new CustomerOrderDTO 
            {
                orderID = o.orderID,
                orderData = o.orderData,
                productDatas = o.orderItems.Select(x => {
                    return OrderProductData.createNewOrderProductData(x.productData.productID, x.productData.Quantity);
                })
            };
        });
    }

    public async Task<IEnumerable<CustomerOrderDTO>> Handle(GetCustomerOrdersQuery query) 
    {
        var customer =  await GetCustomer(query.CustomerID);
        
        return customer.orders.Select(o => {
            return new CustomerOrderDTO 
            {
                orderID = o.orderID,
                orderData = o.orderData,
                productDatas = o.orderItems.Select(x => {
                    return OrderProductData.createNewOrderProductData(x.productData.productID, x.productData.Quantity);
                })
            };
        });
    }

    public async Task<IEnumerable<CustomerOrderItemDTO>> Handle(GetOrderItemsPerOrderQuery query) 
    {
        var orderItems = await this._unitOfWork.customerRepository.GetOrderItemsPerOrder(query.customerID, query.orderID);

        return orderItems.Select(x => {
            return new CustomerOrderItemDTO 
            {
                orderItemID = x.orderItemID,
                customerID = query.customerID,
                orderID = query.orderID,
                ProductID = x.productData.productID,
                ProductPrice = x.productData.ProductPrice,
                Quantity = x.productData.Quantity
            };
        });
    }

    private async Task<Customer> GetCustomer(Guid CustomerID) 
    {
        var customer =  await this._unitOfWork.customerRepository.GetCustomerByID(CustomerID);

        if (customer == null) throw new Exception("Customer does not exist");

        return customer;
    }
}
