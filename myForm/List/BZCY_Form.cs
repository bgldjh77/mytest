using AntdUI;
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

namespace 质检工具
{
    //标准查阅
    public partial class BZCY_Form : AntdUI.Window
    {
        Dictionary<string, string> mufilelist;
        public BZCY_Form()
        {
            InitializeComponent();
            GetMuFileList();          
        }

        private void BZCY_Form_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            add2tree();
        }
        //读取模板资源
        private void GetMuFileList()
        {
            mufilelist = new Dictionary<string, string>();
            if (Directory.Exists(globalpara.mu_standard))
            {
                string[] entries = Directory.GetFileSystemEntries(globalpara.mu_standard);
                foreach (string entry in entries)
                {
                    string name = Path.GetFileName(entry);
                    mufilelist.Add(name, entry); // 存储完整路径和文件名
                }
            }
        }
   
        //添加节点到tree
        private void add2tree()
        {
            foreach (var kvp in mufilelist)
            {
                TreeItem item = new TreeItem(kvp.Key);
                item.Tag = kvp.Value;
                tree1.Items.Add(item);
            }
        }
        //批量下载
        private void uiButton1_Click(object sender, EventArgs e)
        {
            List<string> fullpathls = new List<string>();
            foreach (TreeItem item in tree1.Items)
            {
                if (item.Checked) { fullpathls.Add((string)item.Tag); }              
            }

            if (fullpathls.Count == 0) { return; }
            SelectFileFunc sf = new SelectFileFunc();
            string targetfolderPath = sf.sf_folder();
            if (Directory.Exists(targetfolderPath))
            {
                foreach(string fullPath in fullpathls) { myCopyFunc.CopyFileOrDirectory(fullPath, targetfolderPath); }
            }
            
        }


        private void tree1_NodeMouseClick(object sender, TreeSelectEventArgs e)
        {
            if(!ckc)
            {
                string fullPath = (string)e.Item.Tag;
                System.Diagnostics.Process.Start(fullPath);
            }

            ckc = false;
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            if (selectAll_btn.Text == "全选")
            {
                selectAll_btn.Text = "取消全选";
                foreach (TreeItem item in tree1.Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                selectAll_btn.Text = "全选";
                foreach (TreeItem item in tree1.Items)
                {
                    item.Checked = false;
                }
            }
        }
        bool ckc = false;
        private void tree1_CheckedChanged(object sender, TreeCheckedEventArgs e)
        {
            ckc = true;
        }
    }
}
