using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJZX_ZJAddin.Utils
{
    class CsvUtils
    {
        /// <summary>
        /// 将 CSV 文件读取为 DataTable。默认以逗号为分隔符，支持简单的引号包裹字段。
        /// </summary>
        /// <param name="filePath">CSV 文件路径</param>
        /// <param name="delimiter">分隔符，默认,</param>
        /// <param name="encoding">文本编码，默认 UTF8</param>
        /// <returns>包含表头与数据的 DataTable</returns>
        public static DataTable ReadCsvToDataTable(string filePath, char delimiter, Encoding encoding)
        {
            // 如果文件路径为空，直接抛出异常
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("filePath 不能为空", "filePath");
            }

            // 编码处理：C#4.0 兼容写法，传入为 null 时回退到 UTF8
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            DataTable dataTable = new DataTable();

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filePath, encoding);

                string line;
                bool isFirstLine = true;
                List<string> headers = null;

                while ((line = reader.ReadLine()) != null)
                {
                    // 解析一行，考虑引号中可能包含分隔符的情况
                    List<string> fields = ParseCsvLine(line, delimiter);

                    if (isFirstLine)
                    {
                        headers = new List<string>();
                        foreach (string header in fields)
                        {
                            string columnName = header;
                            if (columnName == null)
                            {
                                columnName = string.Empty;
                            }
                            headers.Add(columnName);
                            dataTable.Columns.Add(columnName);
                        }
                        isFirstLine = false;
                    }
                    else
                    {
                        DataRow dataRow = dataTable.NewRow();
                        int colCount = dataTable.Columns.Count;
                        for (int i = 0; i < colCount; i++)
                        {
                            string value = null;
                            if (i < fields.Count)
                            {
                                value = fields[i];
                            }
                            if (value == null)
                            {
                                value = string.Empty;
                            }
                            dataRow[i] = value;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                // 根据你的需求处理异常，例如记录日志或重新抛出
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return dataTable;
        }

        // 简单的 CSV 行解析：支持字段被双引号包裹，包裹字段中可包含分隔符和双引号
        private static List<string> ParseCsvLine(string line, char delimiter)
        {
            List<string> result = new List<string>();
            if (line == null)
            {
                result.Add(string.Empty);
                return result;
            }

            bool inQuotes = false;
            StringBuilder current = new StringBuilder();

            int i = 0;
            while (i < line.Length)
            {
                char c = line[i];

                if (inQuotes)
                {
                    if (c == '"')
                    {
                        // 处理双引号转义
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            current.Append('"');
                            i += 2;
                            continue;
                        }
                        else
                        {
                            inQuotes = false;
                            i++;
                            continue;
                        }
                    }
                    else
                    {
                        current.Append(c);
                        i++;
                        continue;
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        inQuotes = true;
                        i++;
                        continue;
                    }
                    else if (c == delimiter)
                    {
                        result.Add(current.ToString());
                        current.Length = 0;
                        i++;
                        continue;
                    }
                    else
                    {
                        current.Append(c);
                        i++;
                        continue;
                    }
                }
            }

            // 添加最后一个字段
            result.Add(current.ToString());

            return result;
        }

        /// <summary>
        /// 将 string[,] 数据写入 CSV 文件，起始行列由 startRow、startColumn 指定。默认从左上角开始写入，忽略数据第一维的单元格空值按空字符串处理。
        /// </summary>
        /// <param name="filePath">目标 CSV 文件路径</param>
        /// <param name="data">要写入的二维字符串数组</param>
        /// <param name="startRow">起始行（1-based）</param>
        /// <param name="startColumn">起始列（1-based）</param>
        /// <param name="fileSaveCopyPath">可选：写入完成后保存的副本路径</param>
        public static void WriteDataToCsv(string filePath, string[,] data, int startRow, int startColumn, string fileSaveCopyPath = null)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentException("filePath 不能为空", "filePath");

            // 计算输出目标的行数和列数
            int dataRows = data.GetLength(0);
            int dataCols = data.GetLength(1);

            // 打开或创建目标文件，使用写入流覆盖原文件
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // 将数据按行写入
                for (int r = 0; r < dataRows; r++)
                {
                    var lineFields = new string[dataCols];
                    for (int c = 0; c < dataCols; c++)
                    {
                        lineFields[c] = data[r, c] ?? string.Empty;
                    }
                    string line = ToCsvLine(lineFields);
                    writer.WriteLine(line);
                }
            }

            // 如需要生成副本
            if (!string.IsNullOrEmpty(fileSaveCopyPath))
            {
                File.Copy(filePath, fileSaveCopyPath, true);
            }
        }

        // 将字段数组转为 CSV 行，使用双引号包裹含有逗号/引号/换行的字段
        private static string ToCsvLine(string[] fields)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0) sb.Append(',');

                string field = fields[i] ?? string.Empty;
                bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n");
                if (mustQuote)
                {
                    sb.Append('"');
                    // 转义双引号
                    sb.Append(field.Replace("\"", "\"\""));
                    sb.Append('"');
                }
                else
                {
                    sb.Append(field);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将 CSV 文件中除了第一行以外的内容清空（保留第一行）。
        /// 实现思路：读取第一行，随后仅写入第一行到目标文件，删除其余行。
        /// </summary>
        /// <param name="csvFilePath">CSV 文件路径</param>
        /// <param name="overwriteOriginal">是否覆盖原文件（true）还是写入新文件（false）</param>
        /// <param name="newFilePath">如果不覆盖原文件，指定新文件路径；否则可为 null</param>
        public static void ClearCsvRowsExceptFirst(string csvFilePath, bool overwriteOriginal = true, string newFilePath = null)
        {
            if (string.IsNullOrEmpty(csvFilePath))
                throw new ArgumentException("csvFilePath 不能为空", "csvFilePath");

            //if (!File.Exists(csvFilePath))
            //    throw new FileNotFoundException("CSV 文件未找到", csvFilePath);

            // 读取第一行
            string firstLine=null;
            if (File.Exists(csvFilePath))
            {
                using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
                {
                    firstLine = reader.ReadLine();
                }
            }
                

            if (firstLine == null)
            {
                // 空文件，nothing to do
                firstLine = string.Empty;
            }

            // 根据选项写回文件
            if (overwriteOriginal)
            {
                // 覆盖原文件，仅写入第一行
                using (var writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
                {
                    writer.WriteLine(firstLine);
                }

                Console.WriteLine("CSV 文件已清空，保留第一行。");
            }
            else
            {
                string targetPath = string.IsNullOrEmpty(newFilePath) ? csvFilePath : newFilePath;

                // 将第一行写入新文件
                using (var writer = new StreamWriter(targetPath, false, Encoding.UTF8))
                {
                    writer.WriteLine(firstLine);
                }

                Console.WriteLine("新的 CSV 文件已创建，仅保留第一行。");
            }
        }

        /// <summary>
        /// 在 CSV 文件中按行列方式检索指定值，返回其所在的行号与列号（1-based）。
        /// </summary>
        /// <param name="filePath">CSV 文件路径</param>
        /// <param name="delimiter">分隔符，默认为逗号</param>
        /// <param name="valueToFind">要查找的值</param>
        /// <returns>Tuple(行号, 列号)，若未找到则返回 (-1, -1)</returns>
        public static Tuple<int, int> FindValueInCsv(string filePath, char delimiter, string valueToFind)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath 不能为空", "filePath");

            if (valueToFind == null)
                valueToFind = string.Empty;

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string line;
                int rowIndex = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    rowIndex++;

                    List<string> fields = ParseCsvLine(line, delimiter);

                    for (int colIndex = 0; colIndex < fields.Count; colIndex++)
                    {
                        string field = fields[colIndex] ?? string.Empty;
                        if (field == valueToFind)
                        {
                            // 行列均为 1-based
                            return Tuple.Create(rowIndex, colIndex + 1);
                        }
                    }
                }
            }

            // 未找到
            return Tuple.Create(-1, -1);
        }

      


    }
}
