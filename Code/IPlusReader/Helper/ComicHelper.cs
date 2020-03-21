using IPlusReader.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IPlusReader.Helper
{
    class ComicHelper
    {

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);


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
                var _DFD = DF.GetDirectories().ToList();
                _DFD.Sort((a,b)=>
                {
                    if (string.IsNullOrEmpty(a.Name) && string.IsNullOrEmpty(b.Name)) return 0;
                    if (string.IsNullOrEmpty(a.Name)) return -1;
                    if (string.IsNullOrEmpty(b.Name)) return 1;
                    return StrCmpLogicalW(a.Name, b.Name);
                });
                foreach (var item in _DFD)
                {
                    var _newsub = new ComicItem();
                    _newsub.Name = item.Name;

                    _newsub.Subs = new ObservableCollection<string>();
                    var _FL = item.GetFiles().ToList();
                    _FL.Sort((a, b) => {
                        return StrCmpLogicalW(a.Name, b.Name);
                      });
                    foreach (var sub in _FL)
                    {
                        if("|.jpg|.png|.bmp|.gif|.jpeg".IndexOf(sub.Extension)>0)
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
