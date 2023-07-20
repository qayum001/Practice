using Practice.Data.Dto;

namespace Practice.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterDto userRegisterDto);
        Task<string> LogIn(LoginCredentials loginCredentials);
        Task LogOut(string token);
    }
}
