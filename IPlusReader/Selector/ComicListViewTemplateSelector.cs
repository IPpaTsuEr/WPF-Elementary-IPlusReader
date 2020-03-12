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
    class ComicListViewTemplateSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var _fe = container as FrameworkElement;
            //Console.WriteLine("Type is {0}",item.GetType());

            if(item is string)
            {
                return _fe.TryFindResource("ComicItemDataTemplate") as DataTemplate;
            }
           else if(item is Comic)
            {
                return _fe.TryFindResource("ComicLibDataTemplate") as DataTemplate;
               
            }
           else if(item is ComicItem)
            {
                return  _fe.TryFindResource("ComicDataTemplate") as DataTemplate;
                
            }
            return base.SelectTemplate(item, container);
        }
    }
}
