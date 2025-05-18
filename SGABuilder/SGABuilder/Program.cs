#region

using System;
using System.IO;
using cope.Extensions;
using cope.Relic.SGA;

#endregion

namespace SGABuilder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Error.WriteLine("cope's CoH2 SGAv6 builder v0.992 - 04/10/2013");
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Not enough parameters!");
                Console.Error.WriteLine("Usage: ");
                Console.Error.WriteLine("   pc2sga.exe <directory> <sga name> <ep type - e.g. data or attrib>");
                return;
            }

            var inputDir = Path.GetFullPath(args[0]);
            var sgaName = args[1].SubstringBeforeLast(".sga");
            var fileName = Path.GetFullPath(args[1]);
            var entryPointType = args[2];
            if (sgaName.Length > 64 || entryPointType.Length > 64)
            {
                Console.Error.WriteLine("Both the sga name and the entrypoint type must not exceed 64 characters!");
                return;
            }

            if (!sgaName.IsAscii() || !entryPointType.IsAscii())
            {
                Console.Error.WriteLine("Both the SGA name and the entrypoint type must be valid ASCII!");
                return;
            }

            if (!Directory.Exists(inputDir))
            {
                Console.Error.WriteLine("Input directory '" + inputDir + "' does not exist");
                return;
            }

            var outputDir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(outputDir))
            {
                Console.WriteLine("Creating output directory...");
                try
                {
                    Directory.CreateDirectory(outputDir);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Failed to create output directory'" + outputDir + "'!");
                    Console.Error.WriteLine(ex.GetInfo().Collapse());
                    return;
                }
            }

            Stream outStream = null;
            try
            {
                outStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            }
            catch (Exception ex)
            {
                if (outStream != null)
                    outStream.Close();
                Console.Error.WriteLine("Failed to open output file '" + fileName + "'!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            var settings = new SGAWriterSettings(sgaName, sgaName, entryPointType, false);
            try
            {
                SGAWriter.Write(outStream, inputDir, settings);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("SGA creation failed!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            Console.WriteLine("SGA successfully created!");
        }
    }
}