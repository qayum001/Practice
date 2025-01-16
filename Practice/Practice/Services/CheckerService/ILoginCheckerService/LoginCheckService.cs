using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;

namespace Practice.Services.CheckerService.ILoginCheckerService
{
    public class LoginCheckService : ILoginCheckService
    {
        private readonly AppDbContext _context;

        public LoginCheckService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsLoginCorrect(LoginCredentials loginCredentials)
        {
            var res = await _context.User.FirstAsync(e => e.Email == loginCredentials.Email);

            return res.Password == loginCredentials.Password;
        }
    }
}
