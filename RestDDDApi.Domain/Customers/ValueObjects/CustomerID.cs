using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers
{
    public class CustomerID
    {
        public Guid customerID { get; set; }
        public CustomerID() {}
        public CustomerID(Guid guid) 
        {
            this.customerID = guid;
        }
    }
}