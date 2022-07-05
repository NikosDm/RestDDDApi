using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Api.DTOs;

public class NewCustomerOrderItemDTO
{
    public Guid customerID { get; set; }
    public Guid orderID { get; set; }
    public Guid ProductID { get; set; }
    public Double ProductPrice { get; set; }
    public int Quantity { get; set; }
}