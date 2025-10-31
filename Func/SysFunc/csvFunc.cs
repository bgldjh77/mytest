using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 质检工具.MenuConfig;

namespace 质检工具.Func.SysFunc
{
    class csvFunc
    {
        public List<string> LoadToolConfig(string toolname)
        {
            List<string> toolConfig = new List<string>();
            try
            {
                
                using (StreamReader reader = new StreamReader(globalpara.toolconfigpath, Encoding.GetEncoding("GB2312")))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        string[] columns = line.Split(',');
                        if (columns.Length >= 6 && columns[5].Trim() == toolname)
                        {
                            toolConfig.Add(line);
                            //toolConfig.AddRange(columns);
                            //break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("读取工具配置csv错误",ex);
            }
            return toolConfig;
        }

        public List<string> LoadToolConfig(MenuBean menubean,string csvpath)
        {
            List<string> toolConfig = new List<string>();
            try
            {

                using (StreamReader reader = new StreamReader(csvpath, Encoding.GetEncoding("GB2312")))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        string[] columns = line.Split(',');
                        if (columns.Length >= 6 && columns[5].Trim() == menubean.toolName)
                        {
                            if(columns[0].Trim() == menubean.folderName)
                            {
                                if (columns[1].Trim() == menubean.toolboxName)
                                {
                                    if (columns[3].Trim() == menubean.toolsetName)
                                    {
                                        toolConfig.Add(line);
                                    }
                                    
                                }
                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("读取工具配置csv错误", ex);
            }
            return toolConfig;
        }
        public void PreprocessCsv(string csvPath)
        {
            try
            {
                // 读取所有行
                var lines = File.ReadAllLines(csvPath, Encoding.GetEncoding("GB2312")).ToList();

                // 遍历每一行，检查列数
                for (int i = 0; i < lines.Count; i++)
                {
                    var columns = lines[i].Split(',');

                    // 如果列数不足 12，则添加一个空的 nowValue 列
                    if (columns.Length < 12)
                    {
                        lines[i] = string.Join(",", columns) + ",";
                    }
                }

                // 将预处理后的内容写回文件
                File.WriteAllLines(csvPath, lines, Encoding.GetEncoding("GB2312"));
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("预处理 Control.csv 文件时出错", ex);
            }
        }
        public List<ToolArgs> LoadTaskCache(string csvpath)
        {
            List<ToolArgs> tals = new List<ToolArgs>();
            try
            {
                using (StreamReader reader = new StreamReader(csvpath, Encoding.GetEncoding("GB2312")))
                {
                    int rowindex = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (rowindex > 0) // 跳过表头
                        {
                            string[] columns = line.Split(',');
                            Console.WriteLine($"正在解析第 {rowindex} 行: {line}");
                            if (columns.Length >= 12) // 确保列数足够
                            {
                                ToolArgs tafunc = new ToolArgs();
                                ToolArgs ta = tafunc.CreatArgBean(line);

                                if (ta != null)
                                {
                                    ta.nowValue = columns[11]; // 确保正确解析 nowValue
                                    tals.Add(ta);
                                }
                                else
                                {
                                    Console.WriteLine($"解析失败的行：{line}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"列数不足的行：{line}");
                            }
                        }
                        rowindex += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("读取工具配置 CSV 错误", ex);
            }
            return tals;
        }
        //private List<string> GetToolnameLs(string csvpath)
        //{
        //    List<string> toolnamels = new List<string>();
        //    try
        //    {
        //        using (StreamReader reader = new StreamReader(csvpath, Encoding.GetEncoding("GB2312")))
        //        {
        //            int rowindex = 0;
        //            string line;
        //            while ((line = reader.ReadLine()) != null)
        //            {
        //                if (rowindex > 0)
        //                {
        //                    string[] columns = line.Split(',');
        //                    if (columns.Length >= 7)
        //                    {
        //                        string toolname = columns[6];
        //                        if (!toolnamels.Contains(toolname))
        //                        {
        //                            toolnamels.Add(toolname);
        //                        }

        //                    }
        //                }
        //                rowindex += 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.ErrorLog("读取工具配置csv错误", ex);
        //    }
        //    return toolnamels;
        //}

    }
}
