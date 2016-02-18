using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler.Filters
{
    public class PathExcludeNamesFilter : IFilter
    {
        public IList<string> ExcludedNames { get; protected set; }

        public PathExcludeNamesFilter(IList<string> names)
        {
            if (names == null || names.Count < 1)
                throw new ArgumentException("names");
            ExcludedNames = names;
        }

        public PathExcludeNamesFilter(params string[] names)
            : this(names.ToList())
        {
        }

        public bool Authorize(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");

            return ExcludedNames.All(name => !path.Contains(name));
        }
    }
}
