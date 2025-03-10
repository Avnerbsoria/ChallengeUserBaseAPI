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
    public interface IAuthService
    {
        public Task<Result<Object>> Login(UserDto login, IConfiguration configuration);
    }
}
