using Microsoft.EntityFrameworkCore;
using Practice.Data;

namespace Practice.Services.CheckerService
{
    public class RegisterCheckerService : IRegisterCheckerService
    {
        private readonly AppDbContext _context;

        public RegisterCheckerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailExists(string login)
        {
            var res = await _context.User.FirstOrDefaultAsync(e => e.Email == login) != null;

            return res;
        }
    }
}