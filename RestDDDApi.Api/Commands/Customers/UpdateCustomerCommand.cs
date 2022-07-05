using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Api.DTOs;
using RestDDDApi.Domain.Customers.Orders;

namespace RestDDDApi.Api.Commands.Customers;

public class UpdateCustomerCommand
{
    public Guid CustomerID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    
    public UpdateCustomerCommand() { }

    public UpdateCustomerCommand(CustomerDetailsDTO customerDetailsDTO) 
    {
        this.CustomerID = customerDetailsDTO.CustomerId;
        this.FirstName = customerDetailsDTO.fullName.FirstName;
        this.LastName = customerDetailsDTO.fullName.LastName;
        this.Street = customerDetailsDTO.address.Street;
        this.PostalCode = customerDetailsDTO.address.PostalCode;
    }
}
