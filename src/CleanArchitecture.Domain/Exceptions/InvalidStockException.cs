using System.Runtime.Serialization;

namespace CleanArchitecture.Domain.Exceptions;

[Serializable]
public class InvalidStockException : DomainException
{
    public InvalidStockException()
    {
    }

    public InvalidStockException(string message)
        : base(message)
    {
    }

    public InvalidStockException(decimal stock)
        : base($"Stock {stock} is invalid value")
    {
    }

    public InvalidStockException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected InvalidStockException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
