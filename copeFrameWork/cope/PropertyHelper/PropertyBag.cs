#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#endregion

namespace cope.PropertyHelper
{
    [TypeConverter(typeof (PropertyBagConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public class PropertyBag : ICustomTypeDescriptor
    {
        public PropertyBag()
        {
            Properties = new List<PropertyInfoBase>();
        }

        public List<PropertyInfoBase> Properties { get; private set; }

        public string DisplayedValue { get; set; }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            var properties = new CustomPropertyDescriptor[Properties.Count];
            int idx = 0;
            foreach (var prop in Properties)
            {
                List<Attribute> attribs = new List<Attribute>();
                if (prop.Category != null)
                    attribs.Add(new CategoryAttribute(prop.Category));
                if (prop.Description != null)
                    attribs.Add(new DescriptionAttribute(prop.Description));
                if (prop.EditorType != null)
                    attribs.Add(new EditorAttribute(prop.EditorType, typeof (UITypeEditor)));
                if (prop.ConverterType != null)
                    attribs.Add(new TypeConverterAttribute(prop.ConverterType));
                if (prop.Attributes != null)
                    attribs.AddRange(prop.Attributes);
                properties[idx] = new CustomPropertyDescriptor(prop, prop.Name, attribs.ToArray());
                idx++;
            }
            return new PropertyDescriptorCollection(properties);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        #region Nested type: CustomPropertyDescriptor

        private class CustomPropertyDescriptor : PropertyDescriptor
        {
            private readonly PropertyInfoBase m_propertyInfo;

            public CustomPropertyDescriptor(PropertyInfoBase prop, string name,
                                            Attribute[] attributes)
                : base(name, attributes)
            {
                m_propertyInfo = prop;
            }

            public override Type ComponentType
            {
                get { return m_propertyInfo.GetType(); }
            }

            public override bool IsReadOnly
            {
                get { return Attributes.Matches(ReadOnlyAttribute.Yes); }
            }

            public override Type PropertyType
            {
                get { return Type.GetType(m_propertyInfo.TypeName); }
            }

            public override bool CanResetValue(object component)
            {
                if (m_propertyInfo.GetDefaultValue() == null)
                    return false;
                var val = GetValue(component);
                if (val == null)
                    return false;
                return !val.Equals(m_propertyInfo.GetDefaultValue());
            }

            public override object GetValue(object component)
            {
                CustomPropertyEventArgs e = new CustomPropertyEventArgs(null, m_propertyInfo);
                m_propertyInfo.InvokeOnGetValue(e);
                return e.Value;
            }

            public override void ResetValue(object component)
            {
                SetValue(component, m_propertyInfo.GetDefaultValue());
            }

            public override void SetValue(object component, object value)
            {
                CustomPropertyEventArgs e = new CustomPropertyEventArgs(value, m_propertyInfo);
                m_propertyInfo.InvokeOnSetValue(e);
            }

            public override bool ShouldSerializeValue(object component)
            {
                object val = GetValue(component);
                if (val == null || m_propertyInfo.GetDefaultValue() == null)
                    return false;
                return !val.Equals(m_propertyInfo.GetDefaultValue());
            }
        }

        #endregion
    }
}