using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBaseAPI.Application.Core.Dtos;
using UserBaseAPI.Domain.Extensions.ResultExtentions;


namespace UserBaseAPI.Application.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<Object>> GetUserByEmail(string email);
        public Task<Result<Object>> CreateUser(UserDto user, string role);
        public Task<Result<Object>> ReadUsers();
        public Task<Result<Object>> UpdateUser(UserDto user, string role);
        public Task<Result<Object>> DeleteUser(Guid Id);
    }
}
