using CodeProject.FileCrawler.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler
{
    public class FileFoundEventArgs : FileEnumeratorEventArgs
    {
        public FileData FileData { get; set; }

        public FileFoundEventArgs(FileData file)
            : base(file.Path)
        {
            FileData = file;
        }

    }
}
