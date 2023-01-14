using CleanArchitecture.Application.Models.Email;

namespace CleanArchitecture.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
