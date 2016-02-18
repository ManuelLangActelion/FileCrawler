using CodeProject.FileCrawler;
using CodeProject.FileCrawler.Filters;
using CodeProject.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeProject
{
    static class Program
    {
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}

        static PCQueue<FileData> pcQueue = new PCQueue<FileData>(2000);
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\Manu\Downloads\FastDirectoryEnumerator_src\");

            var consumer = Task.Factory.StartNew(() =>
            {
                // Store files in DB
                using (dbFileEntities c = new dbFileEntities())
                {
                    var i = 0;
                    foreach (FileData fd in pcQueue.GetItems())
                    {
                        if (fd == null || fd.IsDirectory)
                            continue;

                        c.FilesMetadatas.Add(new FilesMetadata(fd));
                        if (i % 100 == 0)
                        {
                            c.SaveChanges();    // Commit every 100 files
                        }
                        i++;
                    }
                    c.SaveChanges();
                }
            });


            string rootFolder = @"I:\";
            IList<IFilter> filters = new List<IFilter>();
            //filters.Add(new FileExtensionExcludeFilter("!bt", "zip", "rar"));
            //filters.Add(new PathExcludeNamesFilter("temp"));
            //filters.Add(new FolderMaxDepthFilter(rootFolder, 3));

            var producer = Task.Factory.StartNew(() =>
            {
                using (FileEnumerator fe = new FileEnumerator())
                {
                    if (fe.AddSource(rootFolder, filters))
                    {
                        fe.FileFound += Fe_FileFound;
                        fe.FolderFound += Fe_FolderFound;
                        fe.Start();
                    }
                }
            });
            // wait for producer to finish producing
            Task.WaitAll(producer);
            pcQueue.CompleteAdding();
            // wait for all consumers to finish consuming
            Task.WaitAll(consumer);

            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }

        private static int nbFolders = 0, nbFiles = 0;
        private static void Fe_FolderFound(object sender, FolderFoundEventArgs e)
        {
            nbFolders++;
            Console.Out.WriteLine("{0} Folder found: {1}", nbFolders, e.Path);
        }
        private static void Fe_FileFound(object sender, FileFoundEventArgs e)
        {
            nbFiles++;
            Console.Out.WriteLine("{0} File found: {1}", nbFiles, e.Path);
            pcQueue.EnqueueItem(e.FileData);
        }
    }
}
