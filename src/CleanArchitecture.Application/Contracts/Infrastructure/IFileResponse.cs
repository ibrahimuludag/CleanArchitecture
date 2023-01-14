namespace CleanArchitecture.Application.Contracts.Infrastructure;

public interface IFileResponse
{
    string FileName { get; set; }
    byte[] Data { get; set; }
}
