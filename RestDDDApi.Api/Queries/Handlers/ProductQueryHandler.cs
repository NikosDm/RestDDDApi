using RestDDDApi.Api.DTOs;
using RestDDDApi.Api.Queries.Products;
using RestDDDApi.Domain.Interfaces;

namespace RestDDDApi.Api.Queries.Handlers;
public class ProductQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductQueryHandler(IUnitOfWork unitOfWork) 
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductDetailsDTO>> Handle(GetAllProductsQuery query)
    {
        var products =  await this._unitOfWork.productRepository.GetAllProducts();

        return products.ToList().Select(c => {
            return new ProductDetailsDTO 
            {
                ProductID = c.productID,
                Name = c.productData.Name,
                Price = c.productData.Price
            };
        });
    }

    public async Task<ProductDetailsDTO> Handle(GetProductDetailsQuery query)
    {
        var product =  await this._unitOfWork.productRepository.GetProductsById(query.ProductID);

        if (product == null) throw new Exception("Product does not exist");

        return new ProductDetailsDTO 
        {
            ProductID = product.productID,
            Name = product.productData.Name,
            Price = product.productData.Price
        };
    }
}