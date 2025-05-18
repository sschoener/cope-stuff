using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cope.Helper;
using cope.DawnOfWar2.SGA;

namespace SGAExtractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnExtractClick(object sender, EventArgs e)
        {
            if (_tbx_outputPath.Text == string.Empty)
            {
                UIHelper.ShowErrorMessage("You must select an output path!");
                return;
            }
            if (_tbx_sgaArchive.Text == string.Empty)
            {
                UIHelper.ShowErrorMessage("You must select an input archive!");
                return;
            }

            if (!File.Exists(_tbx_sgaArchive.Text))
            {
                UIHelper.ShowErrorMessage("The selected archive does not exist!");
                return;
            }
            if (!Directory.Exists(_tbx_outputPath.Text))
                Directory.CreateDirectory(_tbx_outputPath.Text);

            if (!_tbx_outputPath.Text.EndsWith('/') && !_tbx_outputPath.Text.EndsWith('\\'))
                _tbx_outputPath.Text += '\\';
            var sga = new SGAFile(_tbx_sgaArchive.Text);
            sga.ReadData();
            int count = sga.Sum(ep => ep.StoredFiles.Count);
            _pgb_extract.Value = 0;
            _pgb_extract.Maximum = count;
            string sgaDir = _tbx_outputPath.Text + sga.FileName.SubstringBeforeLast('.') + '\\';
            Directory.CreateDirectory(sgaDir);
            foreach (SGAEntryPoint ep in sga)
            {
                string sgaDir2 = sgaDir + ep.Name + '\\';
                foreach (SGAStoredFile sf in ep.StoredFiles.Values)
                {
                    byte[] output = sf.SGA.ExtractFile(sf, sga.Stream);
                    string extractedFile = sgaDir2 + sf.GetPath();
                    string path = extractedFile.SubstringBeforeLast('\\');
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    File.WriteAllBytes(extractedFile, output);
                    _pgb_extract.Value++;
                    Application.DoEvents();
                }
            }
            MessageBox.Show(@"Done extracting files!");
            _pgb_extract.Value = 0;
        }

        private void BtnSelectArchiveClick(object sender, EventArgs e)
        {
            if (_dlg_selectArchive.ShowDialog() == DialogResult.OK)
            {
                _tbx_sgaArchive.Text = _dlg_selectArchive.FileName;
            }
        }

        private void BtnSelectOutputClick(object sender, EventArgs e)
        {
            if (_dlg_selectOutputDir.ShowDialog() == DialogResult.OK)
            {
                _tbx_outputPath.Text = _dlg_selectOutputDir.SelectedPath;
            }
        }
    }
}