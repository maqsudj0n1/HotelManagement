using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class TransactionService : Repository<Transaction>, ITransactionRepo
    {
        public TransactionService(IApplicationDbContext context) : base(context)
        {
            
        }
    }
}
