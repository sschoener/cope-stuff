using System;
using System.IO;
using cope.Extensions;
using cope.Relic.SGA.Patching;

namespace SGAPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new[] { "ZoomMod.sgapatch", @"M:\Steam\steamapps\common\Company of Heroes 2\CoH2\Archives" };
            Console.WriteLine("cope's SGA patcher v0.91 - 10/09/2013");
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Not enough parameters.");
                Console.Error.WriteLine("Usage: ");
                Console.Error.WriteLine("csgap.exe <path of patch> [<path to archive|path containing archives>]");
                return;
            }

            string patchPath = Path.GetFullPath(args[0]);
            string archivePath = Environment.CurrentDirectory;
            if (args.Length > 1)
                archivePath = args[1];
            archivePath = Path.GetFullPath(archivePath);

            if (!File.Exists(patchPath))
            {
                Console.Error.WriteLine("The specified patch " + patchPath + " does not exist!");
                return;
            }

            Stream patchStream = null;
            try
            {
                patchStream = File.Open(patchPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                if (patchStream != null)
                    patchStream.Close();
                Console.Error.WriteLine("Could not open the patch file!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            SGAPatch patch;
            try
            {
                patch = SGAPatchReader.Read(patchStream);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to parse the patch file!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            finally
            {
                patchStream.Close();
            }

            string sgaPath;
            var sgaName = patch.SGAFileName;
            Stream sgaStream = null;
            if (Directory.Exists(archivePath))
            {
                sgaPath = Path.Combine(archivePath, sgaName);
                if (!File.Exists(sgaPath))
                {
                    Console.Error.WriteLine("The specified directory does not contain an archive called " + sgaName + ".");
                    return;
                }
            } else if (File.Exists(archivePath))
            {
                sgaPath = archivePath;
            }
            else
            {
                Console.Error.WriteLine("The specified path is neither a directory nor a file!");
                return;
            }

            try {
                sgaStream = File.Open(sgaPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            }
            catch (Exception ex)
            {
                if (sgaStream != null)
                    sgaStream.Close();
                Console.Error.WriteLine("Failed to open SGA file " + archivePath + "!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            Console.WriteLine("Applying patch " + patch.Name + " to " + archivePath);

            var patchApplied = patch.ApplyPatch(sgaStream);
            if (patchApplied.IsLeft)
            {
                Console.Error.WriteLine("The patch is not applicable! See errors below.");
                patchApplied.Left.Value.ForEach(str => Console.Error.WriteLine(str));
                return;
            }
            Console.WriteLine("Patch " + patch.Name + " has been applied!");
            sgaStream.Flush();
            sgaStream.Close();
            patchStream.Close();
        }
    }
}
