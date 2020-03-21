using IPlusReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IPlusReader.Selector
{
    class IsComicSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item is Comic)
            {
                return (container as FrameworkElement).TryFindResource("M_E_DataStyle") as DataTemplate;
            }
            else if (item is ComicItem)
            {
                return (container as FrameworkElement).TryFindResource("C_E_DataStyle") as DataTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
