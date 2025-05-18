#region

using System;

#endregion

namespace cope.PropertyHelper
{
    public class CustomPropertyInfo<T> : PropertyInfoBase
    {
        public CustomPropertyInfo(Type editorType = null, Type converterType = null)
            : base(typeof (T), editorType, converterType)
        {
        }

        public T DefaultValue { get; set; }

        public event EventHandler<CustomPropertyEventArgs<T>> OnGetValue;

        public override object GetDefaultValue()
        {
            return DefaultValue;
        }

        internal override void InvokeOnGetValue(CustomPropertyEventArgs e)
        {
            var args = new CustomPropertyEventArgs<T>(default(T), e.PropertyInfo);
            if (OnGetValue != null) OnGetValue(this, args);
            e.Value = args.Value;
        }

        public event EventHandler<CustomPropertyEventArgs<T>> OnSetValue;

        internal override void InvokeOnSetValue(CustomPropertyEventArgs e)
        {
            var args = new CustomPropertyEventArgs<T>((T) e.Value, e.PropertyInfo);
            if (OnSetValue != null) OnSetValue(this, args);
        }
    }
}