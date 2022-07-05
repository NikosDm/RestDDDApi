using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Api.Commands.Products;

public class DeleteProductCommand
{
    public Guid ProductID { get; set; }    
}
