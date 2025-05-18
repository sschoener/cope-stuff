using System.IO;

namespace cope.Relic.Module
{
    public abstract class ModuleSection : IGenericClonable<ModuleSection>
    {
        #region fields

        protected string m_sectionName;

        #endregion fields

        #region methods

        public abstract bool Exists(string key, string value);

        public abstract void Add(string key, string value);

        public override string ToString()
        {
            return m_sectionName;
        }

        #endregion methods

        #region properties

        public string SectionName
        {
            get { return m_sectionName; }
        }

        #endregion properties

        public abstract void WriteToTextStream(TextWriter tw);

        public abstract void GetFromTextStream(TextReader tr);

        #region IGenericClonable<ModuleSection> Member

        public abstract ModuleSection GClone();

        #endregion IGenericClonable<ModuleSection> Member
    }
}