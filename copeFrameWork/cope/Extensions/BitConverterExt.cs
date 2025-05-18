namespace cope.Extensions
{
    public class BitConverterExt
    {
        public static unsafe byte[] GetBytes(int[] values)
        {
            byte[] buffer = new byte[values.Length * 4];
            fixed (byte* numRef = buffer)
            {
                int byteIdx = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    *((int*) (numRef + byteIdx)) = values[i];
                    byteIdx += 4;
                }
            }
            return buffer;
        }
    }
}