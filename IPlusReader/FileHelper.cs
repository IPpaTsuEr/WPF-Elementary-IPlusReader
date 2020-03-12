using IPlusReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusReader
{
    public class FileNode
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extention { get; set; }
        public bool IsFile { get; set; }
        public long LastWriteTime { get; set; }
        public ObservableCollection<FileNode> Childrens { get; set; }
        //public ObservableCollection<FileNode> Directories { get; set; }
    }
    public class FileHelper
    {
        public static void GetFileInfo(string path, ICollection<FileNode> node)
        {
            if (File.Exists(path))
            {
                FileNode _nf = new FileNode();
                FileInfo fi = new FileInfo(path);
                _nf.LastWriteTime = fi.LastWriteTime.Ticks;
                _nf.IsFile = true;
                _nf.Extention = fi.Extension;
                _nf.Name = fi.Name;
                _nf.Path = fi.FullName;
                //if (node.childrens == null) node.childrens = new ObservableCollection<FileNode>();
                node.Add(_nf);
            }
            else if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                FileNode _nf = new FileNode();
                _nf.Name = di.Name;
                _nf.LastWriteTime = di.LastWriteTime.Ticks;
                _nf.IsFile = false;
                _nf.Path = di.FullName;
                //if (node.childrens == null) node.childrens = new ObservableCollection<FileNode>();
                _nf.Childrens = new ObservableCollection<FileNode>();
                node.Add(_nf);

                var _f = Directory.GetFiles(path);
                foreach (var subfile in _f)
                {
                    GetFileInfo(subfile, _nf.Childrens);
                }
                var _d = Directory.GetDirectories(path);
                foreach (var subfloder in _d)
                {
                    GetFileInfo(subfloder, _nf.Childrens);
                }
            }
        }
    }

}
