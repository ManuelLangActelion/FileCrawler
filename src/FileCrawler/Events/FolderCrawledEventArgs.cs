using CodeProject.FileCrawler.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler
{
    public class FolderCrawledEventArgs : FileEnumeratorEventArgs
    {
        public FolderCrawledEventArgs(string path)
            : base(path)
        { }
    }
}
