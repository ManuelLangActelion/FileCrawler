using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProject.FileCrawler.Filters
{
    public interface IFilter
    {
        bool Authorize(string path);
    }
}
