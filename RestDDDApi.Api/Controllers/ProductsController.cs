using Microsoft.AspNetCore.Mvc;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Domain.Interfaces;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Api.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    protected ResponseDTO _response; 

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        this._response = new ResponseDTO();
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<ResponseDTO>> GetAllProducts() 
    {
        var products = await _unitOfWork.productRepository.GetAllProducts();
        _response.Result = products;

        return Ok(_response);
    }

    [HttpGet("GetProductById/{productId}")]
    public async Task<ActionResult<ResponseDTO>> GetProductById(Guid productId) 
    {
        try 
        {
             var product = await _unitOfWork.productRepository.GetProductsById(productId);
            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPost("AddNewProduct")]
    public async Task<ActionResult<ResponseDTO>> AddNewProduct([FromBody]ProductData productData) 
    {
        try 
        {
            var product =  await _unitOfWork.productRepository.AddNewProduct(productData);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPut("UpdateProduct")]
    public async Task<ActionResult<ResponseDTO>> UpdateProduct([FromBody]ProductData productData) 
    {
        try 
        {
            var product =  await _unitOfWork.productRepository.UpdateProduct(productData);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            _response.Result = product;
        }
        catch(Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpDelete("DeleteProduct")]
    public async Task<ActionResult<ResponseDTO>> DeleteProduct(Guid productId) 
    {
        try 
        {
            await _unitOfWork.productRepository.DeleteProduct(productId);

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
}
