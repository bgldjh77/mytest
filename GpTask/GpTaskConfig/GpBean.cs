using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.MenuConfig;

namespace 质检工具
{
    public class GpBean
    {
        public MenuBean menuBean { get; set; }
        public List<ToolArgs>toolArglist { get; set; }
        public string createTime { get; set; }
        public string startTime { get; set; }
        public GpStateEnum state { get; set; }
        public string endTime { get; set; }
        public string BSM { get; set; }
        public bool cancel { get; set; }
    }
}
