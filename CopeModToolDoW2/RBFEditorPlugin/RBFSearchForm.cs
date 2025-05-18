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
using cope.DawnOfWar2;
using cope.DawnOfWar2.RelicAttribute;
using cope.Extensions;
using ModTool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RBFPlugin
{
    public partial class RBFSearchForm : Form
    {
        private readonly MethodInvoker m_advanceProgress;
        private Dictionary<string, SearchResult> m_results;
        private string m_sSearchKey;
        private string m_sSearchValue;
        private bool m_bSearchForKey;
        private bool m_bSearchForValue;
        private bool m_bFullText;
        private RBFCrawler m_crawler;

        public RBFSearchForm()
        {
            InitializeComponent();
            m_advanceProgress = new MethodInvoker(m_progBarSearch.PerformStep);
        }

        private void Search(AttributeStructure data, string pathInTree)
        {
            IEnumerable<AttributeValue> attribValues = data.Root.Where(SatisfiesCondition);

            SearchResult result;
            if (!m_results.TryGetValue(pathInTree, out result))
            {
                result = new SearchResult(pathInTree);
                m_results.Add(pathInTree, result);
            }
            result.AddValues(attribValues);
            if (result.Values.Count == 0)
                return;
            MethodInvoker adder = () => m_lbxSearchResults.Items.Add(result);
            Invoke(adder);
        }

        private bool SatisfiesCondition(AttributeValue value)
        {
            if (m_bSearchForKey)
            {
                if (m_bFullText)
                {
                    if (!value.Key.Contains(m_sSearchKey))
                        return false;
                }
                else if (value.Key != m_sSearchKey)
                    return false;
            }

            if (m_bSearchForValue)
            {
                if (value.DataType != AttributeDataType.String)
                    return false;
                if (m_bFullText)
                {
                    if (!(value.Data as string).Contains(m_sSearchValue))
                        return false;
                }
                else if ((value.Data as string) != m_sSearchValue)
                    return false;
            }
            return true;
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            if (!m_chkbxSearchForKey.Checked && !m_chkbxSearchForValue.Checked)
            {
                UIHelper.ShowError("No search criteria specified!");
                return;
            }
            if (FileManager.AttribTree == null)
            {
                UIHelper.ShowError("The attrib-tree could not be found! Ensure that there is a mod loaded");
                return;
            }
            FSNodeDir startingNode = m_tbxInitialNode.Text == string.Empty
                                         ? FileManager.AttribTree.RootNode
                                         : FileManager.AttribTree.RootNode.GetSubNodeByPath(m_tbxInitialNode.Text) as
                                           FSNodeDir;
            if (startingNode == null)
            {
                UIHelper.ShowError(
                    "The specified starting node could not be found or is not a directory! " +
                    "Ensure that it exists in the attrib-tree!");
                return;
            }
            m_lbxSearchResults.Items.Clear();
            m_progBarSearch.Value = 1;
            m_progBarSearch.Step = 1;
            m_progBarSearch.Minimum = 1;
            m_progBarSearch.Maximum = startingNode.GetTotalFileCount();
            m_sSearchKey = m_tbxSearchKey.Text;
            m_sSearchValue = m_tbxSearchValue.Text;
            m_bSearchForKey = m_chkbxSearchForKey.Checked;
            m_bSearchForValue = m_chkbxSearchForValue.Checked;
            m_bFullText = m_chkbxFullText.Checked;
            m_btnSearch.Enabled = false;
            m_results = new Dictionary<string, SearchResult>();
            m_crawler = new RBFCrawler(Search, startingNode, AdvanceProgress);
            m_crawler.OnFinished += CrawlerOnFinished;
            m_crawler.Start();
        }

        private void CrawlerOnFinished()
        {
            m_crawler.OnFinished -= CrawlerOnFinished;
            MethodInvoker done = CrawlerDone;
            Invoke(done);
        }

        private void CrawlerDone()
        {
            m_lbxSearchResults.Visible = true;
            m_btnSearch.Enabled = true;
        }

        private void LbxSearchResultsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lbxSearchResults.SelectedIndex == -1)
                return;

            var result = m_lbxSearchResults.SelectedItem as SearchResult;
            m_rtbResultDisplay.Text = string.Empty;
            var corsixString = new StringBuilder();
            for (int i = 0; i < result.ValuePaths.Count; i++ )
            {
                corsixString.Append(result.ValuePaths[i].SubstringBeforeLast('\\', true));
                corsixString.Append(CorsixStyleConverter.ToCorsixStyle(result.Values[i]));
                corsixString.AppendLine().AppendLine();
            }
            m_rtbResultDisplay.Text = corsixString.ToString();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            m_crawler.Stop();
        }

        private void LbxSearchResultsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_lbxSearchResults.SelectedItem == null)
                return;
            var result = m_lbxSearchResults.SelectedItem as SearchResult;
            FSNodeFile file = FileManager.AttribTree.RootNode.GetFileByPath(result.RelativeFilePath);
            UniFile uni = file.GetUniFile();
            FileManager.LoadFile(uni);
        }

        private void AdvanceProgress()
        {
            Invoke(m_advanceProgress);
        }

        class SearchResult
        {
            public SearchResult(string relativePath)
            {
                RelativeFilePath = relativePath;
                ValuePaths = new List<string>();
                Values = new List<AttributeValue>();
            }

            public readonly string RelativeFilePath;
            public readonly List<string> ValuePaths;
            public readonly List<AttributeValue> Values;

            public void AddValue(AttributeValue value)
            {
                Values.Add(value);
                ValuePaths.Add(value.GetPath());
            }

            public void AddValues(IEnumerable<AttributeValue> values)
            {
                Values.AddRange(values);
                foreach (var value in values)
                    ValuePaths.Add(value.GetPath());
            }

            public override string ToString()
            {
                return RelativeFilePath;
            }
        }
    }
}
