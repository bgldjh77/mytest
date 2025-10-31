using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.GpTask;
using 质检工具.MenuConfig;
using 质检工具.SysFunc;

namespace 质检工具
{
    public partial class PresetTaskForm : AntdUI.Window
    {
        Dictionary<string, string> mufilelist;
        public PresetTaskForm()
        {
            InitializeComponent();
        }

        private void PresetTaskForm_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            //List<ToolArgs> ta = new List<ToolArgs>();
            GetMuFileList();
            add2tree();
        }
        //添加节点到tree
        private void add2tree()
        {
            tree1.Items.Clear();
            foreach (var kvp in mufilelist)
            {
                TreeItem item = new TreeItem(kvp.Key);
                item.Tag = kvp.Value;

                List<string> toolnamels = GetToolName(kvp.Value);
                if (toolnamels.Count > 0)
                {
                    foreach(string toolname in toolnamels)
                    {
                        TreeItem toolitem = new TreeItem(toolname);
                        item.Sub.Add(toolitem);
                        toolitem.Tag = GetArgls(kvp.Value,  toolname);
                    }                  
                }
                tree1.Items.Add(item);
            }
        }
        //获取工具参数
        private List<ToolArgs> GetArgls(string presetpath,string toolname)
        {
            List<ToolArgs> tals = new List<ToolArgs>();
            using (StreamReader reader = new StreamReader(presetpath, Encoding.GetEncoding("GB2312")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    if (columns.Length >= 10 && toolname == columns[5])
                    {
                        ToolArgs ta = new ToolArgs();
                        ToolArgs tmpta= ta.CreatArgBean(line);
                        tals.Add(tmpta);
                    }
                }
            }
            return tals;
        }

        //获取工具列表
        private List<string> GetToolName(string presetpath)
        {
            List<string> toolnamels = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(presetpath, Encoding.GetEncoding("GB2312")))
                {
                    string line;
                    int rowindex = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (rowindex > 0)
                        {
                            string[] columns = line.Split(',');
                            if (columns.Length >= 10 && !toolnamels.Contains(columns[5]))
                            {
                                toolnamels.Add(columns[5]);
                            }
                        }
                        rowindex += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("读取预设配置csv错误", ex);
            }
            return toolnamels;
        }
        //获取文件列表
        private void GetMuFileList()
        {
            mufilelist = new Dictionary<string, string>();
            if (Directory.Exists(globalpara.mu_standard))
            {
                string[] entries = Directory.GetFileSystemEntries(globalpara.mu_task);
                foreach (string entry in entries)
                {
                    //string name = Path.GetFileName(entry);
                    string name = Path.GetFileNameWithoutExtension(entry);
                    mufilelist.Add(name, entry); // 存储完整路径和文件名
                }
            }
        }

        private void tree1_NodeMouseClick(object sender, TreeSelectEventArgs e)
        {
            if (!ckc)
            {
                if (e.Item.Sub.Count == 0)
                {
                    PresetArg form = Application.OpenForms.OfType<PresetArg>().FirstOrDefault();
                    if (form != null) { form.Close(); }
                    //PresetArg paf = new PresetArg((List<ToolArgs>)e.Item.Tag);
                    //paf.Show();
                }
            }
            ckc = false;
            
        }

        private void tree1_SelectChanged(object sender, TreeSelectEventArgs e)
        {
            tree1.SelectItem.Expand = !tree1.SelectItem.Expand;
        }
        //添加任务
        private async void uiButton1_Click(object sender, EventArgs e)
        {

            foreach (TreeItem treeItem in tree1.Items)
            {
                foreach(TreeItem subitem in treeItem.Sub)
                {
                    if (subitem.Checked)
                    {
                        //List<ToolArgs> tls = (List<ToolArgs>)subitem.Tag;
                        //MenuBean menuBean = new MenuBean
                        //{
                        //    folderName = tls[0].folderName,
                        //    toolboxName = tls[0].toolboxName,
                        //    toolsetName = tls[0].toolsetName,
                        //    toolName = tls[0].toolName
                        //};
                        //GpTaskFunc gtf = new GpTaskFunc();
                        //gtf.AddGpTask(menuBean, tls);

                        Task task = Task.Run(() =>
                        {
                            List<ToolArgs> tls = (List<ToolArgs>)subitem.Tag;
                            MenuBean menuBean = new MenuBean
                            {
                                folderName = tls[0].folderName,
                                toolboxName = tls[0].toolboxName,
                                toolsetName = tls[0].toolsetName,
                                toolName = tls[0].toolName
                            };
                            GpTaskFunc gtf = new GpTaskFunc();
                            gtf.AddGpTask(menuBean, tls);
                            Thread.Sleep(500);
                        });
                        await task;
                        
                    }
                }
            }
        }
        bool ckc = false;
        private void tree1_CheckedChanged(object sender, TreeCheckedEventArgs e)
        {
             ckc = true;
        }
        //打开预设目录
        private void openfolder_btn_Click(object sender, EventArgs e)
        {
            string folderpt = globalpara.mu_standard;
            if (Directory.Exists(folderpt))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderpt,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
        }
        //添加预设文件
        private void importfile_btn_Click(object sender, EventArgs e)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string sourceFileName= sf.sf_selectfile("csv");
            if (!File.Exists(sourceFileName)) return;
            string destinationFile = Path.Combine(globalpara.mu_standard, Path.GetFileName(sourceFileName));
            File.Copy(sourceFileName, destinationFile, true);
            GetMuFileList();
            add2tree();
        }
    }
}
