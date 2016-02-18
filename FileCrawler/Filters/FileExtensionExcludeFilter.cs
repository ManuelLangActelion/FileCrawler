using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler.Filters
{
    public class FileExtensionExcludeFilter : IFilter
    {
        public IList<string> ExcludedExtensions { get; protected set; }

        public FileExtensionExcludeFilter(IList<string> extensions)
        {
            if (extensions == null || extensions.Count < 1)
                throw new ArgumentException("extensions");
            ExcludedExtensions = extensions;
        }

        public FileExtensionExcludeFilter(params string[] extensions)
            : this(extensions.ToList())
        {
        }

        public bool Authorize(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");
            string fileExtension = Path.GetExtension(path);
            if (String.IsNullOrWhiteSpace(fileExtension))
                return true;

            return ExcludedExtensions.All(extension => !path.EndsWith(extension));
        }
    }
}
