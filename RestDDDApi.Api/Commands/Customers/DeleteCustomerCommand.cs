using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Api.Commands.Customers;

public class DeleteCustomerCommand
{
    public Guid CustomerID { get; set; }
}
