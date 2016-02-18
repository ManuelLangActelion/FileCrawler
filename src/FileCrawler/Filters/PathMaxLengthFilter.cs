using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler.Filters
{
    public class PathMaxLengthFilter : IFilter
    {
        public int MaxLength { get; protected set; }

        public PathMaxLengthFilter(int maxLength)
        {
            if (maxLength < 1)
                throw new ArgumentException("The max length must be greqter than 0", "maxLength");
            this.MaxLength = maxLength;
        }

        public bool Authorize(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");
            return path.Length <= MaxLength;
        }

    }
}
