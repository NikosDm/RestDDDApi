using Microsoft.AspNetCore.Mvc;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Api.Queries.Handlers;
using RestDDDApi.Api.Queries.Customers;
using RestDDDApi.Api.Commands.Handlers;
using RestDDDApi.Api.Commands.Customers;

namespace RestDDDApi.Api.Controllers;

/// <summary>
/// Customer Api Controller
/// </summary>
/// <remarks>
/// Representes all the actions related to Customer and their respectfull Orders
/// that the RESTfull service can support.
/// Makes use of CQRS queries and commands for retrieving or modifying 
/// product records respectively
/// </remarks>
/// <returns>Every action returns an instance of ResponseDTO class which contains data or any exception that may occur</returns>
public class CustomersController : BaseApiController
{
    private readonly CustomerQueryHandler _customerQueryHandler;
    private readonly CustomerCommandHandler _customerCommandHandler;
    protected ResponseDTO _response; 

    public CustomersController(CustomerQueryHandler customerQueryHandler, CustomerCommandHandler customerCommandHandler)
    {
        _customerQueryHandler = customerQueryHandler;
        _customerCommandHandler = customerCommandHandler;
        this._response = new ResponseDTO();
    }

    /// <summary>
    /// Actions that retrieves all customers
    /// </summary>
    [HttpGet("GetAllCustomers")]
    public async Task<ActionResult<ResponseDTO>> GetAllCustomers() 
    {
        var customers = await _customerQueryHandler.Handle(new GetAllCustomersQuery());
        _response.Result = customers;

        return Ok(_response);
    }

    /// <summary>
    /// Action that retrieves a specific customer based on its ID
    /// </summary>
    /// <param name="customerID">Id of Customer. It is received from URI</param>
    [HttpGet("GetCustomer/{customerID}")]
    public async Task<ActionResult<ResponseDTO>> GetCustomer(Guid customerID) 
    {
        var customers = await _customerQueryHandler.Handle(new GetCustomerDetailsQuery { customerID = customerID });
        _response.Result = customers;

        return Ok(_response);
    }

    /// <summary>
    /// Action that retrieves order by order date.
    /// </summary>
    /// <param name="givenDate">Given order date</param>
    [HttpGet("GetOrdersByDate")]
    public async Task<ActionResult<ResponseDTO>> GetOrdersByDate([FromQuery]DateTime givenDate) 
    {
        var customers = await _customerQueryHandler.Handle(new GetOrdersByDateQuery { dateTime = givenDate });
        _response.Result = customers;

        return Ok(_response);
    }

    /// <summary>
    /// Action that stores a new customer
    /// </summary>
    /// <param name="customerDetailsDTO">New Customer data</param>
    [HttpPost("AddNewCustomer")]
    public async Task<ActionResult<ResponseDTO>> AddNewCustomer([FromBody]NewCustomerDetailsDTO customerDetailsDTO) 
    {
        try 
        {
            var command = new CreateNewCustomerCommand(customerDetailsDTO);
            var newCustomer =  await _customerCommandHandler.Handle(command);
            _response.Result = newCustomer;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Action that updates the details of an existing customer
    /// </summary>
    /// <param name="customerDetailsDTO">Current Customer data</param>
    [HttpPut("UpdateCustomerDetails")]
    public async Task<ActionResult<ResponseDTO>> UpdateCustomerDetails([FromBody]CustomerDetailsDTO customerDetailsDTO) 
    {
        try 
        {
            var command = new UpdateCustomerCommand(customerDetailsDTO);
            var currentCustomer = await _customerCommandHandler.Handle(command);
            _response.Result = currentCustomer;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Action that deletes an existing customer
    /// </summary>
    /// <param name="customerID">Customer ID</param>
    [HttpDelete("DeleteCustomer")]
    public async Task<ActionResult<ResponseDTO>> DeleteCustomer(Guid customerID) 
    {
        try 
        {
            _response.Result = await _customerCommandHandler.Handle(new DeleteCustomerCommand { CustomerID = customerID });
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Action that retieves all the orders of a customer
    /// </summary>
    /// <param name="customerID">Customer ID</param>
    [HttpGet("GetCustomerOrders/{customerID}")]
    public async Task<ActionResult<ResponseDTO>> GetCustomerOrders(Guid customerID) 
    {
        try 
        {
             var orders = await _customerQueryHandler.Handle(new GetCustomerOrdersQuery { CustomerID = customerID });
            _response.Result = orders;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    /// <summary>
    /// Action that stores a new order for an existing customer
    /// </summary>
    /// <param name="customerDTO">Customer ID</param>
    [HttpPost("AddNewOrderForCustomer")]
    public async Task<ActionResult<ResponseDTO>> AddNewOrderForCustomer([FromBody]NewCustomerOrderDTO customerDTO)
    {
        try 
        {
            var command = new CreateNewOrderCommand(customerDTO);
            var newOrder = await _customerCommandHandler.Handle(command);
            _response.Result = newOrder;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    /// <summary>
    /// Action that deletes an order of an existing customer
    /// </summary>
    /// <param name="customerOrderDTO">Model that contains Customer and Order ID</param>
    [HttpDelete("DeleteOrderFromCustomer")]
    public async Task<ActionResult<ResponseDTO>> DeleteOrderFromCustomer([FromQuery]CustomerOrderDTO customerOrderDTO) 
    {
        try 
        {
            _response.Result = await _customerCommandHandler.Handle(new DeleteCustomerOrderCommand { CustomerID = customerOrderDTO.customerID });
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    /// <summary>
    /// Action that retrieves all products contained on a specific order 
    /// </summary>
    /// <param name="customerOrderDTO">Model that contains Customer and Order ID</param>
    [HttpGet("GetOrderItemsPerOrder")]
    public async Task<ActionResult<ResponseDTO>> GetOrderItemsPerOrder([FromQuery]CustomerOrderDTO customerOrderDTO)
    {
        try 
        {
            var query = new GetOrderItemsPerOrderQuery(customerOrderDTO);
            var orderItems = await _customerQueryHandler.Handle(query);
            _response.Result = orderItems;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    /// <summary>
    /// Adds a new order item - product on existing order
    /// </summary>
    /// <param name="customerOrderItemDTO">Model that contains among details of the new order item, id of customer and order</param>
    [HttpPost("AddNewOrderItem")]
    public async Task<ActionResult<ResponseDTO>> AddNewOrderItemOnOrder([FromBody]NewCustomerOrderItemDTO customerOrderItemDTO) 
    {
        try 
        {
            var command = new CreateNewOrderItemCommand(customerOrderItemDTO);
            var newOrderItem =  await _customerCommandHandler.Handle(command);
            _response.Result = newOrderItem;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Updates a current product - order item on an order
    /// </summary>
    /// <param name="customerOrderItemDTO">Model that contains among details of the new order item, id of customer and order</param>
    [HttpPut("UpdateOrderItem")]
    public async Task<ActionResult<ResponseDTO>> UpdateOrderItemOnCustomerOrder([FromBody]CustomerOrderItemDTO customerOrderItemDTO)
    {
        try 
        {
            var command = new UpdateCustomerOrderItemCommand(customerOrderItemDTO);
            var newOrderItem = await _customerCommandHandler.Handle(command);
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

