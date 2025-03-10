using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserBaseAPI.Application.Core.Context;
using UserBaseAPI.Domain.Entities;

namespace UserBaseAPI.Infrastructure
{
    public class UserAPIEFCoreDBContext:DbContext, IUserAPIEFCoreDBContext
    {
        public UserAPIEFCoreDBContext(
            DbContextOptions<UserAPIEFCoreDBContext> options) : base(options) 
        {   }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());  

            base.OnModelCreating(modelBuilder);
        }
    }
}
