namespace Practice.Services.TokenService
{
    public interface ITokenService
    {
        Task<Guid> GetGuid(string token);
        Task<bool> IsTokenValid(string token);
    }
}
