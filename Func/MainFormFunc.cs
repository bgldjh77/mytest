using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.SaveProject;
using 质检工具.GISFunc;
using 质检工具.GpTask;
using 质检工具.SysFunc;

namespace 质检工具
{
    class MainFormFunc
    {

        public static void SaveProject()
        {
            try
            {
                GpTaskFunc gtf = new GpTaskFunc();
                gtf.saveTask2csv(globalpara.taskcachepath);

                if (globalpara.isArcGisInitialized)
                {
                    MXDFunc mxdfunc = new MXDFunc();
                    mxdfunc.SaveMapToMXD_noskip();
                }

                SelectFileFunc sf = new SelectFileFunc();
                string projectfile = sf.sf_savefile("ini");
                if (string.IsNullOrEmpty(projectfile)) return;

                string[] filelist = { globalpara.taskcachepath, IniFunc.inipath, globalpara.savemxdpath };
                ProjectFunc pjf = new ProjectFunc();

                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(projectfile)))
                {
                    pjf.CreateProjectFile(filelist, projectfile);
                }
                if (System.IO.File.Exists(projectfile))
                {
                    LogHelper.normallog("工程文件保存成功：" + projectfile);
                }
                else
                {
                    MessageBox.Show("保存工程未知失败" , "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存工程失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
