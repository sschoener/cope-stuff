#region

using System;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public class DataGridViewNumTextBoxEditingControl :
        NumericUpDown, IDataGridViewEditingControl
    {
        public DataGridViewNumTextBoxEditingControl()
        {
            TabStop = false;
        }

        #region IDataGridViewEditingControl

        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return Text == string.Empty ? "0" : Text;
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(
                    DataGridViewDataErrorContexts.Formatting);
            }
            set { Text = (string) value; }
        }

        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
            switch (dataGridViewCellStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.TopCenter:
                    TextAlign = HorizontalAlignment.Center;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.TopRight:
                    TextAlign = HorizontalAlignment.Right;
                    break;
                default:
                    TextAlign = HorizontalAlignment.Left;
                    break;
            }
        }

        public DataGridView EditingControlDataGridView { get; set; }

        public int EditingControlRowIndex { get; set; }

        public bool EditingControlValueChanged { get; set; }

        public bool EditingControlWantsInputKey(
            Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.End:
                case Keys.Left:
                case Keys.Home:
                    return true;
                default:
                    return false;
            }
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
                Select(0, DecimalPlaces + Maximum.ToString().Length + 1);
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            try
            {
                string v = Text == string.Empty ? "0" : Text;
                if ((Parent.Parent as DataGridView).CurrentCell.Value.ToString() == v)
                    EditingControlDataGridView.NotifyCurrentCellDirty(false);
                else
                    EditingControlDataGridView.NotifyCurrentCellDirty(true);
                (Parent.Parent as DataGridView).CurrentCell.Value = v;
            }
            catch
            {
                EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }

        #endregion
    }
}