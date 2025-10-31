using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.SysFunc;
using 质检工具.SysFunc;
using static 质检工具.Func.AntdTableFunc.MuDownload;

namespace 质检工具
{
    public partial class MuDownloadForm : AntdUI.Window
    {

        Dictionary<string, string> mufilelist; // 修改为字典类型，key为文件名，value为完整路径

        public MuDownloadForm()
        {
            InitializeComponent();
            GetMuFileList();
            
            table1.Columns = new AntdUI.ColumnCollection
            {
                new AntdUI.ColumnCheck("check").SetFixed(),
                new AntdUI.Column("index", "序号", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
                new AntdUI.Column("filename", "模板名称", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
                new AntdUI.Column("btns", "操作", AntdUI.ColumnAlign.Center).SetFixed().SetWidth("auto").SetLocalizationTitleID("Table.Column."),
            };

            // 设置分页参数
            pagination1.Total = mufilelist.Count;
            pagination1.PageSize = 10;
            pagination1.Current = 1;

            // 初始加载第一页数据
            LoadPageData(pagination1.Current, pagination1.PageSize);

            // 添加分页事件处理
            pagination1.ValueChanged += Pagination1_ValueChanged;
        }
        //设置数据源 分页形式
        private void LoadPageData(int current, int pageSize)
        {
            var list = new List<TemplateItem>();
            var pageData = mufilelist.Skip((current - 1) * pageSize).Take(pageSize);
            int startIndex = (current - 1) * pageSize;

            foreach (var item in pageData)
            {
                list.Add(new TemplateItem(startIndex, item.Key));
                startIndex++;
            }

            table1.DataSource = list;
        }

        private void Pagination1_ValueChanged(object sender, AntdUI.PagePageEventArgs e)
        {
            LoadPageData(e.Current, e.PageSize);
        }

        private void MuDownloadForm_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            table1.EnableHeaderResizing = true;//允许调整表头宽度
            //pagination1.ShowTotal = true; // 显示总数

        }
        //读取模板资源
        private void GetMuFileList()
        {
            mufilelist = new Dictionary<string, string>();
            if (Directory.Exists(globalpara.mu_markpath))
            {
                string[] entries = Directory.GetFileSystemEntries(globalpara.mu_markpath);
                foreach (string entry in entries)
                {
                    string name = Path.GetFileName(entry);
                    mufilelist.Add(name, entry); // 存储完整路径和文件名
                }
            }
        }
        
        //单个下载
        private void Table1_CellButtonClick(object sender, AntdUI.TableButtonEventArgs e)
        {
            if (e.Btn.Id == "download")
            {
                var row = (table1.DataSource as List<TemplateItem>)[e.RowIndex-1];
                string fullPath = mufilelist[row.filename];

                // 选择目标文件夹
                SelectFileFunc sf = new SelectFileFunc();
                string targetPath= sf.sf_folder();
                if (Directory.Exists(targetPath))
                {
                    myCopyFunc.CopyFileOrDirectory(fullPath, targetPath);
                    //MessageBox.Show("复制完成！");
                    if (File.Exists(targetPath) | Directory.Exists(targetPath))
                        LogHelper.normallog("已下载模板文件：\n"+ row.filename );
                    
                    this.Focus();
                }
            }
        }

        //批量下载
        private void uiButton1_Click(object sender, EventArgs e)
        {
            var rowlist = table1.DataSource as List<TemplateItem>;
            if (rowlist.Count == 0)
            {
                return;
            }
            // 选择目标文件夹
            SelectFileFunc sf = new SelectFileFunc();
            string targetfolderPath = sf.sf_folder();
            if (Directory.Exists(targetfolderPath))
            {
                //var rowlist = table1.DataSource as List<TemplateItem>;
                foreach (TemplateItem item in rowlist)
                {
                    if(item.check)
                    {
                        string fullPath = mufilelist[item.filename];
                        myCopyFunc.CopyFileOrDirectory(fullPath, targetfolderPath);
                    }
                    //Console.WriteLine(item.filename + ":" + item.check);
                }
                LogHelper.normallog("模板文件已下载");
            }
                
        }
    }
}
