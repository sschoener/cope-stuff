using System.IO;
using cope.Extensions;

namespace cope.Relic.SGA
{
    internal class SGADumper
    {
        private readonly string[] m_sFilepaths;
        private readonly RawSGADescriptor m_sga;

        private SGADumper(RawSGADescriptor sga, string[] filepaths = null)
        {
            m_sFilepaths = filepaths;
            m_sga = sga;
        }

        private void DumpInfo(TextWriter tw)
        {
            DumpFileHeader(m_sga.FileHeader, tw);
            tw.WriteLine();
            DumpDataHeader(m_sga.FileHeader.DataHeaderOffset, m_sga.DataHeader, tw);
            tw.WriteLine();
            for (int ei = 0; ei < m_sga.EntryPoints.Length; ei++)
            {
                DumpEntryPoint(m_sga.EntryPoints[ei], tw, ei);
                tw.WriteLine();
            }

            for (int di = 0; di < m_sga.Directories.Length; di++)
            {
                DumpDirectoryEntry(m_sga.Directories[di], tw, di);
                tw.WriteLine();
            }


            for (int fi = 0; fi < m_sga.Files.Length; fi++)
            {
                DumpFileEntry(m_sga.Files[fi], tw, fi);
                tw.WriteLine();
            }
        }

        public static void DumpInfo(RawSGADescriptor sga, TextWriter tw, string[] filepaths = null)
        {
            var dumper = new SGADumper(sga, filepaths);
            dumper.DumpInfo(tw);
        }

        public static void DumpFileHeader(SGAFileHeader fh, TextWriter tw)
        {
            tw.WriteLine("<File Header>");
            tw.WriteLine("Archive name: " + fh.ArchiveName);
            tw.WriteLine("Archive version: " + fh.Version);
            tw.WriteLine("Data header offset: " + string.Format("0x{0:X8}", fh.DataHeaderOffset));
            tw.WriteLine("Data header size: " + string.Format("0x{0:X8}", fh.DataHeaderSize));
            tw.WriteLine("Data offset: " + string.Format("0x{0:X8}", fh.DataOffset));
        }

        public static void DumpDataHeader(uint dataHeaderOffset, SGADataHeader dh, TextWriter tw)
        {
            tw.WriteLine("<Data Header>");
            tw.WriteLine("EP count: " + dh.EntryPointCount);
            tw.WriteLine("EP section offset: " + string.Format("0x{0:X8}", dataHeaderOffset + dh.EntryPointSectionOffset));
            tw.WriteLine("Directory count: " + dh.DirectoryCount);
            tw.WriteLine("Directory section offset: " + string.Format("0x{0:X8}", dataHeaderOffset + dh.DirectorySectionOffset));
            tw.WriteLine("File count: " + dh.FileCount);
            tw.WriteLine("File section offset: " + string.Format("0x{0:X8}", dataHeaderOffset + dh.FileSectionOffset));
            tw.WriteLine("String count: " + dh.StringCount);
            tw.WriteLine("String section offset: " + string.Format("0x{0:X8}", dataHeaderOffset + dh.StringSectionOffset));
        }

        public static void DumpEntryPoint(RawEntryPoint ep, TextWriter tw, int num)
        {
            tw.WriteLine("<Entry Point " + num + ">");
            tw.WriteLine("Offset: " + string.Format("0x{0:X8}", ep.ThisDescriptorOffset));
            tw.WriteLine("Alias: " + ep.Alias);
            tw.WriteLine("Type: " + ep.Name);
            tw.WriteLine("Unknown value: " + string.Format("0x{0:X8}", ep.UnknownValue));
            tw.WriteLine("From directory: " + ep.IndexOfFirstDirectory);
            tw.WriteLine("Until directoy: " + ep.IndexOfLastDirectory);
            tw.WriteLine("From file: " + ep.IndexOfFirstFile);
            tw.WriteLine("Until file: " + ep.IndexOfLastFile);
        }

        public void DumpDirectoryEntry(RawDirectoryDescriptor dir, TextWriter tw, int num)
        {
            tw.WriteLine("<Directory " + num + ">");
            tw.WriteLine("Offset: " + string.Format("0x{0:X8}", dir.ThisDescriptorOffset));
            tw.WriteLine("Path: " + dir.Path);
            tw.WriteLine("Path offset: " + dir.ThisPathOffset);
            tw.WriteLine("From directory: " + dir.IndexOfFirstDirectory);
            tw.WriteLine("Until directoy: " + dir.IndexOfLastDirectory);
            tw.WriteLine("From file: " + dir.IndexOfFirstFile);
            tw.WriteLine("Until file: " + dir.IndexOfLastFile);
        }

        public void DumpFileEntry(RawFileDescriptor file, TextWriter tw, int num)
        {
            tw.WriteLine("<File " + num + ">");
            tw.WriteLine("Offset: " + string.Format("0x{0:X8}", file.ThisDescriptorOffest));
            tw.WriteLine("Name: " + file.Name);
            if (m_sFilepaths != null && num < m_sFilepaths.Length)
            {
                tw.WriteLine("Path: " + m_sFilepaths[num]);
            }
            tw.WriteLine("Name offset: " + string.Format("0x{0:X8}", file.ThisNameOffset));
            tw.WriteLine("Data offset: " + string.Format("0x{0:X8}", file.DataOffset));
            tw.WriteLine("Data compressed size: " + string.Format("0x{0:X8}", file.CompressedSize));
            tw.WriteLine("Data uncompressed size: " + string.Format("0x{0:X8}", file.UncompressedSize));
            tw.WriteLine("Flags: " + string.Format("0x{0:X8}", file.Flags));
            tw.WriteLine("Time stamp: " + DateTimeExt.GetFromUnixTimeStamp(file.UnixTimeStamp).ToProperString());
        }
    }
}