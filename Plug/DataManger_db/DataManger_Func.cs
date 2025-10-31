using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJZX_ZJAddin.Utils;
using 质检工具.MenuConfig;

namespace 质检工具.Plug.DataManger_db
{
    class DataManger_Func
    {
        public static Encoding DetectEncoding(string filePath)
        {
            byte[] buffer;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int length = (int)Math.Min(4096, fs.Length); // 读取前 4KB
                buffer = new byte[length];
                fs.Read(buffer, 0, length);
            }
            if (IsReasonableUtf8(Encoding.UTF8.GetString(buffer)))
            {
                return Encoding.UTF8;
            }

            //using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            //{
            //    //var bom = new byte[3];
            //    //if (fs.Read(bom, 0, 3) == 3)
            //    //{
            //    //    if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
            //    //        return Encoding.UTF8; // 带 BOM 的 UTF-8
            //    //}


            //}

            // 没有 BOM，可能是 UTF-8 或 GBK，需要进一步判断
            return Encoding.GetEncoding("GBK");
        }
        private static bool IsReasonableUtf8(string text)
        {
            // 如果包含中文字符（Unicode 范围 \u4e00-\u9fa5），且没有出现乱码符号（如 ?）
            return System.Text.RegularExpressions.Regex.IsMatch(text, @"[\u4e00-\u9fa5]") &&
                   !text.Contains("?");
        }
        //模板创建数据库
        public static void model2db(List<ToolArgs> TaskArgs)
        {
            //string toolname = "模板创建数据库";
            string toolname = TaskArgs[0].toolName;
            List<string> arglist = new List<string>();
            foreach(ToolArgs toolArgs in TaskArgs)
            {
                arglist.Add(toolArgs.nowValue);
            }
            try
            {
                string modelPath = arglist[0];
                string gdbName = arglist[1];
                string gdbPath = System.IO.Path.GetDirectoryName(modelPath) + "\\" + gdbName;
                if (Directory.Exists(gdbPath+".gdb"))
                {
                    LogHelper.ErrorLog("模板创建数据库", "未执行，已存在同名数据库");
                    return;
                }
                // 提取模板结构
                //System.Data.DataTable ModelDt = ZJZX_ZJAddin.Utils.ExcelUtils.ReadExcelFileToDataTable(modelPath);
                System.Data.DataTable ModelDt = CsvUtils.ReadCsvToDataTable(modelPath, ',', DetectEncoding(modelPath));
                ZJZX_ZJAddin.Checker.FeatureChecker featureChecker = new ZJZX_ZJAddin.Checker.FeatureChecker();
                // 进行数据库创建
                System.Collections.ArrayList resultList = featureChecker.CreateFeatureByModel(gdbPath, ModelDt);

                LogHelper.add2mainlog(GetNowTime.nowTime() + "已成功执行：" + toolname + "\n");
                LogHelper.Successnotice("已成功执行：" + toolname);
            }
            catch(Exception e)
            {
                LogHelper.ErrorLog(toolname + "失败", e);
            }
        }
        //数据库转模板文件
        public static void db2model(List<ToolArgs> TaskArgs)
        {
            //string toolname = "数据库转模板文件";
            string toolname = TaskArgs[0].toolName;
            List<string> arglist = new List<string>();
            foreach (ToolArgs toolArgs in TaskArgs)
            {
                arglist.Add(toolArgs.nowValue);
            }

            try
            {
                // 用户输入数据库gdb路径，以及输出模板文件的存放路径
                string gdbPath = arglist[0];
                string newMBFilePath = arglist[1];

                GDBFileUtils gdbFileUtils = new GDBFileUtils();
                ExcelUtils excelUtils = new ExcelUtils();

                // 提取数据库结构
                List<FeatureInfo> featureInfoList = gdbFileUtils.ReadGDBFeatures(gdbPath);

                // 获取模板资源excel（与数据库结构检查excel模板相同）
                //...
                //newMBFilePath = （将软件内置的模板提取存在用户输入路径中）

                // 清空模板文件存在的示例数据
                //excelUtils.ClearRowsWithoutFirst(newMBFilePath);
                CsvUtils.ClearCsvRowsExceptFirst(newMBFilePath);
                // 对空白模板表进行重构
                Tuple<int, int> sjj_tuple = ExcelUtils.FindValueInWorksheet(newMBFilePath, ",", "要素数据集");

                // 设置模板映射字段
                var insertlist = new List<string[]>();
                insertlist.Add(new string[] { "要素数据集", "要素数据集别名", "要素类名称", "要素类别名", "要素类几何类型", "字段名", "别名", "字段类型", "字段长度", "小数位数", "是否必填", "是否进行小数位数检查" });

                // 进行数据类型转换 完成模板文件输出
                foreach (FeatureInfo featureInfo in featureInfoList)
                {
                    insertlist.Add(new string[] {
                       featureInfo.FeatureDatasetName,
                       featureInfo.FeatureDatasetAliasName,
                       featureInfo.Name,
                       featureInfo.AliasName,
                       featureInfo.FeatureType,
                       featureInfo.FieldName,
                       featureInfo.FieldAliasName,
                       featureInfo.FieldType,
                       featureInfo.FieldLength.ToString(),
                       featureInfo.NumberFormat.ToString(),
                       featureInfo.isNull,
                       "否"});
                }
                // 如果需要，将List转换成二维数组
                string[,] insertarray = new string[insertlist.Count, insertlist[0].Length];

                for (int i = 0; i < insertlist.Count; i++)
                {
                    for (int j = 0; j < insertlist[i].Length; j++)
                    {
                        insertarray[i, j] = insertlist[i][j];
                    }
                }
                CsvUtils.WriteDataToCsv(newMBFilePath, insertarray, sjj_tuple.Item1, sjj_tuple.Item2);
                //ExcelUtils.InsertDataIntoExcel(newMBFilePath, "Sheet1", insertarray, sjj_tuple.Item1, sjj_tuple.Item2);
                LogHelper.add2mainlog(GetNowTime.nowTime() + "已成功执行：" + toolname + "\n");
                LogHelper.Successnotice("已成功执行：" + toolname);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog(toolname+"失败", e);
            }
        }
    }
}
