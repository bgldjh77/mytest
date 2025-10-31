using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 质检工具.MenuConfig;

namespace 质检工具.Menu
{
    class MenuBeanFunc
    {
        public string GetToolFullPath(MenuBean menuBean)
        {
            string baseDir = Path.GetDirectoryName(globalpara.toolconfigpath);
            string toolsetPath = string.IsNullOrEmpty(menuBean.toolsetName) ? "" : menuBean.toolsetName;
            string fulltoolpath = Path.Combine(baseDir, menuBean.folderName, menuBean.toolboxName + ".tbx", menuBean.toolName);

            return fulltoolpath;
        }
        /// <summary>
        /// 通过csv行文本创建一个MenuBean
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public MenuBean CreatBean(string line)
        {
            var values = line.Split(',');
            var bean = new MenuBean
            {
                folderName = values[0],
                toolboxName = values[1],
                toolboxExpand = str2bool(values[2]),
                toolsetName = values[3],
                toolsetExpand = str2bool(values[4]),
                toolName = values[5],
                //argName = values[6],
                ////argType = (ArgTypeEnum)Enum.Parse(typeof(ArgTypeEnum), values[7]),
                //argType = str2argtype(values[7]),
                ////direction = (DirectionEnum)Enum.Parse(typeof(DirectionEnum), values[8]),
                //direction = str2direction(values[8]),
                //defalutValue = values[9]
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
