using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserBaseAPI.Application.Core.Dtos;
using UserBaseAPI.Application.Core.Interfaces;
using UserBaseAPI.Domain.Entities;

using UserBaseAPI.Domain.Extensions.ResultExtentions;
using UserBaseAPI.Infrastructure.Repositories;

namespace UserBaseAPI.Application.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<Result<Object>> GetUserByEmail(string email)
        {

            var result = _userRepository.GetByEmailAsync(email);

            return await Task.FromResult(new SuccessResult<Object>(new[] { new { Result = result } }));

        }

        public async Task<Result<Object>> CreateUser(UserDto user,string role)
        {

            if (role == "emp" && user.Role == "adm")
                return new UnexpectedResult<Object>( "Empregado não pode cadastrar administrador!" );

            var result = await _userRepository.CreateUserAsync(user.GetUserFromDto());

            return new SuccessResult<Object>(new[] { new { Result = result} });

        }


        public async Task<Result<Object>> ReadUsers()
        {
             
            var result = _userRepository.ReadUserAsync();

            return await Task.FromResult( new SuccessResult<Object>(new[] { new { Result = result } }));

        }


        public async Task<Result<Object>> UpdateUser(UserDto user, string role)
        {

            if (role == "emp" && user.Role == "adm")
                return new SuccessResult<Object>(new[] { "Empregado não pode cadastrar administrador!" });

            var result = await _userRepository.UpdateUserAsync(user.GetUserFromDto());

            return new SuccessResult<Object>(new[] { new { Result = result } });

        }



        public async Task<Result<Object>> DeleteUser(Guid Id)
        {

            var result =await _userRepository.DeleteUserAsync(Id);

            return new SuccessResult<Object>(new[] { new { Result = result } });

        }
    }
}
