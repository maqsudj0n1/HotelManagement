using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
    public DbSet<Customer> Customers { get;}
    public DbSet<Employee> Employees { get;}
    public DbSet<Room> Rooms { get;}
    public DbSet<RoomClass> RoomsClass { get;}
    public DbSet<Reservation> Reservations { get;}
    public DbSet<Payment> Payment { get;}
    public DbSet<Transaction> Transactions { get;}
    public Task<int> SaveChangesAsync(CancellationToken token = default);


}
