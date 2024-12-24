using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;
using Practice.Services.TokenService;
using Practice.Services.UserService;

namespace Practice.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context,
            IUserService userService,
            ITokenService tokenService,
            IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<string> LogIn(LoginCredentials loginCredentials)
        {
            var user = await _context.User.FirstAsync(e => e.Email == loginCredentials.Email);

            var token = _tokenService.GenerateUserJwt(user);

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
            //todo: add validator
            var user = _mapper.Map<User>(userRegisterDto);

            _context.User.Add(user);

            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateUserJwt(user);

            return token;
        }
    }
}