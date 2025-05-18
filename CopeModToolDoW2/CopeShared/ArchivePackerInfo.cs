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
using System.Diagnostics;
using System.IO;
using cope;

namespace ModTool.Core
{
    public class ArchivePackerInfo
    {
        private readonly Process m_packer;
        private readonly string m_sDesignFilePath;

        public event NotifyEventHandler Finished;

        internal ArchivePackerInfo(Process packer, string designFilePath, string archiveFilePath)
        {
            m_packer = packer;
            m_sDesignFilePath = designFilePath;
            ArchiveFilePath = archiveFilePath;
            packer.Exited += OnPackerExited;
            if (m_packer.HasExited)
                DeleteDesignFile();
        }

        private void OnPackerExited(object sender, EventArgs e)
        {
            if (Finished != null)
                Finished();
            DeleteDesignFile();
        }

        private void DeleteDesignFile()
        {
            if (!File.Exists(m_sDesignFilePath))
                return;
            try
            {
                File.Delete(m_sDesignFilePath);
            }
            catch (Exception ex)
            {
                LoggingManager.SendError("ArchiveCreator - tried to delete superfluous SGA-design file but failed");
                LoggingManager.HandleException(ex);
            }
        }

        public string ArchiveFilePath { get; private set; }

        public bool IsDone()
        {
            return m_packer.HasExited;
        }
    }
}