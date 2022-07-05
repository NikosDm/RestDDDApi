using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Customers;

/// <summary>
/// Value object
/// Represents Customer full name
/// </summary>
public class CustomerFullName
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private CustomerFullName() { }

    private CustomerFullName(string FirstName, string LastName) 
    { 
        this.FirstName = FirstName;
        this.LastName = LastName;
    }

    /// <summary>
    /// Static method that creates an instance of CustomerFullName class. 
    /// </summary>
    /// <param name="FirstName">Customer First Name</param>
    /// <param name="LastName">Customer Last Name</param>
    /// <returns>New instance of CustomerFullName Class</returns>
    public static CustomerFullName createNewCustomerFullName(string FirstName, string LastName)
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) 
            throw new Exception("First and Last name are required");

        return new CustomerFullName(FirstName, LastName);
    }

    /// <summary>
    /// Update the customer full name of a customer
    /// </summary>
    /// <param name="customerFullName">CustomerFullName object that contains new full name of the customer</param>
    public void UpdateFullName(CustomerFullName customerFullName)
    {
        this.FirstName = customerFullName.FirstName;
        this.LastName = customerFullName.LastName;
    }
}
