using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using cope;
using cope.Extensions;
using cope.DawnOfWar2.RelicBinary;
using cope.DawnOfWar2;

namespace RBFUpdater
{
    public partial class MainForm : Form
    {
        private FieldNameFile m_flb;
        private bool m_bConvertToRetribution;
        private string m_sInputDir;
        private string m_sOutputDir;
        private string m_sFlbPath;
        private int m_iProgress;
        private string[] m_files;

        public MainForm()
        {
            InitializeComponent();
        }

        #region event handlers

        private void BtnSelectFlbFileClick(object sender, EventArgs e)
        {
            if (m_dlgSelectFLBFile.ShowDialog() == DialogResult.OK)
            {
                m_tbxFLBFile.Text = m_dlgSelectFLBFile.FileName;
            }
        }

        private void BtnInputPathClick(object sender, EventArgs e)
        {
            if (m_dlgSelectInputDir.ShowDialog() == DialogResult.OK)
            {
                m_tbxInputDirectory.Text = m_dlgSelectInputDir.SelectedPath;
            }
        }

        private void BtnOutputPathClick(object sender, EventArgs e)
        {
            if (m_dlgSelectOutputDir.ShowDialog() == DialogResult.OK)
            {
                m_tbxOutputDirectory.Text = m_dlgSelectOutputDir.SelectedPath;
            }
        }

        private void BtnConvertClick(object sender, EventArgs e)
        {
            if (m_tbxFLBFile.Text == string.Empty || !File.Exists(m_tbxFLBFile.Text))
            {
                UIHelper.ShowError("Please specify a valid FLB-file.");
                return;
            }

            if (m_tbxInputDirectory.Text == string.Empty)
            {
                UIHelper.ShowError("Please specify a valid input directory.");
                return;
            }

            if (m_tbxOutputDirectory.Text == string.Empty)
            {
                UIHelper.ShowError("Please specify a valid output directory.");
                return;
            }

            if (m_tbxOutputDirectory.Text == m_tbxInputDirectory.Text)
            {
                UIHelper.ShowError("Output directory and input directory may not overlap!");
                return;
            }
            m_sInputDir = m_tbxInputDirectory.Text;
            if (!m_sInputDir.EndsWith('\\'))
                m_sInputDir += '\\';
            m_sOutputDir = m_tbxOutputDirectory.Text;
            if (!m_sOutputDir.EndsWith('\\'))
                m_sOutputDir += '\\';
            m_sFlbPath = m_tbxFLBFile.Text;
            if (m_radToNew.Checked)
                m_bConvertToRetribution = true;
            else
                m_bConvertToRetribution = false;
            m_btnConvert.Enabled = false;
            StartConversion();
        }

        #endregion

        private void StartConversion()
        {
            if (!Directory.Exists(m_sInputDir))
            {
                try
                {
                    Directory.CreateDirectory(m_sInputDir);
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError("Error while trying to create the input directory: " + ex.Message);
                }
            }

            if (!Directory.Exists(m_sOutputDir))
            {
                try
                {
                    Directory.CreateDirectory(m_sOutputDir);
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError("Error while trying to create the input directory: " + ex.Message);
                }
            }

            try
            {
                m_flb = new FieldNameFile(m_sFlbPath);
                m_flb.ReadData();
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("Failed to open FLB file: " + ex.Message);
                return;
            }
            m_files = Directory.GetFiles(m_tbxInputDirectory.Text, "*.rbf", SearchOption.AllDirectories);
            m_prgbarConversionProgress.Maximum = m_files.Length;
            m_iProgress = 0;
            ThreadPool.QueueUserWorkItem(ConvertToNewFormat);
        }

        private void ConvertToNewFormat(object o)
        {
            foreach (string s in m_files)
            {
                try
                {
                    var rbf = new RelicBinaryFile(s);
                    if (!m_bConvertToRetribution)
                        rbf.KeyProvider = m_flb;
                    else
                        rbf.UseKeyProvider = false;

                    rbf.ReadData();

                    if (m_bConvertToRetribution)
                        rbf.KeyProvider = m_flb;
                    else
                        rbf.UseKeyProvider = false;
                    string newPath = m_sOutputDir + s.SubstringAfterLast(m_sInputDir);
                    rbf.WriteDataTo(newPath);
                    rbf.Close();
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError("Error while converting " + s);
                }

                ProgressCallback();
            }
            Invoke(new MethodInvoker(ConversionDone));
        }

        private void ProgressCallback()
        {
            m_iProgress++;
            if (!IsDisposed)
            {
                try
                {
                    Invoke(new MethodInvoker(UpdateProgressBar));
                }
                catch
                {
                }
            }
        }

        private void ConversionDone()
        {
            m_btnConvert.Enabled = true;
            UIHelper.ShowMessage("Success!", "RBFs converted.");
            m_prgbarConversionProgress.Value = 0;
            if (m_flb.NeedsUpdate())
            {
                var dlgResult = UIHelper.ShowYNQuestion("Question",
                    "The FLB file needs to be updated, do you still need to old one?");
                try
                {
                    if (dlgResult == DialogResult.Yes)
                    {
                        m_flb.FileName = m_flb.FileName.SubstringBeforeFirst('.') + "_new.flb";
                        if (File.Exists(m_flb.FilePath))
                            File.Delete(m_flb.FilePath);
                        m_flb.Update();
                    }
                    else
                        m_flb.Update();
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError("Failed to save FLB file! Error: " + ex.Message);
                    return;
                }
                UIHelper.ShowMessage("Success!", "Updated FLB file written to: " + m_flb.FilePath);
            }
        }

        private void UpdateProgressBar()
        {
            m_prgbarConversionProgress.Value = m_iProgress;
            m_labProgress.Text = m_iProgress + @"/ " + m_prgbarConversionProgress.Maximum;
        }

        private void BtnSwitchDirectoriesClick(object sender, EventArgs e)
        {
            string input = m_tbxInputDirectory.Text;
            m_tbxInputDirectory.Text = m_tbxOutputDirectory.Text;
            m_tbxOutputDirectory.Text = input;
            bool toDoW2 = m_radToOld.Checked;
            m_radToOld.Checked = m_radToNew.Checked;
            m_radToNew.Checked = toDoW2;
        }
    }
}
