using IdentityCat.Domain;
using IdentityCat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityCat.UserAdapter;

public interface IUserAdapter
{
    Task<User> CreateUser(
        string username,
        string password,
        string? email,
        string? name,
        string? givenName);

    Task<User?> FindByUsername(
        string username,
        CancellationToken cancellationToken = default);

    Task<User?> FindBySubjectId(
        string subjectId,
        CancellationToken cancellationToken = default);
}

public class UserAdapter : IUserAdapter
{
    private readonly UserDbContext _userDbContext;

    public UserAdapter(
        UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<User> CreateUser(
        string username,
        string password,
        string? email,
        string? name,
        string? givenName)
    {
        var cmd = new User.CreateUserCommand(username, password, email, name, givenName);
        var user = User.Create(cmd);
        await _userDbContext.Users.AddAsync(user);
        await _userDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> FindByUsername(
        string username,
        CancellationToken cancellationToken = default)
    {
        var user = await _userDbContext
            .Users
            .FirstOrDefaultAsync(x => x.UsernameNormalized == username.ToUpper(),
                cancellationToken);
        return user;
    }

    public async Task<User?> FindBySubjectId(
        string subjectId,
        CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .FirstOrDefaultAsync(x => x.SubjectId == subjectId, cancellationToken);
    }
}