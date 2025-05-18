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
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModTool.Core
{
    public partial class DebugWindow : Form
    {
        readonly List<string> m_commands = new List<string>();
        int m_pos;

        public DebugWindow()
        {
            InitializeComponent();
        }

        internal void Log(string s)
        {
            if (!InvokeRequired)
                _lbx_log.Items.Add(s);
            else
                Invoke(new Func<object, int>(_lbx_log.Items.Add), s);
        }

        internal void ClearLog()
        {
            _lbx_log.Items.Clear();
        }

        private void HandlePreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _tbx_command.Text != string.Empty)
            {
                DebugManager.SendCommand(_tbx_command.Text);
                m_commands.Add(_tbx_command.Text);
                if (m_commands.Count > 50)
                    m_commands.RemoveAt(0);
                m_pos = m_commands.Count - 1;
                _tbx_command.Text = string.Empty;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (m_commands.Count == 0)
                    return;
                if (m_pos < 0)
                    m_pos = m_commands.Count - 1;
                else if (m_pos >= m_commands.Count)
                    m_pos = 0;
                _tbx_command.Text = m_commands[m_pos--];
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (m_commands.Count == 0)
                    return;
                if (m_pos < 0)
                    m_pos = m_commands.Count - 1;
                else if (m_pos >= m_commands.Count)
                    m_pos = 0;
                _tbx_command.Text = m_commands[m_pos++];
            }
        }

        private void DebugWindowPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            HandlePreviewKeyDown(e);
        }

        private void TbxCommandPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            HandlePreviewKeyDown(e);
        }
    }
}