using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Interfaces;
using RestDDDApi.Domain.Products.Interfaces;

namespace RestDDDApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository customerRepository { get; }
        IProductRepository productRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}