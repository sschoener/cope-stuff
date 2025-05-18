using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using cope.DawnOfWar2;
using cope.DawnOfWar2.RelicBinary;
using cope.Helper;

namespace RB2Extractor
{
    public partial class MainForm : Form
    {
        private int m_iProgress;
        private RB2FileExtractor m_rb2;
        private string[] m_files;
        private FieldNameFile m_flb;

        public MainForm()
        {
            InitializeComponent();          
        }

        private void BtnSelectRb2FileClick(object sender, EventArgs e)
        {
            if (m_dlgSelectRB2.ShowDialog() == DialogResult.OK)
            {
                m_tbxRB2Path.Text = m_dlgSelectRB2.FileName;
            }
        }

        private void BtnSelectFlbFileClick(object sender, EventArgs e)
        {
            if (m_dlgSelectFLB.ShowDialog() == DialogResult.OK)
            {
                m_tbxFLBPath.Text = m_dlgSelectFLB.FileName;
            }
        }

        private void BtnSelectOutputDirClick(object sender, EventArgs e)
        {
            if (m_dlgSelectOutputDir.ShowDialog() == DialogResult.OK)
            {
                m_tbxOutputDir.Text = m_dlgSelectOutputDir.SelectedPath;
            }
        }

        private void BtnExtractClick(object sender, EventArgs e)
        {
            if (m_tbxRB2Path.Text == string.Empty && m_tbxRBFDirectory.Text == string.Empty)
            {
                UIHelper.ShowErrorMessage("Select a valid RB2 file or a valid input directory!");
                return;
            }
            if (m_tbxRBFDirectory.Text != string.Empty && !Directory.Exists(m_tbxRBFDirectory.Text))
            {
                    UIHelper.ShowErrorMessage("Select a valid input directory!");
                    return;   
            }
            if (m_tbxRB2Path.Text != string.Empty && !File.Exists(m_tbxRB2Path.Text))
            {
                    UIHelper.ShowErrorMessage("Select a valid RB2 file!");
                    return;   
            }
            if (m_tbxFLBPath.Text == string.Empty || !File.Exists(m_tbxFLBPath.Text))
            {
                UIHelper.ShowErrorMessage("Select a valid FLB file!");
                return;
            }
            if (m_tbxOutputDir.Text == string.Empty)
            {
                UIHelper.ShowErrorMessage("Select a valid output path!");
                return;
            }
            Start();
        }

        private void Start()
        {
            if (!Directory.Exists(m_tbxOutputDir.Text))
            {
                try
                {
                    Directory.CreateDirectory(m_tbxOutputDir.Text);
                }
                catch
                {
                    UIHelper.ShowErrorMessage(
                        "Tried to create the output directory but failed. Please recheck everything.");
                }
            }
            m_flb = new FieldNameFile(m_tbxFLBPath.Text);
            m_flb.ReadData();

            m_prgbarProgress.Maximum = 0;
            m_rb2 = null;
            if (m_tbxRB2Path.Text != string.Empty)
            {
                m_rb2 = new RB2FileExtractor(m_tbxRB2Path.Text, m_flb);
                m_rb2.PerformConversion = m_chkbxPerformConversion.Checked;
                m_rb2.ReadData();
                m_prgbarProgress.Maximum += m_rb2.NumFiles;
            }
            m_files = null;
            if (m_tbxRBFDirectory.Text != string.Empty)
            {
                m_files = Directory.GetFiles(m_tbxRBFDirectory.Text, "*.rbf");
                m_prgbarProgress.Maximum += m_files.Length;
            }
            
            m_labProgress.Text = "0 / " + m_prgbarProgress.Maximum;
            ThreadPool.QueueUserWorkItem(Extract);
        }

        private void Extract(object o)
        {
            if (m_files != null && m_files.Length > 0)
            {
                foreach (string s in m_files)
                {
                    FileStream file = File.Open(s, FileMode.Open);
                    var rbf = RBFReader.Read(file, m_flb);
                    file.Close();
                    File.Delete(s);

                    file = File.Create(s);
                    RBFWriter.Write(file, rbf);
                    file.Flush();
                    file.Close();
                    Callback(0);
                }
            }
            if (m_rb2 != null)
                m_rb2.ExtractAll(m_tbxOutputDir.Text, Callback);
        }

        private void Callback(int progress)
        {
            m_iProgress++;
            if (!IsDisposed)
                Invoke(new MethodInvoker(UpdateProgressBar));
        }

        private void UpdateProgressBar()
        {
            m_prgbarProgress.Value = m_iProgress;
            m_labProgress.Text = m_iProgress + @" / " + m_prgbarProgress.Maximum;
            if (m_iProgress == m_prgbarProgress.Maximum)
            {
                UIHelper.ShowMessage("Success!", "RBFs converted / RB2 file extracted to " + m_tbxOutputDir.Text);
                m_prgbarProgress.Value = 0;
                m_iProgress = 0;
                m_flb.Close();
                m_rb2.Close();
            }
        }

        private void BtnSelectRBFDirectoryClick(object sender, EventArgs e)
        {
            if (m_dlgSelectRBFDirectory.ShowDialog() == DialogResult.OK)
            {
                m_tbxRBFDirectory.Text = m_dlgSelectRBFDirectory.SelectedPath;
            }
        }
    }
}
