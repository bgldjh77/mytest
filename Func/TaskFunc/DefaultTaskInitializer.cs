using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 质检工具.Func.SysFunc;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.Menu;
using 质检工具.MenuConfig;

namespace 质检工具.Func.TaskFunc
{
    /// <summary>
    /// 默认任务初始化器，用于创建和初始化默认任务
    /// </summary>
    public class DefaultTaskInitializer
    {
        /// <summary>
        /// 预设任务配置，用于初始化默认任务
        /// </summary>
        private static readonly List<DefaultTaskInfo> DefaultTasks = new List<DefaultTaskInfo>
        {
            new DefaultTaskInfo
            {
                FolderName = "项目检查",
                ToolboxName = "栅格检查",
                ToolsetName = "DOM检查",
                ToolName = "根据抽样融合输出块",
                SkipArgs = new[] { "数据路径", "输入自定义结合块" }
            },
            // 这里添加新的任务配置
            new DefaultTaskInfo
            {
                FolderName = "项目检查",
                ToolboxName = "栅格检查",
                ToolsetName = "DOM检查",  // DOM检查
                ToolName = "DOM质检意见处理",
                SkipArgs = new[] { "数据路径" } // 根据实际情况设置需要跳过的参数
            },
            // 添加海岸带检查任务
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "地名图层检查（与地名地址）",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "基础数据库属性矢量对比",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "927属性矢量对比",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "海岸线矢量属性对比（与海岸线三上数据）",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "道路和水系边线逻辑检查",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "地形特征图层矢量属性对比及逻辑检查（与地貌要素）",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "逻辑检查",
                 ToolName = "数据库逻辑检查",
                 SkipArgs = new[] { "数据路径" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "项目检查",
                 ToolboxName = "海岸带",
                 ToolsetName = "输出意见",
                 ToolName = "统计检查数据库",
                 SkipArgs = new[] { "数据路径" }
             }
             
             
            // 可以继续添加更多预设任务
        };

        /// <summary>
        /// 初始化所有默认任务
        /// </summary>
        /// <returns>初始化是否成功</returns>
        public static bool InitializeDefaultDomCheckTask()
        {
            bool anySuccess = false;

            // 初始化所有DOM检查预设任务
            foreach (var taskInfo in DefaultTasks)
            {
                if (taskInfo.ToolsetName == "DOM检查")
                {
                    bool success = InitializeTask(taskInfo);
                    //LogHelper.normallog($"初始化任务 {taskInfo.ToolName} {(success ? "成功" : "失败")}");
                    anySuccess = anySuccess || success;
                }
            }

            return anySuccess;
        }

        /// <summary>
        /// 初始化所有海岸带检查预设任务
        /// </summary>
        /// <returns>初始化是否成功</returns>
        public static bool InitializeDefaultCoastalCheckTask()
        {
            bool anySuccess = false;

            // 初始化所有海岸带检查预设任务
            foreach (var taskInfo in DefaultTasks)
            {
                if (taskInfo.ToolboxName == "海岸带")
                {
                    bool success = InitializeTask(taskInfo);
                    anySuccess = anySuccess || success;
                }
            }

            return anySuccess;
        }

        /// <summary>
        /// 初始化所有默认任务（包括DOM和海岸带检查）
        /// </summary>
        /// <returns>初始化是否成功</returns>
        public static bool InitializeAllDefaultTasks()
        {
            bool domSuccess = InitializeDefaultDomCheckTask();
            bool coastalSuccess = InitializeDefaultCoastalCheckTask();

            return domSuccess || coastalSuccess;
        }

        /// <summary>
        /// 根据任务信息初始化指定任务
        /// </summary>
        /// <param name="taskInfo">任务信息</param>
        /// <returns>初始化是否成功</returns>
        private static bool InitializeTask(DefaultTaskInfo taskInfo)
        {
            try
            {
                // 检查是否已存在此任务
                bool taskExists = globalpara.GpBeanList.Any(gb =>
                    gb.menuBean?.toolName == taskInfo.ToolName &&
                    gb.menuBean?.toolsetName == taskInfo.ToolsetName &&
                    gb.state == GpStateEnum.未开始);

                if (taskExists)
                {
                    return true;
                }

                // 创建MenuBean对象
                MenuBean menuBean = new MenuBean
                {
                    folderName = taskInfo.FolderName,
                    toolboxName = taskInfo.ToolboxName,
                    toolsetName = taskInfo.ToolsetName,
                    toolName = taskInfo.ToolName
                };

                // 加载工具配置
                csvFunc cf = new csvFunc();
                ToolArgs toolArgsTemplate = new ToolArgs();
                List<string> lines = cf.LoadToolConfig(menuBean, globalpara.toolconfigpath);

                // 处理找不到配置的情况
                if (lines.Count == 0)
                {
                    lines = cf.LoadToolConfig(taskInfo.ToolName);
                    if (lines.Count == 0)
                    {
                        LogHelper.ErrorLog($"初始化默认任务失败: {taskInfo.ToolName}", "无法找到工具配置");
                        return false;
                    }
                }

                // 创建参数列表
                List<ToolArgs> ta = new List<ToolArgs>();
                HashSet<string> processedArgs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                HashSet<string> skipArgs = new HashSet<string>(taskInfo.SkipArgs ?? new string[0], StringComparer.OrdinalIgnoreCase);

                foreach (string line in lines)
                {
                    ToolArgs arg = toolArgsTemplate.CreatArgBean(line);

                    // 跳过重复参数和指定跳过的参数
                    if (processedArgs.Contains(arg.argName) || skipArgs.Contains(arg.argName))
                        continue;

                    // 标记为已处理
                    processedArgs.Add(arg.argName);

                    // 确保参数的工具路径与menuBean一致
                    arg.folderName = menuBean.folderName;
                    arg.toolboxName = menuBean.toolboxName;
                    arg.toolsetName = menuBean.toolsetName;
                    arg.toolName = menuBean.toolName;

                    // 设置默认参数值
                    if (taskInfo.DefaultValues != null && taskInfo.DefaultValues.TryGetValue(arg.argName, out string customValue))
                    {
                        arg.nowValue = customValue;
                    }
                    else
                    {
                        arg.nowValue = arg.defalutValue;
                    }

                    ta.Add(arg);
                }

                if (ta.Count == 0)
                {
                    LogHelper.ErrorLog($"初始化默认任务失败: {taskInfo.ToolName}", "未能创建任何参数");
                    return false;
                }

                // 添加任务 - 添加"自动"标识符
                string autoId = "自动_" + Guid.NewGuid().ToString("N"); // 生成一个带"自动"前缀的唯一标识符
                GpTaskFunc gpTaskFunc = new GpTaskFunc();
                gpTaskFunc.AddGpTask(menuBean, ta, GpStateEnum.未开始, autoId, false);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog($"初始化默认任务异常: {taskInfo.ToolName}", ex);
                return false;
            }
        }

        /// <summary>
        /// 默认任务信息类，用于配置预设任务
        /// </summary>
        private class DefaultTaskInfo
        {
            /// <summary>文件夹名称</summary>
            public string FolderName { get; set; }

            /// <summary>工具箱名称</summary>
            public string ToolboxName { get; set; }

            /// <summary>工具集名称</summary>
            public string ToolsetName { get; set; }

            /// <summary>工具名称</summary>
            public string ToolName { get; set; }

            /// <summary>要跳过的参数名称列表</summary>
            public string[] SkipArgs { get; set; }

            /// <summary>自定义参数默认值，键为参数名，值为默认值</summary>
            public Dictionary<string, string> DefaultValues { get; set; }
        }
    }
}