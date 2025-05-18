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
using cope.DawnOfWar2.RelicAttribute;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RBFPlugin
{
    public partial class RBFLibraryEditor : Form
    {
        private RBFLibEntry m_current;

        public RBFLibraryEditor()
        {
            InitializeComponent();

            SortedDictionary<string, RBFLibEntry> entries = RBFLibrary.GetAllEntries();
            foreach (RBFLibEntry entry in entries.Values)
                _lbxEntries.Items.Add(entry);

            RBFLibrary.EntryAdded += RBFLibraryEntryAdded;
            RBFLibrary.EntryRemoved += RBFLibraryEntryRemoved;
        }

        private void RBFLibraryEntryRemoved(object sender, RBFLibEntry t)
        {
            if (t == m_current)
            {
                m_current = null;
                rbfEditorCore1.Clear();
            }
            _lbxEntries.Items.Remove(t);
        }

        private void RBFLibraryEntryAdded(object sender, RBFLibEntry t)
        {
            _lbxEntries.Items.Add(t);
        }

        private static void AddNewEntryToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dlg = new AddToLibrary();
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                dlg.Dispose();
                return;
            }
            var tmp = new RBFLibEntry
                          {
                              Name = dlg.ValueName,
                              Tags = dlg.Tags,
                              TagGroups = dlg.TagGroups,
                              Values = new List<AttributeValue>()
                          };
            if (RBFLibrary.GetEntry(tmp.Name) == null)
                RBFLibrary.AddEntry(tmp);
            else if (dlg.AddTags)
            {
                RBFLibEntry entry = RBFLibrary.GetEntry(tmp.Name);
                foreach (string t in tmp.Tags)
                    RBFLibrary.AddEntryToTag(entry, t);
            }
            dlg.Dispose();
        }

        private void RemoveSelectedToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_lbxEntries.SelectedItem == null)
                return;
            RBFLibrary.RemoveEntry(_lbxEntries.SelectedItem as RBFLibEntry);
        }

        private void EditTagsToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_lbxEntries.SelectedItem == null)
                return;
            var entry = _lbxEntries.SelectedItem as RBFLibEntry;
            var dlg = new TagEditor {Tags = {Lines = entry.Tags}, TagGroups = entry.TagGroups};
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            entry.Tags = dlg.Tags.Lines;
            RBFLibrary.RemoveEntry(entry);
            RBFLibrary.AddEntry(entry);
        }

        private void LbxEntriesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lbxEntries.SelectedIndex == -1)
                return;
            rbfEditorCore1.Clear();
            var entry = (_lbxEntries.Items[_lbxEntries.SelectedIndex]) as RBFLibEntry;
            rbfEditorCore1.Analyze(entry.Values);
            m_current = entry;
            _tbx_subMenu.Text = m_current.Submenu ?? string.Empty;
        }

        private void TbxTagFilterTextChanged(object sender, EventArgs e)
        {
            _lbxEntries.Items.Clear();
            SortedDictionary<string, RBFLibEntry> entries = _tbx_tagFilter.Text == string.Empty ? RBFLibrary.GetAllEntries() : RBFLibrary.GetEntriesForTag(_tbx_tagFilter.Text);
            if (entries == null)
                return;

            foreach (RBFLibEntry entry in entries.Values)
                _lbxEntries.Items.Add(entry);
        }

        private void LbxEntriesMouseClick(object sender, MouseEventArgs e)
        {
            int index = _lbxEntries.IndexFromPoint(new Point(e.X, e.Y));
            _lbxEntries.SelectedIndex = index;
        }

        private void TbxSubMenuValidated(object sender, EventArgs e)
        {
            if (m_current == null || _tbx_subMenu.Text == string.Empty)
                return;
            m_current.Submenu = _tbx_subMenu.Text;
        }
    }
}