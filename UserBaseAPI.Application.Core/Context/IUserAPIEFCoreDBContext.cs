using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBaseAPI.Domain.Entities;

namespace UserBaseAPI.Application.Core.Context
{
    public interface IUserAPIEFCoreDBContext
    {
        DbSet<User> Users { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
