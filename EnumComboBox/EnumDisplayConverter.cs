using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace EnumComboBox;

/// <summary>
/// Enumに設定されているDisplay属性のNameに変換します
/// </summary>
class EnumDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        DisplayAttribute attr = field.GetCustomAttribute<DisplayAttribute>();
        if (attr != null)
        {
            return attr.Name;
        }

        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}