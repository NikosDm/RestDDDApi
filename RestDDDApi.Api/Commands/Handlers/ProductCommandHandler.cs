using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Interfaces;
using RestDDDApi.Api.Commands.Products;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Domain.Products;
using RestDDDApi.Domain.Products.ValueObjects;

namespace RestDDDApi.Api.Commands.Handlers;

public class ProductCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCommandHandler(IUnitOfWork unitOfWork) 
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<ProductDetailsDTO> Handle(CreateNewProductCommand createNewProduct)
    {
        var productData = ProductData.createProductData(createNewProduct.Name, createNewProduct.Price);
        var product = await _unitOfWork.productRepository.AddNewProduct(productData);
        
        if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

        return new ProductDetailsDTO 
        {
            ProductID = product.productID,
            Name = productData.Name,
            Price = productData.Price
        };
    }
    
    public async Task<ProductDetailsDTO> Handle(UpdateProductCommand updateProductCommand)
    {
        var productData = ProductData.createProductData(updateProductCommand.Name, updateProductCommand.Price);
        var product = await _unitOfWork.productRepository.UpdateProduct(updateProductCommand.ProductID, productData);
        
        if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

        return new ProductDetailsDTO 
        {
            ProductID = product.productID,
            Name = productData.Name,
            Price = productData.Price
        };
    }

    public async Task<bool> Handle(DeleteProductCommand deleteProductCommand)
    {
        await _unitOfWork.productRepository.DeleteProduct(deleteProductCommand.ProductID);

        if (_unitOfWork.HasChanges()) return await _unitOfWork.Complete();

        return false;
    }
}
