#region

using System;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public class DataGridViewNumTextBoxColumn :
        DataGridViewColumn
    {
        public DataGridViewNumTextBoxColumn()
            : base(new DataGridViewNumTextBoxCell())
        {
        }

        /// <exception cref="InvalidCastException"><c>InvalidCastException</c>.</exception>
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if (!(value is DataGridViewNumTextBoxCell))
                {
                    throw new InvalidCastException(
                        string.Empty);
                }
                base.CellTemplate = value;
            }
        }
    }
}