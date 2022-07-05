using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Api.DTOs;
public class ProductDataRequest
{
    public string Name { get; set; }
    public Double Price { get; set; }
}
