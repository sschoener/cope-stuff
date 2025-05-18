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
using System.Windows.Forms;
using cope.DawnOfWar2.RelicChunky;

namespace ModTool.Core.PlugIns.RelicChunky
{
    public partial class ChunkyHeaderInfo : UserControl
    {
        RelicChunk m_rc;

        public ChunkyHeaderInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the information of a RelicChunk.
        /// </summary>
        /// <param name="rc"></param>
        public void LoadChunk(RelicChunk rc)
        {
            m_rc = rc;
            m_tbxChunkFlags.Text = rc.ChunkHeader.Flags.ToString();
            m_tbxChunkID.Text = rc.ChunkHeader.Signature;
            m_tbxChunkMinVersion.Text = rc.ChunkHeader.MinVersion.ToString();
            m_tbxChunkName.Text = rc.ChunkHeader.Name;
            m_tbxChunkSize.Text = rc.ChunkHeader.ChunkSize.ToString();
            m_tbxChunkVersion.Text = rc.ChunkHeader.Version.ToString();
            m_tbxChunkType.Text = rc.ChunkHeader.Type == ChunkType.DATA ? "DATA" : "FOLD";
        }

        /// <summary>
        /// Clears the textboxes.
        /// </summary>
        public void Clear()
        {
            m_rc = null;
            m_tbxChunkFlags.Text = string.Empty;
            m_tbxChunkID.Text = string.Empty;
            m_tbxChunkMinVersion.Text = string.Empty;
            m_tbxChunkName.Text = string.Empty;
            m_tbxChunkSize.Text = string.Empty;
            m_tbxChunkType.Text = string.Empty;
            m_tbxChunkVersion.Text = string.Empty;
        }

        private void TbxChunkNameTextChanged(object sender, EventArgs e)
        {
            m_rc.ChunkHeader.Name = m_tbxChunkName.Text;
        }
    }
}