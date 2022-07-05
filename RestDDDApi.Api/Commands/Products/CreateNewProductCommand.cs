using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;

namespace RestDDDApi.Api.Commands.Products;

public class CreateNewProductCommand
{
    public string Name { get; set; }
    public Double Price { get; set; }

    public CreateNewProductCommand() { }
    public CreateNewProductCommand(ProductDataRequest productData) {
        this.Name = productData.Name;
        this.Price = productData.Price;
    }
}
