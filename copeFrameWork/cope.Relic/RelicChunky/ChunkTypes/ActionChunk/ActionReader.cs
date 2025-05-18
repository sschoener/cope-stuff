#region

using System.Collections.Generic;
using System.IO;

#endregion

/*
 * FORMAT INFO:
 * 
 * 0x04 uint - action Count
 * Rest: Actions
 *
 * ACTNAction:
 * 0x04 uint - Length of action name
 * 0x^^ ascii string - action name
 * 0x04 uint - Number of Key-Value entries in action
 * Rest: KV-Entries
 * 0x04 float - delay of the action
 *
 * KV-Entry:
 * 0x04 uint Length of Key
 * 0x^^ ascii string - Key
 * 0x04 uint Length of Value
 * 0x^^ ascii string - Value
 * 
 */

namespace cope.Relic.RelicChunky.ChunkTypes.ActionChunk
{
    /// <summary>
    /// Helper class for reading Actions from DataChunks.
    /// </summary>
    public static class ActionReader
    {
        /// <summary>
        /// Reads all available actions from a stream. The actions are expected to be binary-encoded.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<Action> Read(Stream str)
        {
            var br = new BinaryReader(str);
            var actions = new List<Action>();
            uint numOfTables = br.ReadUInt32();
            for (uint i = 0; i < numOfTables; i++)
            {
                var action = ReadAction(br);
                actions.Add(action);
            }
            return actions;
        }

        /// <summary>
        /// Reads an action from the given BinaryReader. The action is expected to be binary-encoded.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        private static Action ReadAction(BinaryReader br)
        {
            int nameLength = (int) br.ReadUInt32();
            string name = br.ReadBytes(nameLength).ToString(true);
            int entryCount = (int) br.ReadUInt32();
            var parameters = new Dictionary<string, string>(entryCount);
            for (uint i = 0; i < entryCount; i++)
            {
                nameLength = (int) br.ReadUInt32();
                string tmpName = (nameLength == 0) ? string.Empty : br.ReadBytes(nameLength).ToString(true);
                uint vLength = br.ReadUInt32();
                string tmpValue = (vLength == 0) ? string.Empty : br.ReadBytes((int) vLength).ToString(true);
                parameters.Add(tmpName, tmpValue);
            }
            float delay = br.ReadSingle();
            return new Action(name, delay, parameters);
        }
    }
}