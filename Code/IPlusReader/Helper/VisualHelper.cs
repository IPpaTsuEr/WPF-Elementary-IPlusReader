using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace IPlusReader.Helper
{
    class VisualHelper
    {
        public static object GetTargetChildren(DependencyObject dp,Type type)
        {
            var _c = VisualTreeHelper.GetChildrenCount(dp);
            for (int i = 0; i < _c; i++)
            {
                var _item = VisualTreeHelper.GetChild(dp,i);
                if (_item.GetType() == type) return _item;
                else GetTargetChildren(_item,type);
            }
            return null;
        }



        public static DependencyObject FindVisualChild(DependencyObject DP, string TargetName, Type TargetType)
        {
            bool IsTarget = false;
            var c = VisualTreeHelper.GetChildrenCount(DP);
            for (int i = 0; i < c; i++)
            {
                var Item = VisualTreeHelper.GetChild(DP, i);
                if (TargetName != null && TargetType != null)
                {
                    if (((FrameworkElement)Item).Name == TargetName && Item.GetType() == TargetType) IsTarget = true;
                }
                else if (TargetName != null && ((FrameworkElement)Item).Name == TargetName) IsTarget = true;
                else if (TargetType != null && Item.GetType() == TargetType) IsTarget = true;
                else IsTarget = false;

                if (IsTarget != false) return Item;
            }
            for (int i = 0; i < c; i++)
            {
                var tg = FindVisualChild(VisualTreeHelper.GetChild(DP, i), TargetName, TargetType);
                if (tg != null) return tg;
            }
            return null;
        }

        public static DependencyObject FindVisualParent(DependencyObject DP, string TargetName, Type TargetType)
        {
            while ((DP = VisualTreeHelper.GetParent(DP)) != null)
            {
                if (TargetType != null && TargetName == null && DP.GetType() == TargetType) return DP;
                if (TargetType == null && TargetName != null && ((FrameworkElement)DP).Name == TargetName) return DP;
                if (TargetType != null && TargetName != null && DP.GetType() == TargetType && ((FrameworkElement)DP).Name == TargetName) return DP;

            }
            return null;
        }
    }
}
