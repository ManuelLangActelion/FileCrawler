namespace CodeProject.CryptographyUtils
{
    public class FileHashingProgressArgs
    {
        public long ProcessedSize { get; protected set; }
        public long TotalSize { get; protected set; }

        public FileHashingProgressArgs(long totalBytesRead, long size)
        {
            this.TotalSize = size;
            this.ProcessedSize = totalBytesRead;
        }
    }
}