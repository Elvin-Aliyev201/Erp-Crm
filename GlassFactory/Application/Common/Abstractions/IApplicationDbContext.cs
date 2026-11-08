using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstractions
{
    public  interface IApplicationDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Role> Roles { get; }

        DbSet<Permission> Permissions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellation=default);
    }
}
