using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cope.DawnOfWar2;
using cope.DawnOfWar2.RelicAttribute;
using cope.Extensions;
using cope.SpaceMarine.BAF;

namespace Xml2Baf
{
    class Program
    {
        private static bool s_bConvertToXML;
        private static bool s_bFileMode;
        private static string s_sInput;
        private static string s_sOutput;

        static void Main(string[] args)
        {
            for (int idx = 0; idx < args.Length; idx++)
            {
                if (args[idx] == "-x")
                    s_bConvertToXML = true;
                if (args[idx] == "-f")
                    s_bFileMode = true;
                if (args[idx] == "-i" && idx < args.Length - 1)
                    s_sInput = args[idx + 1];
                if (args[idx] == "-o" && idx < args.Length - 1)
                    s_sOutput = args[idx + 1];
            }

            Console.WriteLine("cope's XML2BAF converter v.1.1");
            Console.WriteLine("Converting to " + (s_bConvertToXML ? "xml" : "attr_pc"));

            if (!s_bFileMode)
            {
                if (string.IsNullOrWhiteSpace(s_sInput))
                    s_sInput = Directory.GetCurrentDirectory() + '\\';
                else if (!s_sInput.EndsWith('\\'))
                    s_sInput += '\\';
                if (string.IsNullOrWhiteSpace(s_sOutput))
                    s_sOutput = Directory.GetCurrentDirectory() + '\\';
                else if (!s_sOutput.EndsWith('\\'))
                    s_sOutput += '\\';
                ConvertDirectory(s_sInput, s_sOutput);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(s_sInput))
                {
                    Console.WriteLine("Invalid input path!");
                    return;
                }
                s_sInput = Path.GetFullPath(s_sInput);
                if (string.IsNullOrWhiteSpace(s_sOutput))
                {
                    if (s_bConvertToXML)
                        s_sOutput = s_sInput.SubstringBeforeLast(".attr_pc") + ".xml";
                    else
                        s_sOutput = s_sInput.SubstringBeforeLast(".xml") + ".attr_pc";
                }
                ConvertFile(Path.GetFullPath(s_sInput), Path.GetFullPath(s_sOutput));
            }
            Console.WriteLine("Done");
        }

        static private void ConvertFile(string input, string output)
        {
            AttributeStructure attribStruct;
            FileStream inputStream = null;
            try
            {
                inputStream = File.Open(input, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (s_bConvertToXML)
                    attribStruct = BAFReader.Read(inputStream);
                else
                    attribStruct = AttributeXmlReader.Read(inputStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to convert '" + input + "'. Msg: " + ex.Message);
                return;
            }
            finally
            {
                if (inputStream != null)
                    inputStream.Close();
            }

            string outDir = output.SubstringBeforeLast('\\');
            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            FileStream outputStream = null;
            try
            {
                outputStream = File.Open(output, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                          FileShare.None);
                if (s_bConvertToXML)
                    AttributeXmlWriter.Write(outputStream, attribStruct);
                else
                    BAFWriter.Write(outputStream, attribStruct);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to convert '" + input + "'. Msg: " + ex.Message);
            }
            finally
            {
                if (outputStream != null)
                    outputStream.Close();
            }
        }

        static private void ConvertDirectory(string inputDir, string outputDir)
        {
            string pattern = s_bConvertToXML ? "*.attr_pc" : "*.xml";
            string[] files = Directory.GetFiles(inputDir, pattern, SearchOption.AllDirectories);

            foreach (string s in files)
            {
                Console.WriteLine("Converting: " + s.SubstringAfterLast('\\'));
                string subpath = s.SubstringAfterFirst(inputDir);
                string outPath;

                if (s_bConvertToXML)
                    outPath = outputDir + subpath.SubstringBeforeLast(".attr_pc") + ".xml";
                else
                    outPath = outputDir + subpath.SubstringBeforeLast(".xml") + ".attr_pc";
                ConvertFile(s, outPath);
            }
        }
    }
}
