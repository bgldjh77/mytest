using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using 质检工具.Func.GISFunc;
using 质检工具.Func.SysFunc;
using 质检工具.Func.ToolFormFunc;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.Menu;
using 质检工具.MenuConfig;
using 质检工具.Plug;


namespace 质检工具
{
    public partial class ToolForm : AntdUI.Window
    {
        string toolname;
        MenuBean menuBean;
        string BSM;
        bool okbtn = false;
        List<ToolArgs>ta = new List<ToolArgs>();
        
        // 新增：支持PresetArg模式的字段
        bool step = false;
        string formName;
        
        public ToolForm(MenuBean _menuBean)
        {
            menuBean = _menuBean;
            toolname = _menuBean.toolName;
            InitializeComponent();
        }
     
        // 新增：支持PresetArg的构造函数
        public ToolForm(List<ToolArgs> _tals, string _formName)
        {
            ta = _tals;
            formName = _formName;
            toolname = _tals[0].toolName;
            InitializeComponent();
            okbtn = true;
        }
        
        public ToolForm(List<ToolArgs> _tals, bool _step)
        {
            ta = _tals;
            step = _step;
            toolname = _tals[0].toolName;
            InitializeComponent();
            okbtn = true;

        }
        private void ToolForm_Load(object sender, EventArgs e)
        {
            //PlugNameList.namelist.Contains(toolname)
            if (okbtn)//运行插件
            {
                ok_btn.Visible = true;
                start_btn.Visible = false;
                addTask_btn.Visible = false;
            }
            else
            {
                ok_btn.Visible = false;
            }
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            this.Text = toolname;
            pageHeader1.Text= toolname;
            
            

            // 处理工具描述 - 兼容PresetArg模式
            if (menuBean != null)
            {
                //工具描述
                //ReadExcel readExcel = new ReadExcel();
                //Tool_desc_TextBox.Text= readExcel.ReadToolDesc(globalpara.tooldescpath, menuBean);
                //Tool_desc_TextBox.Text = ReadToolDesc.readToolDesc(menuBean);
                //Tool_desc_TextBox.ReadOnly = true;

                if (ta.Count == 0)
                {
                    csvFunc cf = new csvFunc();
                    ToolArgs toolArgs = new ToolArgs();
                    List<string> lines = cf.LoadToolConfig(menuBean,globalpara.toolconfigpath);
                    foreach (string line in lines)
                    {
                        ta.Add(toolArgs.CreatArgBean(line));
                    }
                }
            }

            DynamicTool dytool = new DynamicTool();
            dytool.creatcontrol(this, ta,menuBean);

            SplitContainer sp = this.Controls.Find("splitContainer1", true).FirstOrDefault() as SplitContainer;
            if (sp != null)
            {
                addTask_btn.Location = new System.Drawing.Point(sp.SplitterDistance - addTask_btn.Width - pageHeader1.Height * 5,addTask_btn.Location.Y) ;

                ok_btn.Location = new System.Drawing.Point(Convert.ToInt32(sp.SplitterDistance*0.5- ok_btn.Width*0.5), ok_btn.Location.Y);
            }
        }
        
        
        GpFunc gf = new GpFunc();
        //执行任务
        private void uiButton1_Click(object sender, EventArgs e)
        {
            foreach (GpBean gpBean in globalpara.GpBeanList)
            {
                if (gpBean.state == GpStateEnum.运行中 | gpBean.state == GpStateEnum.运行中)
                {
                    LogHelper.ErrorLog("未开始任务","已有队列中任务，无法执行单任务，多任务请添加到任务列表");
                    return;
                }
            }

            MenuBeanFunc mbf = new MenuBeanFunc();
            string fulltoolpath = mbf.GetToolFullPath(menuBean);
            Console.WriteLine(fulltoolpath);

            ReadTextBoxValues();
            //List<string> ls = new List<string>();
            //foreach(ToolArgs args in ta)
            //{
            //    if(string.IsNullOrEmpty(args.replaceValue))
            //    {
            //        ls.Add(args.nowValue);
            //    }
            //    else
            //    {
            //        ls.Add(args.replaceValue);
            //    }
            //}

            GpTaskFunc gtf = new GpTaskFunc();
            string randomString = gtf.AddGpTask(menuBean, ta, GpStateEnum.队列中, BSM,false);

            EventHandler handler = null;
            handler = (s, args) =>
            {
                Application.Idle -= handler; // 确保只执行一次              
                gtf.runwhich(false, new List<string> { randomString });
            };
            Application.Idle += handler; // 注册事件处理程序
            this.Close(); // 关闭当前窗口
        }
        /// <summary>
        /// 读取控件的值
        /// </summary>
        private void ReadTextBoxValues()
        {
            foreach (ToolArgs args in ta)
            {
                string value = "";

                // 在 Panel1 的所有控件中查找匹配 argName 的 txt_ 或 combox 控件
                foreach (Control control in this.Controls)
                {
                    if (control is SplitContainer spcontain)
                    {
                        foreach (Control child in spcontain.Panel1.Controls)
                        {
                            string expectedTxtName = "txt_" + args.argName;
                            string expectedComboxName = "combox" + args.argName;

                            if (child is UITextBox textBox && textBox.Name == expectedTxtName)
                            {
                                value = textBox.Text;
                                //break;
                            }
                            else if (child is UIComboBox comboBox && comboBox.Name == expectedComboxName)
                            {
                                //value = comboBox.SelectedItem?.ToString();
                                args.replaceValue = comboBox.SelectedItem?.ToString();
                                //break;
                            }
                        }
                    }

                    //if (value != "") break; // 找到就跳出外层循环
                }
                args.nowValue = value;

            }

        }
        //添加任务
        private void uiButton2_Click(object sender, EventArgs e)
        {
            ReadTextBoxValues();

            GpTaskFunc gtf = new GpTaskFunc();

            // 检查是否有"自动"标识符
            if (BSM != null && BSM.Contains("自动"))
            {
                // 创建一个新的任务，不带"自动"标识符
                // 复制一份当前任务的参数，但使用新的BSM
                List<ToolArgs> newTaskArgs = new List<ToolArgs>();
                foreach (ToolArgs arg in ta)
                {
                    ToolArgs newArg = new ToolArgs
                    {
                        folderName = arg.folderName,
                        toolboxName = arg.toolboxName,
                        toolsetName = arg.toolsetName,
                        toolName = arg.toolName,
                        argName = arg.argName,
                        argType = arg.argType,
                        direction = arg.direction,
                        defalutValue = arg.defalutValue,
                        nowValue = arg.nowValue,
                        replaceValue=arg.replaceValue
                    };
                    newTaskArgs.Add(newArg);
                }

                // 添加一个没有"自动"标识符的新任务
                gtf.AddGpTask(menuBean, newTaskArgs);
            }
            else
            {
                // 原来的代码 - 直接添加任务
                gtf.AddGpTask(menuBean, ta);
            }
        }
        //编辑参数和运行plug功能
        private void ok_btn_Click(object sender, EventArgs e)
        {
            ReadTextBoxValues();
            // 处理PresetArg模式的逻辑
            if (step || formName == "TaskListForm")
            {             
                if (step)
                {
                    this.DialogResult = DialogResult.OK;
                }
                this.Close();
            }
            //else // 原ToolForm模式的逻辑
            //{                            
            //    LogHelper.normallog("当前任务：" + toolname);
            //    PlugNameList.RunWhichPlug(toolname, ta);
            //}
            
        }


    }
}
