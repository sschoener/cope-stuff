using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using cope;
using cope.DawnOfWar2;
using cope.Extensions;
using cope.DawnOfWar2.RelicBinary;

namespace RBFUpdateConsole
{
    class Program
    {
        private static FieldNameFile s_flb;
        private static bool s_bConvertToRetribution;
        private static string s_sInputDir;
        private static string s_sOutputDir;
        private static string s_sFlbPath;
        private static string[] s_files;
        private static int s_numFilesConverted;

        static void Main(string[] args)
        {
            /*args = new string[4];
            args[0] = "upgrade";
            args[1] = "fieldnames.flb";
            args[2] = "test";
            args[3] = ".\\test_conv";*/
            if (args.Length < 3)
            {
                Console.WriteLine(
                    "Invalid number of arguments!\n" +
                    "Arguments are: <conversion direction> <FLB path> <source path> [<destination path>]\n" +
                    "where <conversion direction> may either be 'upgrade' or 'downgrade' (without ' ') \n" +
                    "If no destination path has been specified, the source path will be used");
                Console.Read();
                return;
            }
            string dir = args[0].ToLowerInvariant();
            s_bConvertToRetribution = dir == "upgrade";
            s_sFlbPath = args[1];
            s_sInputDir = args[2];
            if (args.Length > 3)
                s_sOutputDir = args[3];
            if (!CheckValues())
                return;
            StartConversion();
            ConversionDone();
        }

        private static bool CheckValues()
        {
            if (s_sFlbPath == string.Empty || !File.Exists(s_sFlbPath))
            {
                Console.WriteLine("Please specify a valid FLB-file.");
                return false;
            }

            if (s_sInputDir == string.Empty)
            {
                Console.WriteLine("Please specify a valid source path.");
                return false;
            }

            if (s_sOutputDir == string.Empty)
            {
                Console.WriteLine("Please specify a valid destination path.");
                return false;
            }

            /*if (m_tbxOutputDirectory.Text == m_tbxInputDirectory.Text)
            {
                Console.WriteLine("Output directory and input directory may not overlap!");
                return;
            }*/
            if (!s_sInputDir.EndsWith('\\'))
                s_sInputDir += '\\';
            if (s_sOutputDir == null)
                s_sOutputDir = s_sInputDir;
            else if (!s_sOutputDir.EndsWith('\\'))
                s_sOutputDir += '\\';
            return true;
        }

        private static void StartConversion()
        {
            if (!Directory.Exists(s_sInputDir))
            {
                try
                {
                    Directory.CreateDirectory(s_sInputDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while trying to create the source directory: " + ex.Message);
                }
            }

            if (!Directory.Exists(s_sOutputDir))
            {
                try
                {
                    Directory.CreateDirectory(s_sOutputDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while trying to create the destination directory: " + ex.Message);
                }
            }

            try
            {
                s_flb = new FieldNameFile(s_sFlbPath);
                s_flb.ReadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open FLB file: " + ex.Message);
                return;
            }
            s_files = Directory.GetFiles(s_sInputDir, "*.rbf", SearchOption.AllDirectories);
            //ThreadPool.QueueUserWorkItem(ConvertToNewFormat);
            ConvertToNewFormat(null);
        }

        private static void ConvertToNewFormat(object o)
        {
            foreach (string s in s_files)
            {
                try
                {
                    var rbf = new RelicBinaryFile(s);
                    if (!s_bConvertToRetribution)
                        rbf.KeyProvider = s_flb;
                    else
                        rbf.UseKeyProvider = false;

                    rbf.ReadData();

                    if (s_bConvertToRetribution)
                        rbf.KeyProvider = s_flb;
                    else
                        rbf.UseKeyProvider = false;
                    string newPath = s_sOutputDir + s.SubstringAfterLast(s_sInputDir);
                    rbf.WriteDataTo(newPath);
                    rbf.Close();
                    s_numFilesConverted++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while converting " + s + ": " + ex.Message);
                }

            }
        }

        private static void ConversionDone()
        {
            Console.WriteLine(s_numFilesConverted + " of " + s_files.Length + " RBFs converted");
            if (s_flb.NeedsUpdate())
            {
                Console.WriteLine("The FLB file needs to be updated, do you still need to old one? Y/N");
                bool keepOldFlb = Console.ReadKey().Key == ConsoleKey.Y;
                try
                {
                    if (keepOldFlb)
                    {
                        s_flb.FileName = s_flb.FileName.SubstringBeforeFirst('.') + "_new.flb";
                        if (File.Exists(s_flb.FilePath))
                            File.Delete(s_flb.FilePath);
                        s_flb.Update();
                    }
                    else
                        s_flb.Update();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to save FLB file! Error: " + ex.Message);
                    return;
                }
                Console.WriteLine("Updated FLB file written to: " + s_flb.FilePath);
            }
        }

    }
}
