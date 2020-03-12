using IPlusReader.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IPlusReader
{
    /// <summary>
    /// ViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class ViewPage : Page
    {
        public ViewPage()
        {
            InitializeComponent();
            IsVisibleChanged += ViewPage_IsVisibleChanged;
        }

        private void ViewPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GC.Collect(2);
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            View_List.ScrollIntoView(View_List.SelectedItem);
        }

        private void ListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int count = View_List.Items.Count;
            if (count > 0)
            {
                var _sv = ViewHelper.GetListVIewScrollView(View_List);

                for (int i = 0; i < count; i++)
                {
                    var item = (View_List.ItemContainerGenerator.ContainerFromIndex(i)) as ListViewItem;

                    if (item != null)
                    {
                        var _p = item.TranslatePoint(new Point(0, 0), View_List);
                        //跨视口边界
                        if (_p.Y < 0 && (_p.Y + (item as FrameworkElement).ActualHeight) > 0)
                        {
                            if ((_p.Y + (item as FrameworkElement).ActualHeight) == _sv.ViewportHeight)//刚好填满
                            {
                                //Console.WriteLine("index:{0}", i);
                                View_List.SelectedIndex = i;
                            }
                            else
                            {
                                if ((_p.Y + (item as FrameworkElement).ActualHeight) / _sv.ViewportHeight < 0.5)//视口占用太少，焦点设置在下一项
                                {
                                    //Console.WriteLine("index:{0}", i + 1 != count ? i + 1 : i);
                                    View_List.SelectedIndex = i + 1 != count ? i + 1 : i;
                                }
                                else
                                {
                                    //Console.WriteLine("index:{0}", i);
                                    View_List.SelectedIndex = i;
                                }
                            }
                            return;
                        }
                        //在视口内
                        else if (_p.Y > 0 && (_p.Y + (item as FrameworkElement).ActualHeight) <= _sv.ViewportHeight)
                        {
                            //Console.WriteLine("index:{0}",i);
                            View_List.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }
        }
        private ScrollViewer sv;
        public void ToTop()
        {
            View_List.SelectedIndex=0;
            View_List.ScrollIntoView(View_List.SelectedItem);
            if (sv == null) sv = ViewHelper.GetListVIewScrollView(View_List);
            sv?.ScrollToTop();


        }
    }
}
