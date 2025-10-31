using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 质检工具;

// 在文件开头的 namespace 声明之前添加这个静态类
public static class StreamExtensions
{
    public static byte[] ReadBytes(this Stream stream, int length)
    {
        byte[] buffer = new byte[length];
        int bytesRead = 0;
        while (bytesRead < length)
        {
            int n = stream.Read(buffer, bytesRead, length - bytesRead);
            if (n == 0)
            {
                throw new EndOfStreamException("请选择正确的工程文件");
            }
            bytesRead += n;
        }
        return buffer;
    }
}

namespace 质检工具.Func.SaveProject
{
    class ProjectFunc
    {
        class FileEntry
        {
            public string FileName { get; set; }
            public long FileSize { get; set; }

            public FileEntry(string fileName, long fileSize)
            {
                FileName = fileName;
                FileSize = fileSize;
            }
        }
        public void CreateProjectFile(string[] filePaths, string outputProjectFile)
        {
            using (FileStream projectStream = new FileStream(outputProjectFile, FileMode.Create))
            {
                // 写入头部信息
                WriteHeader(projectStream, filePaths);

                // 写入文件内容
                foreach (string filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        byte[] fileContent = File.ReadAllBytes(filePath);
                        projectStream.Write(fileContent, 0, fileContent.Length);
                    }
                    else
                    {
                        throw new FileNotFoundException($"文件不存在: {filePath}");
                    }
                }
            }
        }
        private static void WriteHeader(Stream stream, string[] filePaths)
        {
            List<FileEntry> entries = new List<FileEntry>();

            // 遍历文件，生成文件条目
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                long fileSize = new FileInfo(filePath).Length;
                entries.Add(new FileEntry(fileName, fileSize));
            }

            // 写入文件条目数量
            int entryCount = entries.Count;
            stream.Write(BitConverter.GetBytes(entryCount), 0, sizeof(int));

            // 写入每个文件条目的元信息
            foreach (var entry in entries)
            {
                // 写入文件名长度
                int fileNameLength = entry.FileName.Length;
                stream.Write(BitConverter.GetBytes(fileNameLength), 0, sizeof(int));

                // 写入文件名
                byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(entry.FileName);
                stream.Write(fileNameBytes, 0, fileNameBytes.Length);

                // 写入文件大小
                stream.Write(BitConverter.GetBytes(entry.FileSize), 0, sizeof(long));
            }
        }

        public void ExtractFiles(string projectFile, string outputDirectory)
        {
            using (FileStream projectStream = new FileStream(projectFile, FileMode.Open))
            {
                // 读取头部信息
                List<FileEntry> entries = ReadHeader(projectStream);

                // 根据头部信息提取文件内容
                foreach (var entry in entries)
                {
                    string outputPath = Path.Combine(outputDirectory, entry.FileName);

                    // 读取文件内容并保存
                    byte[] fileContent = new byte[entry.FileSize];
                    projectStream.Read(fileContent, 0, fileContent.Length);

                    File.WriteAllBytes(outputPath, fileContent);
                }
            }
        }

        /// <summary>
        /// 读取头部信息
        /// </summary>
        /// <param name="stream">输入流</param>
        /// <returns>文件条目列表</returns>
        private  List<FileEntry> ReadHeader(Stream stream)
        {
            List<FileEntry> entries = new List<FileEntry>();

            // 读取文件条目数量
            int entryCount = BitConverter.ToInt32(stream.ReadBytes(sizeof(int)), 0);

            for (int i = 0; i < entryCount; i++)
            {
                // 读取文件名长度
                int fileNameLength = BitConverter.ToInt32(stream.ReadBytes(sizeof(int)), 0);

                // 读取文件名
                byte[] fileNameBytes = stream.ReadBytes(fileNameLength);
                string fileName = System.Text.Encoding.UTF8.GetString(fileNameBytes);

                // 读取文件大小
                long fileSize = BitConverter.ToInt64(stream.ReadBytes(sizeof(long)), 0);

                // 添加到文件条目列表
                entries.Add(new FileEntry(fileName, fileSize));
            }

            return entries;
        }

        /// <summary>
        /// 扩展方法：从流中读取指定长度的字节数组
        /// </summary>
        // 删除原来的 ReadBytes 方法
    }
}
