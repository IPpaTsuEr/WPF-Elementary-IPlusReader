using ExToolKit;
using IPlusReader.Helper;
using IPlusReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : ExWindow
    {
        public ObservableCollection<Comic> ComicList { get; set; } = new ObservableCollection<Comic>();
        private LibPage lp;
        private InfoPage ip;
        private MovieInfoPage mip;
        private ViewPage vp;
        private Object Locker = new Object();

        public void LoadComicLib(string lib)
        {
            if (Directory.Exists(lib))
            {
                foreach (var item in Directory.GetDirectories(lib))
                {
                    ComicHelper.Load(item, ComicList);
                }
            }
        }

        public MainWindow()
        {
            BindingOperations.EnableCollectionSynchronization(ComicList, Locker);
            this.DataContext = ComicList;
            InitializeComponent();

            new Thread(() =>
            {
                if (File.Exists("./LibViewSetting.txt"))
                {
                    var _list = File.ReadAllLines("./LibViewSetting.txt");
                    lock (Locker)
                    {
                        foreach (var item in _list)
                        {
                            if (Directory.Exists(item))
                                LoadComicLib(item);
                        }
                    }

                }
                else
                {
                    File.Create("./LibViewSetting.txt");
                    MessageBox.Show("请先在 LibViewSetting.txt 文件中添加库路径。");
                    this.Dispatcher.BeginInvoke(new Action(() => 
                    { System.Diagnostics.Process.Start(System.IO.Path.GetFullPath("./LibViewSetting.txt")); Close(); }));
                }
                
            }).Start();
            GotoLib();
        }

        private void GotoLib()
        {
            if (lp == null)
            {
                lp = new LibPage() { DataContext = ComicList };
                lp.OnLibItemDoubleClicked += Lp_LibItemDoubleClicked;
            }
            Main_Panel.Content = lp;
        }
        private void GotoMoveInfo(Comic comic)
        {
            if (mip == null)
            {
                mip = new MovieInfoPage();
            }
            mip.DataContext = comic;
            Main_Panel.Content = mip;
        }
        private void GotoComicInfo(Comic comic)
        {
            if (ip == null)
            {
                ip = new InfoPage();
                ip.OnInfoItemDoubleClicked += Ip_OnInfoItemDoubleClicked;
            }
            ip.DataContext = comic.Subs;
            Main_Panel.Content = ip;
        }
        private void GotoComicView(ComicItem comicItem)
        {
            if (vp == null) vp = new ViewPage();
            vp.DataContext = comicItem?.Subs;
            Main_Panel.Content = vp;
            vp.ToTop();
            GC.Collect(2);
        }

        private void Lp_LibItemDoubleClicked(object sender, RoutedEventArgs e)
        {
            // Main_Panel.Background
            var item = e.OriginalSource as Comic;
            if (item != null)
            {
                if (item.IsComic)
                {
                    GotoComicInfo(item);
                }
                else
                {
                    GotoMoveInfo(item);
                }
            }
        }

        private void Ip_OnInfoItemDoubleClicked(object sender, RoutedEventArgs e)
        {
            var _data = e.OriginalSource as ComicItem;
            GotoComicView(_data);
        }

        private void Side_Bar_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var _Item = Tree_View.SelectedItem;
            if (_Item is Comic) {
                if (((Comic)_Item).IsComic) {
                    GotoComicInfo(_Item as Comic);
                }
                else {
                    GotoMoveInfo(_Item as Comic);
                }
            }
            else if(_Item is ComicItem)
            {
                GotoComicView(_Item as ComicItem);
            }
        }

        double OriginalWidth = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (Side_column.ActualWidth > 32) {
                OriginalWidth = Side_column.ActualWidth;
                Side_column.Width =new GridLength(32);
                Tree_View.Visibility = Visibility.Collapsed;
                Grid_Split.Visibility = Visibility.Collapsed;
            }
            else
            {
                Side_column.Width = new GridLength(OriginalWidth);
                Tree_View.Visibility = Visibility.Visible;
                Grid_Split.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GotoLib();
        }
    }
}
