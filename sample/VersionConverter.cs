using System.Globalization;

namespace Maui.AppStores.SampleApp;

public class VersionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Version version)
        {
            return version.ToString();
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string versionString)
        {
            return Version.TryParse(versionString, out var version)
                ? version
                : new Version();
        }

        return value;
    }
}