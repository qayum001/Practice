using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Practice.Data;
using Practice.Services.AuthorService;
using Practice.Services.AuthService;
using Practice.Services.CheckerService.ILoginCheckerService;
using Practice.Services.CheckerService;
using Practice.Services.CommentService;
using Practice.Services.PostService;
using Practice.Services.TagService;
using Practice.Services.TokenService;
using Practice.Services.UserService;
using System.Reflection;

namespace Practice.Tests.Helpers;

public class ServiceProviderFactory
{
    public static DbContextOptions<AppDbContext> CreateInMemoryDbOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    public static AppDbContext CreateDbContext()
    {
        var options = CreateInMemoryDbOptions();
        return new AppDbContext(options);
    }

    public static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRegisterCheckerService, RegisterCheckerService>();
        services.AddScoped<ILoginCheckService, LoginCheckService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommentService, CommentService>();

        return services.BuildServiceProvider();
    }
}
