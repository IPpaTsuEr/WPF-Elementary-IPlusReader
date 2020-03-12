using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IPlusReader.Convert
{
    class TextFileLoadConvert : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value is string)
            {
                if(File.Exists(value as string))
                {
                   return  File.ReadAllText(value as string).Replace("  ","").Replace("\n\n","\n");
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
