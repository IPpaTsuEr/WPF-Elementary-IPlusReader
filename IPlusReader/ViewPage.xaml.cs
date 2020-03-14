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

        List<int> InViewIndex = new List<int>();

        private void ListView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            int count = View_List.Items.Count;
            if (count > 0)
            {
               if(sv==null ) sv = ViewHelper.GetListVIewScrollView(View_List);
                InViewIndex.Clear();
                bool FindFirst = false;

                for (int i = 0 ; i < count; i++)
                {
                    var item = (View_List.ItemContainerGenerator.ContainerFromIndex(i)) as ListViewItem;

                    if (item != null)
                    {
                        var _p = item.TranslatePoint(new Point(0, 0), View_List);
                        //跨视口边界
                        if (_p.Y < 0 && (_p.Y + (item as FrameworkElement).ActualHeight) >= 0)
                        {
                            if (!FindFirst)
                            {
                                if ((_p.Y + (item as FrameworkElement).ActualHeight) == sv.ViewportHeight)//刚好填满
                                {
                                    //Console.WriteLine("index:{0}", i);
                                    View_List.SelectedIndex = i;
                                }
                                else
                                {
                                    if ((_p.Y + (item as FrameworkElement).ActualHeight) / sv.ViewportHeight < 0.5)//视口占用太少，焦点设置在下一项
                                    {
                                        //Console.WriteLine("index:{0}", i + 1 != count ? i + 1 : i);
                                        View_List.SelectedIndex = i + 1 != count ? i + 1 : i;
                                    }
                                    else
                                    {
                                        //
                                        View_List.SelectedIndex = i;
                                    }
                                }
                                FindFirst = true;
                            }
                            InViewIndex.Add(i);

                        }
                        //在视口内
                        else if (_p.Y >= 0 && (_p.Y + (item as FrameworkElement).ActualHeight) <= sv.ViewportHeight)
                        {
                            if (!FindFirst)
                            {
                                View_List.SelectedIndex = i;
                                FindFirst = true;
                            }
                            InViewIndex.Add(i);
                           
                        }
                        //溢出视口
                        else if (_p.Y <= sv.ViewportHeight && (_p.Y + (item as FrameworkElement).ActualHeight) >= sv.ViewportHeight)
                        {
                            if (FindFirst)
                            {
                                View_List.SelectedIndex = i;
                                FindFirst = true;
                            }
                            InViewIndex.Add(i);
                            
                        }
                        //不在视口内
                        else {

                            if (InViewIndex.Count > 0 && InViewIndex.Last() < i)
                            {
                                if (Math.Abs(i - InViewIndex.First()) > 4 || Math.Abs(i - InViewIndex.Last()) > 4)
                                {
                                   // View_List.ItemContainerGenerator
                                    //item.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    //item.Visibility = Visibility.Visible;
                                }
                                //Console.WriteLine("In View Index--------------------");
                                //foreach (var iin in InViewIndex)
                                //{
                                //    Console.WriteLine("{0}", iin);
                                //}
                                //Console.WriteLine("In View Index--------------------");
                                //Console.WriteLine("index:{0}", View_List.SelectedIndex);
                                //return;
                            }
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

            //Console.WriteLine("To Top");
        }

        //由于GIF中timer的运行导致其无法被释放需手动停止后才能释放
        private void View_List_CleanUpVirtualizedItem(object sender, CleanUpVirtualizedItemEventArgs e)
        {
            (e.UIElement as FrameworkElement).Unloaded += ViewPage_Unloaded;
        }

        private void ViewPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var sd = sender as ListViewItem;
            if (sd != null)
            {
                var _t = VisualHelper.FindVisualChild(sd, "Gif_Image", typeof(ExToolKit.GIFImage)) as ExToolKit.GIFImage;
                if (_t != null) _t.ReleaseImages();
            }
        }
    }
}
