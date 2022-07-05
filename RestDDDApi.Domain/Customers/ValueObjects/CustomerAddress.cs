using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers;

/// <summary>
/// Value object
/// Represents Customer details regarding their address
/// </summary>
public class CustomerAddress
{
    public string Street { get; private set; }
    public string PostalCode { get; private set; }

    private CustomerAddress() { }

    private CustomerAddress(string Street, string PostalCode) 
    { 
        this.Street = Street;
        this.PostalCode = PostalCode;
    }

    /// <summary>
    /// Static method that creates an instance of CustomerAddress class. 
    /// </summary>
    /// <param name="Street">Street name of Customers residency</param>
    /// <param name="PostalCode">Postal code of Customers residency</param>
    /// <returns>New instance of CustomerAddress Class</returns>
    public static CustomerAddress createNewCustomerAddress(string Street, string PostalCode)
    {
        return new CustomerAddress(Street, PostalCode);
    }

    /// <summary>
    /// Update the customer address details of a customer
    /// </summary>
    /// <param name="customerAddress">CustomerAddress object that contains new information regarding customers address</param>
    public void UpdateAddress(CustomerAddress customerAddress)
    {
        this.Street = customerAddress.Street;
        this.Street = customerAddress.Street;
    }
}
