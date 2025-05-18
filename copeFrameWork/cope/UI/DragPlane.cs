#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace cope.UI
{
    public partial class DragPlane : UserControl
    {
        #region fields

        #region Delegates

        public delegate void SelectionChangedEventHandler(
            object sender, IDraggable newSelection, IDraggable oldSelection);

        #endregion

        private readonly GridOverlaySized m_grid;

        private IDraggable m_currentMove;
        private Point m_oldPosition;

        #endregion fields

        #region ctors

        public DragPlane()
        {
            InitializeComponent();
            m_grid = new GridOverlaySized(20, 20);
        }

        #endregion ctors

        #region methods

        internal void InvokeMouseClick(MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        internal void InvokeMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        internal void InvokeMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetCurrentMoveByLocation(e.Location);
                if (m_currentMove != null)
                    return;
            }
            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetCurrentMoveByLocation(e.Location);
                if (m_currentMove != null)
                {
                    m_oldPosition = e.Location;
                    return;
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_currentMove != null)
                {
                    Point newPosition = e.Location;
                    int x = m_currentMove.PosX + newPosition.X - m_oldPosition.X;
                    int y = m_currentMove.PosY + newPosition.Y - m_oldPosition.Y;
                    if (x < 0)
                        m_currentMove.PosX = 0;
                    else
                    {
                        int deltaWidth = Width - m_currentMove.Size.Width;
                        m_currentMove.PosX = x > deltaWidth ? deltaWidth : x;
                    }
                    if (y < 0)
                        m_currentMove.PosY = 0;
                    else
                    {
                        int deltaHeight = Height - m_currentMove.Size.Height;
                        m_currentMove.PosY = y > deltaHeight ? deltaHeight : y;
                    }
                    m_oldPosition = e.Location;
                    Invalidate();
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            m_grid.Render(e.Graphics, e.ClipRectangle);
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected void SetCurrentMoveByLocation(Point loc)
        {
            Control c = GetChildAtPoint(loc,
                                        GetChildAtPointSkip.Transparent | GetChildAtPointSkip.Invisible |
                                        GetChildAtPointSkip.Disabled);
            if (c != null && c is IDraggable)
            {
                var d = c as IDraggable;
                loc = PointToScreen(loc);
                loc = c.PointToClient(loc);
                if (!d.IsInDragSection(loc))
                    return;
                if (OnSelectionChanging != null)
                    OnSelectionChanging(this, d, m_currentMove);
                if (m_currentMove != null)
                    m_currentMove.IsSelected = false;
                IDraggable old = m_currentMove;
                m_currentMove = d;
                (c as IDraggable).IsSelected = true;
                if (OnSelectionChanged != null)
                    OnSelectionChanged(this, d, old);
            }
            else
                m_currentMove = null;
        }

        #endregion methods

        #region properties

        #endregion properties

        #region events

        public event SelectionChangedEventHandler OnSelectionChanged;
        public event SelectionChangedEventHandler OnSelectionChanging;

        #endregion events
    }
}