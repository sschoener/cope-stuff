using System.IO;
using cope.Extensions;

namespace cope.Relic
{
    /// <summary>
    /// Helper class to read Company of Heroes Save Game files, which consist of some meta-info, a picture and a RelicChunky.
    /// </summary>
    public static class CoHSaveGameReader
    {
        private const string IDENTIFIER = "RGMH";

        /// <summary>
        /// Reads a CoH savegame from a stream.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="RelicException"><c>RelicException</c>.</exception>
        public static CoHSaveGame Read(Stream str)
        {
            BinaryReader br = new BinaryReader(str);
            // read header
            string identifier = br.ReadAsciiString(4);
            if (identifier != IDENTIFIER)
                throw new RelicException("The input is not a CoHSaveGame, invalid identifier: " + identifier);
            br.ReadUInt32(); // always 0x1
            uint jpegOffset = br.ReadUInt32();
            br.ReadUInt32();
            br.ReadUInt32(); // both unknown
            int jpegSize = (int) br.ReadUInt32();
            string guid = br.ReadBytes(0x10).ToHexString(false);
            string gameName = br.ReadBytes(0x800).ToString(false).SubstringBeforeFirst('\0');
            string saveName = br.ReadBytes(0x800).ToString(false).SubstringBeforeFirst('\0');
            string mapName = br.ReadBytes(0x800).ToString(false).SubstringBeforeFirst('\0');
            string mapDesc = br.ReadBytes(0x800).ToString(false).SubstringBeforeFirst('\0');
            byte[] jpeg = br.ReadBytes(jpegSize);
            int bytesLeft = (int) (str.Length - str.Position);
            byte[] chunkyData = br.ReadBytes(bytesLeft);
            return new CoHSaveGame(gameName, mapName, mapDesc, saveName, guid, jpeg, chunkyData);
        }
    }

    /// <summary>
    /// Container class which provides easy access to the various parts of a Company of Heroes save game.
    /// </summary>
    public sealed class CoHSaveGame
    {
        private readonly byte[] m_jpegData;
        private readonly byte[] m_chunkyData;

        internal CoHSaveGame(string game, string map, string mapDesc, string name, string guid, byte[] jpeg, byte[] chunky)
        {
            GameName = game;
            MapName = map;
            MapDescription = mapDesc;
            SaveName = name;
            Guid = guid;
            m_jpegData = new byte[jpeg.Length];
            jpeg.CopyTo(m_jpegData, 0);

            m_chunkyData = new byte[chunky.Length];
            chunky.CopyTo(m_chunkyData, 0);
            
        }

        #region properties
        
        /// <summary>
        /// Gets the name of the game. Should normally be 'Company of Heroes'.
        /// </summary>
        public string GameName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the name of the map this saved game is set on.
        /// </summary>
        public string MapName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the description of the map this saved game is set on.
        /// </summary>
        public string MapDescription
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the name of the saved game.
        /// </summary>
        public string SaveName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns a GUID string which identifies the save game.
        /// </summary>
        public string Guid
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns a copy of the data of the JPEG contained by the savegame.
        /// </summary>
        /// <returns></returns>
        public byte[] GetJpegData()
        {
            return m_jpegData;
        }

        /// <summary>
        /// Returns a copy of the data of the RelicChunky contained by the savegame.
        /// </summary>
        /// <returns></returns>
        public byte[] GetChunkyData()
        {
            return m_chunkyData;
        }

        #endregion
    }
}
