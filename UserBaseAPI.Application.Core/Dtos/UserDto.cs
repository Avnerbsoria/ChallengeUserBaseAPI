using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UserBaseAPI.Domain.Entities;

namespace UserBaseAPI.Application.Core.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("managerid")]
        public Guid ManagerId { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("lastname")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;
        [JsonPropertyName("phone2")]
        public string Phone2 { get; set; } = string.Empty;

        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("docnumber")]
        public string DocNumber { get; set; }

        public User GetUserFromDto()
        {
            return new User()
            {
                Id = this.Id != Guid.Empty ? this.Id : Guid.NewGuid(),
                Email = this.Email,
                Password = this.Hash(this.Password),
                Role = this.Role,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Phone = this.Phone,
                Phone2 = this.Phone2,
                Birthday = this.Birthday,
                DocNumber = Convert.ToInt64( string.IsNullOrEmpty( this.DocNumber)?0: this.DocNumber),
                ManagerID = this.ManagerId
            };
        }







        private const int SaltSize = 16;
        private const int HashSize = 30;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

    }
}
