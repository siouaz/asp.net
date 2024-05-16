using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OeuilDeSauron.Infrastructure.Files
{
    /// <summary>
    /// File manager.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Retrieve folder list.
        /// </summary>
        Task<List<string>> GetFoldersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve files for a folder.
        /// </summary>
        /// <param name="folder">Folder  path.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<List<File>> GetFilesAsync(string folder, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrives a file by path.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<File> GetFileAsync(string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Uploads a file.
        /// </summary>
        /// <param name="file">File properties.</param>
        /// <param name="content">File content stream.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UploadFileAsync(File file, Stream content, CancellationToken cancellationToken = default);

        /// <summary>
        /// Downloads a file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<Stream> DownloadFileAsync(string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteFileAsync(string path, CancellationToken cancellationToken = default);
    }
}
