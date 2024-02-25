using System.IO.Compression;

public class ZipExtractor
    {
        public event EventHandler<int> UnzipProgressChanged;

        public async Task ExtractZipAsync(string zipFilePath, string extractPath, string skippedExtension)
        {
            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Open))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                // Calculate total size for progress reporting excluding skipped files
                long totalBytes = archive.Entries
                    .Where(entry => !entry.FullName.EndsWith("/") && !Path.GetExtension(entry.FullName).Equals(skippedExtension, StringComparison.OrdinalIgnoreCase))
                    .Sum(entry => entry.Length);

                long extractedBytes = 0;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Skip entries that are directories or have the specified extension
                    if (entry.FullName.EndsWith("/") || Path.GetExtension(entry.FullName).Equals(skippedExtension, StringComparison.OrdinalIgnoreCase))
                        continue;

                    // Get the file name without the top-level folder
                    string relativePath = entry.FullName.Substring(entry.FullName.IndexOf('/') + 1);
                    string entryFullPath = Path.Combine(extractPath, relativePath);

                    // Ensure the extraction directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(entryFullPath));

                    using (Stream entryStream = entry.Open())
                    using (FileStream entryFile = new FileStream(entryFullPath, FileMode.Create))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;

                        while ((bytesRead = await entryStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await entryFile.WriteAsync(buffer, 0, bytesRead);

                            // Update progress
                            extractedBytes += bytesRead;
                            int progressPercentage = (int)((extractedBytes * 100) / totalBytes);

                            // Raise the progress changed event
                            OnUnzipProgressChanged(progressPercentage);
                        }
                    }
                }
            }
        }

        private void OnUnzipProgressChanged(int progressPercentage)
        {
            UnzipProgressChanged?.Invoke(this, progressPercentage);
        }
    }