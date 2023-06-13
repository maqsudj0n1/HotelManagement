using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReservationService : Repository<Reservation>,IReservationRepo
    {
        public ReservationService(IApplicationDbContext context) : base(context)
        {
            
        }
    }
}
