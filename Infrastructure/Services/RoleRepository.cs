using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities.IdentityEntities;

namespace Infrastructure.Services;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(IApplicationDbContext context) : base(context)
    {

    }
}
