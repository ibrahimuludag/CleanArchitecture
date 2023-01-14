namespace CleanArchitecture.Application.Models.Email;


[ExcludeFromCodeCoverage]
public class Email
{
    public string To { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Body { get; set; } = default!;
}
