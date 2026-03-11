namespace MAUI_UI.Converters
{
    using System.Globalization;

    public class NullableIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
                return intValue.ToString();

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str && int.TryParse(str, out var result))
                return result;

            return null;
        }
    }
}

