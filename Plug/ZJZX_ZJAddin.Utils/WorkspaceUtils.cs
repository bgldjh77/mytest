using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJZX_ZJAddin.Utils
{
    class WorkspaceUtils
    {
        static public IFeatureWorkspace OpenMDBFile(string mdbFilePath)
        {
            // 创建一个工作空间工厂  
            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactory();
            IWorkspace workspace = workspaceFactory.OpenFromFile(mdbFilePath, 0); // 打开 MDB 文件  

            // 将工作空间转换为 IFeatureWorkspace  
            IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
            return featureWorkspace;
        }
    }
}
