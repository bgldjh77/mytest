using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.SysFunc
{
    class ListMuSrcData
    {
        public List<string> ListFolderContents(string folderPath)
        {
            List<string> contents = new List<string>();
            try
            {
                // 获取文件夹
                string[] folders = Directory.GetDirectories(folderPath);
                foreach (string folder in folders)
                {
                    contents.Add(Path.GetFileName(folder));
                }

                // 获取文件
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    contents.Add(Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取文件夹失败：{ex.Message}");
            }
            return contents;
        }
    }
}
