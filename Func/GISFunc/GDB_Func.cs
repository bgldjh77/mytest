using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.GISFunc
{
    class GDB_Func
    {
        //创建GDB
        public static void creatGDB(string gdbpath)
        {
            if (gdbpath.EndsWith(".gdb"))
            {
                string gdbname = System.IO.Path.GetFileNameWithoutExtension(gdbpath);
                // 设置工作空间工厂，这里使用 FileGDBWorkspaceFactory
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactory();

                // 指定要创建的文件地理数据库的路径
                string gdbPath = System.IO.Path.GetDirectoryName(gdbpath);

                try
                {
                    // 创建文件地理数据库
                    IWorkspaceName workspaceName = workspaceFactory.Create(gdbPath, gdbname, null, 0);

                    // 打开新创建的文件地理数据库
                    IName name = (IName)workspaceName;
                    IWorkspace workspace = (IWorkspace)name.Open();

                    // 如果需要，在新创建的文件地理数据库中创建要素类等内容
                    // 这里可以添加代码来创建要素类、表等其他数据集

                    // 输出提示信息
                    Console.WriteLine("文件地理数据库创建成功。");
                }
                catch (Exception ex)
                {
                    // 处理异常
                    Console.WriteLine("创建文件地理数据库时出现异常：" + ex.Message);
                }
            }

        }
        //判断是否为要素数据集
        public bool IsFeatureDataset(string datapath)
        {
            if (datapath != null && datapath != "")
            {
                string gdbpath = System.IO.Path.GetDirectoryName(datapath);

                if (IsGdbDatabase(gdbpath))
                {
                    // 获取工作空间对象
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspace workspace = workspaceFactory.OpenFromFile(gdbpath, 0);
                    IEnumDataset enumDataset2 = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);//获取要素数据集
                    IDataset dataset;
                    while ((dataset = enumDataset2.Next()) != null)//获取要素数据集
                    {
                        string fullpath = System.IO.Path.Combine(gdbpath, dataset.Name);
                        if (fullpath == datapath)
                        {
                            //Console.WriteLine(fullpath + "是数据集");

                            System.Runtime.InteropServices.Marshal.ReleaseComObject(enumDataset2);

                            return true;

                        }
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(dataset);
                    }

                }
            }


            return false;
        }
        
        //判断是否为gdb
        public bool IsGdbDatabase(string folderPath)
        {
            if (Directory.Exists(folderPath) && folderPath.EndsWith(".gdb"))
            {
                string[] files = Directory.GetFiles(folderPath, "*" + ".gdbtable");
                if (files.Length == 0)
                {
                    return false;
                }
                return true;
            }

            return false;
        }
    }
}
