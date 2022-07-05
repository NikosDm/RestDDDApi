using RestDDDApi.Api.DTOs;

namespace RestDDDApi.Api.Commands.Customers;

public class CreateNewCustomerCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }

    public CreateNewCustomerCommand() { }

    public CreateNewCustomerCommand(NewCustomerDetailsDTO customerDetailsDTO) 
    {
        this.FirstName = customerDetailsDTO.fullName.FirstName;
        this.LastName = customerDetailsDTO.fullName.LastName;
        this.Street = customerDetailsDTO.address.Street;
        this.PostalCode = customerDetailsDTO.address.PostalCode;
    }
}
