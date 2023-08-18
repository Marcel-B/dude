using Identity.Servus.Domain;
using MediatR;

namespace Identity.Servus.Authentication.Queries;

public record GetUserQuery(
    string Email) : IRequest<AppUser>;