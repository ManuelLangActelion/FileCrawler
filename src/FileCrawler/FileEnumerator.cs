using CodeProject.FileCrawler.Filters;
using CodeProject.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler
{
    public class FileEnumerator : IDisposable
    {
        public IDictionary<string, IList<IFilter>> Sources { get; protected set; }

        public event EventHandler<FileFoundEventArgs> FileFound;
        public event EventHandler<FolderFoundEventArgs> FolderFound;


        public FileEnumerator()
        {
            Sources = new Dictionary<string, IList<IFilter>>();
        }

        public bool AddSource(string rootFolderPath, IList<IFilter> filters)
        {
            if (Sources.Any(k => rootFolderPath.Contains(k.Key)))
                return false;   // root folder or a parent is already in sources

            Sources.Add(rootFolderPath, filters);
            return true;
        }

        public bool ShouldStop { get; protected set; }

        public void Start()
        {
            if (Sources.Count() < 1)
                throw new InvalidOperationException("At least one source must be defined prior to start");

            foreach (KeyValuePair<string, IList<IFilter>> kvp in Sources)
            {
                foreach (FileData file in FastDirectoryEnumerator.EnumerateFiles(kvp.Key))
                {
                    if (ShouldStop)
                        return;

                    // if filters are defined, apply them
                    if (kvp.Value != null && kvp.Value.Count() > 0)
                    {
                        if (!kvp.Value.All(f => f.Authorize(file.Path)))
                            continue;
                    }

                    // raise events
                    if (file.IsDirectory)
                        OnFolderFound(file);
                    else
                        OnFileFound(file);
                }
            }
        }

        protected virtual void OnFileFound(FileData file)
        {
            if (FileFound != null)
            {
                FileFound(this, new FileFoundEventArgs(file));
            }
        }
        protected virtual void OnFolderFound(FileData file)
        {
            if (FolderFound != null)
            {
                FolderFound(this, new FolderFoundEventArgs(file.Path));
            }
        }

        public void Stop()
        {
            ShouldStop = true;
        }

        public void Dispose()
        {
        }
    }
}
