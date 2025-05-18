#region

using System;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public class DataGridViewNumTextBoxCell :
        DataGridViewTextBoxCell
    {
        public DataGridViewNumTextBoxCell()
        {
            Minimum = 0;
            Maximum = 10000;
            Increment = 1;
        }

        public override Type EditType
        {
            get { return typeof (DataGridViewNumTextBoxEditingControl); }
        }

        public override Type ValueType
        {
            get { return typeof (object); }
        }

        public bool ThousandsSeparator { get; set; }

        public bool Hexadecimal { get; set; }

        public int DecimalPlaces { get; set; }

        public decimal Maximum { get; set; }

        public decimal Minimum { get; set; }

        public decimal Increment { get; set; }

        public override void InitializeEditingControl(
            int rowIndex, object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex,
                                          initialFormattedValue, dataGridViewCellStyle);
            var numBox =
                DataGridView.EditingControl as
                DataGridViewNumTextBoxEditingControl;

            if (numBox != null)
            {
                if ((RowIndex < 0) || (ColumnIndex < 0))
                    return;

                numBox.Minimum = Minimum;
                numBox.Maximum = Maximum;
                numBox.DecimalPlaces = DecimalPlaces;
                numBox.Hexadecimal = Hexadecimal;
                numBox.Increment = Increment;
                numBox.ThousandsSeparator = ThousandsSeparator;

                numBox.Text = initialFormattedValue != null ? initialFormattedValue.ToString() : "0";
            }
        }
    }
}