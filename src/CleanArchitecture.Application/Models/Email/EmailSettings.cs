namespace CleanArchitecture.Application.Models.Email;

[ExcludeFromCodeCoverage]
public class EmailSettings
{
    public string ApiKey { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
    public string FromName { get; set; } = default!;
}
