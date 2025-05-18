#region

using System.Windows.Forms;

#endregion

namespace cope
{
    public static class UIHelper
    {
        public static DialogResult ShowMessage(string caption, string formatString, params object[] args)
        {
            string message = string.Format(formatString, args);
            return MessageBox.Show(message, caption);
        }

        public static DialogResult ShowError(string formatString, params object[] args)
        {
            string error = string.Format(formatString, args);
            return MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowYNQuestion(string caption, string formatString, params object[] args)
        {
            return MessageBox.Show(string.Format(formatString, args), caption, MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);
        }

        public static DialogResult ShowWarning(string formatString, params object[] args)
        {
            string warning = string.Format(formatString, args);
            return MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}