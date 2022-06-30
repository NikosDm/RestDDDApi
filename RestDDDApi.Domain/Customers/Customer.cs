using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Domain.Customers
{
    public class Customer
    {
        public CustomerID customerID { get; set; }
        public CustomerAddress address { get; set; }
        public CustomerFullName fullName { get; set; }
        public ICollection<Order> orders { get; set; }
    }
}