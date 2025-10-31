using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.SaveProject;
using 质检工具.Func.SysFunc;
using 质检工具.GISFunc;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.MenuConfig;
using 质检工具.SysFunc;

namespace 质检工具
{
    public partial class popover_saveProject : UserControl
    {
        AntdUIForm form;
        MXDFunc mxdfunc = new MXDFunc();
        GpTaskFunc gtf = new GpTaskFunc();
        ProjectFunc pjf = new ProjectFunc();
        string[] filelist = { globalpara.taskcachepath, IniFunc.inipath, globalpara.savemxdpath };

        public popover_saveProject(AntdUIForm _form)
        {
            form = _form;
            InitializeComponent();
        }
        //保存工程
        private void button2_Click(object sender, EventArgs e)
        {
            gtf.saveTask2csv(globalpara.taskcachepath);
            mxdfunc.SaveMapToMXD_noskip();
          
            SelectFileFunc sf = new SelectFileFunc();
            string projectfile = sf.sf_savefile("ini");
            if (projectfile == "") return;
            if (Directory.Exists(Path.GetDirectoryName(projectfile)))
            {
                pjf.CreateProjectFile(filelist, projectfile);
            }
            if (File.Exists(projectfile)) { LogHelper.normallog("工程文件保存成功"); }
        }
        //打开工程
        private void button1_Click(object sender, EventArgs e)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string projectfile = sf.sf_selectfile("ini");
            if (!File.Exists(projectfile)) { return; }
            pjf.ExtractFiles(projectfile, Path.GetDirectoryName(globalpara.taskcachepath));
            //task    任务列表      
            gtf.addTaskByCache(globalpara.taskcachepath);
            //mxd
            mxdfunc.addformMXD(globalpara.savemxdpath);
            //主窗体地图tab页
            form.tabs1_selectcache();
        }

        private void popover_saveProject_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetAntdUIbtn(open_btn);
            UIPreSet.ControlPreSet.SetAntdUIbtn(save_btn);
        }
    }
}
