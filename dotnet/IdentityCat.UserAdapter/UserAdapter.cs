using IdentityCat.Domain;
using IdentityCat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityCat.UserAdapter;

public interface IUserAdapter
{
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

    public async Task<User?> FindByUsername(
        string username,
        CancellationToken cancellationToken = default)
    {
        var user = await _userDbContext
            .Users
            .Include(x => x.Claims)
            .FirstOrDefaultAsync(x => x.Username.ToUpper() == username.ToUpper(),
                cancellationToken);
        return user;
    }

    public async Task<User?> FindBySubjectId(
        string subjectId,
        CancellationToken cancellationToken = default)
    {
        return await _userDbContext
            .Users
            .Include(x => x.Claims)
            .FirstOrDefaultAsync(x => x.SubjectId == subjectId, cancellationToken);
    }
}