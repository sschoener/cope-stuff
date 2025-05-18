using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Extensions;
using cope.FileSystem;
using cope.Relic.SGA;
using cope.Relic.SGA.Patching;

namespace SGAPatchCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("cope's SGA patch creator v0.92 - 29/09/2013");

            if (args.Length == 4)
            {
                if (args[0] != "-s")
                {
                    Console.Error.WriteLine("Unknown argument " + args[0]);
                    PrintUsage();
                    return;
                }
                Console.WriteLine("Switched to size validation mode.");
                ValidationMode(args);
            }

            // input: name of the patch, archive to patch, directory w/ the patches
            if (args.Length < 3)
            {
                PrintUsage();
                return;
            }
            CreationMode(args);
        }

        static void PrintUsage()
        {
            Console.Error.WriteLine("Not enough parameters!");
            Console.Error.WriteLine("Usage: ");
            Console.Error.WriteLine("csgapc.exe <ascii name of the patch> <path to the archive> <root directory of the patch>");
            Console.Error.WriteLine("or to check the size of a single file: ");
            Console.Error.WriteLine("csgapc.exe -s <path to the archive> <path of the file to check> <path in the SGA>");
        }

        static void ValidationMode(string[] args)
        {
            string archivePath = Path.GetFullPath(args[1]);
            if (!File.Exists(archivePath))
            {
                Console.Error.WriteLine("The specified archive does not exist!");
                return;
            }

            string patchFile = Path.GetFullPath(args[2]);
            if (!File.Exists(patchFile))
            {
                Console.Error.WriteLine("The specified file to check does not exist!");
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

            SGAEntryPoint ep;
            try
            {
                ep = SGAReader.Read(sgaStream)[0];
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to parse the archive as an SGA!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            sgaStream.Close();

            byte[] data;
            try
            {
                data = File.ReadAllBytes(patchFile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to open input file for size validation!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            string pathInSga = args[3];
            if (!ep.DoesFileExist(pathInSga))
            {
                Console.Error.WriteLine("The path " + pathInSga + " does not point to any file in the SGA!");
                return;
            }
            bool isCompressed = ep.IsCompressedInArchive(pathInSga);
            uint size = ep.GetSizeInArchive(pathInSga);

            if (size != data.Length)
            {
                Console.WriteLine("Your file does NOT have the right size: It has " + data.Length + " bytes but should have " + size);
                return;
            }

            if (isCompressed)
            {
                var compressedData = Ionic.Zlib.ZlibStream.CompressBuffer(data);
                var compressedSize = compressedData.Length;
                if (compressedSize != size)
                {
                    Console.WriteLine("Your file does not have the right size when compressed. It has " + compressedSize + " bytes but should have " + size);
                    return;
                }
            }
            Console.WriteLine("Your file has the right size of " + size + ".");
        }

        static void CreationMode(string[] args)
        {
            string patchName = args[0];
            if (!patchName.IsAscii())
            {
                Console.Error.WriteLine("The name of the patch must be an Ascii string!");
                return;
            }
            if (patchName.ContainsAny(Path.GetInvalidPathChars()))
            {
                Console.Error.WriteLine(
                    "The name of the patch must not contain any characters which are illegal in file names!");
                return;
            }

            string archivePath = Path.GetFullPath(args[1]);
            if (!File.Exists(archivePath))
            {
                Console.Error.WriteLine("The specified archive does not exist!");
                return;
            }

            string patchFilesDir = Path.GetFullPath(args[2]);
            if (!Directory.Exists(patchFilesDir))
            {
                Console.Error.WriteLine("The specified patch root directory does not exist!");
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

            SGAEntryPoint ep;
            try
            {
                ep = SGAReader.Read(sgaStream)[0];
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to parse the archive as an SGA!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            var fs = new LocalFileSystem(patchFilesDir);
            var fileWalker = fs.GetFileWalker();
            List<SGAFilePatch> filePatches;
            try
            {
                filePatches = fileWalker.Select(file => CreateFilePatch(ep, (LocalFileDescriptor)file)).Where(patch => patch != null).ToList();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to create file patch!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }

            var sgaName = Path.GetFileName(archivePath);
            var sgaPatch = new SGAPatch(patchName, sgaName, filePatches);

            Stream patchOutstream = null;
            try
            {
                patchOutstream = File.Create(patchName + ".sgapatch");
            }
            catch (Exception ex)
            {
                if (patchOutstream != null)
                    patchOutstream.Close();
                Console.Error.WriteLine("Failed to create output file " + patchName + ".sgpatch");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            try
            {
                SGAPatchWriter.Write(patchOutstream, sgaPatch);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to write patch file!");
                Console.Error.WriteLine(ex.GetInfo().Collapse());
                return;
            }
            patchOutstream.Flush();
            patchOutstream.Close();
            sgaStream.Close();
        }

        /// <exception cref="Exception"><c>Exception</c>.</exception>
        static SGAFilePatch CreateFilePatch(SGAEntryPoint ep, LocalFileDescriptor file)
        {
            string relativePath = file.GetPath();

            if (!ep.DoesFileExist(relativePath))
            {
                Console.Error.WriteLine("The file " + file.FullPath +
                                        " does not exist in the archive. It will not be included in the patch.");
                return null;
            }
                

            bool isCompressed = ep.IsCompressedInArchive(relativePath);
            uint fileSizeInArchive = ep.GetSizeInArchive(relativePath);

            byte[] fileData;
            try
            {
                fileData = File.ReadAllBytes(file.FullPath);
            } 
            catch(Exception ex)
            {
                throw new Exception("Could not read " + file.FullPath, ex);
            }
            uint uncompressedSize = (uint) fileData.Length;

            if (isCompressed)
            {
                using (var compressStream = new MemoryStream())
                {
                    using (var compressor = new Ionic.Zlib.ZlibStream(compressStream, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestCompression))
                    {
                        compressor.Write(fileData, 0, fileData.Length);
                        compressor.Close();
                        fileData = compressStream.ToArray();
                    }
                }
            }
                

            uint dataSize = (uint) fileData.Length;
            if (dataSize != fileSizeInArchive)
            {
                throw new Exception("The size of the file " + file.FullPath +
                                    " (" + dataSize + ")does not match the size of the file in the archive (" + fileSizeInArchive + "). It is " + 
                                    (!isCompressed ? "not" : string.Empty) + "compressed.");
            }
            return new SGAFilePatch(relativePath, fileData, uncompressedSize, isCompressed);
        }
    }
}
