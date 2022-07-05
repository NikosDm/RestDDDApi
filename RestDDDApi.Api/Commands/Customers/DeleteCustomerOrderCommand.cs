using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Api.Commands.Customers
{
    public class DeleteCustomerOrderCommand
    {
        public Guid CustomerID { get; set; }
        public Guid OrderID { get; set; }
    }
}