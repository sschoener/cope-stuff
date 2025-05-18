#region

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public partial class DragBox<T> : UserControl, IDraggable
    {
        #region fields

        private T m_data;
        private Point m_position;

        #endregion fields

        #region ctors

        public DragBox()
        {
            InitializeComponent();
            _pnl_header.MouseClick += DragBoxMouseClick;
            _pnl_header.MouseDown += DragBoxMouseDown;
            _pnl_header.MouseMove += DragBoxMouseMove;
            m_position = new Point(Left, Top);
            _lab_title.Text = Name;
        }

        #endregion ctors

        #region properties

        public string Title
        {
            get { return _lab_title.Text; }
            set { _lab_title.Text = value; }
        }

        public Color TitleColor
        {
            get { return _lab_title.ForeColor; }
            set
            {
                _lab_title.ForeColor = value;
                _lab_title.Invalidate();
            }
        }

        public Color HeaderColor
        {
            get { return _pnl_header.BackColor; }
            set { _pnl_header.BackColor = value; }
        }

        public T Data
        {
            get { return m_data; }
            set
            {
                m_data = value;
                _lab_title.Text = m_data.ToString();
            }
        }

        public bool IsSelected { get; set; }

        public Point Position
        {
            get { return m_position; }
            set
            {
                m_position = value;
                Left = m_position.X;
                Top = m_position.Y;
            }
        }

        public int PosX
        {
            get { return Left; }
            set
            {
                Left = value;
                m_position.X = value;
            }
        }

        public int PosY
        {
            get { return Top; }
            set
            {
                Top = value;
                m_position.Y = value;
            }
        }

        #endregion properties

        #region eventhandlers

        private void DragBoxMouseClick(object sender, MouseEventArgs e)
        {
            var d = Parent as DragPlane;
            Point loc = e.Location;
            var c = sender as Control;
            loc = c.PointToScreen(loc);
            loc = d.PointToClient(loc);
            var evnt = new MouseEventArgs(e.Button, e.Clicks, loc.X, loc.Y, e.Delta);
            d.InvokeMouseClick(evnt);
        }

        private void DragBoxMouseDown(object sender, MouseEventArgs e)
        {
            var d = Parent as DragPlane;
            Point loc = e.Location;
            var c = sender as Control;
            loc = c.PointToScreen(loc);
            loc = d.PointToClient(loc);
            var evnt = new MouseEventArgs(e.Button, e.Clicks, loc.X, loc.Y, e.Delta);
            d.InvokeMouseDown(evnt);
        }

        private void DragBoxMouseMove(object sender, MouseEventArgs e)
        {
            var d = Parent as DragPlane;
            Point loc = e.Location;
            var c = sender as Control;
            loc = c.PointToScreen(loc);
            loc = d.PointToClient(loc);
            var evnt = new MouseEventArgs(e.Button, e.Clicks, loc.X, loc.Y, e.Delta);
            d.InvokeMouseMove(evnt);
            //d.InvokeMouseMove(e);
        }

        #endregion eventhandlers

        #region methods

        public bool IsInDragSection(Point pos)
        {
            Control c = GetChildAtPoint(pos, GetChildAtPointSkip.Disabled | GetChildAtPointSkip.Invisible);
            if (c == _pnl_header)
                return true;
            return false;
        }

        #endregion methods
    }
}