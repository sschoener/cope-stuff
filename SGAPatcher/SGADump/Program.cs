using System;
using System.IO;
using cope.Extensions;

namespace SGADump
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("cope's SGA dumper v1.01 - 29/09/2013");
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Not enough parameters!");
                Console.Error.WriteLine("Usage: ");
                Console.Error.WriteLine("csgadmp.exe <sga file path>");
                return;
            }

            string archivePath = Path.GetFullPath(args[0]);
            if (!File.Exists(archivePath))
            {
                Console.Error.WriteLine("The specified archive does not exist!");
                return;
            }

            Stream sgaStream = null;
            try
            {
                sgaStream = File.Open(archivePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                if (sgaStream != null)
                    sgaStream.Close();
                Console.Error.WriteLine("Failed to open the archive " + archivePath);
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            string fileName = Path.GetFileNameWithoutExtension(archivePath) + "_dump.txt";
            Stream outStream = null;
            try
            {
                outStream = File.Open(Path.GetFullPath(fileName), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                var tw = new StreamWriter(outStream);
                cope.Relic.SGA.SGAReader.DumpSGAInfo(sgaStream, tw);
                tw.Flush();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to write output file " + archivePath);
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            finally
            {
                if (outStream != null)
                    outStream.Close();
            }
            Console.WriteLine("Dumpfile '" + fileName + "' successfully created");
        }
    }
}
