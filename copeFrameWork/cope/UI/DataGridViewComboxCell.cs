#region

using System;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public class DataGridViewComboxCell : DataGridViewComboBoxCell
    {
        public override Type EditType
        {
            get { return typeof (DataGridViewComboxEditingControl); }
        }

        public override Type ValueType
        {
            get { return typeof (object); }
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            var comboBox = DataGridView.EditingControl as DataGridViewComboxEditingControl;

            if (comboBox != null)
            {
                if ((RowIndex < 0) || (ColumnIndex < 0))
                    return;

                comboBox.Text = initialFormattedValue != null ? initialFormattedValue.ToString() : string.Empty;
            }
        }
    }

    public class DataGridViewComboxEditingControl : ComboBox, IDataGridViewEditingControl
    {
        public DataGridViewComboxEditingControl()
        {
            DropDownStyle = ComboBoxStyle.DropDown;
        }

        #region IDataGridViewEditingControl Members

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (ModifierKeys == Keys.Control)
                return !dataGridViewWantsInputKey;
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Escape:
                    return !dataGridViewWantsInputKey;
                default:
                    return true;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            DropDownStyle = ComboBoxStyle.DropDown;
            if (selectAll)
                Select(0, Text.Length);
        }

        public DataGridView EditingControlDataGridView { get; set; }

        public object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(
                    DataGridViewDataErrorContexts.Formatting);
            }
            set { Text = (string) value; }
        }

        public int EditingControlRowIndex { get; set; }

        public bool EditingControlValueChanged { get; set; }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        #endregion
    }
}