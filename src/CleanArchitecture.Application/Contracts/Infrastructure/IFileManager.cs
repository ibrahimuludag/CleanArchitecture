namespace CleanArchitecture.Application.Contracts.Infrastructure;

public interface IFileManager
{
    string Upload(Stream fileStream, string fileName, string contentType);
    Uri? Download(string storeId);
}
