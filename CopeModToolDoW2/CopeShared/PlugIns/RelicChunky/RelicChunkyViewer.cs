using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using cope;
using cope.DawnOfWar2.RelicChunky;
using cope.DawnOfWar2.RelicChunky.Chunks;
using cope.DawnOfWar2;

namespace ModTool.Core.PlugIns.RelicChunky
{
    // Todo: rewrite the code, totally outdated (yet still working)
    public partial class RelicChunkyViewer : FileTool
    {
        #region fields

        protected static RelicChunk s_clipboard;

        protected RelicChunkyFile m_rcf;
        protected Dictionary<RelicChunk, BaseHandler> m_chunkHandler;
        protected bool m_bHasChanges;

        #endregion fields

        #region ctors

        public RelicChunkyViewer()
        {
            InitializeComponent();
            m_chunkHandler = new Dictionary<RelicChunk, BaseHandler>();
        }


        /// <exception cref="CopeException">Can't open the file as RelicChunky!</exception>
        public RelicChunkyViewer(UniFile file)
            : this()
        {
            var rcf = new RelicChunkyFile(file);
            try
            {
                rcf.ReadData();
            }
            catch (Exception ex)
            {
                throw new CopeException(ex, "Can't open the file as RelicChunky!");
            }
            file.Close();

            m_rcf = rcf;
            _trv_chunks.Nodes.Clear();
            foreach (RelicChunk chunk in m_rcf.Chunks)
            {
                var tnC = new TreeNode();
                string tnName = chunk.ChunkHeader.TypeString + chunk.ChunkHeader.Signature;
                tnC.Name = tnName;
                tnC.Text = tnName;
                chunk.Tag = tnC;
                tnC.Tag = chunk;

                if (chunk is FoldChunk)
                    GetChunks(tnC, (FoldChunk)chunk);
                _trv_chunks.Nodes.Add(tnC);
            }
        }

        #endregion ctors

        #region eventhandlers

        #region buttons

        private void BtnSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void BtnCloseAllChunksClick(object sender, EventArgs e)
        {
            foreach (TabPage tp in _tbc_chunkHandlers.TabPages)
            {
                CloseHandler(((BaseHandler)tp.Tag));
                tp.Dispose();
            }
            _tbc_chunkHandlers.TabPages.Clear();
        }

        private void BtnCloseChunkClick(object sender, EventArgs e)
        {
            if (_tbc_chunkHandlers.SelectedTab == null)
                return;
            CloseHandler(((BaseHandler)_tbc_chunkHandlers.SelectedTab.Tag));
            _tbc_chunkHandlers.SelectedTab.Dispose();
            if (_tbc_chunkHandlers.TabCount != 0)
                _tbc_chunkHandlers.SelectedTab = _tbc_chunkHandlers.TabPages[_tbc_chunkHandlers.TabCount - 1];
        }

        private void BtnShowInfoClick(object sender, EventArgs e)
        {
            new ChunkyFileInfoBox(m_rcf.FileHeader).ShowDialog();
        }

        #endregion buttons

        #region tree view

        private void TrvChunksAfterSelect(object sender, TreeViewEventArgs e)
        {
            _chi_info.LoadChunk((RelicChunk)e.Node.Tag);
        }

        private void TrvChunksNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (((RelicChunk)_trv_chunks.SelectedNode.Tag).ChunkHeader.Type == ChunkType.FOLD)
                return;
            var rc = (RelicChunk)e.Node.Tag;
            if (m_chunkHandler.ContainsKey(rc))
            {
                _tbc_chunkHandlers.SelectedTab = (TabPage)m_chunkHandler[rc].Parent;
                return;
            }
            BaseHandler chnd = ChunkManager.LaunchFromID(rc.ChunkHeader.Signature, rc);
            chnd.Dock = DockStyle.Fill;
            chnd.AutoSize = true;
            chnd.AutoSizeMode = AutoSizeMode.GrowOnly;

            var tpChk = new TabPage(rc.ChunkHeader.TypeString + rc.ChunkHeader.Signature);
            tpChk.Controls.Add(chnd);
            _tbc_chunkHandlers.TabPages.Add(tpChk);
            tpChk.Tag = chnd; // THE TAG OF THE TAB PAGES IS THE CHUNK HANDLER!
            m_chunkHandler.Add(rc, chnd);
            _tbc_chunkHandlers.SelectedTab = tpChk;
        }

        private void TrvChunksNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
                _trv_chunks.SelectedNode = e.Node;
        }

        #endregion tree view

        #region context menu

        private void CutChunkToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_trv_chunks.SelectedNode == null)
                return;
            s_clipboard = ((RelicChunk)_trv_chunks.SelectedNode.Tag);

            if (m_chunkHandler.ContainsKey(s_clipboard))
            {
                m_chunkHandler[s_clipboard].CloseChunk();
                m_chunkHandler[s_clipboard].Dispose();
            }

            if (s_clipboard.Parent == null)
                m_rcf.Chunks.Remove(s_clipboard);
            s_clipboard.Parent = null;
            _trv_chunks.SelectedNode.Remove();
            m_bHasChanges = true;
        }

        private void CopyChunkToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_trv_chunks.SelectedNode == null)
                return;
            s_clipboard = ((RelicChunk)_trv_chunks.SelectedNode.Tag).GClone();
        }

        private void InsertChunkToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_trv_chunks.SelectedNode == null)
                return;
            if (_trv_chunks.SelectedNode.Tag is FoldChunk)
            {
                RelicChunk tmp = s_clipboard.GClone();
                ((FoldChunk)_trv_chunks.SelectedNode.Tag).SubChunks.Add(tmp);
                var tnTmp = new TreeNode(tmp.ChunkHeader.TypeString + tmp.ChunkHeader.Signature) {Tag = tmp};
                tmp.Tag = tnTmp;
                _trv_chunks.SelectedNode.Nodes.Add(tnTmp);
                if (tmp is FoldChunk)
                    GetChunks(tnTmp, (FoldChunk)tmp);
                m_bHasChanges = true;
            }
        }

        private void InsertAtRootLevelToolStripMenuItemClick(object sender, EventArgs e)
        {
            RelicChunk tmp = s_clipboard.GClone();
            var tnTmp = new TreeNode(tmp.ChunkHeader.TypeString + tmp.ChunkHeader.Signature) {Tag = tmp};
            tmp.Tag = tnTmp;
            tmp.ChunkHeader.FileVersion = m_rcf.FileHeader.Version;
            m_rcf.Chunks.Add(tmp);
            _trv_chunks.Nodes.Add(tnTmp);
            if (tmp is FoldChunk)
                GetChunks(tnTmp, (FoldChunk)tmp);
            m_bHasChanges = true;
        }

        private void DeleteChunkToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_trv_chunks.SelectedNode == null)
                return;
            var tmp = ((RelicChunk)_trv_chunks.SelectedNode.Tag);
            if (m_chunkHandler.ContainsKey(tmp))
            {
                m_chunkHandler[tmp].CloseChunk();
                m_chunkHandler[tmp].Dispose();
            }
            if (tmp.Parent == null)
                m_rcf.Chunks.Remove(tmp);
            tmp.Parent = null;
            _trv_chunks.SelectedNode.Remove();
            m_bHasChanges = true;
        }

        #endregion context menu

        #endregion eventhandlers

        #region methods

        protected void GetChunks(TreeNode tn, FoldChunk rcp)
        {
            foreach (RelicChunk rc in rcp)
            {
                var tnC = new TreeNode();
                string tnName = rc.ChunkHeader.TypeString + rc.ChunkHeader.Signature;
                tnC.Name = tnName;
                tnC.Text = tnName;
                rc.Tag = tnC;
                tnC.Tag = rc;
                if (rc is FoldChunk)
                    GetChunks(tnC, (FoldChunk)rc);
                tn.Nodes.Add(tnC);
            }
        }

        protected void CloseHandler(BaseHandler bh)
        {
            if (bh.HasChanges)
            {
                if (MessageBox.Show("The chunk hasn't been saved since the last editing! Save now?",
                                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bh.SaveChunk();
                    HasChanges = true;
                }
            }
            RelicChunk tmp = bh.Chunk;
            m_chunkHandler.Remove(tmp);
            bh.CloseChunk();
        }

        public override void SaveFile()
        {
            foreach (BaseHandler bh in m_chunkHandler.Values)
            {
                bh.SaveChunk();
            }
            m_rcf.WriteDataTo(m_rcf.FilePath);
            HasChanges = false;
            InvokeOnSaved(this, new FileActionEventArgs(FileActionType.Save, m_rcf, m_rcf.Tag));
        }

        #endregion methods

        #region properties

        public override bool HasChanges
        {
            get
            {
                if (m_bHasChanges)
                    return m_bHasChanges;
                foreach (BaseHandler bh in m_chunkHandler.Values)
                {
                    m_bHasChanges |= bh.HasChanges;
                    if (m_bHasChanges)
                        return m_bHasChanges;
                }
                return m_bHasChanges;
            }
        }

        public override UniFile File
        {
            get { return m_rcf; }
        }

        #endregion properties
    }
}