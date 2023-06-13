using Application.Abstractions;
using Application.Interfaces;
using Domain.Entities.IdentityEntities;

namespace Infrastructure.Services;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(IApplicationDbContext context) : base(context)
    {
    }
}
