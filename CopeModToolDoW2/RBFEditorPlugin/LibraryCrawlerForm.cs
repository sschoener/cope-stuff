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
using System.Linq;
using System.Windows.Forms;

namespace RBFPlugin
{
    public partial class LibraryCrawlerForm : Form
    {
        private readonly MethodInvoker m_advanceProgress;
        private List<CrawlerInfo> m_crawlTargets;
        private readonly List<string> m_keyFilter = new List<string>();
        private RBFCrawler m_crawler;
        private bool m_bIsRunning;

        public LibraryCrawlerForm()
        {
            InitializeComponent();
            m_advanceProgress = new MethodInvoker(m_prgProgress.PerformStep);

            var taggroups = RBFLibrary.GetTagGroupNames();
            foreach (string tag in taggroups)
                m_chklbxFilter.Items.Add(tag);
            if (RBFLibrary.GetTagGroup("modifiers") != null)
                m_tbxModifierTagGroup.Text = @"modifiers";
            if (RBFLibrary.GetTagGroup("actions") != null)
                m_tbxActionTagGroup.Text = @"actions";
            if (RBFLibrary.GetTagGroup("targets") != null)
                m_tbxTargetTagGroup.Text = @"targets";
            if (RBFLibrary.GetTagGroup("buffs") != null)
                m_tbxBuffTagGroup.Text = @"buffs";
            if (RBFLibrary.GetTagGroup("expendable_actions") != null)
                m_tbxExpActionTagGroup.Text = @"expendable_actions";
            if (RBFLibrary.GetTagGroup("requirements") != null)
                m_tbxRequirementTagGroup.Text = @"requirements";
        }

        private void OnCrawlerDone()
        {
            Invoke(new MethodInvoker(OnCrawlerDoneForm));
        }

        private void OnCrawlerDoneForm()
        {
            m_crawler.OnFinished -= OnCrawlerDone;
            m_bIsRunning = false;
            m_btnStartStop.Text = @"Start";
            m_tbxActionTagGroup.Enabled = true;
            m_tbxBuffTagGroup.Enabled = true;
            m_tbxExpActionTagGroup.Enabled = true;
            m_tbxModifierTagGroup.Enabled = true;
            m_tbxRequirementTagGroup.Enabled = true;
            m_tbxTargetTagGroup.Enabled = true;
            m_chklbxFilter.Enabled = true;

            DialogResult result = UIHelper.ShowYNQuestion("Question",
                                                          "Would you like to add the new entries to the library? " +
                                                          "This might OVERWRITE entries from the current library. " +
                                                          "You currently can't review the entries before adding them, " +
                                                          "it is advisable to create a backup of the library first.");
            if (result == DialogResult.No)
                return;
            AddToLibrary();
        }

        private void AdvanceProgress()
        {
            Invoke(m_advanceProgress);
        }

        private void BtnStartStopClick(object sender, EventArgs e)
        {
            if (m_bIsRunning)
                m_crawler.Stop();
            else
            {
                if (m_tbxActionTagGroup.Text == string.Empty)
                {
                    UIHelper.ShowError("You need to select a valid tag group for actions!");
                    return;
                }
                if (m_tbxModifierTagGroup.Text == string.Empty)
                {
                    UIHelper.ShowError("You need to select a valid tag group for modifiers!");
                    return;
                }
                if (m_tbxRequirementTagGroup.Text == string.Empty)
                {
                    UIHelper.ShowError("You need to select a valid tag group for requirements!");
                    return;
                }
                if (m_tbxBuffTagGroup.Text == string.Empty)
                {
                    UIHelper.ShowError("You need to select a valid tag group for buffs!");
                    return;
                }
                if (m_tbxExpActionTagGroup.Text == string.Empty)
                {
                    UIHelper.ShowError("You need to select a valid tag group for expendable actions!");
                    return;
                }
                if (m_tbxTargetTagGroup.Text == string.Empty)
                {
                    UIHelper.ShowError("You need to select a valid tag group for targets!");
                    return;
                }
                if (FileManager.AttribTree == null)
                {
                    UIHelper.ShowError("Could not find the Attrib-Tree! Please ensure that there is a mod loaded");
                    return;
                }
                Start();
            }
                
        }

        private void Start()
        {
            m_btnStartStop.Text = @"Stop";
            m_bIsRunning = true;
            m_tbxActionTagGroup.Enabled = false;
            m_tbxBuffTagGroup.Enabled = false;
            m_tbxExpActionTagGroup.Enabled = false;
            m_tbxModifierTagGroup.Enabled = false;
            m_tbxRequirementTagGroup.Enabled = false;
            m_tbxTargetTagGroup.Enabled = false;
            m_chklbxFilter.Enabled = false;

            m_prgProgress.Value = 1;
            m_prgProgress.Step = 1;
            m_prgProgress.Minimum = 1;
            m_prgProgress.Maximum = FileManager.AttribTree.RootNode.GetTotalFileCount();

            foreach (object item in m_chklbxFilter.CheckedItems)
            {
                string tagGroupName = item as string;
                var tags = RBFLibrary.GetTagGroup(tagGroupName);
                if (tags != null)
                    m_keyFilter.AddRange(tags);
            }

            m_crawlTargets = new List<CrawlerInfo>
                                 {
                                     new CrawlerInfo("GameData", v => HasRefWithStart(v, "entity_extensions"),
                                                     s => "entity_extensions", false),

                                     new CrawlerInfo("GameData", v => HasRefWithStart(v, "squad_extensions"),
                                                     s => "squad_extensions", false),

                                     new CrawlerInfo(m_tbxActionTagGroup.Text, v => HasRefWithStart(v, "actions"),
                                                     s => s.SubstringBetweenOccurrencs(1, 2, '\\') + "_actions"),

                                     new CrawlerInfo(m_tbxBuffTagGroup.Text, v => HasRefWithStart(v, "buffs"),
                                                     s => s.SubstringBetweenOccurrencs(1, 2, '\\') + "_buffs"),

                                     new CrawlerInfo(m_tbxExpActionTagGroup.Text,
                                                     v => HasRefWithStart(v, "wargear\\expendable_actions"),
                                                     s => "expendable_actions"),

                                     new CrawlerInfo(m_tbxModifierTagGroup.Text, v => HasRefWithStart(v, "modifiers"),
                                                     s => s.SubstringBetweenOccurrencs(1, 2, '\\')),

                                     new CrawlerInfo(m_tbxRequirementTagGroup.Text,
                                                     v => HasRefWithStart(v, "requirements"), s => "requirements"),

                                     new CrawlerInfo(m_tbxTargetTagGroup.Text, v => HasRefWithStart(v, "types\\targets"),
                                                     s => "targets"),
                                 };

            m_crawler = new RBFCrawler(ScanFile, string.Empty, AdvanceProgress);
            m_crawler.OnFinished += OnCrawlerDone;
            m_crawler.Start();
        }

        private void ScanFile(AttributeStructure data, string pathInFileTree)
        {
            foreach (AttributeValue attribValue in data.Root)
                ScanValue(attribValue);
        }

        private void ScanValue(AttributeValue value)
        {
            bool match = false;
            foreach (var crawlInfo in m_crawlTargets)
            {
                if (crawlInfo.Selector(value))
                {
                    string refPath = (GetRef(value).Data as string);
                    string category = crawlInfo.CategoryMaker(refPath);
                    crawlInfo.Add(refPath, new AttribInfo(value, category));
                    match = true;
                    break;
                }
            }
            if (!match)
                return;

            // filter the childs to exclude things such as subactions etc...
            var table = value.Data as AttributeTable;
            foreach (var attribValue in table)
            {
                if (attribValue.DataType == AttributeDataType.Table && m_keyFilter.Contains(attribValue.Key))
                {
                    var childTable = attribValue.Data as AttributeTable;
                    childTable.Clear();
                }
            }
        }

        #region helpers

        private static bool HasRefWithStart(AttributeValue value, string start)
        {
            var table = value.Data as AttributeTable;
            if (table == null)
                return false;
            return
                table.Any(
                    attrib =>
                    attrib.Key == "$REF" && attrib.DataType == AttributeDataType.String &&
                    (attrib.Data as string).StartsWith(start));
        }

        private static AttributeValue GetRef(AttributeValue value)
        {
            var table = value.Data as AttributeTable;
            return table.First(attrib => attrib.Key == "$REF");
        }

        #endregion

        private class AttribInfo
        {
            public AttribInfo(AttributeValue data, string category)
            {
                Value = data;
                Category = category;
            }

            public readonly AttributeValue Value;
            public readonly string Category;
        }

        private class CrawlerInfo
        {
            public CrawlerInfo(string tagGroupName, Func<AttributeValue, bool> selector, Func<string, string> categoryMaker, bool usesTagGroup = true)
            {
                TagGroupName = tagGroupName;
                Selector = selector;
                CategoryMaker = categoryMaker;
                UsesTagGroup = usesTagGroup;
                if (usesTagGroup)
                {
                    Tags = new HashSet<string>();
                    var currentTags = RBFLibrary.GetTagGroup(tagGroupName);
                    if (currentTags != null)
                        Tags.AddRange(currentTags);
                }
                Entries = new Dictionary<string, AttribInfo>();
            }

            public readonly Func<string, string> CategoryMaker;
            public readonly Func<AttributeValue, bool> Selector;
            public readonly ICollection<string> Tags;
            public readonly string TagGroupName;
            public readonly bool UsesTagGroup;
            public readonly Dictionary<string, AttribInfo> Entries;

            public void Add(string refpath, AttribInfo info)
            {
                if (UsesTagGroup)
                {
                    string parentName = info.Value.Parent.Owner.Key;
                    if (!Tags.Contains(parentName))
                        Tags.Add(parentName);
                }
                if (!Entries.ContainsKey(refpath))
                    Entries.Add(refpath, info);
            }
        }

        private static void AddToLibrary(string[] tags, string[] tagGroups, AttribInfo info)
        {
            RBFLibEntry entry = new RBFLibEntry();
            entry.Values = new List<AttributeValue>();
            entry.Submenu = info.Category;
            entry.TagGroups = tagGroups ?? new string[0];
            entry.Tags = tags ?? new string[0];
            entry.Values.Add(info.Value);
            entry.Name = info.Value.Key;
            RBFLibrary.RemoveEntry(entry.Name);
            RBFLibrary.AddEntry(entry);
        }

        private void AddToLibrary()
        {
            foreach (var crawlInfo in m_crawlTargets)
            {
                foreach (var entry in crawlInfo.Entries.Values)
                {
                    if (crawlInfo.UsesTagGroup)
                    {
                        RBFLibrary.AddTagsToGroup(crawlInfo.TagGroupName, crawlInfo.Tags);
                        AddToLibrary(null, new[] { crawlInfo.TagGroupName }, entry);
                    }
                    else
                        AddToLibrary(new[] { crawlInfo.TagGroupName }, null, entry);
                }
            }
        }
    }
}
