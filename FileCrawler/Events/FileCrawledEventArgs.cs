using CodeProject.FileCrawler.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler
{
    public class FileCrawledEventArgs : FileEnumeratorEventArgs
    {
        public FileCrawledEventArgs(string path)
            : base(path)
        { }

    }
}
