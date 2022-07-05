using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers;

namespace RestDDDApi.Api.DTOs
{
    public class NewCustomerDetailsDTO
    {
        public CustomerFullName fullName { get; set; }
        public CustomerAddress address { get; set; }
    }
}