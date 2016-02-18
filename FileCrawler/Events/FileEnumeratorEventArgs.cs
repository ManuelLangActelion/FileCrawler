using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler.Events
{
    public abstract class FileEnumeratorEventArgs : EventArgs
    {
        public string Path { get; protected set; }

        public FileEnumeratorEventArgs(string path)
        {
            Path = path;
        }
    }
}
