using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cope.Extensions;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;

namespace RGDHashBruteforce
{
	class Program
	{
		public static void Main (string[] args)
		{
			if (args.Length < 3) {
				Console.Error.WriteLine("cope's RGD Hash Bruteforcer v1.11 - 12/19/12");
				Console.Error.WriteLine("Invalid number of arguments");
				Console.Error.WriteLine("Usage: rgdHashBrute.exe <unknown_hashes> <attack_words> <dict_to_update>");
				Console.Error.WriteLine("Whereby the hashes file contains one hash per line" +
										" and the attack dictionary one word for the brute force per line.");
				return;
			}
			string hashesFile = Path.GetFullPath(args[0]);
			string dictFile = Path.GetFullPath(args[1]);
			string hashDict = Path.GetFullPath(args[2]);
			if (!File.Exists (hashesFile)) {
				Console.Error.WriteLine("Specified hash file does not exist.");
				return;
			}

			if (!File.Exists (dictFile)) {
				Console.Error.WriteLine("Specified dictionary file does not exist");
				return;
			}

			string[] dict = null;
			try {
				dict = File.ReadAllLines (dictFile);
			} catch (Exception ex) {
				Console.Error.WriteLine("Failed to read dictionary file.");
				Console.Error.WriteLine(ex.GetInfo().Collapse());
				return;
			}

			string[] hashStrings = null;
			try {
				hashStrings = File.ReadAllLines (hashesFile);
			} catch (Exception ex) {
				Console.Error.WriteLine("Failed to read hash file.");
				Console.Error.WriteLine(ex.GetInfo().Collapse());
				return;
			}
			uint[] hashes = hashStrings.Select (s => Convert.ToUInt32(s.SubstringBeforeFirst('#').Trim(), 16)).ToArray();
			var results = BruteForce(dict, hashes);

			if (results.Count > 0)
			{
				int newKeys = UpdateDictionary(results, hashDict);
				if (newKeys > 0)
					Console.WriteLine("Found " + newKeys + " new hashes!");
				else
					Console.WriteLine("No new hashes have been found.");
			}
		}

		static Dictionary<uint,string> BruteForce (IEnumerable<string> toHash, uint[] codes)
		{
			var dict = new Dictionary<uint,string>();
			foreach (string s in toHash.AsParallel())
			{
				uint hash = RGDHasher.ComputeHash(s);
				foreach (uint t in codes)
				{
					if (hash == t) {
						if (!dict.ContainsKey(t))
							dict.Add(t, s);
						break;
					}
				}
			}
			return dict;
		}

		static int UpdateDictionary(Dictionary<uint,string> results, string hashDictPath)
		{
			// update RGD dictionary
			RGDDictionary rgdDict;
			if (!File.Exists(hashDictPath))
			{
				string dir = hashDictPath.SubstringBeforeLast('\\');
				try
				{
					Directory.CreateDirectory(dir);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Failed to create directory holding the new hash directory.");
					Console.Error.WriteLine(ex.GetInfo().Collapse());
					return -1;
				}
				rgdDict = new RGDDictionary(new Dictionary<ulong, string>());
			}
			else
			{
				TextReader tr = null;
				try
				{
					tr = File.OpenText(hashDictPath);
					rgdDict = new RGDDictionary(RGDDictionaryReader.Read(tr));
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Failed to open hash dictionary!");
					Console.Error.WriteLine(ex.GetInfo().Collapse());
					return -1;
				}
				finally
				{
					if (tr != null)
						tr.Close();
				}
			}
			int before = rgdDict.NumHashes;
			foreach (var kvp in results)
				rgdDict.KeyToHash(kvp.Value);

			FileStream str = null;
			try
			{
				str = File.Open(hashDictPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
				StreamWriter sw = new StreamWriter(str);
				RGDDictionaryWriter.Write(sw, rgdDict);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Failed to write RGD dictionary!");
				Console.Error.WriteLine(ex.GetInfo().Collapse());
				return -1;
			}
			finally
			{
				if (str != null)
					str.Close();
			}
			return rgdDict.NumHashes - before;
		}
	}
}
