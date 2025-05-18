#region

using System;
using System.Collections.Generic;

#endregion

namespace cope.PropertyHelper
{
    public abstract class PropertyInfoBase
    {
        protected PropertyInfoBase()
        {
            Attributes = new List<Attribute>();
        }

        protected PropertyInfoBase(Type type, Type editorType = null, Type converterType = null)
            : this()
        {
            TypeName = type.AssemblyQualifiedName;
            if (editorType != null)
                EditorType = editorType.AssemblyQualifiedName;
            if (converterType != null)
                ConverterType = converterType.AssemblyQualifiedName;
        }

        public virtual string Name { get; set; }
        public string Category { get; set; }
        public string EditorType { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string ConverterType { get; set; }
        public List<Attribute> Attributes { get; private set; }

        public abstract object GetDefaultValue();

        internal abstract void InvokeOnGetValue(CustomPropertyEventArgs e);

        internal abstract void InvokeOnSetValue(CustomPropertyEventArgs e);
    }
}