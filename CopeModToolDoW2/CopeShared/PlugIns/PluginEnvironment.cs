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

namespace ModTool.Core.PlugIns
{
    public class PluginEnvironment
    {
        readonly ToolStripItemCollection m_submenu;
        readonly ToolStripItemCollection m_combinedContext;
        readonly ToolStripItemCollection m_virtualContext;

        public PluginEnvironment(ToolStripItemCollection submenu, ToolStripItemCollection combinedContext, ToolStripItemCollection virtualContext)
        {
            m_submenu = submenu;
            m_combinedContext = combinedContext;
            m_virtualContext = virtualContext;
        }

        public ToolStripItemCollection PluginSubMenu
        {
            get
            {
                return m_submenu;
            }
        }

        public ToolStripItemCollection CombinedFileTreeContextMenu
        {
            get
            {
                return m_combinedContext;
            }
        }

        public ToolStripItemCollection VirtualFileTreeContextMenu
        {
            get
            {
                return m_virtualContext;
            }
        }
    }
}