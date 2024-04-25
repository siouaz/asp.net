using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.IdentityModel.Tokens;

namespace siwar.Infrastructure.Files
{
    /// <summary>
    /// Azure storage file manager.
    /// </summary>
    public class FileManager : IFileManager
    {
        private const string ContainerName = "documents";

        private readonly BlobContainerClient _container;

        /// <summary>
        /// Gets excluded files and folders when listing.
        /// </summary>
        private readonly IList<string> Exclusions = new List<string> { "Agreements/", "Controls/", "Certifications/" };

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManager"/> class.
        /// </summary>
        /// <param name="blob">Azure blob service client.</param>
        public FileManager(BlobServiceClient blob)
        {
            _container = blob.GetBlobContainerClient(ContainerName);
            _container.CreateIfNotExists(PublicAccessType.BlobContainer);
        }

        /// <inheritdoc/>
        public Task<List<string>> GetFoldersAsync(CancellationToken cancellationToken = default)
        {
            // Only First Page
            var blobs = _container.GetBlobsByHierarchy(delimiter: "/");
            var folders = blobs
                .Where(x => !x.IsBlob)
                .Where(x => !Exclusions.Contains(x.Prefix))
                .Select(x => Path.GetDirectoryName(x.Prefix))
                .ToList();

            return Task.FromResult(folders);
        }

        /// <inheritdoc/>
        public Task<List<File>> GetFilesAsync(string folder, CancellationToken cancellationToken = default)
        {
            // Only First Page
            var blobs = _container.GetBlobsByHierarchy(BlobTraits.Metadata, prefix: $"{folder}/");
            var files = blobs
                .Where(x => x.IsBlob)
                .Select(x => new File(x.Blob.Name,
                    x.Blob.Metadata.ContainsKey(nameof(File.OriginalName))
                        ? Base64UrlEncoder.Decode(x.Blob.Metadata[nameof(File.OriginalName)])
                        : null,
                    x.Blob.Properties.ContentType, x.Blob.Properties.ContentLength, x.Blob.Properties.CreatedOn))
                .OrderByDescending(x => x.Created)
                .ToList();

            return Task.FromResult(files);
        }

        /// <inheritdoc/>
        public async Task<File> GetFileAsync(string path, CancellationToken cancellationToken = default)
        {
            var file = _container.GetBlobClient(path);

            if (!await file.ExistsAsync(cancellationToken))
            {
                return null;
            }

            var properties = await file.GetPropertiesAsync(cancellationToken: cancellationToken);

            return new File(file.Name,
                properties.Value.Metadata.ContainsKey(nameof(File.OriginalName))
                    ? Base64UrlEncoder.Decode(properties.Value.Metadata[nameof(File.OriginalName)])
                    : null,
                properties.Value.ContentType, properties.Value.ContentLength, properties.Value.CreatedOn);
        }

        /// <inheritdoc/>
        public async Task UploadFileAsync(File file, Stream content, CancellationToken cancellationToken = default)
        {
            var blob = _container.GetBlobClient(file.Path);
            await blob.UploadAsync(content, new BlobHttpHeaders { ContentType = file.ContentType },
                cancellationToken: cancellationToken);
            await blob.SetMetadataAsync(
                new Dictionary<string, string>
                {
                    { nameof(File.OriginalName), Base64UrlEncoder.Encode(file.OriginalName) }
                },
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Stream> DownloadFileAsync(string path, CancellationToken cancellationToken = default)
        {
            var file = _container.GetBlobClient(path);
            if (!await file.ExistsAsync(cancellationToken))
            {
                return null;
            }

            var stream = new MemoryStream();
            await file.DownloadToAsync(stream, cancellationToken);
            stream.Seek(0, SeekOrigin.Begin); // Reset Stream Position

            return stream;
        }

        /// <inheritdoc/>
        public async Task DeleteFileAsync(string path, CancellationToken cancellationToken = default) =>
            await _container.DeleteBlobAsync(path, DeleteSnapshotsOption.IncludeSnapshots,
                cancellationToken: cancellationToken);
    }
}
