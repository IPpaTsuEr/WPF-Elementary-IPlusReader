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
    /// InfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoPage : Page
    {


        public InfoPage()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler OnInfoItemDoubleClicked
        {
            add { AddHandler(InfoItemDoubleClickedEven,value); }
            remove { RemoveHandler(InfoItemDoubleClickedEven,value); }
        }

        public  RoutedEvent InfoItemDoubleClickedEven = EventManager.RegisterRoutedEvent("InfoItemDoubleClicked", RoutingStrategy.Bubble, typeof(ComicItem), typeof(InfoPage));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // Console.WriteLine("list ====>{0}",(e.OriginalSource as Button).DataContext.GetType());
            RaiseEvent(new RoutedEventArgs(InfoItemDoubleClickedEven, (e.OriginalSource as Button).DataContext));
        }
    }
}
