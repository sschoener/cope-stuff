/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System;
using System.Diagnostics;
using System.IO;
using cope;
using cope.Extensions;
using CopeShared.Properties;

namespace ModTool.Core
{
    public class ArchivePacker
    {
        private const string ARCHIVE_ARGUMENTS = "-c -r \"{0}\" -a \"{1}\" -cf \"{2}.sga_design\" -log \"{2}.log\"";

        public ArchivePacker(string archiveName, string tocName, string inputFilePath, string regex)
        {
            ArchiveName = archiveName;
            TocName = tocName;
            InputDirectory = inputFilePath;
            Regex = regex;
            ArchiveAlias = tocName;
        }

        public ArchivePacker(string archiveName, string inputDirectory, string regex, bool isAttrib)
            : this(archiveName, isAttrib ? "ATTRIB" : "DATA", inputDirectory, regex)
        {
            if (isAttrib)
                ArchiveAlias = "Attributes";
            else
                ArchiveAlias = "Data";
        }

        public ArchivePacker(string archiveName, string inputDirectory, bool isAttrib)
            : this(archiveName, isAttrib ? "ATTRIB" : "DATA", inputDirectory, ".+")
        {
        }

        #region methods

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        public ArchivePackerInfo PackArchive(string outputPath)
        {
            if (!ArchiveToolHelper.IsArchiveToolPresent())
            {
                string msg = "Archive tool has not been found! Searched at: " +
                             ArchiveToolHelper.GetArchiveToolPath();
                LoggingManager.SendError("ArchiveCreator - " + msg);
                throw new CopeException(msg);
            }
            if (!outputPath.EndsWith('\\'))
                outputPath += '\\';

            string uniqueName = ArchiveName + '_' + DateTime.Now.ToProperString('_');
            string designFilePath = ArchiveToolHelper.GetArchiveToolDirectory() + uniqueName + ".sga_design";
            WriteDesignFile(designFilePath, outputPath + ArchiveName);

            string arguments = string.Format(ARCHIVE_ARGUMENTS, InputDirectory.RemoveLast(1), outputPath + ArchiveName, uniqueName);
            Process packer = StartPacker(arguments);
            if (packer == null || packer.Id == 0 || packer.Id == 1)
            {
                LoggingManager.SendError("ArchiveCreator - failed to start packer. Arguments: " + arguments);
                throw new CopeException("Failed to start archive.exe");
            }

            return new ArchivePackerInfo(packer, designFilePath, outputPath + ArchiveName);
        }

        /// <exception cref="CopeException"><c>CopeException</c>.</exception>
        private void WriteDesignFile(string designFilePath, string archiveOutputPath)
        {
            if (File.Exists(designFilePath))
            {
                try
                {
                    File.Delete(designFilePath);
                }
                catch (Exception ex)
                {
                    string msg = "Tried to delete archive design file, but failed. Path: " + designFilePath;
                    LoggingManager.SendError("ArchiveCreator - " + msg);
                    LoggingManager.HandleException(ex);
                    throw new CopeException(ex, msg);
                }
            }

            try
            {
                StreamWriter designFile = File.CreateText(designFilePath);
                designFile.WriteLine(string.Format(Resources.ArchiveDesign, ArchiveAlias, TocName, Regex));
                designFile.Flush();
                designFile.Close();
            }
            catch (Exception ex)
            {
                string msg = "Tried to write archive design file, but failed. Path: " + designFilePath;
                LoggingManager.SendError("ArchiveCreator - " + msg);
                LoggingManager.HandleException(ex);
                throw new CopeException(ex, msg);
            }
        }

        #endregion

        #region properties

        public string ArchiveName { get; private set; }

        /// <summary>
        /// Returns the name of the Table of Contents. For attrib-archives, this is "ATTRIB", for data "DATA" and so forth.
        /// </summary>
        public string TocName { get; private set; }

        public string Regex { get; private set; }

        public string InputDirectory { get; private set; }

        public string ArchiveAlias { get; set; }

        #endregion

        #region static helpers

        /// <exception cref="CopeException">Could not start archive.exe</exception>
        private static Process StartPacker(string arguments)
        {
            Process archivePacker = null;
            try
            {
                string archiveToolPath = ArchiveToolHelper.GetArchiveToolPath();
                var startInfo = new ProcessStartInfo
                                    {
                                        Arguments = arguments,
                                        CreateNoWindow = true,
                                        ErrorDialog = true,
                                        UseShellExecute = false,
                                        FileName = archiveToolPath,
                                        WorkingDirectory = archiveToolPath.SubstringBeforeLast('\\')
                                    };

                LoggingManager.SendMessage("ArchiveCreator - Starting archive.exe with " + arguments);
                archivePacker = Process.Start(startInfo);
                return archivePacker;
            }
            catch (Exception ex)
            {
                if (archivePacker != null)
                {
                    archivePacker.Kill();
                    archivePacker.Dispose();
                }
                LoggingManager.SendError("ArchiveCreator - Failed to start archive.exe");
                LoggingManager.HandleException(ex);
                throw new CopeException(ex, "Could not start archive.exe" + ex.Message);
            }
        }

        #endregion
    }
}