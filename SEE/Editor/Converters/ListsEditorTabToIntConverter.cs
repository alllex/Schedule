using System;
using System.Windows.Data;
using Editor.ViewModels;

namespace Editor.Converters
{
    public class ListsEditorTabToIntConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int) ((ListsEditorTab) value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (ListsEditorTab) ((int) value);
        }
    }
}
