using System;
using System.ComponentModel;
using System.Globalization;

namespace DataPicker.Modules.CsvModule.Converter;

public class BooleanConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string s)
        {
            return s == "1";
        }
        return base.ConvertFrom(context, culture, value);
    }
}