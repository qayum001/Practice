using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.GenerateTokenService;
using Practice.Services.TokenService;

namespace Practice.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IGenerateJwt _generateJwt;
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context,
            IConfiguration configuration,
            IGenerateJwt generateJwt,
            ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _generateJwt = generateJwt;
            _tokenService = tokenService;
        }

        public async Task<string> LogIn(LoginCredentials loginCredentials)
        {
            var user = await _context.User.FirstAsync(e => e.Email == loginCredentials.Email);

            var token = _generateJwt.GenerateUserJwt(user);

            return token;
        }

        public async Task LogOut(string token)
        {
            var res = await _tokenService.GetGuid(token);
            var user = await _context.User.FirstAsync(e => e.Id == res);

            await _context.UsedToken.AddAsync( new UsedToken {  Id = Guid.NewGuid(), Token = token, User = user });
            await _context.SaveChangesAsync();
        }

        public async Task<string> Register(UserRegisterDto userRegisterDto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = userRegisterDto.FullName,
                BirthDay = userRegisterDto.BirthDay,
                Email = userRegisterDto.Email,
                PhoneNumber = userRegisterDto.Phone,
                Password = userRegisterDto.Password,
                Created = DateTime.Now,
                Gender = userRegisterDto.Gender,
            };

            _context.User.Add(user);

            await _context.SaveChangesAsync();

            var token = _generateJwt.GenerateUserJwt(user);

            return token;
        }
    }
}