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
using Be.Windows.Forms;
using cope.DawnOfWar2.RelicChunky;

namespace ModTool.Core.PlugIns.RelicChunky
{
    public partial class StandardChunkHandler : BaseHandler
    {
        public StandardChunkHandler()
        {
            InitializeComponent();
        }

        #region methods

        public override void LoadChunk(RelicChunk rc)
        {
            m_relicChunk = rc;
            m_hbxRawData.ByteProvider = new DynamicByteProvider(m_relicChunk.RawData);
            m_hbxRawData.ByteProvider.ApplyChanges();
        }

        public override void CloseChunk()
        {
            m_hbxRawData.ByteProvider = null;
            m_relicChunk = null;
        }

        public override void SaveChunk()
        {
            if (m_hbxRawData.ByteProvider != null)
            {
                if (m_hbxRawData.ByteProvider.HasChanges())
                    m_hbxRawData.ByteProvider.ApplyChanges();
                m_relicChunk.RawData = ((DynamicByteProvider)m_hbxRawData.ByteProvider).Bytes.ToArray();
            }
            if (m_relicChunk != null)
                m_relicChunk.ChunkHeader.ChunkSize = (uint)m_relicChunk.RawData.Length;
        }

        #endregion methods

        #region properties

        public override bool HasChanges
        {
            get
            {
                if (m_hbxRawData.ByteProvider != null)
                    return m_hbxRawData.ByteProvider.HasChanges();
                return false;
            }
        }

        #endregion properties

        #region eventhandlers

        private void BtnSaveClick(object sender, EventArgs e)
        {
            SaveChunk();
        }

        #endregion eventhandlers
    }
}