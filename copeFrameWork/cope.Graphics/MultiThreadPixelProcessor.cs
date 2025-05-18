using System;
using System.Drawing;
using System.Threading;

namespace cope.Graphics
{
    /// <summary>
    /// Multithreaded processor for Bitmaps which processes one pixel at a time.
    /// Can be set to operate in-place or generate a new Bitmap (which is useful
    /// when the PixelProcessor needs access to a certain number of pixels).
    /// </summary>
    public abstract class MultiThreadPixelProcessor : IBitmapProcessor
    {
        int m_iFinishCount;
        readonly int m_iMaxThreads;
        readonly object m_finishLock = new object();
        readonly bool m_bInPlace;
        private int m_iNumPixelsProcessed;
        private readonly object m_processedPixelsLock = new object();
        protected Bitmap m_inputBitmap;
        private Bitmap m_outputBitmap;

        protected MultiThreadPixelProcessor(bool inPlace, int maxThreads)
        {
            m_bInPlace = inPlace;
            m_iMaxThreads = maxThreads;
        }

        protected MultiThreadPixelProcessor(bool inPlace)
        {
            m_bInPlace = inPlace;
            m_iMaxThreads = Environment.ProcessorCount - 1;
        }

        public int GetPixelCount()
        {
            return m_inputBitmap.Width * m_inputBitmap.Height;
        }

        public int GetProcessedPixels()
        {
            lock (m_processedPixelsLock)
            {
                return m_iNumPixelsProcessed;
            }
        }

        public Bitmap ProcessBitmap(Bitmap bmp)
        {
            m_inputBitmap = bmp;
            m_iFinishCount = 0;
            var heightPerThread = (int)Math.Floor((double)bmp.Height / (Environment.ProcessorCount - 1));

            if (m_bInPlace)
                m_outputBitmap = bmp;
            else
                m_outputBitmap = bmp.Clone() as Bitmap;
            int curHeight = 0;
            // might be zero if the picture is really small
            if (heightPerThread > 0)
            {
                for (int i = 0; i < m_iMaxThreads - 1; i++)
                {
                    int newHeight = curHeight + heightPerThread;
                    ThreadPool.QueueUserWorkItem(ProcessChunk, new ChunkInfo(curHeight, newHeight));
                    curHeight = newHeight;
                }
            }
            ThreadPool.QueueUserWorkItem(ProcessChunk, new ChunkInfo(curHeight, bmp.Height));

            while (true)
            {
                bool exit;
                lock (m_finishLock)
                {
                    exit = m_iFinishCount == m_iMaxThreads;
                }
                if (exit)
                    break;
                Thread.Sleep(500);
            }
            bmp = m_outputBitmap;
            m_outputBitmap = null;
            m_inputBitmap = null;
            return bmp;
        }

        private void ProcessChunk(object bmpinf)
        {
            int end = ((ChunkInfo) bmpinf).EndHeight;
            Bitmap bmp = m_inputBitmap;
            for (int y = (bmpinf as ChunkInfo).StartHeight; y < end; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // greyscale code
                    Color pixel = bmp.GetPixel(x, y);
                    m_outputBitmap.SetPixel(x, y, PixelProcessor(pixel, x, y));
                    lock (m_processedPixelsLock)
                    {
                        m_iNumPixelsProcessed++;
                    }
                }
            }
            lock (m_finishLock)
            {
                ++m_iFinishCount;
            }
        }

        protected abstract Color PixelProcessor(Color c, int x, int y);

        public bool InPlace
        {
            get { return m_bInPlace; }
        }

        public int MaxThreads
        {
            get { return m_iMaxThreads; }
        }

        private class ChunkInfo
        {
            public readonly int StartHeight;
            public readonly int EndHeight;

            public ChunkInfo(int startHeight, int endHeight)
            {
                StartHeight = startHeight;
                EndHeight = endHeight;
            }
        }
    }
}