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
using cope.DawnOfWar2;
using ModTool.Core;
using ModTool.Core.PlugIns;
using System;
using System.Windows.Forms;

namespace RBFPlugin
{
    public class RBFEditorPlugin : FileTypePlugin
    {
        RBFSearchForm m_searchForm;

        public override void Init(PluginEnvironment env)
        {
            LoggingManager.SendMessage("RBFPlugin - Setup started");
            RBFSettings.Instance = this;

            // adding stuff to the menu
            ToolStripItem openRBFLib = new ToolStripMenuItem("Open RBF-Library") {Name = "openRBFLib"};
            openRBFLib.Click += OpenRBFLibClick;
            ToolStripItem options = new ToolStripMenuItem("Options") {Name = "options"};
            options.Click += OptionsClick;
            ToolStripItem search = new ToolStripMenuItem("RBF-Search") {Name = "RBFSearch"};
            search.Click += SearchClick;
            ToolStripItem openDictionaryCrawler = new ToolStripMenuItem("Open Dictionary Builder")
                                                      {Name = "dictCrawler"};
            openDictionaryCrawler.Click += OpenDictionaryCrawlerClick;
            ToolStripItem openLibraryCrawler = new ToolStripMenuItem("Open Library Builder") {Name = "libraryCrawler"};
            openLibraryCrawler.Click += OpenLibraryCrawlerClick;
            env.PluginSubMenu.Add(openRBFLib);
            env.PluginSubMenu.Add(openDictionaryCrawler);
            env.PluginSubMenu.Add(openLibraryCrawler);
            env.PluginSubMenu.Add(options);
            env.PluginSubMenu.Add(search);

            RBFLibrary.Init();
            RBFDictionary.Init();
            LoggingManager.SendMessage("RBFPlugin - Setup finished");
        }

        #region eventhandlers

        static void OptionsClick(object sender, EventArgs e)
        {
            var options = new RBFOptionsDialog();
            options.ShowDialog();
        }

        static void OpenRBFLibClick(object sender, EventArgs e)
        {
            RBFLibrary.ShowLibraryForm();
        }

        private static void OpenLibraryCrawlerClick(object sender, EventArgs e)
        {
            var libCrawler = new LibraryCrawlerForm();
            libCrawler.ShowDialog();
        }

        static void OpenDictionaryCrawlerClick(object sender, EventArgs e)
        {
            var dictCrawler = new DictionaryCrawler();
            dictCrawler.ShowDialog();
        }

        void SearchClick(object sender, EventArgs e)
        {
            if (m_searchForm == null || m_searchForm.IsDisposed)
                m_searchForm = new RBFSearchForm();
            m_searchForm.Show();
        }

        #endregion eventhandlers

        #region options

        public bool AutoReloadInTestMode
        {
            get
            {
                string setting = GetSetting("bAutoReloadRBFTestMode");
                if (setting == null)
                    return false;
                return bool.Parse(setting);
            }
            set
            {
                SetSetting("bAutoReloadRBFTestMode", value.ToString());
            }
        }

        public bool UseKeyProviderForLoading
        {
            get
            {
                string setting = GetSetting("bUseKeyProviderForLoading");
                if (setting == null)
                    return ToolSettings.IsInRetributionMode;
                return bool.Parse(setting);
            }
            set { SetSetting("bUseKeyProviderForLoading", value.ToString()); }
        }

        public bool UseKeyProviderForSaving
        {
            get
            {
                string setting = GetSetting("bUseKeyProviderForSaving");
                if (setting == null)
                    return ToolSettings.IsInRetributionMode;
                return bool.Parse(setting);
            }
            set { SetSetting("bUseKeyProviderForSaving", value.ToString()); }
        }

        public bool UseAutoCompletion
        {
            get
            {
                string setting = GetSetting("bUseAutoCompletion");
                if (setting == null)
                    return false;
                return bool.Parse(setting);
            }
            set { SetSetting("bUseAutoCompletion", value.ToString()); }
        }

        #endregion options

        #region plugin

        public override string[] FileExtensions
        {
            get { return new[] { "rbf", "attr_pc"}; }
        }

        public override FileTool LoadFile(UniFile file)
        {
            return new RBFEditor(file);
        }

        public override string Author
        {
            get { return "Copernicus"; }
        }

        public override string PluginName
        {
            get { return "Cope's RBF Editor"; }
        }

        public override string Version
        {
            get { return "1.991"; }
        }

        #endregion plugin
    }
}