using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeProject
{
    public partial class FilesMetadata
    {
        public FilesMetadata(FileData fd)
        {
            if (fd == null)
                throw new ArgumentNullException("fd");
            Path = fd.Path;
            Size = fd.Size;
            DateCreated = fd.CreationTimeUtc;
            DateLastAccessed = fd.LastAccessTimeUtc;
            DateLastModified = fd.LastWriteTimeUtc;
            FileExtension = System.IO.Path.GetExtension(Path); // returns .exe
            FileName = System.IO.Path.GetFileNameWithoutExtension(Path); // returns File
            PathLength = Path.Length;
            Depth = Path.Trim('\\').Split('\\').Length;
            Attributes = fd.Attributes.ToString();
        }

    }
}
