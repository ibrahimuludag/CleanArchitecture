using CleanArchitecture.Application.Contracts.Authorization;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly string _email;
    private readonly string _userName;
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly List<string> _roles;
    private readonly List<Claim> _claims;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var _context = httpContextAccessor?.HttpContext;
        _roles = _context?.User?.Claims?.Where(x => x.Type == ClaimTypes.Role)?.Select(x => x.Value).ToList() ?? new List<string>();
        _email = _context?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        _userName = _context?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Upn)?.Value ?? string.Empty;
        _firstName = _context?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
        _lastName = _context?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value ?? string.Empty;
      
        _claims = _context?.User?.Claims.ToList() ?? new List<Claim>();
    }

    public string Email => _email;
    public string UserName => _userName;
    public string FirstName => _firstName;
    public string LastName => _lastName;
    public List<string> Roles => _roles;
    public List<Claim> Claims => _claims;
}