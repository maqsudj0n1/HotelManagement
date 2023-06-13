using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;
public class CustomerService : Repository<Customer>, ICustomerRepo
{
    public CustomerService(IApplicationDbContext context) : base(context)
    {
      
    }
}