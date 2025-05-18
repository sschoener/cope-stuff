namespace cope
{
    /// <summary>
    /// Implementation of Marsaglia's Random Number Generator
    /// </summary>
    public class MarsagliaRng
    {
        private uint m_z;
        private uint m_w;

        /// <summary>
        /// Creates a new instance of MarsagliaRng using the specified values as seeds.
        /// </summary>
        /// <param name="seed1">The first seed. If not specified, 0xDEADBEEF is used.</param>
        /// <param name="seed2">The second seed. If not specified 0xCAFE1234 is used.</param>
        public MarsagliaRng(uint seed1 = 0xDEADBEEF, uint seed2 = 0xCAFE1234)
        {
            m_z = seed1;
            if (seed1 == 0)
                m_z = 0xDEADBEEF;
            m_w = seed2;
            if (seed2 == 0)
                m_w = 0xCAFE1234;
        }

        /// <summary>
        /// Returns a random unsigned integer.
        /// </summary>
        /// <returns></returns>
        public uint GetUint()
        {
            m_z = 36969 * (m_z & 65535) + (m_z >> 16);
            m_w = 18000 * (m_w & 65535) + (m_w >> 16);
            return (m_z << 16) + m_w;
        }

        /// <summary>
        /// Returns a random unsigned integer in the range [lower,upper).
        /// </summary>
        /// <param name="lower">The lower (inclusive) limit for the random value.</param>
        /// <param name="upper">The upper (exclusive) limit for the random value.</param>
        /// <returns></returns>
        public uint GetUint(uint lower, uint upper)
        {
            return lower + GetUint() % (upper - lower);
        }

        /// <summary>
        /// Returns a random signed integer.
        /// </summary>
        /// <returns></returns>
        public int GetInt()
        {
            return unchecked((int) GetUint());
        }

        /// <summary>
        /// Returns a random signed integer in the range [lower,upper).
        /// </summary>
        /// <param name="lower">The lower (inclusive) limit for the random value.</param>
        /// <param name="upper">The upper (exclusive) limit for the random value.</param>
        /// <returns></returns>
        public int GetInt(int lower, int upper)
        {
            int k = GetInt();
            if (k > 0)
                return lower + k % (upper - lower);
            return lower + (-k) % (upper - lower);
        }
    }
}
