using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Customers.Interfaces;
using RestDDDApi.Domain.Interfaces;
using RestDDDApi.Domain.Products.Interfaces;
using RestDDDApi.Infrastructure.Database;
using RestDDDApi.Infrastructure.Domain.Customers;
using RestDDDApi.Infrastructure.Domain.Products;

namespace RestDDDApi.Infrastructure.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public ICustomerRepository customerRepository => new CustomerRepository(_context);

        public IProductRepository productRepository => new ProductRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}