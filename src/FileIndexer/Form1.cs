using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace CodeProject
{
    public partial class Form1 : Form
    {
        String directoryRoot = @"K:\Films\X Rated\";
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtDirectory.Text = dlg.SelectedPath;
                }
            }
        }

        private void cmdCreateFiles_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                for (int x = 0; x < txtNumFiles.Value; x++)
                {
                    using (StreamWriter f = new StreamWriter(Path.Combine(txtDirectory.Text, x.ToString())))
                    {
                        f.WriteLine("Test file " + x.ToString());
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.WaitCursor;
            }

            MessageBox.Show(this, "Finished creating " + txtNumFiles.Value +
                " files", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmdEnumTest1_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            DateTime lastWriteTime = GetLastFileModifiedSlow(directoryRoot, txtFilter.Text, this.SearchOption);

            watch.Stop();

            MessageBox.Show(this, "Total time to enumerate=" + watch.ElapsedMilliseconds +
                "ms; Last write time=" + lastWriteTime,
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        DateTime GetLastFileModifiedSlow(string dir, string searchPattern, SearchOption searchOption)
        {
            DateTime retval = DateTime.MinValue;

            string[] files = Directory.GetFiles(dir, searchPattern, searchOption);
            for (int i = 0; i < files.Length; i++)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(files[i]);
                if (lastWriteTime > retval)
                {
                    retval = lastWriteTime;
                }
            }

            return retval;
        }

        private void cmdEnumTest2_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            DateTime lastWriteTime = GetLastFileModifiedFast(directoryRoot, txtFilter.Text, this.SearchOption);

            watch.Stop();

            MessageBox.Show(this, "Total time to enumerate=" + watch.ElapsedMilliseconds +
                "ms; Last write time=" + lastWriteTime,
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        DateTime GetLastFileModifiedFast(string dir, string searchPattern, SearchOption searchOption)
        {
            DateTime retval = DateTime.MinValue;

            List<FileData> files = FastDirectoryEnumerator.EnumerateFiles(dir, searchPattern, searchOption).ToList();
            foreach (FileData f in files)
            {
                if (f.LastWriteTimeUtc > retval)
                {
                    retval = f.LastWriteTimeUtc;
                }
            }

            return retval;
        }

        private void cmdEnumTest3_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            DateTime lastWriteTime = GetLastFileModifiedSlow2(directoryRoot, txtFilter.Text, this.SearchOption);

            watch.Stop();

            MessageBox.Show(this, "Total time to enumerate=" + watch.ElapsedMilliseconds +
                "ms; Last write time=" + lastWriteTime,
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        DateTime GetLastFileModifiedSlow2(string dir, string searchPattern, SearchOption searchOption)
        {
            DateTime retval = DateTime.MinValue;

            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            FileInfo[] files = dirInfo.GetFiles(searchPattern, searchOption);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].LastWriteTime > retval)
                {
                    retval = files[i].LastWriteTime;
                }
            }

            return retval;
        }

        private void cmdEnumTest4_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            DateTime lastWriteTime = GetLastFileModifiedFast2(directoryRoot, txtFilter.Text, this.SearchOption);

            watch.Stop();

            MessageBox.Show(this, "Total time to enumerate=" + watch.ElapsedMilliseconds +
                "ms; Last write time=" + lastWriteTime,
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        DateTime GetLastFileModifiedFast2(string dir, string searchPattern, SearchOption searchOption)
        {
            DateTime retval = DateTime.MinValue;

            FileData[] files = FastDirectoryEnumerator.EnumerateFiles(dir, searchPattern, SearchOption).ToArray();
            for (int i = 0; i < files.Length; i++)
            {
                if ((files[i].Attributes & FileAttributes.Directory) == 0)
                {
                    if (files[i].LastWriteTime > retval)
                    {
                        retval = files[i].LastWriteTime;
                    }
                }
            }

            return retval;
        }

        private SearchOption SearchOption
        {
            get
            {
                return chkSearchSubdirectories.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            }
        }
    }
}
