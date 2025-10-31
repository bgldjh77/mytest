using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.AntdTableFunc
{
    class TaskList
    {
        public class TaskItem : AntdUI.NotifyProperty
        {
            private bool _check = false;
            public bool check
            {
                get => _check;
                set
                {
                    if (_check == value) return;
                    _check = value;
                    OnPropertyChanged();
                }
            }
            private AntdUI.CellLink[] _btns;
            public AntdUI.CellLink[] btns
            {
                get => _btns;
                set
                {
                    _btns = value;
                    OnPropertyChanged();
                }
            }

            public bool checkbox { get; set; }
            public int index { get; set; }
            public AntdUI.CellBadge state { get; set; }
            public string toolname { get; set; }
            public string createtime { get; set; }
            public string starttime { get; set; }
            public string endtime { get; set; }
            public string foldername { get; set; }
            public string toolboxname { get; set; }
            public string toolsetname { get; set; }
            public string BSM { get; set; }

            public TaskItem(int index, GpBean gpBean)
            {
                //this.index = (index + 1).ToString();
                this.index = (index + 1);
                this.toolname = gpBean.menuBean.toolName;
                this.createtime = gpBean.createTime;
                this.starttime = gpBean.startTime;
                this.endtime = gpBean.endTime;
                this.foldername = gpBean.menuBean.folderName;
                this.toolboxname = gpBean.menuBean.toolboxName;
                this.toolsetname = gpBean.menuBean.toolsetName;
                this.btns = new AntdUI.CellLink[] { new AntdUI.CellLink("edit_btn", "编辑") };
                this.BSM = gpBean.BSM;
                // 根据任务状态设置不同的状态标记
                if (gpBean.state.ToString() == "运行中")
                    this.state = new AntdUI.CellBadge(AntdUI.TState.Processing, "运行中");
                else if (gpBean.state.ToString() == "已完成")
                    this.state = new AntdUI.CellBadge(AntdUI.TState.Success, "已完成");
                else if (gpBean.state.ToString() == "失败")
                    this.state = new AntdUI.CellBadge(AntdUI.TState.Error, "失败");
                else
                    this.state = new AntdUI.CellBadge(AntdUI.TState.Default, gpBean.state.ToString());
            }
        }
    }
}
