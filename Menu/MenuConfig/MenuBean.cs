using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具
{
    public class MenuBean
    {
        public MenuBean()
        {

        }
        public string folderName { get; set; }//一级菜单
        public string toolboxName { get; set; }//二级菜单
        public bool toolboxExpand { get; set; }//二级菜单是否展开
        public string toolsetName { get; set; }//三级菜单
        public bool toolsetExpand { get; set; }//三级菜单是否展开
        public string toolName { get; set; }//工具名称
                                            //public int newlevel { get; set; }//当前等级
                                            //public Font font { get; set; }//字体

        public MenuBean CreatMenuBean(string line)
        {
            var values = line.Split(',');
            var bean = new MenuBean
            {
                folderName = values[0],
                toolboxName = values[1],
                toolboxExpand = str2bool(values[2]),
                toolsetName = values[3],
                toolsetExpand = str2bool(values[4]),
                toolName = values[5]
            };
            return bean;
        }
        private bool str2bool(string instr)
        {
            if (instr == "是") { return true; }
            return false;
        }
    }
}
