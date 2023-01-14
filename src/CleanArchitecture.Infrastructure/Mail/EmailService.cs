using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models.Email;

namespace CleanArchitecture.Infrastructure.Mail;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    
    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task<bool> SendEmail(Email email)
    {
        _logger.LogInformation("An email has been sent to {To} with subject {Subject}",
            email.To, email.Subject);

        return Task.FromResult(true);
    }
}
