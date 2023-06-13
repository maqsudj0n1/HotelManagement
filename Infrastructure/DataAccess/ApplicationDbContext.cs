using Application.Abstractions;
using Domain.Entities;
using Infrastructure.DataAccess.Interceptor;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _interceptor;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor interceptor) : base(options)
    {
        _interceptor = interceptor;
    }
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<RoomClass> RoomsClass { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<Payment> Payment { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_interceptor);
    }
}
