using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.MenuConfig
{
    public static class ArgTypeExtensions
    {
        public static string argtype2str(this ArgTypeEnum argtype)
        {
            switch (argtype)
            {
                case ArgTypeEnum.FeatureClass:
                    return "要素类";
                case ArgTypeEnum.Int:
                    return "长整型";
                case ArgTypeEnum.Folder:
                    return "文件夹";
                case ArgTypeEnum.Workspace:
                    return "工作空间";
                case ArgTypeEnum.Double:
                    return "双精度";
                case ArgTypeEnum.File:
                    return "文件";
                case ArgTypeEnum.Raster:
                    return "栅格图层";
                case ArgTypeEnum.Coordinate:
                    return "坐标系";
                case ArgTypeEnum.Table: 
                    return "表";
                default:
                    return "字符串";
            }
        }
    }
    public class ToolArgs
    {
        public ToolArgs()
        {

        }
        public string folderName { get; set; }//一级菜单
        public string toolboxName { get; set; }//二级菜单
        public string toolsetName { get; set; }//三级菜单
        public string toolName { get; set; }//工具名称
        public string argName { get; set; }//参数名称
        public ArgTypeEnum argType { get; set; }//参数类型
        public DirectionEnum direction { get; set; }//参数方向
        public string defalutValue { get; set; }//参数默认值
        public string nowValue { get; set; }//当前参数值
        public string replaceValue { get; set; }//替换的值 显示与传递值不一定一样
        public string fileExtension { get; set; }//文件后缀
        public ToolArgs CreatArgBean(string line)
        {
            var values = line.Split(',');
            if (values.Length < 10) // 确保字段数量足够
            {
               
                return null; // 或抛出异常
            }

            try
            {
                var bean = new ToolArgs
                {
                    folderName = string.IsNullOrEmpty(values[0]) ? null : values[0],
                    toolboxName = string.IsNullOrEmpty(values[1]) ? null : values[1],
                    toolsetName = string.IsNullOrEmpty(values[3]) ? null : values[3],
                    toolName = values[5],
                    argName = values[6],
                    argType = str2argtype(values[7]),
                    direction = str2direction(values[8]),
                    defalutValue = values[9],
                    nowValue = string.IsNullOrEmpty(values[9]) ? "" : values[9] // 确保 nowValue 不为空
                };
                if (bean.argType == ArgTypeEnum.File) { bean.fileExtension = values[7].Replace("文件", ""); }
                return bean;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog($"解析 CSV 行时出错：{line}", ex);
                return null; // 或抛出异常
            }
        }

        private ArgTypeEnum str2argtype(string instr)
        {
            if (instr == "要素类") { return ArgTypeEnum.FeatureClass; }
            else if (instr == "长整型") { return ArgTypeEnum.Int; }
            else if (instr == "文件夹") { return ArgTypeEnum.Folder; }
            else if (instr == "工作空间") { return ArgTypeEnum.Workspace; }
            else if (instr == "双精度") { return ArgTypeEnum.Double; }          
            else if (instr == "栅格图层") { return ArgTypeEnum.Raster; }
            else if (instr == "栅格数据集") { return ArgTypeEnum.Raster; }
            else if (instr == "坐标系") { return ArgTypeEnum.Coordinate; }
            else if (instr == "表") { return ArgTypeEnum.Table; }
            else if (instr.Contains("文件")) { return ArgTypeEnum.File; }
            return ArgTypeEnum.Str;
        }

        private DirectionEnum str2direction(string instr)
        {
            //if (instr == "输入") { return DirectionEnum.input; }
            if (instr == "输入") { return DirectionEnum.输入; }
            return DirectionEnum.输出;
        }
    }
}
