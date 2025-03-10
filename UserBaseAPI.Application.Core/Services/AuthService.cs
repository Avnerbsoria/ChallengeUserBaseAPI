using Microsoft.Extensions.Configuration;
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
using UserBaseAPI.Application.Core.Exceptions;
using UserBaseAPI.Application.Core.Interfaces;
using UserBaseAPI.Domain.Entities; 
using UserBaseAPI.Domain.Extensions.ResultExtentions;
using UserBaseAPI.Infrastructure.Repositories;

namespace UserBaseAPI.Application.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<Result<Object>> Login(UserDto login, IConfiguration configuration)
        {
            var response = _userRepository.GetByEmailAsync(login.Email);

            if (response.Result != null && this.Verify(login.Password, response.Result.Password))
            {


                string secretKey = configuration["Jwt:Secret"]!;
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim("firstname", response.Result.FirstName.ToString()),
                        new Claim("secondname", response.Result.FirstName.ToString()),
                        new Claim("email", login.Email),
                        new Claim("role", response.Result.Role),
                        new Claim("email_verified", "true")
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                    SigningCredentials = credentials,
                    Issuer = configuration["Jwt:Issuer"],
                    Audience = configuration["Jwt:Audience"]
                };

                var handler = new JsonWebTokenHandler();

                string token = handler.CreateToken(tokenDescriptor);

                return await Task.FromResult( new SuccessResult<Object>(new[] { new { Token = token } }));

            }
            else
            {
                throw new CustomException("Invalid mail or password", null, System.Net.HttpStatusCode.Unauthorized);
            }



            //}
        }



        private const int SaltSize = 16;
        private const int HashSize = 30;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
        public bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }

    }
}
