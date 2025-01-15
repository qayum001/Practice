using Practice.Data;
using Practice.Data.Model;

namespace Practice.Tests.Helpers;

public class DbFiller
{
    private readonly Random _random = new();
    public void Fill(AppDbContext context)
    {
        var users = new List<User>()
        {
            CreateRandomUser(),
            CreateRandomUser(),
            CreateRandomUser(),
        };

        users.ForEach(AddPostToUser);

        context.User.AddRange(users);
        context.SaveChanges();
    }

    private void AddPostToUser(User user)
    {
        var userPostCount = _random.Next(3);
        var userPosts = new List<Post>();

        for (var i = 0; i < userPostCount; i++)
        {
            userPosts.Add(new Post()
            {
                Id = Guid.NewGuid(),
                Title = user.FullName + "'s Post",
                Text = user.FullName + "'s Post's text",
                ReadTime = _random.Next(5),
                Created = DateTime.Now,
                UserId = user.Id,
                User = user,
            });
        }

        user.Posts = userPosts;
        user.IsAuthor = userPostCount > 0;
    }

    private User CreateRandomUser()
    {
        var id = Guid.NewGuid();
        var baseEmail = "@user.com";
        var name = "name";
        var age = _random.Next(18, 100);
        var passowd = _random.Next(9999999, 20000000);
        var gender = _random.Next(0, 2);

        var created = DateTime.Now;

        var unq = id.ToString().Split("-")[0];

        return new User
        {
            Id = id,
            FullName = unq + name,
            BirthDay = DateTime.Now.AddYears(-age),
            Created = created,
            Gender = gender == 0 ? Gender.Male : Gender.Female,
            PhoneNumber = passowd.ToString(),
            Email = unq + baseEmail,
            Password = unq
        };
    }
}
