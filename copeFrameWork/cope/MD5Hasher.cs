#region

using cope.Extensions;

#endregion

namespace cope.Helper
{
    public class MD5Hash
    {
        private readonly uint[] m_bits = new uint[2];
        private readonly uint[] m_buffer = new uint[4];
        private readonly byte[] m_input = new byte[64];

        public MD5Hash()
        {
            m_buffer[0] = 0x67452301;
            m_buffer[1] = 0xefcdab89;
            m_buffer[2] = 0x98badcfe;
            m_buffer[3] = 0x10325476;
            m_bits[0] = 0;
            m_bits[1] = 0;
        }

        public void Update(string s)
        {
            Update(s.ToByteArray(true));
        }

        public void Update(byte[] buffer)
        {
            // update bit count
            uint t = m_bits[0];
            m_bits[0] = (uint) (t + buffer.Length * 8);
            if (m_bits[0] < t)
                m_bits[1]++;
            m_bits[1] += ((uint) buffer.Length) >> 29;
            t = (t / 8) & 0x3F; // & 0x3F is equivalent to % 64

            // handle any leading odd-sized chunks
            int currentIndex = 0;
            if (t != 0)
            {
                int startIndex = (int) t;
                t = 64 - t;
                if (buffer.Length < t)
                {
                    m_input.SetValues(startIndex, buffer);
                    return;
                }
                m_input.SetValues(startIndex, buffer, 0, (int) t);
                Transform();
                currentIndex += (int) t;
            }

            // process data in 64-byte chunks as required by MD5
            while (buffer.Length - currentIndex >= 64)
            {
                m_input.SetValues(0, buffer, currentIndex, 64);
                Transform();
                currentIndex += 64;
            }
            // handle any remaining bytes of data
            m_input.SetValues(0, buffer, currentIndex, buffer.Length - currentIndex);
        }

        public byte[] FinalizeHash()
        {
            // Compute number of bytes mod 64
            uint count = (m_bits[0] / 8) & 0x3F;
            int currentIndex = (int) count;

            // Set the first char of padding to 0x80. This is safe since there is always at least one byte free
            m_input[currentIndex] = 0x80;
            currentIndex++;

            // num padding bytes required to get to 64 bytes
            count = 64 - 1 - count;

            if (count < 8)
            {
                m_input.Clear(currentIndex, (int) count);
                Transform();
                m_input.Clear(0, 56);
            }
            else
                m_input.Clear(currentIndex, (int) count - 8);

            m_input.SetValues(56, m_bits[0].ToByteArray());
            m_input.SetValues(60, m_bits[1].ToByteArray());
            Transform();

            byte[] hashValue = new byte[16];
            hashValue.SetValues(0, m_buffer[0].ToByteArray());
            hashValue.SetValues(4, m_buffer[1].ToByteArray());
            hashValue.SetValues(8, m_buffer[2].ToByteArray());
            hashValue.SetValues(12, m_buffer[3].ToByteArray());

            m_buffer[0] = 0x67452301;
            m_buffer[1] = 0xefcdab89;
            m_buffer[2] = 0x98badcfe;
            m_buffer[3] = 0x10325476;
            m_bits[0] = 0;
            m_bits[1] = 0;

            return hashValue;
        }

        private void Transform()
        {
            // split the block into 16 parts á 512 bits
            var words = new uint[16];
            for (int i = 0; i < 16; i++)
                words[i] = m_input.GetValues(i * 4, 4).ToUInt32();

            uint a = m_buffer[0];
            uint b = m_buffer[1];
            uint c = m_buffer[2];
            uint d = m_buffer[3];
            a = Ff(a, b, c, d, words[0], 7, 0xD76AA478);
            d = Ff(d, a, b, c, words[1], 12, 0xE8C7B756);
            c = Ff(c, d, a, b, words[2], 17, 0x242070DB);
            b = Ff(b, c, d, a, words[3], 22, 0xC1BDCEEE);
            a = Ff(a, b, c, d, words[4], 7, 0xF57C0FAF);
            d = Ff(d, a, b, c, words[5], 12, 0x4787C62A);
            c = Ff(c, d, a, b, words[6], 17, 0xA8304613);
            b = Ff(b, c, d, a, words[7], 22, 0xFD469501);
            a = Ff(a, b, c, d, words[8], 7, 0x698098D8);
            d = Ff(d, a, b, c, words[9], 12, 0x8B44F7AF);
            c = Ff(c, d, a, b, words[10], 17, 0xFfFf5BB1);
            b = Ff(b, c, d, a, words[11], 22, 0x895CD7BE);
            a = Ff(a, b, c, d, words[12], 7, 0x6B901122);
            d = Ff(d, a, b, c, words[13], 12, 0xFD987193);
            c = Ff(c, d, a, b, words[14], 17, 0xA679438E);
            b = Ff(b, c, d, a, words[15], 22, 0x49B40821);

            a = Gg(a, b, c, d, words[1], 5, 0xF61E2562);
            d = Gg(d, a, b, c, words[6], 9, 0xC040B340);
            c = Gg(c, d, a, b, words[11], 14, 0x265E5A51);
            b = Gg(b, c, d, a, words[0], 20, 0xE9B6C7AA);
            a = Gg(a, b, c, d, words[5], 5, 0xD62F105D);
            d = Gg(d, a, b, c, words[10], 9, 0x02441453);
            c = Gg(c, d, a, b, words[15], 14, 0xD8A1E681);
            b = Gg(b, c, d, a, words[4], 20, 0xE7D3FBC8);
            a = Gg(a, b, c, d, words[9], 5, 0x21E1CDE6);
            d = Gg(d, a, b, c, words[14], 9, 0xC33707D6);
            c = Gg(c, d, a, b, words[3], 14, 0xF4D50D87);
            b = Gg(b, c, d, a, words[8], 20, 0x455A14ED);
            a = Gg(a, b, c, d, words[13], 5, 0xA9E3E905);
            d = Gg(d, a, b, c, words[2], 9, 0xFCEFA3F8);
            c = Gg(c, d, a, b, words[7], 14, 0x676F02D9);
            b = Gg(b, c, d, a, words[12], 20, 0x8D2A4C8A);

            a = Hh(a, b, c, d, words[5], 4, 0xFfFA3942);
            d = Hh(d, a, b, c, words[8], 11, 0x8771F681);
            c = Hh(c, d, a, b, words[11], 16, 0x6D9D6122);
            b = Hh(b, c, d, a, words[14], 23, 0xFDE5380C);
            a = Hh(a, b, c, d, words[1], 4, 0xA4BEEA44);
            d = Hh(d, a, b, c, words[4], 11, 0x4BDECFA9);
            c = Hh(c, d, a, b, words[7], 16, 0xF6BB4B60);
            b = Hh(b, c, d, a, words[10], 23, 0xBEBFBC70);
            a = Hh(a, b, c, d, words[13], 4, 0x289B7EC6);
            d = Hh(d, a, b, c, words[0], 11, 0xEAA127FA);
            c = Hh(c, d, a, b, words[3], 16, 0xD4EF3085);
            b = Hh(b, c, d, a, words[6], 23, 0x04881D05);
            a = Hh(a, b, c, d, words[9], 4, 0xD9D4D039);
            d = Hh(d, a, b, c, words[12], 11, 0xE6DB99E5);
            c = Hh(c, d, a, b, words[15], 16, 0x1FA27CF8);
            b = Hh(b, c, d, a, words[2], 23, 0xC4AC5665);

            a = Ii(a, b, c, d, words[0], 6, 0xF4292244);
            d = Ii(d, a, b, c, words[7], 10, 0x432AFf97);
            c = Ii(c, d, a, b, words[14], 15, 0xAB9423A7);
            b = Ii(b, c, d, a, words[5], 21, 0xFC93A039);
            a = Ii(a, b, c, d, words[12], 6, 0x655B59C3);
            d = Ii(d, a, b, c, words[3], 10, 0x8F0CCC92);
            c = Ii(c, d, a, b, words[10], 15, 0xFfEFf47D);
            b = Ii(b, c, d, a, words[1], 21, 0x85845DD1);
            a = Ii(a, b, c, d, words[8], 6, 0x6FA87E4F);
            d = Ii(d, a, b, c, words[15], 10, 0xFE2CE6E0);
            c = Ii(c, d, a, b, words[6], 15, 0xA3014314);
            b = Ii(b, c, d, a, words[13], 21, 0x4E0811A1);
            a = Ii(a, b, c, d, words[4], 6, 0xF7537E82);
            d = Ii(d, a, b, c, words[11], 10, 0xBD3AF235);
            c = Ii(c, d, a, b, words[2], 15, 0x2AD7D2BB);
            b = Ii(b, c, d, a, words[9], 21, 0xEB86D391);

            m_buffer[0] += a;
            m_buffer[1] += b;
            m_buffer[2] += c;
            m_buffer[3] += d;
        }

        #region MD5 functions

        private static uint F(uint x, uint y, uint z)
        {
            return (x & y) | ((~x) & z);
        }

        private static uint G(uint x, uint y, uint z)
        {
            return (x & z) | (y & (~z));
        }

        private static uint H(uint x, uint y, uint z)
        {
            return z ^ (x ^ y);
        }

        private static uint I(uint x, uint y, uint z)
        {
            return y ^ (x | (~z));
        }

        private static uint Ff(uint a, uint b, uint c, uint d, uint w, uint s, uint ac)
        {
            a += F(b, c, d) + w + ac;
            a = a.RotateLeft((int) s);
            a += b;
            return a;
        }

        private static uint Gg(uint a, uint b, uint c, uint d, uint w, uint s, uint ac)
        {
            a += G(b, c, d) + w + ac;
            a = a.RotateLeft((int) s);
            a += b;
            return a;
        }

        private static uint Hh(uint a, uint b, uint c, uint d, uint w, uint s, uint ac)
        {
            a += H(b, c, d) + w + ac;
            a = a.RotateLeft((int) s);
            a += b;
            return a;
        }

        private static uint Ii(uint a, uint b, uint c, uint d, uint w, uint s, uint ac)
        {
            a += I(b, c, d) + w + ac;
            a = a.RotateLeft((int) s);
            a += b;
            return a;
        }

        #endregion
    }
}