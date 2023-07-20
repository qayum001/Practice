using Practice.Data.Dto;

namespace Practice.Services.CheckerService.ILoginCheckerService
{
    public interface ILoginCheckService
    {
        public Task<bool> IsLoginCorrect(LoginCredentials loginCredentials);
    }
}
