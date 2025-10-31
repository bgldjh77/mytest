using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.SysFunc
{
    class myCopyFunc
    {
        /// <summary>
        /// 复制文件或者文件夹到指定目录
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        public static void CopyFileOrDirectory(string sourcePath, string targetPath)
        {
            try
            {
                if (File.Exists(sourcePath))
                {
                    // 如果是文件，直接复制
                    string fileName = Path.GetFileName(sourcePath);
                    string destFile = Path.Combine(targetPath, fileName);
                    File.Copy(sourcePath, destFile, true);
                }
                else if (Directory.Exists(sourcePath))
                {
                    // 如果是目录，递归复制
                    string dirName = Path.GetFileName(sourcePath);
                    string destDir = Path.Combine(targetPath, dirName);

                    // 创建目标目录
                    if (!Directory.Exists(destDir))
                    {
                        Directory.CreateDirectory(destDir);
                    }

                    // 复制所有文件
                    foreach (string file in Directory.GetFiles(sourcePath))
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(destDir, fileName);
                        File.Copy(file, destFile, true);
                    }

                    // 递归复制子目录
                    foreach (string dir in Directory.GetDirectories(sourcePath))
                    {
                        CopyFileOrDirectory(dir, destDir);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"复制过程中出现错误：{ex.Message}");
                LogHelper.ErrorLog("复制过程中出现错误",ex);
            }
        }
    }
}
