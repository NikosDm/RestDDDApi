using Microsoft.AspNetCore.Mvc;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Api.Queries.Handlers;
using RestDDDApi.Api.Queries.Products;
using RestDDDApi.Api.Commands.Handlers;
using RestDDDApi.Api.Commands.Products;

namespace RestDDDApi.Api.Controllers;

/// <summary>
/// Product Api Controller
/// </summary>
/// <remarks>
/// Representes all the actions related Products
/// that the RESTfull service can support.
/// Makes use of CQRS queries and commands for retrieving or modifying 
/// product records respectively
/// </remarks>
/// <returns>Every action returns an instance of ResponseDTO class which contains data or any exception that may occur</returns>
public class ProductsController : BaseApiController
{
    private readonly ProductQueryHandler _productQueryHandler;
    private readonly ProductCommandHandler _productCommandHandler;
    protected ResponseDTO _response; 

    public ProductsController(ProductQueryHandler productQueryHandler, ProductCommandHandler productCommandHandler)
    {
        _productQueryHandler = productQueryHandler;
        _productCommandHandler = productCommandHandler;
        this._response = new ResponseDTO();
    }

    /// <summary>
    /// Actions that retrieves all products
    /// </summary>
    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<ResponseDTO>> GetAllProducts() 
    {
        var products = await _productQueryHandler.Handle(new GetAllProductsQuery());
        _response.Result = products;

        return Ok(_response);
    }

    /// <summary>
    /// Action that retrieves a specific product based on its ID
    /// </summary>
    /// <param name="productId">Id of Product. It is received from URI</param>
    [HttpGet("GetProductById/{productId}")]
    public async Task<ActionResult<ResponseDTO>> GetProductById(Guid productId) 
    {
        try 
        {
            var query = new GetProductDetailsQuery { ProductID = productId };
            var product = await _productQueryHandler.Handle(query);
            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Action that stores a new product
    /// </summary>
    /// <param name="productData">Product data which will be stored</param>
    [HttpPost("AddNewProduct")]
    public async Task<ActionResult<ResponseDTO>> AddNewProduct([FromBody]ProductDataRequest productData) 
    {
        try 
        {
            var command = new CreateNewProductCommand(productData);
            var product =  await _productCommandHandler.Handle(command);

            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Action that updates an existing product
    /// </summary>
    /// <param name="productData">Product data which will be updated</param>
    [HttpPut("UpdateProduct")]
    public async Task<ActionResult<ResponseDTO>> UpdateProduct([FromBody]ProductDetailsDTO productData) 
    {
        try 
        {
            var command = new UpdateProductCommand(productData);
            var product =  await _productCommandHandler.Handle(command);

            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    /// <summary>
    /// Action that deletes an existing product
    /// </summary>
    /// <param name="productId">Id of product</param>
    [HttpDelete("DeleteProduct")]
    public async Task<ActionResult<ResponseDTO>> DeleteProduct(Guid productId) 
    {
        try 
        {
            var command = new DeleteProductCommand { ProductID = productId };
            await _productCommandHandler.Handle(command);
            _response.Result = true;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
}
