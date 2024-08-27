using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Data;
using Practice.Data.Dto;
using Practice.Data.Model;
using System.Runtime.CompilerServices;

namespace Practice.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Response> EditUserProfileAsync(Guid id, EditUserDto editUserDto)
        {
            var user = await _context.User.FirstAsync(e => e.Id == id);

            user.Email = String.IsNullOrEmpty(editUserDto.Email) ? user.Email : editUserDto.Email;
            user.FullName = String.IsNullOrEmpty(editUserDto.FullName) ? user.FullName : editUserDto.FullName;
            user.BirthDay = editUserDto.BirthDay;
            user.Gender = editUserDto.Gender;
            user.PhoneNumber = String.IsNullOrEmpty(editUserDto.Phone) ? user.PhoneNumber : editUserDto.Phone;

            await _context.SaveChangesAsync();

            var res = new Response
            {
                Stasus = "Success",
                Message = "Profile Edited"
            };

            return res;
        }

        public async Task<UserDto> GetUserProfileAsync(Guid id)
        {
            var user = await _context.User.FirstAsync(e => e.Id == id);

            var dto = _mapper.Map<UserDto>(user);

            return dto;
        }
    }
}
