using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBaseAPI.Domain.Entities;

namespace UserBaseAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserAPIEFCoreDBContext _userAPIEFCoreDBContext;
        public UserRepository(UserAPIEFCoreDBContext userAPIEFCoreDBContext)
        {
            _userAPIEFCoreDBContext = userAPIEFCoreDBContext;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _userAPIEFCoreDBContext.Users.FirstOrDefaultAsync(A => A.Email == email);

            return result;
        }


        public async Task<object> CreateUserAsync(User user)
        {
            var result = await _userAPIEFCoreDBContext.Users.AddAsync(user);
            await _userAPIEFCoreDBContext.SaveChangesAsync();

            return "Salvo com sucesso!";
        }

        public List<User> ReadUserAsync()
        {
            var result = _userAPIEFCoreDBContext.Users.Where(a => a.Deleted == false).ToList();
            return result;
        }

        public async Task<object> UpdateUserAsync(User user)
        {
            try
            {

                var result = await _userAPIEFCoreDBContext.Users.FirstOrDefaultAsync(a => a.Id == user.Id);
              
                if (result != null)
                {

                    result.FirstName = user.FirstName;
                    result.LastName = user.LastName;
                    if (string.IsNullOrEmpty(user.Password))
                        result.Password = user.Password;
                    result.Email = user.Email;
                    result.DocNumber = user.DocNumber;
                    result.Phone = user.Phone;
                    result.Phone2 = user.Phone2;
                    result.ManagerID = user.ManagerID;
                    result.Role = user.Role;
                    result.Birthday= user.Birthday; 
                }

                await _userAPIEFCoreDBContext.SaveChangesAsync();

                return "Salvo com sucesso!";
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<object> DeleteUserAsync(Guid Id)
        {
            var result = await _userAPIEFCoreDBContext.Users.FirstOrDefaultAsync(a => a.Id == Id);
            if (result != null)
                result.Deleted = true;

            await _userAPIEFCoreDBContext.SaveChangesAsync();

            return "Salvo com sucesso!";
        }
    }
}
