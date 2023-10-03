using IdentityCat.Domain;

namespace IdentityCat.Application;

public interface IUserStore
{
    Task<User> CreateUser(
        string username,
        string password,
        string email,
        string? name,
        string? givenName);

    Task<bool> ValidateCredentials(
        string username,
        string password,
        CancellationToken cancellationToken = default);

    Task<User?> FindByExternalProvider(
        string provider,
        string userId);

    Task<User?> FindBySubjectId(
        string subjectId,
        CancellationToken cancellationToken = default);

    Task<User?> FindByUsername(
        string username,
        CancellationToken cancellationToken = default);
}