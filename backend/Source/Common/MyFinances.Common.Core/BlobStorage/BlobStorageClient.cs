using Azure.Storage.Blobs;

namespace MyFinances.Common.Core.BlobStorage;

public class BlobStorageClient(BlobServiceClient blobStorageClient) : IBlobStorageClient
{
    public async Task CreateBlobContainerIfNotExistsAsync(Guid containerId, CancellationToken cancellationToken)
    {
        var containerClient = blobStorageClient.GetBlobContainerClient(containerId.ToString());

        if (containerClient is null)
            await blobStorageClient.CreateBlobContainerAsync(containerId.ToString(),
                cancellationToken: cancellationToken);
    }

    public async Task<string> UploadLogoFileAsync(Guid id, Stream content, CancellationToken cancellationToken)
    {
        var filePath = $"{id}/logo_{id}";
        await UploadFileAsync(id.ToString(), filePath, content, cancellationToken);
        return filePath;
    }

    public async Task<string> UploadBannerFileAsync(Guid id, Stream content, CancellationToken cancellationToken)
    {
        var filePath = $"{id}/banner_{id}";
        await UploadFileAsync(id.ToString(), filePath, content, cancellationToken);
        return filePath;
    }

    private async Task UploadFileAsync(string containerId, string filePath, Stream content,
        CancellationToken cancellationToken)
    {
        var containerClient = blobStorageClient.GetBlobContainerClient(containerId);
        var blobClient = containerClient.GetBlobClient(filePath);
        await blobClient.UploadAsync(content, true, cancellationToken);
    }
}