﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RGDHash;

namespace QuickHash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            tbxOut.Text = "0x" + RGDHashMachine.RGHHash(tbxIn.Text).ToString("X8");
        }
    }
}
