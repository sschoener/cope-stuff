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
using System.IO;
using System.Xml;
using cope.DawnOfWar2;
using ICSharpCode.AvalonEdit.Highlighting;
using ModTool.Core;
using ModTool.Core.PlugIns;


namespace ScarPlugin
{
    public class ScarEditorPlugin : FileTypePlugin
    {
        /// <exception cref="InvalidOperationException">Could not find embedded resource</exception>
        public override void Init(PluginEnvironment env)
        {
            LoggingManager.SendMessage("ScarEditorPlugin - Init started");

            IHighlightingDefinition luaHighlighting;
            using (Stream s = typeof(ScarEditorControl).Assembly.GetManifestResourceStream("ScarPlugin.LUAHighlighting.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource for LUA-syntax Highlighting");
                using (XmlReader xr = new XmlTextReader(s))
                {
                    luaHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(xr, HighlightingManager.Instance);
                }
            }
            HighlightingManager.Instance.RegisterHighlighting("LUA Highlighting", new[] { ".scar", ".lua" }, luaHighlighting);

            LoggingManager.SendMessage("ScarEditorPlugin - Init finished");
        }

        public override string Author
        {
            get { return "Copernicus"; }
        }

        public override string PluginName
        {
            get { return "Scar Editor"; }
        }

        public override string Version
        {
            get { return "0.1"; }
        }

        public override string[] FileExtensions
        {
            get { return new[] {"scar", "lua"}; }
        }

        public override FileTool LoadFile(UniFile file)
        {
            return new ScarEditorControl(file);

        }
    }
}
