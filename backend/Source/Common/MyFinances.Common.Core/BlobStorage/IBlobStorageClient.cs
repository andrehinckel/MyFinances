namespace MyFinances.Common.Core.BlobStorage;

public interface IBlobStorageClient
{
    Task CreateBlobContainerIfNotExistsAsync(Guid containerId, CancellationToken cancellationToken);
    Task<string> UploadLogoFileAsync(Guid id, Stream content, CancellationToken cancellationToken);
    Task<string> UploadBannerFileAsync(Guid id, Stream content, CancellationToken cancellationToken);
}