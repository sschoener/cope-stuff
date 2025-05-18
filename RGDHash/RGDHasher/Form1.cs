using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using cope.Helper;
using cope.StringExt;
using RGDHash;

namespace RGDHasher
{
    public partial class Form1 : Form
    {
        Dictionary<uint, string> keys = new Dictionary<uint,string>();
        List<uint> unresolvedKeys = new List<uint>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnString_Click(object sender, EventArgs e)
        {
            dlgOpenFile.FileName = "";
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                tbxStringFile.Text = dlgOpenFile.FileName;
            }
        }

        private void btnDict_Click(object sender, EventArgs e)
        {
            dlgOpenFile.FileName = "";
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                tbxDictionary.Text = dlgOpenFile.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // read dictionary
            keys.Clear();
            unresolvedKeys.Clear();
            FileStream dictFile = File.Open(tbxDictionary.Text, FileMode.Open);
            StreamReader rgdDict = new StreamReader(dictFile);
            while (!rgdDict.EndOfStream)
            {
                string s = rgdDict.ReadLine();
                string k = s.SubstringBeforeFirst('=');
                string value = s.SubstringAfterFirst('=');
                uint key = uint.Parse(k.SubstringAfterFirst('x'), System.Globalization.NumberStyles.HexNumber);
                if (value == "!")
                    unresolvedKeys.Add(key);
                keys.Add(key, value);
            }

            int count = unresolvedKeys.Count;

            // read strings
            string[] candidates = File.ReadAllLines(tbxStringFile.Text);
            foreach (string s in candidates)
            {
                uint hash = RGDHashMachine.RGHHash(s);
                if (unresolvedKeys.Contains(hash))
                {
                    unresolvedKeys.Remove(hash);
                    keys[hash] = s;
                }
            }

            count -= unresolvedKeys.Count;

            // write dictionary
            StreamWriter rgdDictW = new StreamWriter(dictFile);
            rgdDictW.AutoFlush = true;
            rgdDictW.BaseStream.Position = 0;
            rgdDictW.WriteLine("#RGD_DIC");
            rgdDictW.WriteLine("# created by cope's RGD brute force");
            foreach (KeyValuePair<uint, string> kvp in keys)
            {
                if (kvp.Value == "!")
                    rgdDictW.WriteLine("# 0x" + kvp.Key.ToString("X8") + " is an unknown value!!");
                else
                    rgdDictW.WriteLine("0x" + kvp.Key.ToString("X8") + "=" + kvp.Value);
            }

            dictFile.Close();
            MessageBox.Show(count.ToString());
        }
    }
}
