using IPlusReader.Model;
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
    /// LibPage.xaml 的交互逻辑
    /// </summary>
    public partial class LibPage : Page
    {
        private PropertyGroupDescription typePGD = new PropertyGroupDescription("IsComic");
        public LibPage()
        {
            InitializeComponent();
            Loaded += LibPage_Loaded;
        }

        private void LibPage_Loaded(object sender, RoutedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(DataContext);
            if(!view.GroupDescriptions.Contains(typePGD))
                view.GroupDescriptions.Add(typePGD);
        }

        public event RoutedEventHandler OnLibItemDoubleClicked 
        {
            add { AddHandler(LibItemDoubleClicked, value); }
            remove { RemoveHandler(LibItemDoubleClicked, value); }
        }

        public static readonly RoutedEvent LibItemDoubleClicked =EventManager.RegisterRoutedEvent("LibItemDoubleClicked ", RoutingStrategy.Bubble,typeof(Comic),typeof(LibPage));
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            RaiseEvent(new RoutedEventArgs(LibItemDoubleClicked, Lib_List.SelectedItem as Comic));
        }
    }
}
