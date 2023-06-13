using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class RoomsClasservice : Repository<RoomClass>,IRoomClassRepo
    {
        public RoomsClasservice(IApplicationDbContext context) : base(context)
        {
            
        }
    }
}
