using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using cope.Extensions;
using cope.FileSystem;
using cope.Relic.RelicChunky;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;

namespace RGDHashCrawler
{
	class Program
	{
		static void Main (string[] args)
		{
			if (args.Length == 0) {
				Console.WriteLine ("Invalid number of arguments!");
				Console.WriteLine ("Usage: rgdHashCrawl.exe <path_to_crawl>");
				return;
			}
			string input = Path.GetFullPath (args [0]);
			if (!Directory.Exists (input)) {
				Console.WriteLine ("Specified directory does not exist!");
				return;
			}
			var fs = new LocalFileSystem (input);
			FileWalker fw = new FileWalker (fs.GetRoot ());
			RGDDictionary rgdDict = new RGDDictionary (new Dictionary<ulong, string> ());
			foreach (var file in fw) {
				string path = file.GetPath ();
				if (!path.EndsWith (".rgd"))
					continue;
				Stream fileStream = null;
				try {
					fileStream =
						file.Open (new FileOpenOptions
									  {Access = FileAccess.Read, Mode = FileMode.Open, Share = FileShare.Read});
					var chunks = ChunkyFileReader.Read (fileStream);
					DataChunk chunk = (DataChunk)chunks [0];
					MemoryStream ms = new MemoryStream (chunk.GetData ());
					var data = RGDReader.Read (ms, rgdDict);
					ms.Close ();
				} catch (Exception ex) {
					Console.WriteLine ("Error while working on file " + file.GetPath ());
					Console.WriteLine (ex.GetInfo ().Collapse ());
				} finally {
					if (fileStream != null)
						fileStream.Close ();
				}
			}
		    var hashes = rgdDict.UnknownHashes.ToList();
		    hashes.Sort();
		    File.WriteAllLines("unknown_hashes.txt", hashes.Select(x => "0x" + x.ToString("X8")), Encoding.ASCII);
		}
	}
}
