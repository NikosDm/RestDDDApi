using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Api.Queries.Customers
{
    public class GetCustomerOrdersQuery
    {
        public Guid CustomerID { get; set; }
    }
}