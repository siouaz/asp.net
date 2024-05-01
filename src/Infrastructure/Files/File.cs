using System;

using Microsoft.AspNetCore.Http;

namespace OeuilDeSauron.Infrastructure.Files
{
    /// <summary>
    /// Represents a storage file.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Gets file path constituted from a folder name
        /// and the file generated name.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets file name without extension.
        /// </summary>
        public string Name => System.IO.Path.GetFileName(Path);

        /// <summary>
        /// Gets file original upload name without the extension.
        /// </summary>
        /// <remarks>
        /// This information will be stored in the file metadata.
        /// </remarks>
        public string OriginalName { get; private set; }

        /// <summary>
        /// Gets file content type.
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Gets file size in bytes.
        /// </summary>
        public long? Length { get; private set; }

        /// <summary>
        /// Gets file creation date.
        /// </summary>
        public DateTimeOffset? Created { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        public File(string path, string originalName, string contentType, long? length, DateTimeOffset? created = default)
        {
            Path = path;
            OriginalName = originalName;
            ContentType = contentType;
            Length = length;
            Created = created;
        }

        /// <summary>
        /// Converts a <see cref="IFormFile"/> to a document.
        /// </summary>
        public static File FromFormFile(string folder, IFormFile formFile)
        {
            if (formFile is null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            return new File($"{folder}/{Guid.NewGuid()}{System.IO.Path.GetExtension(formFile.FileName)}",
                System.IO.Path.GetFileName(formFile.FileName),
                formFile.ContentType,
                formFile.Length);
        }
    }
}
