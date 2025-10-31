using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.AntdTableFunc
{
    class MuDownload
    {
        public class TemplateItem : AntdUI.NotifyProperty
        {
            public int id { get; set; }
            public string filename { get; set; }

            private bool _check = false;
            public bool check
            {
                get => _check;
                set
                {
                    if (_check == value) return;
                    _check = value;
                    OnPropertyChanged();
                }
            }

            private string _index;
            public string index
            {
                get => _index;
                set
                {
                    if (_index == value) return;
                    _index = value;
                    OnPropertyChanged();
                }
            }

            private AntdUI.CellLink[] _btns;
            public AntdUI.CellLink[] btns
            {
                get => _btns;
                set
                {
                    _btns = value;
                    OnPropertyChanged();
                }
            }

            public TemplateItem(int id, string filename)
            {
                this.id = id;
                this.filename = filename;
                this.index = (id + 1).ToString();
                this.btns = new AntdUI.CellLink[] { new AntdUI.CellLink("download", "下载") };
            }
        }
    }
}
