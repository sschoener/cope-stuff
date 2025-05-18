#region

using System;

#endregion

namespace cope.PropertyHelper
{
    public sealed class CustomPropertyEventArgs<T> : EventArgs
    {
        public CustomPropertyEventArgs(T value, PropertyInfoBase propertyInfo)
        {
            Value = value;
            PropertyInfo = propertyInfo;
        }

        public T Value { get; set; }
        public PropertyInfoBase PropertyInfo { get; private set; }
    }

    public sealed class CustomPropertyEventArgs : EventArgs
    {
        public CustomPropertyEventArgs(object value, PropertyInfoBase propertyInfo)
        {
            Value = value;
            PropertyInfo = propertyInfo;
        }

        public object Value { get; set; }
        public PropertyInfoBase PropertyInfo { get; private set; }
    }
}