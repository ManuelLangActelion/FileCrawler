using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler.Filters
{
    public class FolderMaxDepthFilter : IFilter
    {
        public int RootDepth { get; protected set; }
        public int MaxDepth { get; protected set; }

        public FolderMaxDepthFilter(string currentRootFolderPath, int maxDepth)
        {
            if (String.IsNullOrWhiteSpace(currentRootFolderPath))
                throw new ArgumentNullException("currentRootFolderPath");
            RootDepth = currentRootFolderPath.Trim('\\').Split('\\').Length;
            if (maxDepth < 0)
                throw new ArgumentException("The max depth must be greater or equal than 0", "maxDepth");

            this.MaxDepth = maxDepth;
        }

        public bool Authorize(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");
            int pathDept = path.Trim('\\').Split('\\').Length;
            return pathDept <= (RootDepth + MaxDepth + 1);  // list all at root
        }
    }
}
