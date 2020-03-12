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
    class ComicTreeViewTemplateSelector:DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            var _fe = container as FrameworkElement;
            if (item is string) {
                return _fe.TryFindResource("ComicItemTreeViewDataTemplate") as DataTemplate; 
            }
            else if(item is Comic) {
                return _fe.TryFindResource("ComicLibTreeViewDataTemplate") as DataTemplate;
                 }
            else if(item is ComicItem) {
                return _fe.TryFindResource("ComicTreeViewDataTemplate") as DataTemplate;
                }
            return base.SelectTemplate(item, container);
        }
    }
}
