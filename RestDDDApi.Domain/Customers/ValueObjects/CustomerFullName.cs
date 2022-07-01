using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers
{
    public class CustomerFullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void UpdateFullName(CustomerFullName customerFullName)
        {
            this.FirstName = customerFullName.FirstName;
            this.LastName = customerFullName.LastName;
        }
    }
}