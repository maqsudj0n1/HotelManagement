using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class PaymentService : Repository<Payment>, IPaymentRepo
{
    public PaymentService(IApplicationDbContext context) : base(context)
    {
        
    }
}
