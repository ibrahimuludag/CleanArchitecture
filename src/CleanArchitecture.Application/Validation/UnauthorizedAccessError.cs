namespace CleanArchitecture.Application.Validation;

public class UnauthorizedAccessError : Error
{
    public UnauthorizedAccessError(string message) : base(message)
    {
    }
}
