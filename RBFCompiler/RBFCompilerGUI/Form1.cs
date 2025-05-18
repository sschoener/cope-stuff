using System;
using System.IO;
using System.Windows.Forms;

namespace RBFCompilerGUI
{
    public partial class Form1
    {
        private RBFCompiler.RBFCompiler m_compiler;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnAboutClick(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
            about.Dispose();
        }

        private void BtnLuaFileClick(object sender, EventArgs e)
        {
            if (m_dlgOpenLuaFile.ShowDialog() == DialogResult.OK)
            {
                m_tbxLuaFile.Text = m_dlgOpenLuaFile.FileName;
            }
        }

        private void BtnModuleFileClick(object sender, EventArgs e)
        {
            if (m_dlgOpenModuleFile.ShowDialog() == DialogResult.OK)
            {
                m_tbxModuleFile.Text = m_dlgOpenModuleFile.FileName;
            }
        }

        private void BtnSourceDirClick(object sender, EventArgs e)
        {
            if (m_dlgFolderBrowserSource.ShowDialog() == DialogResult.OK)
            {
                m_tbxSourceDir.Text = m_dlgFolderBrowserOutput.SelectedPath;
            }
        }

        private void BtnStartClick(object sender, EventArgs e)
        {
            m_lbxReports.Items.Clear();
            _log_file = File.CreateText("rbf_compile.log");
            m_compiler = new RBFCompiler.RBFCompiler(m_tbxModuleFile.Text, m_tbxTargetDir.Text, m_tbxSourceDir.Text,
                                                     m_tbxLuaFile.Text);
            m_compiler.OnLog += Log;
            m_compiler.OnLog += LogFile;
            m_compiler.Start();
            _log_file.Flush();
            _log_file.Close();
        }

        private void BtnTargetDirClick(object sender, EventArgs e)
        {
            if (m_dlgFolderBrowserOutput.ShowDialog() == DialogResult.OK)
            {
                m_tbxTargetDir.Text = m_dlgFolderBrowserOutput.SelectedPath;
            }
        }

        private void Log(string message)
        {
            m_lbxReports.Items.Add(message);
        }

        private void LogFile(string message)
        {
            _log_file.WriteLine(message);
        }
    }
}