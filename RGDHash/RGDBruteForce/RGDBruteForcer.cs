using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using cope.StringExt;
using cope.Helper;
using RGDHash;

namespace RGDBruteForce
{
    class RGDBruteForcer
    {
        public List<string> found = new List<string>();
        List<uint> search;
        uint[] lengths;
        string values;
        RGDHasher hasher = new RGDHasher();

        public RGDBruteForcer(List<uint> search, string values, uint[] lengths)
        {
            this.search = new List<uint>(search);
            this.values = values;
            this.lengths = lengths;
        }

        public void Start()
        {
            if (lengths == null)
                return;
            for (int count = 0; count < lengths.Length; count++)
            {
                uint curl = lengths[count];
                if (curl == 0)
                    continue;
                StringBuilder strb = new StringBuilder((int)curl);
                strb.Append(values[0], (int)curl);
                uint hash = hasher.RGDHash(strb.ToString());
                if (search.Contains(hash))
                    found.Add("0x" + hash.ToString("X8") + "=" + strb.ToString());
                Vary(strb, 0);
            }
        }

        public void Vary(StringBuilder strb, int j)
        {
            if (j >= strb.Length)
                return;
            for (int k = 0; k < values.Length; k++)
            {
                StringBuilder tmp = new StringBuilder(strb.ToString(), strb.Capacity);
                tmp[j] = values[k];
                uint hash = hasher.RGDHash(tmp.ToString());
                if (search.Contains(hash))
                    found.Add("0x" + hash.ToString("X8") + "=" + tmp.ToString());
                Vary(tmp, j+1);
            }
        }
    }
}
