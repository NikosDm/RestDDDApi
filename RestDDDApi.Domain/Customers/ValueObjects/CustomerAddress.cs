using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers
{
    public class CustomerAddress
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }
        
        public void UpdateAddress(CustomerAddress customerAddress)
        {
            this.Street = customerAddress.Street;
            this.Street = customerAddress.Street;
        }
    }
}