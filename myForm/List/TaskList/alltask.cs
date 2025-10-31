using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using 质检工具.Func.ToolFormFunc;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.MenuConfig;

namespace 质检工具.myForm.List.TaskList
{
    public partial class alltask : AntdUI.Window
    {
        public List<GpBean> UpdatedTasks { get; private set; }

        private List<GpBean> tasks;
        private DynamicTool dynamicTool;

        public alltask(List<string> BSMlist)
        {
            
            InitializeComponent();

            // 初始化 DynamicTool
            dynamicTool = new DynamicTool();

            // 根据 BSM 获取任务
            tasks = globalpara.GpBeanList.Where(t => BSMlist.Contains(t.BSM)).ToList();
            UpdatedTasks = new List<GpBean>();

            // 动态生成任务属性编辑控件
            GenerateTaskControls();
            // 设置 pageHeader 的动态背景色
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
        }

        private void GenerateTaskControls()
        {
            // 合并所有选中任务的参数列表
            var allToolArgs = tasks.SelectMany(t => t.toolArglist)
                .GroupBy(arg => arg.argName) // 按 argName 分组
                .Select(group =>
                {
                    // 合并同名参数的默认值和当前值
                    var firstArg = group.First();
                    return new ToolArgs
                    {
                        folderName = firstArg.folderName,
                        toolboxName = firstArg.toolboxName,
                        toolsetName = firstArg.toolsetName,
                        toolName = firstArg.toolName,
                        argName = firstArg.argName,
                        argType = firstArg.argType,
                        direction = firstArg.direction,
                        replaceValue = firstArg.replaceValue,
                        defalutValue = group.Select(arg => arg.defalutValue).FirstOrDefault(val => !string.IsNullOrEmpty(val)) ?? firstArg.defalutValue,
                        nowValue = group.Select(arg => arg.nowValue).FirstOrDefault(val => !string.IsNullOrEmpty(val)) ?? firstArg.nowValue
                    };
                })
                .ToList();

            // 使用 DynamicTool 动态生成控件
            dynamicTool.creatcontrol(flowLayoutPanel1, allToolArgs);
        }

        private async void addTask_btn_Click(object sender, EventArgs e)//批量运行任务
        {
            // 1. 从动态控件中读取用户设置的属性值
            Dictionary<string, string> argValues = new Dictionary<string, string>();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is UITextBox textBox && textBox.Name.StartsWith("txt_"))
                {
                    string argName = textBox.Name.Substring(4); // 移除"txt_"前缀
                    argValues[argName] = textBox.Text;
                }
                else if (control is UIComboBox comboBox && comboBox.Name.StartsWith("combox"))
                {
                    string argName = comboBox.Name.Substring(6); // 移除"combox"前缀
                    argValues[argName] = comboBox.SelectedItem?.ToString();
                }
            }

            // 2. 将属性值分配到每个任务的 ToolArgs 中
            foreach (var task in tasks)
            {
                foreach (var arg in task.toolArglist)
                {
                    if (argValues.ContainsKey(arg.argName))
                    {
                        arg.nowValue = argValues[arg.argName]; // 分配值
                    }
                }
            }

            // 3. 更新 UpdatedTasks 列表
            UpdatedTasks = tasks;

            // 4. 调用任务执行逻辑
            GpTaskFunc gtf = new GpTaskFunc();
            List<string> BSMlist = tasks.Select(t => t.BSM).ToList(); // 获取任务的 BSM 列表
            gtf.SortGpBeanList(); // 排序任务
            gtf.GpTaskStateChange(BSMlist, GpStateEnum.队列中); // 更新任务状态为队列中
            this.Close(); // 关闭当前窗口
            //Application.DoEvents();
            await gtf.runwhich(false, BSMlist); // 执行任务（非逐步模式）

            
        }

        private void saveAll_Click(object sender, EventArgs e)//暂存编辑的任务属性
        {
            // 1. 从动态控件中读取用户设置的属性值
            Dictionary<string, string> argValues = new Dictionary<string, string>();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is UITextBox textBox && textBox.Name.StartsWith("txt_"))
                {
                    string argName = textBox.Name.Substring(4); // 移除"txt_"前缀
                    argValues[argName] = textBox.Text;
                }
                else if (control is UIComboBox comboBox && comboBox.Name.StartsWith("combox"))
                {
                    string argName = comboBox.Name.Substring(6); // 移除"combox"前缀
                    argValues[argName] = comboBox.SelectedItem?.ToString();
                }
            }

            // 2. 将属性值分配到每个任务的 ToolArgs 中
            foreach (var task in tasks)
            {
                foreach (var arg in task.toolArglist)
                {
                    if (argValues.ContainsKey(arg.argName))
                    {
                        arg.nowValue = argValues[arg.argName]; // 暂存值到 nowValue
                    }
                }
            }

            // 3. 更新 UpdatedTasks 列表
            UpdatedTasks = tasks;

            // 4. 关闭窗口
            this.Close();
        }
    }
}