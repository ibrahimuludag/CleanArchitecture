using System.Runtime.Serialization;

namespace CleanArchitecture.Application.Exceptions;

[Serializable]
public class ForbiddenAccessException : CleanArchitectureApplicationException
{
    protected ForbiddenAccessException(SerializationInfo info,
     StreamingContext context) : base(info, context)
    {
    }
    public ForbiddenAccessException() : base() { }

    public ForbiddenAccessException(string message)
       : base(message)
    {
    }

    public ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}