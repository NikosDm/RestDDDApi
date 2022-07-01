using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestDDDApi.Domain.Interfaces;

namespace RestDDDApi.Api.Controllers;

[Route("[controller]")]
public class CustomersController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
