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
using System.IO;
using cope.DawnOfWar2;
using ICSharpCode.AvalonEdit.Highlighting;
using ModTool.Core.PlugIns;
using ICSharpCode.AvalonEdit;

namespace ScarPlugin
{
    public partial class ScarEditorControl : FileTool//UserControl
    {
        private readonly UniFile m_file;
        private readonly TextEditor m_editor;

        public ScarEditorControl(UniFile file)
        {
            InitializeComponent();
            m_btnSave.Click += BtnSaveClick;

            m_file = file;

            m_editor = new TextEditor();
            m_wpfTextEditor.Child = m_editor;

            m_editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("LUA Highlighting");
            m_editor.ShowLineNumbers = true;
            m_editor.TextChanged += EditorTextChanged;
            m_editor.Load(file.Stream);
        }

        #region eventhandlers

        void EditorTextChanged(object sender, System.EventArgs e)
        {
            if (m_editor.IsModified)
            {
                HasChanges = true;
            }
        }

        void BtnSaveClick(object sender, System.EventArgs e)
        {
            SaveFile();
        }

        #endregion

        #region methods

        public override void SaveFile()
        {
            string directory = m_file.FilePath.SubstringBeforeLast('\\');
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fs = new FileStream(m_file.FilePath, FileMode.Create);
            m_editor.Save(fs);
            var e = new FileActionEventArgs(FileActionType.Save, m_file);
            InvokeOnSaved(this, e);
            HasChanges = false;
        }

        #endregion

        public override UniFile File
        {
            get { return m_file; }
        }
    }
}
