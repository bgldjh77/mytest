using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJZX_ZJAddin.Checker;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.MenuConfig;
using 质检工具.Plug.DataManger_db;

namespace 质检工具.Plug
{
    class PlugNameList
    {
        public static List<string> namelist = new List<string> 
        { "模板创建数据库", "数据库转模板文件","检查非法字段","检查细斑细线" };

        public static void RunWhichPlug(string toolname, List<ToolArgs> ta, List<string>BSMlist)
        {
            GpTaskFunc gpTaskFunc = new GpTaskFunc();
            try
            {
                switch (toolname)
                {
                    case "模板创建数据库":
                        DataManger_Func.model2db(ta);
                        break;
                    case "数据库转模板文件":
                        DataManger_Func.db2model(ta);
                        break;
                    case "检查非法字段":
                        FieldChecker.FindIllegalCharFieldValue(ta);
                        break;
                    case "检查细斑细线":
                        FieldChecker.FindTinyGeometry(ta);
                        break;
                }
                if (namelist.Contains(toolname)) 
                {
                    gpTaskFunc.GpTaskStateChange(BSMlist, GpStateEnum.已完成);
                } 
            }
            catch(Exception e)
            {
                gpTaskFunc.GpTaskStateChange(BSMlist, GpStateEnum.失败);
                LogHelper.ErrorLog("执行错误",e);
            }


        }
    }
}
