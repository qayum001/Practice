
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Data;
using Practice.Data.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        public string GenerateUserJwt(User user)
        {
            var key = _configuration["Jwt:Key"];

            if(key == null) { throw new ArgumentNullException(nameof(key)); }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
