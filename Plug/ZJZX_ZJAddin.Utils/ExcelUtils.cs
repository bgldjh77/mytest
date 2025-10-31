using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ZJZX_ZJAddin.Utils
{
    class ExcelUtils
    {
        /// <summary>
        /// 读取Excel表转为表格
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static System.Data.DataTable ReadExcelFileToDataTable(string filePath)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[1]; // 获取第一个工作表  

                // 获取列数  
                Excel.Range range = worksheet.UsedRange;
                int colCount = range.Columns.Count;
                int rowCount = range.Rows.Count;

                // 读取表头  
                for (int col = 1; col <= colCount; col++)
                {
                    var headerValue = (range.Cells[1, col] as Excel.Range).Value2;
                    dataTable.Columns.Add(headerValue != null ? headerValue.ToString() : string.Empty);
                }

                // 读取数据  
                for (int row = 2; row <= rowCount; row++) // 从第二行开始读取数据  
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        var cellValue = (range.Cells[row, col] as Excel.Range).Value2;
                        dataRow[col - 1] = cellValue != null ? cellValue.ToString() : string.Empty;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // 关闭 Excel 应用  
                if (workbook != null)
                {
                    workbook.Close(false);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }

            return dataTable;
        }


        /// <summary>
        /// 检查指定的二维数组数据是否已经存在于 Excel 表中。
        /// </summary>
        /// <param name="filePath">Excel 文件路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="data">要检查的二维数组数据</param>
        /// <returns>如果数据已存在，返回 true；否则返回 false</returns>
        public static bool IsDataAlreadyInWorksheet(string filePath, string sheetName, string[,] data)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // 打开 Excel 文件
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = workbook.Worksheets[sheetName] as Excel.Worksheet;

                if (worksheet == null)
                {
                    throw new Exception($"工作表 '{sheetName}' 不存在.");
                }

                // 获取工作表的已用范围
                Excel.Range usedRange = worksheet.UsedRange;
                int rowCount = usedRange.Rows.Count;
                int colCount = usedRange.Columns.Count;

                // 遍历工作表，检查是否存在完全相同的数据
                for (int row = 1; row <= rowCount - data.GetLength(0) + 1; row++)
                {
                    for (int col = 1; col <= colCount - data.GetLength(1) + 1; col++)
                    {
                        bool isMatch = true;

                        // 检查数据是否匹配
                        for (int i = 0; i < data.GetLength(0); i++)
                        {
                            for (int j = 0; j < data.GetLength(1); j++)
                            {
                                var cellValue = (usedRange.Cells[row + i, col + j] as Excel.Range)?.Value2?.ToString();
                                if (cellValue != data[i, j])
                                {
                                    isMatch = false;
                                    break;
                                }
                            }
                            if (!isMatch) break;
                        }

                        if (isMatch) return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"检查数据是否已存在时发生错误：{ex.Message}");
                throw;
            }
            finally
            {
                // 释放 Excel 对象
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                // 强制垃圾回收
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// 按照 Excel 表第三行的列名为准，从 DataTable 中提取匹配的列并加载到 Excel 表中。
        /// 数据从第四行开始写入，避免覆盖表头。
        /// </summary>
        /// <param name="excelPath">Excel 文件路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="dataTable">数据源 DataTable</param>
        /// <param name="startRow">插入数据的起始行（应为 4 或更大）</param>
        public static void LoadDataToExcelByColumnNames(string excelPath, string sheetName, System.Data.DataTable dataTable, int startRow)
        {
       

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // 打开 Excel 文件
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(excelPath);
                worksheet = workbook.Worksheets[sheetName] as Excel.Worksheet;

                if (worksheet == null)
                {
                    throw new Exception($"工作表 '{sheetName}' 不存在.");
                }

                // 获取 Excel 表第三行的列名
                Excel.Range usedRange = worksheet.UsedRange;
                int columnCount = usedRange.Columns.Count;
                List<string> excelColumnNames = new List<string>();

                for (int col = 1; col <= columnCount; col++)
                {
                    var cellValue = (usedRange.Cells[3, col] as Excel.Range)?.Value2;
                    excelColumnNames.Add(cellValue != null ? cellValue.ToString().Trim() : string.Empty);
                }

                // 遍历 Excel 列名，逐列检查 DataTable 中是否存在对应的列
                foreach (string excelColumnName in excelColumnNames)
                {
                    if (dataTable.Columns.Contains(excelColumnName))
                    {
                        // 获取 DataTable 中对应列的数据
                        DataColumn dataColumn = dataTable.Columns[excelColumnName];
                        for (int row = 0; row < dataTable.Rows.Count; row++)
                        {
                            var cellValue = dataTable.Rows[row][dataColumn];
                            worksheet.Cells[startRow + row, excelColumnNames.IndexOf(excelColumnName) + 1] = cellValue;
                        }
                    }
                }

                // 保存 Excel 文件
                workbook.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载数据到 Excel 时发生错误：{ex.Message}");
                throw;
            }
            finally
            {
                // 释放 Excel 对象
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                // 强制垃圾回收
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public static string[] GetWorksheetColumnNamesInterop(string excelPath, string sheetName)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // 创建 Excel 应用程序对象
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(excelPath);

                // 查找指定的工作表
                worksheet = workbook.Worksheets[sheetName] as Excel.Worksheet;
                if (worksheet == null)
                {
                    throw new Exception($"未找到工作表：{sheetName}");
                }

                // 获取第一行的列名
                Excel.Range usedRange = worksheet.UsedRange;
                int columnCount = usedRange.Columns.Count;
                string[] columnNames = new string[columnCount];

                for (int col = 1; col <= columnCount; col++)
                {
                    var cellValue = (usedRange.Cells[3, col] as Excel.Range)?.Value2;
                    columnNames[col - 1] = cellValue != null ? cellValue.ToString().Trim() : string.Empty;
                }

                return columnNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取工作表列名时发生错误：{ex.Message}");
                throw;
            }
            finally
            {
                // 释放 Excel 对象
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                // 强制垃圾回收
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public static List<string> GetAllWorksheetNamesInterop(string excelPath)
        {
            List<string> sheetNames = new List<string>();
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(excelPath);

                foreach (Excel.Worksheet sheet in workbook.Sheets)
                {
                    sheetNames.Add(sheet.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"使用 Interop 读取 Excel 文件时发生错误：{ex.Message}");
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
            }

            return sheetNames;
        }
        /// <summary>
        /// 在对应工作表中插入数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="data"></param>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        public static void InsertDataIntoExcel(string filePath, string sheetName, string[,] data, int startRow, int startColumn, string fileSaveCopyPath = null)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // 创建 Excel 应用程序对象
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = workbook.Worksheets[sheetName] as Excel.Worksheet;

                if (worksheet == null)
                {
                    throw new Exception($"工作表 '{sheetName}' 不存在.");
                }

                // 插入数据
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        worksheet.Cells[startRow + row, startColumn + col] = data[row, col];
                    }
                }

                // 保存文件
                workbook.Save();
                if (!string.IsNullOrEmpty(fileSaveCopyPath))
                {
                    workbook.SaveCopyAs(fileSaveCopyPath);
                }
            }
            finally
            {
                // 释放 Excel 对象
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                // 强制垃圾回收
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Excel根据某值来检索标签的行列号
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static Tuple<int, int> FindValueInWorksheet(string filePath, string sheetName, string valueToFind)
        {
            // 创建 Excel 应用程序对象  
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                // 打开 Excel 文件  
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = workbook.Worksheets[sheetName] as Excel.Worksheet;
                if (worksheet == null)
                {
                    throw new Exception("工作表不存在.");
                }

                // 获取最大行和列  
                int rowCount = worksheet.UsedRange.Rows.Count;
                int colCount = worksheet.UsedRange.Columns.Count;

                // 遍历单元格查找值  
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        Excel.Range cell = worksheet.Cells[row, col] as Excel.Range;
                        if (cell.Value2 != null && cell.Value2.ToString() == valueToFind)
                        {
                            return Tuple.Create(row, col); // 返回行和列  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误: " + ex.Message);
            }
            finally
            {
                // 关闭 Excel 工作簿和应用程序  
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
                if (worksheet != null)
                {
                    Marshal.ReleaseComObject(worksheet);
                }

                // 强制垃圾回收  
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return Tuple.Create(-1, -1); // 如果没有找到，返回 (-1, -1)  
        }

      /*  /// <summary>
        /// 提取资源存在临时路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string ExecuteExcelFromEmbeddedExcel(string filename)
        {
            ResourcesUtils resourcesUtils = new ResourcesUtils();
            string tempExcelPath = Path.GetTempFileName() + ".xlsx"; // 创建临时文件路径
            resourcesUtils.ExtractEmbeddedResource(filename, tempExcelPath);
            return tempExcelPath;
        }
*/

        /// <summary>
        /// 将指定的内存表，输出成为列名表
        /// </summary>
        /// <param name="dt">要转换的 DataTable</param>
        /// <returns></returns>
        public static string[] GetDataTableColumnNames(System.Data.DataTable dt)
        {
            return dt.Columns.Cast<DataColumn>()
                             .Select(col => col.ColumnName)
                             .ToArray();
        }

        /// <summary>  
        /// 将 DataTable 转为 string[,] 根据指定的列名  
        /// </summary>  
        /// <param name="dataTable">要转换的 DataTable</param>  
        /// <param name="desiredColumns">需要保留的列名列表</param>  
        /// <returns>返回二维字符串数组</returns>  
        public static string[,] ConvertDataTableToStringArray(System.Data.DataTable dataTable, string[] desiredColumns)
        {
            // 获取 DataTable 中的行数和所需列的数量  
            int rowCount = dataTable.Rows.Count;
            int columnCount = desiredColumns.Length;

            // 创建二维字符串数组，+1 用于列名作为第一行  
            string[,] stringArray = new string[rowCount + 1, columnCount];

            // 填充第一行：列名  
            for (int j = 0; j < columnCount; j++)
            {
                stringArray[0, j] = desiredColumns[j];
            }

            // 填充剩余行：数据  
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = dataTable.Rows[i];
                for (int j = 0; j < columnCount; j++)
                {
                    string columnName = desiredColumns[j];
                    if (dataTable.Columns.Contains(columnName))
                    {
                        stringArray[i + 1, j] = row[columnName].ToString(); // 使用 null 条件运算符  
                    }
                }
            }

            return stringArray;
        }

        public void ClearRowsWithoutFirst(string excelFile)
        {
            // 创建Excel应用对象
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // 打开工作簿
            Excel.Workbook workbook = excelApp.Workbooks.Open(excelFile);
            // 取第一个工作表
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            // 获取最后一行和最后一列
            int lastRow = worksheet.UsedRange.Rows.Count;
            int lastCol = worksheet.UsedRange.Columns.Count;

            // 仅保留第一行，清空其他所有行
            if (lastRow > 1)
            {
                Range rangeToClear = worksheet.Range[worksheet.Rows[2], worksheet.Rows[lastRow]];
                rangeToClear.ClearContents();
            }

            // 保存并关闭
            workbook.Save();
            workbook.Close();
            excelApp.Quit();

            Console.WriteLine("除了第一行外的内容已被清空。");
        }

        ///// <summary>  
        /// 打开指定工作簿中的指定工作表，并删除某列中符合条件的行。  
        /// </summary>  
        /// <param name="excelFile">Excel 文件路径</param>  
        /// <param name="sheetName">要打开的工作表名称</param>  
        /// <param name="columnIndex">要检查的列索引（从 1 开始）</param>  
        /// <param name="deleteValue">要删除的行的值</param>  
        public static void  DeleteRowsWithValue(string excelFile, string sheetName, int columnIndex, string deleteValue)
        {
            // 创建 Excel 应用实例  
            Excel.Application excelApp = new Excel.Application();

            // 不显示 Excel 窗口  
            excelApp.Visible = false;

            // 打开工作簿  
            Excel.Workbook workbook = excelApp.Workbooks.Open(excelFile);

            // 设置Excel为手动计算模式  
            excelApp.Calculation = Excel.XlCalculation.xlCalculationManual; // 设置为手动计算 

            // 尝试获取指定名称的工作表  
            Excel.Worksheet worksheet = null;

            try
            {
                worksheet = (Excel.Worksheet)workbook.Sheets[sheetName];

                // 获取工作表的最后一行  
                Excel.Range lastCell = worksheet.Cells[worksheet.Rows.Count, columnIndex];
                int lastRow = lastCell.End[Excel.XlDirection.xlUp].Row;

                // 从最后一行开始向上遍历  
                for (int row = lastRow; row >= 1; row--)
                {
                    // 读取指定列的值  
                    string cellValue = (worksheet.Cells[row, columnIndex] as Excel.Range).Text;

                    // 检查单元格的值  
                    if (cellValue.Equals(deleteValue, StringComparison.OrdinalIgnoreCase))
                    {
                        // 删除该行  
                        Excel.Range rowToDelete = (Excel.Range)(worksheet.Rows[row]);
                        rowToDelete.Delete();
                    }
                }

                // 遍历所有工作表并计算  
                foreach (Excel.Worksheet cal_worksheet in workbook.Worksheets)
                {
                    cal_worksheet.Calculate(); // 计算当前工作表  
                }



            }
            catch (System.Exception ex)
            {
                MessageBox.Show("发生错误：" + ex.Message.ToString());
            }
            finally
            {

                // 保存并关闭工作簿  
                workbook.Save();
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);

                // 退出 Excel 应用  
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
            }
        }


    }
}
