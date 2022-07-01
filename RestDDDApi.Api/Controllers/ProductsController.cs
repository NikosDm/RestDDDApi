using Microsoft.AspNetCore.Mvc;
using RestDDDApi.Domain.Interfaces;

namespace RestDDDApi.Api.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
}
