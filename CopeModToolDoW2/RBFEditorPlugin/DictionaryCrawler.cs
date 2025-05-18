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

using cope;
using cope.DawnOfWar2.RelicAttribute;
using cope.Extensions;
using ModTool.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RBFPlugin
{
    public partial class DictionaryCrawler : Form
    {
        private readonly MethodInvoker m_advanceProgress;
        private Dictionary<string, DictEntry> m_results;
        private RBFCrawler m_crawler;
        private DictEntry m_selectedItem;

        public DictionaryCrawler()
        {
            InitializeComponent();
            m_advanceProgress = new MethodInvoker(m_progBarSearch.PerformStep);
        }

        private void OnCrawlerDone()
        {
            m_crawler.OnFinished -= OnCrawlerDone;
            foreach (var entry in m_results.Values)
            {
                entry.Options.Sort();
                m_chklbxEntries.Items.Add(entry, true);
            }
            m_results.Clear();
            m_chklbxEntries.Sorted = true; // just to sort it once at the end
            m_chklbxEntries.Sorted = false;
            m_chklbxEntries.Visible = true;
            m_btnSearch.Enabled = true;
        }

        private void AdvanceProgress()
        {
            Invoke(m_advanceProgress);
        }

        private void Search(AttributeStructure data, string pathInTree)
        {
            // exclude the tuning-file
            if (pathInTree == "simulation\\attrib\\tuning\\tuning_info.rbf")
                return;
            foreach (var attribValue in data.Root)
            {
                if (attribValue.DataType != AttributeDataType.String)
                    continue;
                string attribData = attribValue.Data as string;
                if (string.IsNullOrWhiteSpace(attribData))
                    continue;
                // exclude file references
                if (!attribData.ContainsAny('\\', '/'))
                {
                    DictEntry entry;
                    if (!m_results.TryGetValue(attribValue.Key, out entry))
                    {
                        entry = new DictEntry(attribValue.Key);
                        m_results.Add(entry.Key, entry);
                    }
                    if (!entry.Options.Contains(attribData))
                        entry.Options.Add(attribData);
                }
            }
        }

        #region eventhandlers

        void CrawlerOnFinished()
        {
            m_crawler.OnFinished -= CrawlerOnFinished;
            MethodInvoker done = OnCrawlerDone;
            Invoke(done);
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (FileManager.AttribTree == null)
            {
                 UIHelper.ShowError("The attrib-tree could not be found! Ensure that there is a mod loaded");
                return;
            }
            m_selectedItem = null;
            m_chklbxEntries.Items.Clear();
            m_progBarSearch.Value = 1;
            m_progBarSearch.Step = 1;
            m_progBarSearch.Minimum = 1;
            m_progBarSearch.Maximum = FileManager.AttribTree.RootNode.GetTotalFileCount();
            m_btnSearch.Enabled = false;
            m_results = new Dictionary<string, DictEntry>();

            m_crawler = new RBFCrawler(Search, FileManager.AttribTree.RootNode, AdvanceProgress);
            m_crawler.OnFinished += CrawlerOnFinished;
            m_crawler.Start();
        }

        private void EntriesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_selectedItem != null)
            {
                m_selectedItem.Options.Clear();
                m_selectedItem.Options.AddRange(m_rtbResultDisplay.Lines);
            }
            if (m_chklbxEntries.SelectedIndex == -1)
                return;

            var entry = m_chklbxEntries.SelectedItem as DictEntry;
            m_rtbResultDisplay.Lines = entry.Options.ToArray();
            m_selectedItem = entry;
        }

        private void BtnCopyIntoDictionaryClick(object sender, EventArgs e)
        {
            foreach (var tmpEntry in m_chklbxEntries.CheckedItems)
            {
                DictEntry entry = tmpEntry as DictEntry;
                if (RBFDictionary.HasSearchpath(entry.Key))
                {
                    if (UIHelper.ShowYNQuestion("Question", "There already exists a searchpath for key '" + entry.Key +
                                                "', remove it and replace it with regular a RBF dictionary entry?") == DialogResult.No)
                        continue;
                    RBFDictionary.RemoveSearchpath(entry.Key);
                }
                RBFDictionary.AddDictEntry(entry.Key, entry.Options);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            m_crawler.Stop();
        }

        #endregion

        class DictEntry
        {
            public DictEntry(string key)
            {
                Key = key;
                Options = new List<string>();
            }

            public readonly string Key;
            public readonly List<string> Options;

            public override string ToString()
            {
                return Key;
            }
        }
    }
}
