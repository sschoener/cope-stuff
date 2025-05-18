#region

using System;
using System.IO;
using System.Security.Cryptography;

#endregion

namespace cope
{
    public sealed class Crc32 : HashAlgorithm
    {
        public const UInt32 DEFAULT_POLYNOMIAL = 0xedb88320;
        public const UInt32 DEFAULT_SEED = 0xffffffff;
        private static UInt32[] s_defaultTable;

        private readonly UInt32 m_seed;
        private readonly UInt32[] m_table;
        private UInt32 m_hash;

        public Crc32()
        {
            m_table = InitializeTable(DEFAULT_POLYNOMIAL);
            m_seed = DEFAULT_SEED;
            Initialize();
        }

        public Crc32(UInt32 polynomial, UInt32 seed)
        {
            m_table = InitializeTable(polynomial);
            m_seed = seed;
            Initialize();
        }

        public override int HashSize
        {
            get { return 32; }
        }

        public override void Initialize()
        {
            m_hash = m_seed;
        }

        protected override void HashCore(byte[] buffer, int start, int length)
        {
            m_hash = CalculateHash(m_table, m_hash, buffer, start, length);
        }

        protected override byte[] HashFinal()
        {
            byte[] hashBuffer = UInt32ToBigEndianBytes(~m_hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        public static UInt32 Compute(Stream stream)
        {
            return ~CalculateHash(InitializeTable(DEFAULT_POLYNOMIAL), DEFAULT_SEED, stream, (int)stream.Length);
        }

        public static UInt32 Compute(byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DEFAULT_POLYNOMIAL), DEFAULT_SEED, buffer, 0, buffer.Length);
        }

        public static UInt32 Compute(UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DEFAULT_POLYNOMIAL), seed, buffer, 0, buffer.Length);
        }

        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        private static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if (polynomial == DEFAULT_POLYNOMIAL && s_defaultTable != null)
                return s_defaultTable;

            UInt32[] createTable = new UInt32[256];
            for (int i = 0; i < 256; i++)
            {
                UInt32 entry = (UInt32) i;
                for (int j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry = entry >> 1;
                createTable[i] = entry;
            }

            if (polynomial == DEFAULT_POLYNOMIAL)
                s_defaultTable = createTable;

            return createTable;
        }

        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, Stream stream, int length)
        {
            byte[] buffer = new byte[1024 * 1024];
            uint hash = seed;
            int toRead = length;
            long endPos = stream.Position + length;
            while (stream.Position < endPos)
            {
                int chunkSize = Math.Min(toRead, buffer.Length);
                int actualSize = stream.Read(buffer, 0, chunkSize);
                hash = CalculateHash(table, hash, buffer, 0, actualSize);
                toRead -= actualSize;
            }
            return hash;
        }

        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, byte[] buffer, int start, int size)
        {
            UInt32 crc = seed;
            for (int i = start; i < size; i++)
                unchecked
                {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            return crc;
        }

        private static byte[] UInt32ToBigEndianBytes(UInt32 x)
        {
            return new[]
                       {
                           (byte) ((x >> 24) & 0xff),
                           (byte) ((x >> 16) & 0xff),
                           (byte) ((x >> 8) & 0xff),
                           (byte) (x & 0xff)
                       };
        }
    }
}