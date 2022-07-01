using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Domain.Customers;
using RestDDDApi.Domain.Interfaces;

namespace RestDDDApi.Api.Controllers;

public class CustomersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    protected ResponseDTO _response; 

    public CustomersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        this._response = new ResponseDTO();
    }

    [HttpGet("GetAllCustomers")]
    public async Task<ActionResult<ResponseDTO>> GetAllCustomers() 
    {
        var customers = await _unitOfWork.customerRepository.GetAllCustomers();
        _response.Result = customers;

        return Ok(_response);
    }

    [HttpPost("AddNewCustomer")]
    public async Task<ActionResult<ResponseDTO>> AddNewCustomer([FromBody]CustomerDetailsDTO customerDetailsDTO) 
    {
        try 
        {
            var newCustomer =  await _unitOfWork.customerRepository.AddNewCustomer(customerDetailsDTO.address, customerDetailsDTO.fullName);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = newCustomer;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPut("UpdateCustomerDetails")]
    public async Task<ActionResult<ResponseDTO>> UpdateCustomerDetails([FromBody]CustomerDetailsDTO customerDetailsDTO) 
    {
        try 
        {
            var currentCustomer = await _unitOfWork.customerRepository.UpdateCustomerDetails(customerDetailsDTO.CustomerId, customerDetailsDTO.address, customerDetailsDTO.fullName);
            
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = currentCustomer;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpDelete("DeleteCustomer")]
    public async Task<ActionResult<ResponseDTO>> DeleteCustomer(Guid customerID) 
    {
        try 
        {
            await _unitOfWork.customerRepository.DeleteCustomer(customerID);
            
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = true;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpGet("GetCustomerOrders/{customerID}")]
    public async Task<ActionResult<ResponseDTO>> GetCustomerOrders(Guid customerID) 
    {
        try 
        {
             var orders = await _unitOfWork.customerRepository.GetCustomerOrdersByCustomerID(customerID);
            _response.Result = orders;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpPost("AddNewOrderForCustomer")]
    public async Task<ActionResult<ResponseDTO>> AddNewOrderForCustomer([FromBody]CustomerOrderDTO customerDTO)
    {
        try 
        {
             var newOrder = await _unitOfWork.customerRepository.AddNewOrderForCustomer(customerDTO.customerID, customerDTO.orderData, customerDTO.productDatas);
             
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = newOrder;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpPut("UpdateCustomerOrder")]
    public async Task<ActionResult<ResponseDTO>> UpdateCustomerOrder([FromBody]CustomerOrderDTO customerDTO)
    {
        try 
        {
             var newOrder = await _unitOfWork.customerRepository.UpdateCustomerOrder(customerDTO.customerID, customerDTO.orderID, customerDTO.orderData, customerDTO.productDatas);
             
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = newOrder;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpDelete("DeleteOrderFromCustomer")]
    public async Task<ActionResult<ResponseDTO>> DeleteOrderFromCustomer([FromQuery]CustomerOrderDTO customerOrderDTO) 
    {
        try 
        {
            await _unitOfWork.customerRepository.DeleteOrderFromCustomer(customerOrderDTO.customerID, customerOrderDTO.orderID);
            
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = true;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpGet("GetOrderItemsPerOrder")]
    public async Task<ActionResult<ResponseDTO>> GetOrderItemsPerOrder([FromQuery]CustomerOrderDTO customerOrderDTO)
    {
        try 
        {
             var orderItems = await _unitOfWork.customerRepository.GetOrderItemsPerOrder(customerOrderDTO.customerID, customerOrderDTO.orderID);
            _response.Result = orderItems;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpPost("AddNewOrderItem")]
    public async Task<ActionResult<ResponseDTO>> AddNewOrderItemOnOrder([FromBody]CustomerOrderItemDTO customerOrderItemDTO) 
    {
        try 
        {
            var newOrderItem =  await _unitOfWork.customerRepository.AddNewOrderItemOnOrder(customerOrderItemDTO.customerID, customerOrderItemDTO.orderID, customerOrderItemDTO.orderProductData);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = newOrderItem;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPut("UpdateOrderItem")]
    public async Task<ActionResult<ResponseDTO>> UpdateOrderItemOnCustomerOrder([FromBody]CustomerOrderItemDTO customerOrderItemDTO)
    {
        try 
        {
             var newOrderItem = await _unitOfWork.customerRepository.UpdateOrderItemOnCustomerOrder(customerOrderItemDTO.customerID, customerOrderItemDTO.orderID, customerOrderItemDTO.orderItemID, customerOrderItemDTO.orderProductData);
             
            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = newOrderItem;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
}

