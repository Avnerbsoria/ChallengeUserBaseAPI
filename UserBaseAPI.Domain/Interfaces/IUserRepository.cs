using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBaseAPI.Domain.Entities;

namespace UserBaseAPI.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<object> CreateUserAsync(User user);
        List<User> ReadUserAsync();
        Task<object> UpdateUserAsync(User user);
        Task<object> DeleteUserAsync(Guid Id);
    }
}
