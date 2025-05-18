/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System;
using System.IO;
using cope.Extensions;
using cope.DawnOfWar2;

namespace ModTool.Core.PlugIns.Text
{
    public partial class TextEditor : FileTool//UserControl
    {
        #region fields

        readonly UniFile m_file;

        #endregion fields

        #region ctors

        public TextEditor()
        {
            InitializeComponent();
        }

        public TextEditor(UniFile file)
            : this()
        {
            m_file = file;
            var r = new StreamReader(file.Stream, true);
            m_rtbText.Text = r.ReadToEnd();
            r.Close();
            m_rtbText.TextChanged += RtbTextTextChanged;
        }

        #endregion ctors

        #region methods

        public override void SaveFile()
        {
            string dir = m_file.FilePath.SubstringBeforeLast('\\');
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            FileStream fs = System.IO.File.Create(m_file.FilePath);
            var w = new StreamWriter(fs);
            w.Write(m_rtbText.Text);
            w.Close();
            fs.Close();
            HasChanges = false;
        }

        public override bool Close()
        {
            if (m_file != null)
                m_file.Close();
            return true;
        }

        #endregion methods

        #region properties

        public override UniFile File
        {
            get
            {
                return m_file;
            }
        }

        #endregion properties

        #region eventhandler

        private void RtbTextTextChanged(object sender, EventArgs e)
        {
            HasChanges = true;
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        #endregion eventhandler
    }
}