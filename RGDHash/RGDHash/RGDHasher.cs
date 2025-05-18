using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cope.Helper;
using cope.StringExt;

namespace RGDHash
{
    public class RGDHasher
    {
        public RGDHasher()
        { }

        public uint RGDHash(string s)
        {
            return RGDHash(s.ToByteArray(true));
        }

        public uint RGDHash(byte[] data)
        {
            byte[] k = (byte[])data.Clone();
            uint a, b, c, len;
            len = (uint)data.Length;
            a = b = 0x9e3779b9;
            c = 0;

            while (len >= 12)
            {
                a += (k[0] + ((uint)k[1] << 8) + ((uint)k[2] << 16) + ((uint)k[3] << 24));
                b += (k[4] + ((uint)k[5] << 8) + ((uint)k[6] << 16) + ((uint)k[7] << 24));
                c += (k[8] + ((uint)k[9] << 8) + ((uint)k[10] << 16) + ((uint)k[11] << 24));

                Mix(ref a, ref b, ref c);

                k = k.GetBytes(12, k.Length - 12);
                len -= 12;
            }
            c += (uint)data.Length;
            if (len == 11)
            {
                c += ((uint)k[10] << 24);
                len--;
            }
            if (len == 10)
            {
                c += ((uint)k[9] << 16);
                len--;
            }
            if (len == 9)
            {
                c += ((uint)k[8] << 8);
                len--;
            }
            if (len == 8)
            {
                b += ((uint)k[7] << 24);
                len--;
            }
            if (len == 7)
            {
                b += ((uint)k[6] << 16);
                len--;
            }
            if (len == 6)
            {
                b += ((uint)k[5] << 8);
                len--;
            }
            if (len == 5)
            {
                b += k[4];
                len--;
            }
            if (len == 4)
            {
                a += ((uint)k[3] << 24);
                len--;
            }
            if (len == 3)
            {
                a += ((uint)k[2] << 16);
                len--;
            }
            if (len == 2)
            {
                a += ((uint)k[1] << 8);
                len--;
            }
            if (len == 1)
            {
                a += k[0];
                len--;
            }
            Mix(ref a, ref b, ref c);
            return c;
        }

        private void Mix(ref uint a, ref uint b, ref uint c)
        {
            a -= b; a -= c; a ^= (c >> 13);
            b -= c; b -= a; b ^= (a << 8);
            c -= a; c -= b; c ^= (b >> 13);
            a -= b; a -= c; a ^= (c >> 12);
            b -= c; b -= a; b ^= (a << 16);
            c -= a; c -= b; c ^= (b >> 5);
            a -= b; a -= c; a ^= (c >> 3);
            b -= c; b -= a; b ^= (a << 10);
            c -= a; c -= b; c ^= (b >> 15);
        }
    }

    public static class RGDHashMachine
    {
        public static uint RGHHash(string s)
        {
            return RGDHash(s.ToByteArray(true));
        }

        public static uint RGDHash(byte[] data)
        {
            byte[] k = (byte[])data.Clone();
            uint a, b, c, len;
            len = (uint)data.Length;
            a = b = 0x9e3779b9;
            c = 0;

            while (len >= 12)
            {
                a += (k[0] + ((uint)k[1] << 8) + ((uint)k[2] << 16) + ((uint)k[3] << 24));
                b += (k[4] + ((uint)k[5] << 8) + ((uint)k[6] << 16) + ((uint)k[7] << 24));
                c += (k[8] + ((uint)k[9] << 8) + ((uint)k[10] << 16) + ((uint)k[11] << 24));

                Mix(ref a, ref b, ref c);

                k = k.GetBytes(12, k.Length - 12);
                len -= 12;
            }
            c += (uint)data.Length;
            if (len == 11)
            {
                c += ((uint)k[10] << 24);
                len--;
            }
            if (len == 10)
            {
                c += ((uint)k[9] << 16);
                len--;
            }
            if (len == 9)
            {
                c += ((uint)k[8] << 8);
                len--;
            }
            if (len == 8)
            {
                b += ((uint)k[7] << 24);
                len--;
            }
            if (len == 7)
            {
                b += ((uint)k[6] << 16);
                len--;
            }
            if (len == 6)
            {
                b += ((uint)k[5] << 8);
                len--;
            }
            if (len == 5)
            {
                b+=k[4];
                len--;
            }
            if (len == 4)
            {
                a += ((uint)k[3] << 24);
                len--;
            }
            if (len == 3)
            {
                a += ((uint)k[2] << 16);
                len--;
            }
            if (len == 2)
            {
                a += ((uint)k[1] << 8);
                len--;
            }
            if (len == 1)
            {
                a += k[0];
                len--;
            }
            Mix(ref a, ref b, ref c);
            return c;
        }

        private static void Mix(ref uint a, ref uint b, ref uint c)
        {
              a -= b; a -= c; a ^= (c>>13);
              b -= c; b -= a; b ^= (a<<8);
              c -= a; c -= b; c ^= (b>>13);
              a -= b; a -= c; a ^= (c>>12); 
              b -= c; b -= a; b ^= (a<<16);
              c -= a; c -= b; c ^= (b>>5);
              a -= b; a -= c; a ^= (c>>3); 
              b -= c; b -= a; b ^= (a<<10);
              c -= a; c -= b; c ^= (b>>15);
        }
    }
}