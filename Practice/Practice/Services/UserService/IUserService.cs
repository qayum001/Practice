using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserProfileAsync(Guid id);
        Task<Response> EditUserProfileAsync(Guid id, EditUserDto editUserDto);
    }
}