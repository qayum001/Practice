
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Practice.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly AppDbContext _context;

        public TokenService(AppDbContext context)
        {
            _context = context;
        }
        public Task<Guid> GetGuid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var readToken = (JwtSecurityToken) jsonToken;

            var res = readToken.Claims.First(e => e.Type == ClaimTypes.NameIdentifier).Value;

            return Task.FromResult(Guid.Parse(res));
        }
        public async Task<bool> IsTokenValid(string token)
        {
            var res = await _context.UsedToken.FirstOrDefaultAsync(e => e.Token == token);

            return res == null;
        }
    }
}
