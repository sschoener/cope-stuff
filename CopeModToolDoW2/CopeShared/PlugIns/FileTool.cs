using System.Windows.Forms;
using cope;
using cope.DawnOfWar2;

namespace ModTool.Core.PlugIns
{
    public abstract partial class FileTool : UserControl
    {
        #region fields

        bool m_hasChanges;

        #endregion fields

        #region ctors

        protected FileTool()
        {
            InitializeComponent();
        }

        #endregion ctors

        #region methods

        /// <summary>
        /// This method will be called when this instance of the plugin is asked to be closed, thus giving it the possibility to finish things off.
        /// If this method returns false this instance of the plugin is not going to be closed.
        /// </summary>
        /// <returns></returns>
        public virtual bool Close()
        {
            return true;
        }

        /// <summary>
        /// This method is called upon closing the editor if there are still unsaved changes.
        /// </summary>
        public virtual void SaveFile()
        {
        }

        #endregion methods

        #region properties

        /// <summary>
        /// Gets whether the file this instance is operating on has changes.
        /// </summary>
        public virtual bool HasChanges
        {
            get
            {
                return m_hasChanges;
            }
            protected set
            {
                bool old = m_hasChanges;
                m_hasChanges = value;
                if (old != m_hasChanges && OnHasChangesChanged != null)
                    OnHasChangesChanged(this, value);
            }
        }

        /// <summary>
        /// Gets the file this instance of the plugin is operating on.
        /// </summary>
        public abstract UniFile File
        {
            get;
        }

        #endregion properties

        #region events

        public event FileActionEventHandler OnSaved;

        virtual protected void InvokeOnSaved(object sender, FileActionEventArgs e)
        {
            if (OnSaved != null)
                OnSaved(sender, e);
        }

        public event GenericHandler<bool> OnHasChangesChanged;

        virtual protected void InvokeOnHasChangesChanged(object sender, bool value)
        {
            if (OnHasChangesChanged != null)
                OnHasChangesChanged(sender, value);
        }

        #endregion events
    }
}