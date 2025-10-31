using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using 质检工具.MenuConfig;

namespace 质检工具.Func.SysFunc
{
    class ReadExcel
    {
        
        /// <summary>
        /// 获取工具描述
        /// </summary>
        /// <param name="xlsxpath"></param>
        /// <param name="menuBean"></param>
        /// <returns></returns>
        public string ReadToolDesc(string xlsxpath, MenuBean menuBean)
        {
            FileInfo fileInfo = new FileInfo(xlsxpath);
            string result = "";
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 1; row <= rowCount; row++)
                {
                    string col1 = worksheet.Cells[row, 1].Text.Trim();
                    string col2 = worksheet.Cells[row, 2].Text.Trim();
                    string col3 = worksheet.Cells[row, 3].Text.Trim();

                    if (col1.Equals(menuBean.folderName, StringComparison.OrdinalIgnoreCase) &&
                        col2.Equals(menuBean.toolboxName, StringComparison.OrdinalIgnoreCase) &&
                        col3.Equals(menuBean.toolName, StringComparison.OrdinalIgnoreCase))
                    {
                        result = worksheet.Cells[row, 4].Text;
                        break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取工具名称列表
        /// </summary>
        /// <param name="xlsxpath"></param>
        //public void GetToolnameValues(string xlsxpath)
        //{
        //    List<string> values = new List<string>();
        //    FileInfo fileInfo = new FileInfo(xlsxpath);
            
        //    using (ExcelPackage package = new ExcelPackage(fileInfo))
        //    {
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
        //        int rowCount = worksheet.Dimension.Rows;

        //        // 从第2行开始（跳过标题行）
        //        for (int row = 2; row <= rowCount; row++)
        //        {
        //            string value = worksheet.Cells[row, 1].Text.Trim();
        //            if (!string.IsNullOrEmpty(value))
        //            {
        //                values.Add(value);
        //            }
        //        }
        //    }
        //    globalpara.toolnamelist=values;

        //}
            
    }
}
