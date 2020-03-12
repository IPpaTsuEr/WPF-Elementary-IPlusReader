using IPlusReader.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace IPlusReader.Convert
{
    class TreeViewExpandedConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                if ((bool)value) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class TreeViewExpandeButtonVisiableConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;
            else if(value is ICollection)
            {
                if (((ICollection)value).Count == 0) return Visibility.Hidden;
                else return Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class TreeViewExpandeMarginConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is TreeViewItem)) return new System.Windows.Thickness(0,0,0,0);
            else
            {
                var P = VisualHelper.FindVisualParent((TreeViewItem)value, null, typeof(TreeViewItem));
                if (P != null)
                {
                    var C = VisualHelper.FindVisualChild(P, "Head_Grid", typeof(Grid)) as Grid;
                    if (C != null) return new Thickness(C.Margin.Left + 10, 0, 0, 0);
                }
               
            }
            return new System.Windows.Thickness(0, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
