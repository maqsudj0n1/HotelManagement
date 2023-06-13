using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class RoomService : Repository<Room>, IRoomRepo
    {
        public RoomService(IApplicationDbContext context):base(context)
        {
            
        }
    }
}
