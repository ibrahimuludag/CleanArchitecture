using System.Security.Claims;

namespace CleanArchitecture.Application.Contracts.Authorization;

public interface ICurrentUserService
{
    public string Email { get; }
    public string UserName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public List<string> Roles { get; }
    public List<Claim> Claims { get; }
}