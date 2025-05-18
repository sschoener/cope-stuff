using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using cope.StringExt;
using cope.Helper;

namespace RGDBruteForce
{
    public partial class RGDBFMain : Form
    {
        List<uint> unresolvedKeys = new List<uint>();
        RGDBruteForcer[] threads;
        Thread[] ts;

        public RGDBFMain()
        {
            InitializeComponent();
        }

        private void btnInputFile_Click(object sender, EventArgs e)
        {
            if (dlgSelectInputFile.ShowDialog() == DialogResult.OK)
            {
                tbxInput.Text = dlgSelectInputFile.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            lbxResults.Items.Clear();
            unresolvedKeys.Clear();
            FileStream dictFile = File.Open(tbxInput.Text, FileMode.Open);
            StreamReader rgdDict = new StreamReader(dictFile);
            while (!rgdDict.EndOfStream)
            {
                string s = rgdDict.ReadLine();
                string k = s.SubstringBeforeFirst('=');
                string value = s.SubstringAfterFirst('=');
                uint key = uint.Parse(k.SubstringAfterFirst('x'), System.Globalization.NumberStyles.HexNumber);
                if (value == "!")
                    unresolvedKeys.Add(key);
            }
            dictFile.Close();

            DateTime time1 = DateTime.Now;
            threads = new RGDBruteForcer[Environment.ProcessorCount - 1];
            ts = new Thread[Environment.ProcessorCount - 1];
            uint l = (uint)nudMaxLength.Value - (uint)nudMinLength.Value;
            uint maxl = (uint)nudMaxLength.Value;
            uint pt = (uint)Math.Ceiling(((double)l/(Environment.ProcessorCount - 1)));
            uint[][] pts = new uint[Environment.ProcessorCount - 1][];
            for (uint i = 0; i < pt; i++)
            {
                for (uint j = 0; j < Environment.ProcessorCount - 1; j++)
                {
                    if (pts[j] == null)
                    {
                        pts[j] = new uint[pt];
                    }
                    if (l == 0)
                        goto DISTRIDONE;
                    pts[j][i] = maxl--;
                }
            }
            DISTRIDONE:
            for (int i = 0; i < Environment.ProcessorCount - 1; i++)
            {
                threads[i] = new RGDBruteForcer(unresolvedKeys, (string)tbxValues.Text.Clone(), pts[i]);
                ts[i] = new Thread(threads[i].Start);
                ts[i].Start();
            }

            while (true)
            {
                bool br = true;
                for (int i = 0; i < Environment.ProcessorCount - 1; i++)
                {
                    br &= (ts[i].ThreadState == ThreadState.Stopped) ? true : false;
                }
                if (br)
                    break;
                Thread.Sleep(10);
            }
            DateTime time2 = DateTime.Now;

            for (int i = 0; i < threads.Length; i++)
            {
                foreach (string s in threads[i].found)
                {
                    if (!lbxResults.Items.Contains(s))
                        lbxResults.Items.Add(s);
                }
            }
            MessageBox.Show((time2 - time1).TotalSeconds.ToString() + " seconds");
        }

        private void btnDump_Click(object sender, EventArgs e)
        {
            if (dlgSaveDump.ShowDialog() == DialogResult.OK)
            {
                FileStream tmp = new FileStream(dlgSaveDump.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(tmp);
                sw.AutoFlush = true;
                foreach (string s in lbxResults.Items)
                {
                    sw.WriteLine(s);
                }
                tmp.Close();
            }
        }

        private void nudMaxLength_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxLength.Value <= nudMinLength.Value)
                nudMinLength.Value = nudMaxLength.Value - 1;
        }

        private void nudMinLength_ValueChanged(object sender, EventArgs e)
        {
            if (nudMaxLength.Value <= nudMinLength.Value)
                nudMaxLength.Value = nudMinLength.Value + 1;
        }
    }
}
