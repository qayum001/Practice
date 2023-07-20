using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response> EditUserProfileAsync(Guid id, EditUserDto editUserDto)
        {
            var user = await _context.User.FirstAsync(e => e.Id == id);

            user.Email = editUserDto.Email;
            user.FullName = editUserDto.FullName;
            user.BirthDay = editUserDto.BirthDate;
            user.Gender = editUserDto.Gender;
            user.PhoneNumber = editUserDto.Phone;

            await _context.SaveChangesAsync();

            var res = new Response
            {
                Stasus = "edited",
                Message = "Your profile updated"
            };

            return res;
        }

        public async Task<UserDto> GetUserProfileAsync(Guid id)
        {
            var user = await _context.User.FirstAsync(e => e.Id == id);

            var dto = new UserDto
            {
                Id = id,
                CreateTime = user.Created,
                BirthDay = user.BirthDay,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Gender = user.Gender,
            };

            return dto;
        }
    }
}
