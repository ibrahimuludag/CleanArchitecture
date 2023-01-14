using System.Runtime.Serialization;

namespace CleanArchitecture.Application.Exceptions;

[Serializable]
public class UnauthorizedException : CleanArchitectureApplicationException
{
    protected UnauthorizedException(SerializationInfo info,
     StreamingContext context) : base(info, context)
    {
    }
    public UnauthorizedException()
        : base()
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}