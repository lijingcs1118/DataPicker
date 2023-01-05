using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;

namespace DataPicker.Modules.CsvModule.Converter;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is true ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility and Visibility.Visible;
    }
}