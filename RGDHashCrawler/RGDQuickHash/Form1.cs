using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cope.Relic.RelicChunky.ChunkTypes.GameDataChunk;

namespace RGDQuickHash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            tbxInputText.Text = "0x" + RGDHasher.ComputeHash(tbxHash.Text).ToString("X8");
        }
    }
}
