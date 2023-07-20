using Practice.Data.Dto;

namespace Practice.Services.CheckerService
{
    public interface IRegisterCheckerService
    {
        public Task<bool> IsEmailExists(string login);
    }
}