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
using cope.DawnOfWar2.RelicAttribute;
using cope.DawnOfWar2.RelicBinary;
using ModTool.Core;
using System;
using System.Threading;

namespace RBFPlugin
{
    class RBFCrawler
    {
        private readonly EventWaitHandle m_stopSearch = new EventWaitHandle(false, EventResetMode.ManualReset);
        private readonly FSNodeDir m_startNode;
        private readonly Action m_advanceProgressCallback;
        private readonly CrawlerCallback m_foreachRBF;

        public delegate void CrawlerCallback(AttributeStructure data, string pathInFileTree);

        /// <summary>
        /// This event is called when the RBFCrawler fails to open a file and allows you to handle the error.
        /// </summary>
        public event Action<Exception> OnFileOpenFailed;
        /// <summary>
        /// This event is called when the RBFCrawler has done its work or is stopped.
        /// </summary>
        public event System.Action OnFinished;

        /// <summary>
        /// Creates a new RBFCrawler that call the specified delegate for every RBF and starts at a given directory.
        /// </summary>
        /// <param name="foreachRBF">This delegate is called for every RBF.</param>
        /// <param name="pathOfStartNode">The crawler will begin searching for RBF files from this node.</param>
        public RBFCrawler(CrawlerCallback foreachRBF, string pathOfStartNode)
            : this(foreachRBF, pathOfStartNode, null)
        {
        }

        /// <summary>
        /// Creates a new RBFCrawler that call the specified delegate for every RBF and starts at a given directory.
        /// </summary>
        /// <param name="foreachRBF">This delegate is called for every RBF.</param>
        /// <param name="pathOfStartNode">The crawler will begin searching for RBF files from this node.</param>
        /// <param name="advanceProgressCallback">Called everytime a file has been processed.</param>
        public RBFCrawler(CrawlerCallback foreachRBF, string pathOfStartNode, Action advanceProgressCallback)
            : this(foreachRBF, (FSNodeDir)(string.IsNullOrEmpty(pathOfStartNode) ? FileManager.AttribTree.RootNode : FileManager.AttribTree.RootNode.GetSubNodeByPath(pathOfStartNode)), advanceProgressCallback)
        {
        }

        /// <summary>
        /// Creates a new RBFCrawler that call the specified delegate for every RBF and starts at a given directory.
        /// </summary>
        /// <param name="foreachRBF">This delegate is called for every RBF.</param>
        /// <param name="startNode">The crawler will begin searching for RBF files from this node.</param>
        public RBFCrawler(CrawlerCallback foreachRBF, FSNodeDir startNode)
            : this(foreachRBF, startNode, null)
        {
        }

        /// <summary>
        /// Creates a new RBFCrawler that call the specified delegate for every RBF and starts at a given directory.
        /// </summary>
        /// <param name="foreachRBF">This delegate is called for every RBF.</param>
        /// <param name="startNode">The crawler will begin searching for RBF files from this node.</param>
        /// <param name="advanceProgressCallback">Called everytime a file has been processed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="foreachRBF" /> is <c>null</c>.</exception>
        public RBFCrawler(CrawlerCallback foreachRBF, FSNodeDir startNode, Action advanceProgressCallback)
        {
            if (foreachRBF == null) throw new ArgumentNullException("foreachRBF");
            if (startNode == null) throw new ArgumentNullException("startNode");

            m_foreachRBF = foreachRBF;
            m_startNode = startNode;
            m_advanceProgressCallback = advanceProgressCallback;
            UseDedicatedThread = true;
        }

        /// <summary>
        /// Gets or sets whether this RBFCrawler should be running in its own thread.
        /// Default is true.
        /// </summary>
        public bool UseDedicatedThread
        {
            get;
            set;
        }

        /// <summary>
        /// Starts the crawling process.
        /// </summary>
        public void Start()
        {
            m_stopSearch.Reset();
            if (UseDedicatedThread)
                ThreadPool.QueueUserWorkItem(Start);
            else
                Start(null);
        }

        /// <summary>
        /// Stops the crawler.
        /// </summary>
        public void Stop()
        {
            m_stopSearch.Set();
        }

        private void Start(object o)
        {
            Visit(m_startNode);
            if (OnFinished != null)
                OnFinished.Invoke();
        }

        private void Visit(FSNodeDir startDir)
        {
            if (m_stopSearch.WaitOne(0))
                return;

            foreach (FSNodeFile file in startDir.Files)
            {
                if (m_stopSearch.WaitOne(0))
                    return;
                if (file.Name.EndsWith(".rbf"))
                {
                    UniFile uni = file.GetUniFile();
                    RelicBinaryFile rbf;
                    try
                    {
                        rbf = new RelicBinaryFile(uni)
                        {
                            KeyProvider = ModManager.RBFKeyProvider,
                            UseKeyProvider = RBFSettings.UseKeyProviderForLoading
                        };
                        rbf.ReadData();
                    }
                    catch (Exception ex)
                    {
                        if (OnFileOpenFailed != null)
                            OnFileOpenFailed(ex);
                        if (m_advanceProgressCallback != null)
                            m_advanceProgressCallback.Invoke();
                        continue;
                    }

                    m_foreachRBF.Invoke(rbf.AttributeStructure, file.PathInTree);
                }
                if (m_advanceProgressCallback != null)
                    m_advanceProgressCallback.Invoke();
            }

            foreach (FSNodeDir dir in startDir.Directories)
            {
                if (m_stopSearch.WaitOne(0))
                    return;
                Visit(dir);
            }
        }
    }
}
