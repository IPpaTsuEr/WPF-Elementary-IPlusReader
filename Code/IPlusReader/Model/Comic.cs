using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusReader.Model
{
    public class Comic : Notify
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    ChangeNotify("Name");
                }
            }
        }
        private string _cover;

        public string Cover
        {
            get { return _cover; }
            set
            {
                if (value != _cover)
                {
                    _cover = value;
                    ChangeNotify("Cover");
                }
            }
        }

        private string _info;

        public string Info
        {
            get { return _info; }
            set
            {
                if (value != _info)
                {
                    _info = value;
                    ChangeNotify("Info");
                }
            }
        }

        private string _Synopsis;

        public string Synopsis
        {
            get { return _Synopsis; }
            set
            {
                if (value != _Synopsis)
                {
                    _Synopsis = value;
                    ChangeNotify("Synopsis");
                }
            }
        }

        private bool _IsComic;

        public bool IsComic
        {
            get { return _IsComic; }
            set
            {
                if (value != _IsComic)
                {
                    _IsComic = value;
                    ChangeNotify("IsComic");
                }
            }
        }

        private ObservableCollection<ComicItem> _subs;

        public ObservableCollection<ComicItem> Subs
        {
            get { return _subs; }
            set
            {
                if (value != _subs)
                {
                    _subs = value;
                    ChangeNotify("Subs");
                }
            }
        }
        
    }

    public class ComicItem:Notify
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    ChangeNotify("Name");
                }
            }
        }

        private ObservableCollection<string> _subs;

        public ObservableCollection<string> Subs
        {
            get { return _subs; }
            set
            {
                if (value != _subs)
                {
                    _subs = value;
                    ChangeNotify("Subs");
                }
            }
        }
    }

    public class Notify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeNotify(string name)
        {
            if(PropertyChanged!=null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
