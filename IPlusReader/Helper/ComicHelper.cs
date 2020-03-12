using IPlusReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPlusReader.Helper
{
    class ComicHelper
    {
        public static int GetValue(string name)
        {
            if(name.IndexOf(" ") >= 1)
            {
                name = name.Split(' ')[0];
            }
            StringBuilder st = new StringBuilder(10);
            foreach (var item in name)
            {
                if (item >= '0' && item <= '9') st.Append(item);
            }
            return st.Length == 0 ? -1 : System.Convert.ToInt32(st.ToString());
        }

        public static bool Load(string path, ICollection<Comic> list)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo DF = new DirectoryInfo(path);

                var _new = new Comic();
                _new.Name = DF.Name;
                
                try { _new.Cover = DF.GetFiles("cover.*")[0].FullName; } catch (Exception) { }
                try { _new.Info = DF.GetFiles("info.txt")[0].FullName; } catch (Exception) { }
                try { _new.Synopsis = DF.GetFiles("synopsis.txt")[0].FullName; } catch (Exception) { }
                _new.Subs = new ObservableCollection<ComicItem>();

                foreach (var item in DF.GetDirectories().OrderBy(m => GetValue(m.Name)))
                {
                    var _newsub = new ComicItem();
                    _newsub.Name = item.Name;

                    _newsub.Subs = new ObservableCollection<string>();
                    foreach (var sub in item.GetFiles().OrderBy(f => GetValue(f.Name)))
                    {
                        _newsub.Subs.Add(sub.FullName);
                    }
                    _new.Subs.Add(_newsub);
                }

                if (_new.Subs.Count == 0) _new.IsComic = false;
                else _new.IsComic = true;

                list.Add(_new);
                return true;
            }
            else return false;
        }
    }
}
