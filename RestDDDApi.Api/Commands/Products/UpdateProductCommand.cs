using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;

namespace RestDDDApi.Api.Commands.Products;

public class UpdateProductCommand
{
    public Guid ProductID { get; set; }
    public string Name { get; set; }
    public Double Price { get; set; }

    public UpdateProductCommand() { }

    public UpdateProductCommand(ProductDetailsDTO productDetailsDTO)
    {
        this.ProductID = productDetailsDTO.ProductID;
        this.Name = productDetailsDTO.Name;
        this.Price = productDetailsDTO.Price;
    }
}
