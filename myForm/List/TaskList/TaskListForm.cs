using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.GISFunc;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.Menu;
using 质检工具.MenuConfig;
using 质检工具.myForm.List.TaskList;
using static 质检工具.Func.AntdTableFunc.TaskList;

namespace 质检工具
{
   

    public partial class TaskListForm : AntdUI.Window
    {
       
        public TaskListForm()
        {
            InitializeComponent();

            // 创建一个包含所有列配置的集合，便于复用
            var columns = new AntdUI.ColumnCollection
    {
        new AntdUI.ColumnCheck("check").SetFixed(),
        new AntdUI.Column("index", "序号", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("state", "状态", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("btns", "操作", AntdUI.ColumnAlign.Center).SetFixed().SetWidth("auto").SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("toolname", "工具名称", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("createtime", "创建时间", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("starttime", "开始时间", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("endtime", "结束时间", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("foldername", "文件夹名", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("toolboxname", "工具箱名称", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
        new AntdUI.Column("toolsetname", "工具集名称", AntdUI.ColumnAlign.Center).SetLocalizationTitleID("Table.Column."),
    };

            // 应用列配置到两个表格
            custom_Tasktable.Columns = columns;
            DOM_table.Columns = columns;  //确保DOM_table有相同的列配置
            HAD_table.Columns = columns;
            // 为DOM_table绑定编辑按钮点击事件（与custom_Tasktable使用相同的事件处理程序）
            DOM_table.CellButtonClick += table1_CellButtonClick;
            HAD_table.CellButtonClick += table1_CellButtonClick;
            // 创建数据源
            var list = new List<TaskItem>();
            int index = 0;
            foreach (var gpBean in globalpara.GpBeanList)
            {
                list.Add(new TaskItem(index, gpBean));
                index++;
            }

            // 设置分页参数
            pagination1.Total = globalpara.GpBeanList.Count;
            pagination1.PageSize = 10;
            pagination1.Current = 1;

            // 初始加载第一页数据
            LoadPageData(pagination1.Current, pagination1.PageSize);

            // 添加分页事件处理
            pagination1.ValueChanged += Pagination1_ValueChanged;

        }
        public void LoadPageData()
        {
            LoadPageData(pagination1.Current, pagination1.PageSize);
        }
        private void LoadPageData(int current, int pageSize)
        {
            try
            {
                // 保存当前选中状态
                var customCheckedBSMs = SaveTableCheckState(custom_Tasktable);
                var domCheckedBSMs = SaveTableCheckState(DOM_table);
                var hadCheckedBSMs = SaveTableCheckState(HAD_table);

                // 根据ToolboxName分类任务

                // 1. DOM检查任务 - ToolboxName为"栅格检查"且带有"自动"标识符的任务
                var domTasks = globalpara.GpBeanList
                    .Where(gp => (gp.BSM != null && gp.BSM.Contains("自动") &&
                                 gp.menuBean != null && gp.menuBean.toolboxName == "栅格检查"))
                    .Select((gp, idx) => new TaskItem(idx, gp))
                    .ToList();

                // 2. 海岸带检查任务 - ToolboxName为"海岸带"且带有"自动"标识符的任务
                var coastalTasks = globalpara.GpBeanList
                    .Where(gp => (gp.BSM != null && gp.BSM.Contains("自动") &&
                                 gp.menuBean != null && gp.menuBean.toolboxName == "海岸带"))
                    .Select((gp, idx) => new TaskItem(idx, gp))
                    .ToList();

                // 3. 普通任务 - 不含"自动"标识符的任务
                var otherTasks = globalpara.GpBeanList
                    .Where(gp => !(gp.BSM != null && gp.BSM.Contains("自动")))
                    .Select((gp, idx) => new TaskItem(idx, gp))
                    .ToList();

                // 恢复选中状态
                RestoreTableCheckState(domTasks, domCheckedBSMs);
                RestoreTableCheckState(coastalTasks, hadCheckedBSMs);
                RestoreTableCheckState(otherTasks, customCheckedBSMs);

                // 分配数据到对应表格
                DOM_table.DataSource = domTasks;
                HAD_table.DataSource = coastalTasks;
                custom_Tasktable.DataSource = otherTasks;

                // 设置分页参数
                pagination1.Total = otherTasks.Count;
                DOM_pagination.Total = domTasks.Count;
                HAD_pagination.Total = coastalTasks.Count;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("任务分类失败", ex);
            }
        }

        // 封装标准列表创建逻辑为独立方法
        private void CreateStandardList(int current, int pageSize)
        {
            var list = new List<TaskItem>();
            var pageData = globalpara.GpBeanList.Skip((current - 1) * pageSize).Take(pageSize);
            int startIndex = (current - 1) * pageSize;

            foreach (var gpBean in pageData)
            {
                list.Add(new TaskItem(startIndex, gpBean));
                startIndex++;
            }

            custom_Tasktable.DataSource = list;
            pagination1.Total = globalpara.GpBeanList.Count;
        }

        private void Pagination1_ValueChanged(object sender, AntdUI.PagePageEventArgs e)
        {
            LoadPageData(e.Current, e.PageSize);
        }

        private void suitheight()
        {
            collapseItem1.Height = List_panel.Height - 3 * 12 - 3 * 5;
            collapseItem2.Height = List_panel.Height - 3 * 12 - 3 * 5;
            collapseItem3.Height = List_panel.Height - 3 * 12 - 3 * 5;
            zhedie_collapse1.Height = (collapseItem1.Height + 3 * 12 + 3 * 5) + 130;
        }
        // 初始化时绑定事件
        private void TaskListForm_Load(object sender, EventArgs e)
        {
            // 绑定事件到所有表格
            var tables = new List<AntdUI.Table> { custom_Tasktable, DOM_table, HAD_table };
            foreach (var table in tables)
            {
                table.SortRows += table1_SortRows;
                table.SelectIndexChanged += table1_SelectIndexChanged;
            }

            // 其他初始化逻辑
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            custom_Tasktable.EnableHeaderResizing = true;
            suitheight();
        }
        //判断是否正在执行任务
        private bool ifruningGP()
        {
            //是否正在执行任务
            foreach (GpBean gpBean in globalpara.GpBeanList)
            {
                if (gpBean.state == GpStateEnum.运行中 | gpBean.state == GpStateEnum.队列中)
                {
                    
                    return true;
                }
            }
            return false;
        }
        //判断是否存在执行过的任务
        private bool hasDoneTask(List<TaskItem> currentData)
        {
            bool goon = true;
            //遍历表格 判断有已经执行过的任务
            foreach (var item in currentData)
            {
                if (item.check)
                {
                    if (item.state.Text == GpStateEnum.已完成.ToString() | item.state.Text == GpStateEnum.失败.ToString())
                    {
                        goon = false;
                        AntdUI.Modal.open(new AntdUI.Modal.Config(this, "提示", "包含已结束任务，是否重复执行？", AntdUI.TType.Warn)
                        {
                            OnOk = config =>
                            {
                                goon = true;
                                return true;
                            }
                        });
                        break;
                    }

                }
            }
            return goon;
        }
        //获取勾选的
        private List<string> getChecked()
        {
            List<string> BSMlist = new List<string>();

            // 普通任务表格
            var customData = custom_Tasktable.DataSource as List<TaskItem>;
            if (customData != null && customData.Count > 0)
            {
                if (hasDoneTask(customData))
                {
                    foreach (var item in customData)
                    {
                        if (item.check)
                        {
                            BSMlist.Add(item.BSM);
                        }
                    }
                }
            }

            // DOM任务表格
            var domData = DOM_table.DataSource as List<TaskItem>;
            if (domData != null && domData.Count > 0)
            {
                foreach (var item in domData)
                {
                    if (item.check)
                    {
                        BSMlist.Add(item.BSM);
                    }
                }
            }

            // 海岸带任务表格
            var hadData = HAD_table.DataSource as List<TaskItem>;
            if (hadData != null && hadData.Count > 0)
            {
                foreach (var item in hadData)
                {
                    if (item.check)
                    {
                        BSMlist.Add(item.BSM);
                    }
                }
            }

            return BSMlist;
        }
        //开始任务
        private void start_task(bool step)
        {
            // 获取选中的任务
            List<string> BSMlist = getChecked();
            if (BSMlist.Count == 0)
            {
                LogHelper.normallog("未选择任何任务");
                return;
            }

            // 弹出 alltask 窗体
            using (alltask allTaskForm = new alltask(BSMlist))
            {
                if (allTaskForm.ShowDialog() == DialogResult.OK)
                {
                    //// 用户确认后，获取编辑后的任务属性
                    //List<GpBean> updatedTasks = allTaskForm.UpdatedTasks;

                    //// 更新全局任务列表
                    //foreach (var updatedTask in updatedTasks)
                    //{
                    //    var existingTask = globalpara.GpBeanList.FirstOrDefault(t => t.BSM == updatedTask.BSM);
                    //    if (existingTask != null)
                    //    {
                    //        existingTask.toolArglist = updatedTask.toolArglist;
                    //    }
                    //}

                    //// 开始执行任务
                    //GpTaskFunc gtf = new GpTaskFunc();
                    //gtf.SortGpBeanList();
                    //gtf.GpTaskStateChange(BSMlist, GpStateEnum.队列中);
                    //await gtf.runwhich(step, BSMlist);
                }
            }
        }
        //分步开始任务
        private void start_taskstep(bool step)
        {
            //是否正在执行任务
            if (ifruningGP())
            {
                LogHelper.ErrorLog("不能开始任务", "存在队列中的任务");
                return;
            }

            List<string> BSMlist = getChecked();
            //根据标识码放入队列执行
            if (BSMlist.Count == 0) return;
            GpTaskFunc gtf = new GpTaskFunc();
            gtf.SortGpBeanList();
            gtf.GpTaskStateChange(BSMlist, GpStateEnum.队列中);
            gtf.runwhich(step, BSMlist);//执行 

        }

        #region//按钮
        //批量执行
        private void button1_Click(object sender, EventArgs e)
        {
            if (isSortingEnabled) swichOpenSort(false);
            start_task(false);
        }
        //分步执行
        private void step_run_btn_Click(object sender, EventArgs e)
        {
            if(isSortingEnabled) swichOpenSort(false);
            start_taskstep(true);
        }

        //删除
        private void delete_btn_Click(object sender, EventArgs e)
        {
            // 收集所有表格勾选且非运行中的任务BSM
            List<string> checkedBSM = new List<string>();

            // 普通任务
            var customData = custom_Tasktable.DataSource as List<TaskItem>;
            if (customData != null && customData.Count > 0)
            {
                checkedBSM.AddRange(customData
                    .Where(item => item.check && item.state.Text != GpStateEnum.运行中.ToString())
                    .Select(item => item.BSM));
            }

            // DOM任务
            var domData = DOM_table.DataSource as List<TaskItem>;
            if (domData != null && domData.Count > 0)
            {
                checkedBSM.AddRange(domData
                    .Where(item => item.check && item.state.Text != GpStateEnum.运行中.ToString())
                    .Select(item => item.BSM));
            }

            // 海岸带任务
            var hadData = HAD_table.DataSource as List<TaskItem>;
            if (hadData != null && hadData.Count > 0)
            {
                checkedBSM.AddRange(hadData
                    .Where(item => item.check && item.state.Text != GpStateEnum.运行中.ToString())
                    .Select(item => item.BSM));
            }

            if (checkedBSM.Count == 0)
            {
                return;
            }

            // 按BSM从全局任务列表删除
            globalpara.GpBeanList.RemoveAll(gp => checkedBSM.Contains(gp.BSM));

            // 更新分页总数
            pagination1.Total = globalpara.GpBeanList.Count;

            // 重新加载当前页数据
            LoadPageData();
        }
        //停止
        private void stopTask_btn_Click(object sender, EventArgs e)
        {
            // 1. 收集所有正在运行和队列中的任务BSM
            List<string> runningBSM = globalpara.GpBeanList
                .Where(gp => gp.state == GpStateEnum.运行中 || gp.state == GpStateEnum.队列中)
                .Select(gp => gp.BSM)
                .ToList();

            if (runningBSM.Count == 0)
            {
                LogHelper.normallog("当前没有正在执行的任务，无需停止。");
                return;
            }

            // 2. 终止所有正在运行的任务
            GpTaskFunc gtf = new GpTaskFunc();
            gtf.stopGPtask(runningBSM);

            // 3. 回退所有相关任务状态为未开始，清空时间、取消标记
            foreach (GpBean gpBean in globalpara.GpBeanList)
            {
                if (runningBSM.Contains(gpBean.BSM))
                {
                    gpBean.state = GpStateEnum.未开始;
                    gpBean.startTime = "";
                    gpBean.endTime = "";
                    gpBean.cancel = false;
                }
            }

            // 4. 刷新任务列表界面
            LoadPageData();

            LogHelper.normallog("所有任务已停止并回退到初始状态。");
        }
        //编辑按钮
        private void table1_CellButtonClick(object sender, AntdUI.TableButtonEventArgs e)
        {
            if (e.Btn.Id == "edit_btn")
            {
                TaskItem row;

                // 判断事件来源是哪个表格
                if (sender == custom_Tasktable)
                {
                    row = (custom_Tasktable.DataSource as List<TaskItem>)[e.RowIndex - 1];
                }
                else if (sender == DOM_table)
                {
                    row = (DOM_table.DataSource as List<TaskItem>)[e.RowIndex - 1];
                }
                else if (sender == HAD_table)  // 添加对海岸带表格的支持
                {
                    row = (HAD_table.DataSource as List<TaskItem>)[e.RowIndex - 1];
                }
                else
                {
                    return;  // 如果不是已知表格，则退出
                }

                if (row.state.Text == GpStateEnum.运行中.ToString() | row.state.Text == GpStateEnum.队列中.ToString())
                {
                    LogHelper.normallog($"正在{row.state.Text}，不能修改");
                    return;
                }

                // 通过 BSM 查找对应的 GpBean
                var gpBean = globalpara.GpBeanList.FirstOrDefault(gb => gb.BSM == row.BSM);
                if (gpBean != null)
                {
                    ToolForm tf = new ToolForm(gpBean.toolArglist, this.Name);
                    tf.Show();
                }
            }
        }
        //导入任务
        private void importTask_btn_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<PresetTaskForm>();
        }
        #endregion
        #region//任务拖拽排序


        // 控制排序功能是否启用
        private bool isSortingEnabled = false;
     
        // 点击按钮，统一开启/关闭排序功能
        private void button2_Click(object sender, EventArgs e)
        {
            // 切换排序状态
            swichOpenSort(!isSortingEnabled);
        }
        
        private void swichOpenSort(bool sort)
        {          
            if(isSortingEnabled!= sort)
            {
                // 获取所有表格
                var tables = new List<AntdUI.Table> { custom_Tasktable, DOM_table, HAD_table };

                foreach (var table in tables)
                {
                    // 检查是否已存在排序列
                    var sortColumn = table.Columns.FirstOrDefault(col => col is AntdUI.ColumnSort);
                    if (sort)
                    {
                        if (sortColumn == null)
                        {
                            // 添加排序列
                            table.Columns.Insert(0, new AntdUI.ColumnSort() { Fixed = true });
                        }
                    }
                    else
                    {
                        if (sortColumn != null)
                        {
                            // 移除排序列
                            table.Columns.Remove(sortColumn);
                        }
                    }
                }
                // 刷新所有表格
                RefreshAllTables();

                isSortingEnabled = sort;
                LogHelper.normallog(sort ? "所有表格已进入排序状态。" : "所有表格已退出排序状态。");
            }
        }
        // 刷新所有表格的方法
        private void RefreshAllTables()
        {
            try
            {
                // 重新加载数据源
                LoadPageData();

                // 强制刷新表格显示
                custom_Tasktable.Refresh();
                DOM_table.Refresh();
                HAD_table.Refresh();
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("刷新表格时发生错误", ex);
            }
        }
        // 拖拽排序逻辑，动态处理每个表格
        private void table1_SortRows(object sender, AntdUI.IntEventArgs e)
        {
            AntdUI.Table currentTable = sender as AntdUI.Table;
            if (currentTable == null) return;

            List<TaskItem> currentData = GetcurrentData(currentTable);

            int targetIndex = currentTable.SelectedIndex;//释放位置
            //var draggedItem = currentData.FirstOrDefault(item => item.index == selectindex);

            if (draggedItem != null)
            {
                int oldIndex = draggedItem.index;//起始位置


                // 判断拖动方向：向上 or 向下
                if (targetIndex < oldIndex)
                {
                    foreach (var item in currentData.Where(i => i != draggedItem && i.index >= targetIndex && i.index < oldIndex))
                    {
                        item.index += 1;
                    }
                }
                else if (targetIndex > oldIndex)
                {
                    foreach (var item in currentData.Where(i => i != draggedItem && i.index <= targetIndex && i.index > oldIndex))
                    {
                        item.index -= 1;
                    }
                }
                draggedItem.index = targetIndex;
                // 重新排序并刷新表格
                currentData = currentData.OrderBy(i => i.index).ToList();
                currentTable.DataSource = null;
                currentTable.DataSource = currentData;
            }

        }
        //根据table获取数据源
        private List<TaskItem> GetcurrentData(AntdUI.Table currentTable)
        {
            if (currentTable == null) return null;
            // 根据表格类型获取数据源
            List<TaskItem> currentData = null;
            if (currentTable == custom_Tasktable)
            {
                currentData = custom_Tasktable.DataSource as List<TaskItem>;
            }
            else if (currentTable == DOM_table)
            {
                currentData = DOM_table.DataSource as List<TaskItem>;
            }
            else if (currentTable == HAD_table)
            {
                currentData = HAD_table.DataSource as List<TaskItem>;
            }

            if (currentData == null || currentData.Count == 0) return null;

            return currentData;
        }

        // 保存表格选中状态的辅助方法
        private HashSet<string> SaveTableCheckState(AntdUI.Table table)
        {
            var checkedBSMs = new HashSet<string>();
            if (table?.DataSource is List<TaskItem> dataSource)
            {
                foreach (var item in dataSource.Where(x => x.check))
                {
                    checkedBSMs.Add(item.BSM);
                }
            }
            return checkedBSMs;
        }

        // 恢复表格选中状态的辅助方法
        private void RestoreTableCheckState(List<TaskItem> taskItems, HashSet<string> checkedBSMs)
        {
            if (taskItems != null && checkedBSMs != null)
            {
                foreach (var item in taskItems)
                {
                    item.check = checkedBSMs.Contains(item.BSM);
                }
            }
        }

        private TaskItem draggedItem = null;//正在拖拽的对象
        // 记录选中的索引
        private void table1_SelectIndexChanged(object sender, EventArgs e)
        {
            AntdUI.Table currentTable = sender as AntdUI.Table;
            List<TaskItem> currentData = GetcurrentData(currentTable);
            draggedItem = currentData.FirstOrDefault(item => item.index == currentTable.SelectedIndex);

        }

        #endregion
        


        private void TaskListForm_SizeChanged(object sender, EventArgs e)
        {
            suitheight();
        }

        
    }
}
