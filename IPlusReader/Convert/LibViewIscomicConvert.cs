using IPlusReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IPlusReader.Convert
{
    class LibViewIscomicConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value is string)
            {
                try
                {
                    if (System.Convert.ToBoolean(value as string)) return "漫画";
                    else return "电影";
                }
                catch (Exception)
                {
                    return "N/A";
                }
            }
            else if(value is bool)
            {
                if ((bool)value)
                    return "漫画";
                else return "电影";
            }
            return  "N/A"; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
