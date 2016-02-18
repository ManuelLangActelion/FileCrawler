using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Linq;
using System.Security;
using System.Security.Cryptography;

namespace CodeProject
{



    /// <summary>
    /// A fast enumerator of files in a directory.  Use this if you need to get attributes for 
    /// all files in a directory.
    /// </summary>
    /// <remarks>
    /// This enumerator is substantially faster than using <see cref="Directory.GetFiles(string)"/>
    /// and then creating a new FileInfo object for each path.  Use this version when you 
    /// will need to look at the attibutes of each file returned (for example, you need
    /// to check each file in a directory to see if it was modified after a specific date).
    /// </remarks>
    public static class FastDirectoryEnumerator
    {
        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <returns>An object that implements <see cref="IEnumerable{FileData}"/> and 
        /// allows you to enumerate the files in the given directory.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is a null reference (Nothing in VB)
        /// </exception>
        public static IEnumerable<FileData> EnumerateFiles(string path)
        {
            return FastDirectoryEnumerator.EnumerateFiles(path, "*");
        }

        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory that match a 
        /// specific filter.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The search string to match against files in the path.</param>
        /// <returns>An object that implements <see cref="IEnumerable{FileData}"/> and 
        /// allows you to enumerate the files in the given directory.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is a null reference (Nothing in VB)
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filter"/> is a null reference (Nothing in VB)
        /// </exception>
        public static IEnumerable<FileData> EnumerateFiles(string path, string searchPattern)
        {
            return FastDirectoryEnumerator.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Gets <see cref="FileData"/> for all the files in a directory that 
        /// match a specific filter, optionally including all sub directories.
        /// </summary>
        /// <param name="path">The path to search.</param>
        /// <param name="searchPattern">The search string to match against files in the path.</param>
        /// <param name="searchOption">
        /// One of the SearchOption values that specifies whether the search 
        /// operation should include all subdirectories or only the current directory.
        /// </param>
        /// <returns>An object that implements <see cref="IEnumerable{FileData}"/> and 
        /// allows you to enumerate the files in the given directory.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is a null reference (Nothing in VB)
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="filter"/> is a null reference (Nothing in VB)
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption"/> is not one of the valid values of the
        /// <see cref="System.IO.SearchOption"/> enumeration.
        /// </exception>
        public static IEnumerable<FileData> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            if (searchPattern == null)
            {
                throw new ArgumentNullException("searchPattern");
            }

            string fullPath = Path.GetFullPath(path);

            return ListAllFiles(path, searchPattern);
        }


        public static IEnumerable<FileData> ListAllFiles(string directoryFullPath, string searchPattern)
        {
            // check for necessary permissions
            FileIOPermission p = null;
            bool accessGranted = false;
            try
            {
                p = new FileIOPermission(FileIOPermissionAccess.PathDiscovery, directoryFullPath);
                p.Demand(); // Access granted
                accessGranted = true;
            }
            catch (SecurityException)
            {
                // Access deined: just ignore and move to the next file
            }
            if (accessGranted)
            {
                string searchPath = Path.Combine(directoryFullPath, searchPattern);
                NativeWin32.WIN32_FIND_DATA findData;
                using (NativeWin32.SafeFindHandle hFindFile = NativeWin32.FindFirstFile(searchPath, out findData))
                {
                    if (!hFindFile.IsInvalid)
                    {
                        do
                        {
                            if (findData.cFileName.Equals(@".")
                                || findData.cFileName.Equals(@".."))
                                continue;


                            FileData result = new FileData(directoryFullPath, findData);
                            if ((findData.dwFileAttributes & FileAttributes.Directory) != FileAttributes.Directory)
                            {
                                // file
                                //try {
                                //    using (var md5 = MD5.Create())
                                //    {
                                //        using (var stream = new BufferedStream(File.OpenRead(result.Path), 1200000))
                                //        {
                                //            result.Md5Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                //        }
                                //    }
                                //} catch(UnauthorizedAccessException uae)
                                //{
                                //    // skip
                                //}

                                yield return result;
                            }
                            else
                            {
                                // directory
                                result.IsDirectory = true;
                                yield return result;
                                foreach (FileData res in ListAllFiles(result.Path, searchPattern))
                                    yield return res;
                            }
                        } while (NativeWin32.FindNextFile(hFindFile, out findData));
                    }
                }
            }
        }


    }
}
