using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murmur;
using System.Security.Cryptography;
using System.IO;
using CodeProject.CryptographyUtils;
using System.Threading;
using CodeProject.FileCrawler;
using CodeProject.FileCrawler.Filters;
using CodeProject.Threading;
using CodeProject;
using System.Collections.Concurrent;

namespace MurmurRunner
{
    class Program
    {
        static readonly HashAlgorithm Unmanaged = MurmurHash.Create128(managed: false);
        static readonly HashAlgorithm Managed = MurmurHash.Create128(managed: true);
        static readonly HashAlgorithm Unmanagedx86 = MurmurHash.Create128(managed: false, preference: AlgorithmPreference.X86);
        static readonly HashAlgorithm Managedx86 = MurmurHash.Create128(managed: true, preference: AlgorithmPreference.X86);
        static readonly HashAlgorithm Unmanagedx64 = MurmurHash.Create128(managed: false, preference: AlgorithmPreference.X64);
        static readonly HashAlgorithm Managedx64 = MurmurHash.Create128(managed: true, preference: AlgorithmPreference.X64);
        static readonly HashAlgorithm Unmanaged32 = MurmurHash.Create32(managed: false);
        static readonly HashAlgorithm Managed32 = MurmurHash.Create32(managed: true);

        static readonly HashAlgorithm Sha1 = SHA1Managed.Create();
        static readonly HashAlgorithm Sha256 = SHA256Managed.Create();
        static readonly HashAlgorithm Md5 = MD5.Create();

        static readonly byte[] RandomData = GenerateRandomData();
        static readonly byte[] SampleData = CreateSampleData();
        const int FAST_ITERATION_COUNT = 10000000;
        const int SLOW_ITERATION_COUNT = 100000;
        static readonly IndentingConsoleWriter OutputWriter = new IndentingConsoleWriter();

        static BlockingCollection<FileData> bc = new BlockingCollection<FileData>(100);

        public static void Test(string[] args)
        {
            PCQueue<string> pcQueue1 = new PCQueue<string>(5);
            for (int i = 0; i < 4; i++)
            {
                Console.Write("p1=" + i + ", ");
                pcQueue1.EnqueueItem(i.ToString());
                Thread.Sleep(1000);
            }
            pcQueue1.CompleteAdding();

            string l;
            while (pcQueue1.TryTake(out l))
            {
                Console.Write("c1=" + l + ", ");
                Thread.Sleep(1000);
            }

            foreach (var i in pcQueue1.GetItems())
            {
                Console.Write("c2=" + i + ", ");
                //Thread.Sleep(1000);
            }

            int c1Nb = 0, c2Nb = 0;
            PCQueue<string> pcQueue = new PCQueue<string>(100);
            var consumer1 = Task.Factory.StartNew(() =>
            {
                foreach (string data in pcQueue.GetItems())
                {
                    Console.WriteLine("c1=" + data + ", ");
                    c1Nb++;
                    Thread.Sleep(10);
                }
            });
            var consumer2 = Task.Factory.StartNew(() =>
            {
                foreach (string data in pcQueue.GetItems())
                {
                    Console.WriteLine("c2=" + data + ", ");
                    c2Nb++;
                    Thread.Sleep(10);
                }
            });

            // produce 100 integers on a producer thread
            var str = "abcdefghijklmnopqrstuvwxyz";
            var item = "";
            var producer = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < str.Length; i++)
                {
                    item = str[i].ToString();
                    pcQueue.EnqueueItem(item);
                    Console.WriteLine("p1=" + item + ", ");
                }
            });
            var item2 = "";
            var producer2 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < str.Length; i++)
                {
                    item2 = str[i].ToString();
                    pcQueue.EnqueueItem(item2);
                    Console.WriteLine("p2=" + item2 + ", ");
                }
            });

            // wait for producer to finish producing
            Task.WaitAll(producer, producer2);
            pcQueue.CompleteAdding();
            // wait for all consumers to finish consuming
            Task.WaitAll(consumer1, consumer2);

            Console.WriteLine("done: c1=" + c1Nb + ", c2=" + c2Nb);

            /*
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\Manu\Downloads\FastDirectoryEnumerator_src\");
            using (dbFileEntities c = new dbFileEntities())
            {
                //c.Database.Connection.ConnectionString=
                c.FilesMetadatas.Add(new FilesMetadata() { Path = @"C:\Users\Manu\Downloads\FastDirectoryEnumerator_src\Data" });
                c.SaveChanges();
            }
            //OutputWriter.WriteLine("* Environment Architecture: {0}", Environment.Is64BitProcess ? "x64" : "x86");
            //OutputWriter.NewLines(1);

            //using (OutputWriter.Indent(2))
            //{
            //    // guid output
            //    //var guidSteps = new Dictionary<string, Tuple<HashAlgorithm, int>> 
            //    //{
            //    //    { "Murmur 32 Managed", Tuple.Create(Managed32, FAST_ITERATION_COUNT) },
            //    //    { "Murmur 32 Unanaged", Tuple.Create(Unmanaged32, FAST_ITERATION_COUNT) },
            //    //    { "Murmur 128 Managed", Tuple.Create(Managed, FAST_ITERATION_COUNT) },    
            //    //    { "Murmur 128 Unmanaged", Tuple.Create(Unmanaged, FAST_ITERATION_COUNT) },
            //    //    { "SHA1", Tuple.Create(Sha1, SLOW_ITERATION_COUNT) },
            //    //    { "MD5", Tuple.Create(Md5, SLOW_ITERATION_COUNT) }
            //    //};

            //    //Run(name: "Guid x 8", dataLength: SampleData.LongLength, hasher: a => a.ComputeHash(SampleData), steps: guidSteps);
            //    //Run(name: "Guid x 8 Partial", dataLength: SampleData.LongLength - 3, hasher: a => a.ComputeHash(SampleData, 3, (int)(SampleData.LongLength - 3)), steps: guidSteps);

            //    // random data tests
            //    var randomSteps = new Dictionary<string, Tuple<HashAlgorithm, int>>
            //    {
            //        //{ "Murmur 32 Managed", Tuple.Create(Managed32, SLOW_ITERATION_COUNT) },
            //        //{ "Murmur 32 Unanaged", Tuple.Create(Unmanaged32, SLOW_ITERATION_COUNT) },
            //        { "Murmur 128 Managed", Tuple.Create(Managed, 2500) },
            //        { "Murmur 128 Unmanaged", Tuple.Create(Unmanaged, 2500) },
            //        { "SHA1", Tuple.Create(Sha1, 2500) },
            //        { "MD5", Tuple.Create(Md5, 2500) }
            //    };

            //    //Run(name: "Random", dataLength: RandomData.LongLength, hasher: a => a.ComputeHash(RandomData), steps: randomSteps);

            //    using (var stream = new MemoryStream(RandomData))
            //    {
            //        Func<HashAlgorithm,byte[]> streamhasher = a =>
            //        {
            //            stream.Position = 0L;
            //            return a.ComputeHash(stream);
            //        };

            //        Run(name: "Stream", dataLength: stream.Length, hasher: streamhasher, steps: randomSteps);
            //    }
            //}

            //using (BlockingCollection<int> bc = new BlockingCollection<int>())
            //{

            //    // Kick off a producer task
            //    Task.Factory.StartNew(() =>
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            bc.Add(i);
            //            Thread.Sleep(100); // sleep 100 ms between adds
            //        }

            //        // Need to do this to keep foreach below from hanging
            //        bc.CompleteAdding();
            //    });

            //    // Now consume the blocking collection with foreach.
            //    // Use bc.GetConsumingEnumerable() instead of just bc because the
            //    // former will block waiting for completion and the latter will
            //    // simply take a snapshot of the current state of the underlying collection.
            //    foreach (var item in bc.GetConsumingEnumerable())
            //    {
            //        Console.WriteLine(item);
            //    }
            //}


            string rootFolder = @"K:\Downloads\In progress torrents";
            IList<IFilter> filters = new List<IFilter>();
            //filters.Add(new FileExtensionExcludeFilter("!bt", "zip", "rar"));
            filters.Add(new PathExcludeNamesFilter("temp"));
            filters.Add(new FolderMaxDepthFilter(rootFolder, 3));


            Task.Factory.StartNew(() =>
            {
                using (FileEnumerator fe = new FileEnumerator())
                {
                    if (fe.AddSource(rootFolder, filters))
                    {
                        fe.FileFound += Fe_FileFound;
                        //fe.FolderFound += Fe_FolderFound;
                        fe.Start();
                    }
                }
                //foreach (FileData f in FastDirectoryEnumerator.EnumerateFiles(rootFolder))
                //{
                //    if (bc.Count >= (bc.BoundedCapacity - 1))
                //    {
                //        while (bc.Count > 10)
                //        {
                //            Thread.Sleep(1000);
                //        }
                //    }
                //    bc.Add(f);
                //    Console.WriteLine("produce: " + f);
                //}
                bc.CompleteAdding();
            });

            foreach(FileData f in bc.GetConsumingEnumerable())
            {
                Console.WriteLine("consume: " + f);
                Thread.Sleep(1000);
            }

            //foreach (var item in bc.GetConsumingEnumerable())
            //{
            //    Console.WriteLine("item: " + item);
            //}

            string filePath = @"K:\Films\SHREK HQ Fr.AVI";
            //hashFile(filePath, Sha256);
            //hashFile(filePath, Sha1);
            //hashFile(filePath, Md5);
            //hashFile(filePath, Managedx86);
            //hashFile(filePath, Managedx64);

            string result = "";
            var timer = Stopwatch.StartNew();
            for (int i = 0; i < 3; i++)
            {
                ASyncFileHashAlgorithm hasher = new ASyncFileHashAlgorithm(MD5.Create());
                //hasher.FileHashingProgress += OnFileHashingProgress;
                using (var stream = new BufferedStream(File.OpenRead(filePath), 1200000))
                {
                    Thread t = new Thread(
                        delegate ()
                        {
                            hasher.ComputeHash(stream);
                            result = hasher.ToString();
                        }
                    );
                    t.Start();
                    t.Join();
                }
            }
            timer.Stop();

            double totalSeconds = timer.ElapsedMilliseconds / 1000.0;
            Console.WriteLine(result + ": " + totalSeconds + "s");

            Thread.Sleep(1000);
            // hasher.Hash has the byte[] computed hash
            // Console.WriteLine(hasher); // ToString() converts to a string representing the hex value of the hash
            */


            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit.");
                Console.Read();
            }
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
            bc.Add(e.FileData);
        }

        public static void OnFileHashingProgress(object sender, FileHashingProgressArgs e)
        {
            Console.WriteLine(e.ProcessedSize + " of " + e.TotalSize);
        }

        private static void hashFile(string filePath, HashAlgorithm hashalgo)
        {
            var timer = Stopwatch.StartNew();
            string result = "";
            for (int i = 0; i < 10000; i++)
            {
                using (var stream = new BufferedStream(File.OpenRead(filePath), 1200000))
                {
                    byte[] checksum = hashalgo.ComputeHash(stream);
                    result = BitConverter.ToString(checksum).Replace("-", String.Empty);
                }
            }
            timer.Stop();

            double totalSeconds = timer.ElapsedMilliseconds / 1000.0;
            Console.WriteLine(result + ": " + totalSeconds + "s");
        }

        private static void Run(string name, long dataLength, Func<HashAlgorithm, byte[]> hasher, Dictionary<string, Tuple<HashAlgorithm, int>> steps)
        {
            OutputWriter.WriteLine("* Data Set: {0}", name);
            using (OutputWriter.Indent())
            {
                foreach (var step in steps)
                {
                    var algorithmFriendlyName = step.Key;
                    var algorithm = step.Value.Item1;
                    var iterations = step.Value.Item2;

                    OutputWriter.WriteLine("{1} x {0:N0}", iterations, algorithmFriendlyName);
                    Profile(algorithm, iterations, dataLength, hasher);
                }
            }
        }

        static void Profile(HashAlgorithm algorithm, int iterations, long dataLength, Func<HashAlgorithm, byte[]> hasher)
        {
            using (OutputWriter.Indent())
            {
                var referenceHash = hasher(algorithm);
                //WriteProfilingResult("Runs", "{0:N0}", iterations);
                WriteProfilingResult("Output", GetHashAsString(referenceHash));

                // warmup
                for (int i = 0; i < 1000; i++)
                    hasher(algorithm);

                // profile
                var timer = Execute(algorithm, iterations, referenceHash, hasher);

                // results
                WriteProfilingResult("Length", "{0}   ", dataLength);
                WriteProfilingResult("Duration", "{0:N0} ms ({1:N0} ticks)", timer.ElapsedMilliseconds, timer.ElapsedTicks);
                WriteProfilingResult("Ops/Tick", "{0:N3}", Divide(iterations, timer.ElapsedTicks));
                WriteProfilingResult("Ops/ms", "{0:N3}", Divide(iterations, timer.ElapsedMilliseconds));

                // calculate throughput
                WriteThroughput(dataLength, iterations, timer);
            }

            OutputWriter.NewLines();
        }


        private static Stopwatch Execute(HashAlgorithm algorithm, int iterations, byte[] expected, Func<HashAlgorithm, byte[]> hasher)
        {

            int batches = 100;
            int batchSize = iterations / batches;
            var timer = Stopwatch.StartNew();
            for (int i = 0; i < batches; i++)
            {
                // write our progress
                WriteProfilingResult("Progress", "{0:P0}", Divide(i, batches));

                // run our batch
                for (int j = 0; j < batchSize; j++)
                {
                    var result = hasher(algorithm);
                    if (!Equal(expected, result))
                        throw new Exception("Received inconsistent hash.");
                }

            }

            // stop profiling
            timer.Stop();
            return timer;
        }


        private static void WriteThroughput(long length, long iterations, Stopwatch timer)
        {
            double totalBytes = length * iterations;
            double totalSeconds = timer.ElapsedMilliseconds / 1000.0;

            double bytesPerSecond = totalBytes / totalSeconds;
            double mbitsPerSecond = (bytesPerSecond / (1024.0 * 1024.0));

            WriteProfilingResult("MiB/s", "{0:N3}", mbitsPerSecond);
        }

        private static bool Equal(byte[] expected, byte[] result)
        {
            if (expected.Length != result.Length) return false;
            for (int i = 0; i < expected.Length; i++)
                if (expected[i] != result[i])
                    return false;

            return true;
        }

        static double Divide(long a, long b)
        {
            return ((double)a / (double)b);
        }

        static void WriteProfilingResult(string name, string format = "{0}", params object[] args)
        {
            //var value = string.Format("* {0}:\t===>\t", name);
            var value = string.Format("* {0}:\t", name);
            OutputWriter.WriteLine(value + format, args);
        }

        private static string GetHashAsString(byte[] hash)
        {
            var builder = new StringBuilder(16);
            for (int i = 0; i < hash.Length; i++)
                builder.Append(hash[i].ToString("x2"));

            return builder.ToString();
        }

        public static byte[] Build(Guid[] values)
        {
            var target = new byte[values.Length * 16];
            int offset = 0;
            foreach (var v in values)
            {
                Array.Copy(v.ToByteArray(), 0, target, offset, 16);
                offset += 16;
            }

            return target;
        }

        private static byte[] GenerateRandomData()
        {
            byte[] data = new byte[256 * 1024 + 13];
            using (var gen = RandomNumberGenerator.Create())
                gen.GetBytes(data);

            return data;
        }

        public static byte[] CreateSampleData()
        {
            var data = Build(new Guid[] {
                new Guid("1DB2A25C-57A3-471A-B81B-A01900A63F49"),
                new Guid("24185DD0-1CB6-48EF-90A7-9F4A00D9BA0D"),
                new Guid("6D21CDF4-70CC-4424-B72C-9F4A00D9BA0D"),
                new Guid("6D194DF5-F28E-43B6-BBD2-9FB300D0CE52"),
                new Guid("46F54EF7-5C8C-48C5-82EF-9F4A00D9BA0D"),
                new Guid("DC34023A-A985-4EA2-BD22-9F4A00D9BA0D"),
                new Guid("D12CAC55-E7C5-45D9-AE39-A0200092ACB0"),
                new Guid("AD9EE455-27E2-4C0D-B3F1-9F4A00D9BA0D"),
            });

            return data;
        }
    }

    class IndentingConsoleWriter
    {
        private int IndentAmount { get; set; }
        private string IndentString { get; set; }
        public void SetIndent(int value)
        {
            IndentAmount = value;
            IndentString = IndentAmount == 0 ? "" : new string(' ', IndentAmount);
        }

        public IDisposable Indent(int count = 4)
        {
            // create a new scope with current amount, then increment our indent
            var scope = new IndentationScope(this, IndentAmount);
            SetIndent(IndentAmount += count);

            return scope;
        }

        public void WriteLine(string format, params object[] args)
        {
            if (IndentAmount > 0)
                Console.Write(IndentString);

            Console.WriteLine(format, args);
        }

        public void NewLines(int count = 1)
        {
            for (int i = 0; i < count; i++)
                Console.WriteLine();
        }

        class IndentationScope : IDisposable
        {
            readonly IndentingConsoleWriter Writer;
            readonly int Amount;
            public IndentationScope(IndentingConsoleWriter writer, int amount)
            {
                Writer = writer;
                Amount = amount;
            }

            public void Dispose()
            {
                // restore indent amount
                Writer.SetIndent(Amount);
            }
        }
    }
}
