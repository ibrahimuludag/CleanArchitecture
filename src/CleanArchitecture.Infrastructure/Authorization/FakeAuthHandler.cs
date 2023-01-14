using Microsoft.AspNetCore.Authentication;
using System.Text.Encodings.Web;

namespace CleanArchitecture.Infrastructure.Authorization;

public class FakeAuthHandler : AuthenticationHandler<FakeAuthHandlerOptions>
{
    private const string UserId = "UserId";

    public const string AuthenticationScheme = "Fake";
    private readonly string _defaultUserId;

    public FakeAuthHandler(
        IOptionsMonitor<FakeAuthHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
        _defaultUserId = options.CurrentValue.DefaultUserId;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Upn, "fakeuser@gmail.com"),
            new Claim(ClaimTypes.GivenName, "Fake"),
            new Claim(ClaimTypes.Surname, "User"),
            new Claim(ClaimTypes.MobilePhone, "+90 555 555 55 55"),
            new Claim(ClaimTypes.Email, "fakeuser@gmail.com"),
            new Claim(ClaimTypes.Role, "admin"),
            new Claim(ClaimTypes.Role, "user"),
        };

        // Extract User ID from the request headers if it exists,
        // otherwise use the default User ID from the options.
        if (Context.Request.Headers.TryGetValue(UserId, out var userId))
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId[0]));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, _defaultUserId));
        }

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}

public class FakeAuthHandlerOptions : AuthenticationSchemeOptions
{
    public string DefaultUserId { get; set; } = "616d525d-71ec-46f5-ab4e-42d365f30102";
}