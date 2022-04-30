using System;
using System.Globalization;
using System.Windows.Data;
using BCC.Pharm.Shared;

namespace BCC.Pharm.App.Converters
{
    public class SourceChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SourceChange sourceChange)
            {
                switch (sourceChange)
                {
                    case SourceChange.Automatically:
                        return "Автоматическое";
                    case SourceChange.Manual:
                        return "Ручное";
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}