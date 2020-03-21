using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace IPlusReader.Helper
{
    class ViewHelper
    {
        public static ScrollViewer GetTreeViewScrollViewer(TreeView treeView)
        {
            //对于TreeView而言:
            TreeViewAutomationPeer lvap = new TreeViewAutomationPeer(treeView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap?.Owner as ScrollViewer;
            return scroll;
            //////////////////////////此处添加你想要对TreeView自身滚动条的操作///////////////////////////
           // scroll.ScrollToVerticalOffset(scroll.VerticalOffset+1);    //向下调节垂直滚动条的位置;

        }

        public static ScrollViewer GetListVIewScrollView(ListView listView)
        {
            //对于ListView而言：
            ListViewAutomationPeer lvap = new ListViewAutomationPeer(listView);
            var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
            var scroll = svap?.Owner as ScrollViewer;
            return scroll;
            //////////////////////////此处添加你想要对TreeView自身滚动条的操作///////////////////////////
            //scroll.ScrollToVerticalOffset(scroll.VerticalOffset+1);    //向下调节垂直滚动条的位置;
        }
        

    }
}
