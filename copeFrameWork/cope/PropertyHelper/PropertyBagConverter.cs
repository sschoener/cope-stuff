#region

using System;
using System.ComponentModel;

#endregion

namespace cope.PropertyHelper
{
    public sealed class PropertyBagConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
                                         object value, Type destinationType)
        {
            if (destinationType == typeof (string))
            {
                PropertyBag propBag = value as PropertyBag;
                return propBag.DisplayedValue;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value,
                                                                   Attribute[] attributes)
        {
            PropertyBag propBag = value as PropertyBag;
            var props = propBag.GetProperties(attributes);
            return props;
        }
    }
}