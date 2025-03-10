using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserBaseAPI.Application.Controllers;
using UserBaseAPI.Application.Core.Interfaces;
using UserBaseAPI.Application.Core.Services;

namespace UserBaseAPI.XUnitTests
{
    public class AuthServicesTests
    {
        [Fact]
        public async Task Verify_Password_Hash_Return_True()
        {

            var service = new AuthService(null);

            Assert.Equal(service.Verify("123456", "216F0D350B73BBD17449F0341D2FEADCA4698EEDF857990E4AFC06DBC12A-E7C8A3B3B892CB68866201BE2B562FAA"), true);

        }
    }
}