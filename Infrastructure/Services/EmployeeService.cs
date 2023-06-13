using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class EmployeeService : Repository<Employee>, IEmployeeRepo
{
    public EmployeeService(IApplicationDbContext context) : base(context)
    {
        
    }
}
