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
using cope.Extensions;
using FreeImageAPI;
using ModTool.Core.PlugIns;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageViewerPlugin
{
    public partial class ImageViewer : FileTool
    {
        #region fields

        UniFile m_file;
        FreeImageBitmap m_image;
        FREE_IMAGE_FORMAT m_format;
        MemoryStream m_binary;

        #endregion fields

        public override UniFile File
        {
            get
            {
                return m_file;
            }
        }

        #region ctors

        public ImageViewer(UniFile file)
        {
            InitializeComponent();

            // set the image-display panel to DoubleBuffered
            System.Reflection.PropertyInfo dbProp = typeof(Control).GetProperty("DoubleBuffered",
                                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dbProp.SetValue(m_pnlImage, true, null);

            OpenFile(file);
        }

        #endregion ctors

        #region eventhandlers

        private void BtnSaveClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void BtnSaveAsClick(object sender, EventArgs e)
        {
            if (m_image == null)
            {
                 UIHelper.ShowError(@"No picture loaded!");
                return;
            }
            switch (m_format)
            {
                case FREE_IMAGE_FORMAT.FIF_DDS:
                    _dlg_saveFile.Filter = @"*.DDS|*.dds";
                    break;
                case FREE_IMAGE_FORMAT.FIF_TARGA:
                    _dlg_saveFile.Filter = @"*.TGA|*.tga";
                    break;
            }
            if (_dlg_saveFile.ShowDialog() != DialogResult.OK)
                return;
            string path = _dlg_saveFile.FileName;
            SaveFile(path);
        }

        #endregion eventhandlers

        #region methods

        public override bool Close()
        {
            if (m_file != null)
                m_file.Close();
            if (m_picbxImage.Image != null)
            {
                m_picbxImage.Image.Dispose();
                m_picbxImage.Image = null;
            }
            if (m_binary != null)
            {
                m_binary.Close();
                m_binary.Dispose();
            }
            m_picbxImage.Dispose();
            if (m_image != null)
                m_image.Dispose();
            return true;
        }

        public override void SaveFile()
        {
            SaveFile(m_file.FilePath);
        }

        private void OpenFile(UniFile file)
        {
            m_file = file;
            try
            {
                if (file.FileExtension.ToLowerInvariant() == "dds")
                {
                    m_image = new FreeImageBitmap(file.Stream, FREE_IMAGE_FORMAT.FIF_DDS);
                    m_format = FREE_IMAGE_FORMAT.FIF_DDS;
                }
                else if (file.FileExtension.ToLowerInvariant() == "tga")
                {
                    m_image = new FreeImageBitmap(file.Stream, FREE_IMAGE_FORMAT.FIF_TARGA);
                    m_format = FREE_IMAGE_FORMAT.FIF_TARGA;
                }
                m_picbxImage.Image = (Bitmap)m_image;

                m_binary = new MemoryStream();
                file.Stream.Position = 0;
                file.Stream.CopyTo(m_binary);
            }
            catch (Exception e)
            {
                 UIHelper.ShowError("Failed to open image! Error: " + e.Message);
                ModTool.Core.LoggingManager.SendMessage("Failed to open image " + file.FilePath);
                ModTool.Core.LoggingManager.HandleException(e);
            }
            finally
            {
                file.Close();
            }
        }

        private void SaveFile(string path)
        {
            if (m_image == null)
            {
                 UIHelper.ShowError("No picture loaded!");
                return;
            }
            string dir = path.SubstringBeforeLast('\\');
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            FileStream stream = System.IO.File.Create(path);
            m_binary.Position = 0;
            m_binary.CopyTo(stream);
            stream.Close();
            stream.Dispose();
        }

        #endregion methods
    }
}