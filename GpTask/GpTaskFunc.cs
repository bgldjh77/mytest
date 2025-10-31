using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 质检工具.Func.GISFunc;
using 质检工具.Func.SysFunc;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.Menu;
using 质检工具.MenuConfig;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using 质检工具.Plug;

namespace 质检工具.GpTask
{
    class GpTaskFunc
    {


        //保存任务列表到csv
        public void saveTask2csv(string csvPath)
        {
            List<string> lines = new List<string>();

            foreach (GpBean gpBean in globalpara.GpBeanList)
            {
                foreach (ToolArgs ta in gpBean.toolArglist)
                {
                    string foldername = ta.folderName;
                    string toolboxname = ta.toolboxName;
                    string toolsetname = ta.toolsetName;
                    string toolname = ta.toolName;
                    string argname = ta.argName;
                    ArgTypeEnum argType = ta.argType;
                    DirectionEnum direction = ta.direction;
                    string defalutValue = ta.defalutValue;
                    string nowValue = ta.nowValue;

                   

                    // 根据任务分类决定是否添加“自动”标识符
                    string bsm = gpBean.BSM;
                   

                    string combineline = $"{foldername},{toolboxname},,{toolsetname},,{toolname},{argname},{argType.argtype2str()},{direction},{defalutValue},{bsm},{nowValue}";
                    lines.Add(combineline);

                    
                }
            }

            try
            {
                // 读取现有文件的表头
                string header = File.ReadLines(csvPath, Encoding.GetEncoding("GB2312")).First();

                // 将表头和新数据组合后写入文件
                lines.Insert(0, header);
                File.WriteAllLines(csvPath, lines, Encoding.GetEncoding("GB2312"));

                
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("保存任务到 CSV 文件时出错", ex);
                
            }
        }
        //任务执行优先级排序
        public void SortGpBeanList()
        {
            globalpara.GpBeanList = globalpara.GpBeanList
                .OrderBy(gp =>
                {
                    // 定义任务类型的优先级
                    if (gp.menuBean.toolboxName == "栅格检查") return 1; // DOM
                    if (gp.menuBean.toolboxName == "海岸带") return 2; // HAD
                    return 0; // custom
                })
                .ThenBy(gp => gp.BSM.StartsWith("自动_") ? 1 : 0) // 自定义任务在最前面
                .ToList();
        }
        //从缓存文件中添加任务
        List<string> bsmls = new List<string>();
        public void addTaskByCache(string csvpath)
        {
            // 1. 清空全局任务列表
            globalpara.GpBeanList.Clear();

            // 2. 刷新任务表界面
            TaskListForm tlf = Application.OpenForms.OfType<TaskListForm>().FirstOrDefault();
            if (tlf != null)
            {
                tlf.LoadPageData();
            }

            // 3. 加载新任务
            csvFunc cvf = new csvFunc();
            List<ToolArgs> toolArgs = cvf.LoadTaskCache(csvpath);
            var groupedTools = toolArgs.GroupBy(t => new { t.folderName, t.toolboxName, t.toolsetName, t.toolName });

            // 用于记录已处理的 toolName
            HashSet<string> processedToolNames = new HashSet<string>();

            foreach (var group in groupedTools)
            {
                MenuBean menuBean = new MenuBean
                {
                    folderName = group.Key.folderName,
                    toolboxName = group.Key.toolboxName,
                    toolsetName = group.Key.toolsetName,
                    toolName = group.Key.toolName
                };

                List<ToolArgs> tals = group.Select(arg => new ToolArgs
                {
                    folderName = arg.folderName,
                    toolboxName = arg.toolboxName,
                    toolsetName = arg.toolsetName,
                    toolName = arg.toolName,
                    argName = arg.argName,
                    argType = arg.argType,
                    direction = arg.direction,
                    defalutValue = arg.defalutValue,
                    nowValue = arg.nowValue
                }).ToList();

                // 根据分类逻辑判断任务类型
                string bsm;
                if ((menuBean.toolboxName == "栅格检查" || menuBean.toolboxName == "海岸带") &&
                    !processedToolNames.Contains(menuBean.toolName))
                {
                    // 对于同名任务，仅对第一个添加自动前缀
                    bsm = $"自动_{Guid.NewGuid():N}";
                    processedToolNames.Add(menuBean.toolName);
                }
                else
                {
                    // 普通任务
                    bsm = Guid.NewGuid().ToString("N");
                }

                AddGpTask(menuBean, tals, GpStateEnum.未开始, bsm, false);
            }
            SortGpBeanList();
            // 4. 刷新任务表界面以显示新任务
            if (tlf != null)
            {
                tlf.LoadPageData();
            }
        }

        #region//添加任务
        public string AddGpTask(MenuBean menuBean,List<ToolArgs> ta)
        {
            GpStateEnum state = GpStateEnum.未开始;
            string randomString = AddGpTask(menuBean, ta, state);
            return randomString;
        }
        public string AddGpTask(MenuBean menuBean, List<ToolArgs> ta, GpStateEnum state, string bsm = "",bool showAddInfo=true)
        {
            string randomString;
            if (!string.IsNullOrEmpty(bsm))
            {
                // 查找是否存在相同BSM的任务
                var existingGpBean = globalpara.GpBeanList.FirstOrDefault(x => x.BSM == bsm);
                if (existingGpBean != null)
                {
                    // 更新已存在的任务
                    existingGpBean.menuBean = menuBean;
                    existingGpBean.toolArglist = ta;
                    existingGpBean.state = state;
                    randomString = bsm;
                }
                else
                {
                    // 使用提供的BSM创建新任务
                    randomString = bsm;
                    CreateNewTask(menuBean, ta, state, randomString);
                }
            }
            else
            {
                // 生成新的BSM并创建任务
                Guid guid = Guid.NewGuid();
                randomString = guid.ToString("N");
                CreateNewTask(menuBean, ta, state, randomString);
            }

            TaskListForm tlf = System.Windows.Forms.Application.OpenForms.OfType<TaskListForm>().FirstOrDefault();
            if (tlf != null) { tlf.LoadPageData(); }
            if(showAddInfo)
                LogHelper.normallog("已" + (string.IsNullOrEmpty(bsm) ? "添加" : "更新") + "任务：" + menuBean.toolName);
            return randomString;
        }

        public void CreateNewTask(MenuBean menuBean, List<ToolArgs> ta, GpStateEnum state, string bsm)
        {
            GpBean gpBean = new GpBean();
            gpBean.menuBean = menuBean;
            gpBean.toolArglist = ta;
            gpBean.createTime = GetNowTime.nowtime2();
            gpBean.state = state;
            gpBean.BSM = bsm;

            globalpara.GpBeanList.Add(gpBean);
        }
        #endregion

        /// <summary>
        /// 任务状态更新
        /// </summary>
        /// <param name="BSMlist"></param>
        /// <param name="state"></param>
        public void GpTaskStateChange(List< string> BSMlist, GpStateEnum state)
        {
            foreach(GpBean gb in globalpara.GpBeanList)
            {
                if(BSMlist.Contains(gb.BSM))
                {
                    if(state== GpStateEnum.运行中)
                    {
                        gb.startTime = GetNowTime.nowtime2();
                    }
                    else if(state == GpStateEnum.已完成| state == GpStateEnum.失败)
                    {
                        gb.endTime= GetNowTime.nowtime2();
                    }
                    else if (state == GpStateEnum.队列中)
                    {
                        gb.cancel = false;
                    }
                    gb.state = state;
                }
            }
            TaskListForm tlf = System.Windows.Forms.Application.OpenForms.OfType<TaskListForm>().FirstOrDefault();
            if (tlf != null) { tlf.LoadPageData(); }
            
        }

        GpFunc gf = new GpFunc();
        /// <summary>
        /// 执行任务
        /// </summary>
        private async Task ExecuteTaskAsync(GpBean gpBean, string fulltoolpath, string toolname)
        {
            if (PlugNameList.namelist.Contains(toolname))//插件工具
            {
                LogHelper.normallog("当前任务：" + toolname);
                GpTaskStateChange(new List<string> { gpBean.BSM }, GpStateEnum.运行中);
                
                // 获取主窗口引用
                var mainForm = Application.OpenForms.OfType<Form>().FirstOrDefault();

                // 在后台线程中等待1秒，确保UI完全关闭\处理
                await Task.Run(async () =>
                {
                    await Task.Delay(1000); // 等待1秒

                    // 回到主线程执行ArcObject任务
                    mainForm?.BeginInvoke(new Action(() =>
                    {
                        PlugNameList.RunWhichPlug(toolname, gpBean.toolArglist, new List<string> { gpBean.BSM });
                    }));
                });

                
            }
            else
            {
                List<string> ls = new List<string>();
                foreach (ToolArgs args in gpBean.toolArglist)
                {
                    if (String.IsNullOrEmpty(args.replaceValue))
                    {
                        ls.Add(args.nowValue);
                    }
                    else
                    {
                        ls.Add(args.replaceValue);
                    }
                }

                await gf.gp_sim_task(fulltoolpath, ls, gpBean.BSM);
            }
        }

        public async Task runwhich(bool step, List<string> BSMlist)
        {
            for(int index=0;index< globalpara.GpBeanList.Count;index++ )
            {
                GpBean gpBean = globalpara.GpBeanList[index];
                if (!BSMlist.Contains(gpBean.BSM)) continue;
                
                MenuBeanFunc mbf = new MenuBeanFunc();
                string fulltoolpath = mbf.GetToolFullPath(gpBean.menuBean);
                Console.WriteLine(fulltoolpath);
                string toolname = gpBean.toolArglist[0].toolName;
                Console.WriteLine(gpBean.BSM);
                if (gpBean.cancel || gpBean.state != GpStateEnum.队列中) continue;
                if (step)
                {
                    using (ToolForm pa = new ToolForm(gpBean.toolArglist, true))
                    {
                        // 使用 ShowDialog() 显示模态对话框
                        DialogResult result = pa.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            gpBean = globalpara.GpBeanList[index];
                            await ExecuteTaskAsync(gpBean, fulltoolpath, toolname);
                        }
                        
                    }
                }
                else
                {
                    await ExecuteTaskAsync(gpBean, fulltoolpath, toolname);
                }
                
            }
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="identify"></param>
        public void stopGPtask(List<string> BSMlist)
        {
            foreach(string identify in BSMlist)
            {
                foreach(GpBean gpBean in globalpara.GpBeanList)
                {
                    if(gpBean.BSM== identify)
                    {
                        if(gpBean.state== GpStateEnum.运行中)
                        {
                            gf.CancelExecution(identify);
                        }
                        break;
                    }
                }
            }
            //gf.CancelExecution(identify);
        }

        




    }




}
