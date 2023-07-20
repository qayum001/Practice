using Microsoft.IdentityModel.Tokens;
using Practice.Data.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice.Services.GenerateTokenService
{
    public interface IGenerateJwt
    {
        public string GenerateUserJwt(User user);

    }
}
