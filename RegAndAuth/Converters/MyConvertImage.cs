using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;
using Avalonia.Media.Imaging;
using System.IO;

namespace RegAndAuth.Converters
{
    internal class MyConvertImage : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null ? new Bitmap(AssetLoader.Open(new Uri("avares://RegAndAuth/Assets/Net_foto.png"))) : new Bitmap(new MemoryStream((byte[])value));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
