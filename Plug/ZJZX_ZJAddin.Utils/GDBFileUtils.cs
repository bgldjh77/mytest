using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ZJZX_ZJAddin.Utils
{
    class GDBFileUtils
    {
        /// <summary>
        /// 检查是否为GDB文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsFileGDB(string path)
        {
            // 检查路径是否存在  
            if (!Directory.Exists(path))
            {
                return false;
            }

            // 检查路径是否为目录  
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                return false;
            }

            // 检查包含特定的文件类型  
            // 在这里我们检查 .gdbtable 和 .gdbindexes 文件是否存在  
            // 定义需要检查的文件后缀
            string[] expectedExtensions = { ".gdbtable", ".gdbindexes" }; // 示例后缀，请根据实际情况调整  

            foreach (string extension in expectedExtensions)
            {
                // 获取目录下所有文件  
                string[] files = Directory.GetFiles(path);

                // 检查是否存在具有指定后缀的文件  
                bool fileExists = false;
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(extension, StringComparison.OrdinalIgnoreCase))
                    {
                        fileExists = true;
                        break;
                    }
                }

                if (!fileExists)
                {
                    return false; // 如果任意一个后缀文件不存在，返回 false  
                }
            }

            return true; // 如果都存在，则是一个有效的 GDB  
        }

        /// <summary>
        /// 读取GDB数据库中所有要素信息
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public List<FeatureInfo> ReadGDBFeatures(string gdbPath)
        {
            List<FeatureInfo> featureInfos = new List<FeatureInfo>();
            // 创建工作空间工厂  
            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactory();
            IWorkspace workspace = workspaceFactory.OpenFromFile(gdbPath, 0);
            // 定义需要排除的字段名称数组  
            string[] excludedFields = new string[] { "OBJECTID", "SHAPE_AREA", "SHAPE_LENGTH", "SHAPE" };

            try
            {
                //遍历工作空间下的featureclass
                IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
                IEnumDataset pEnumDatasets = workspace.get_Datasets(esriDatasetType.esriDTAny) as IEnumDataset;
                IDataset pDataset = pEnumDatasets.Next();

                while (pDataset != null)
                {
                    // 若为要素类
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        IFeatureClass featureClass = pDataset as IFeatureClass;
                        string featureName = pDataset.Name; // 要素名称
                        string featureAliasName = featureClass.AliasName; // 要素别名
                        string featureDatasetName = ""; // 没有要素集的情况  
                        string featureType = "";
                        switch (featureClass.ShapeType)
                        {
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                                featureType = "面";
                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                                featureType = "线";
                                break;
                            case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                                featureType = "点";
                                break;
                        }
                        // 获取字段信息  
                        for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                        {
                            IField field = featureClass.Fields.get_Field(i);
                            // 判断当前字段是否在排除字段数组中  
                            if (excludedFields.Contains(field.Name))
                            {
                                continue; // 跳过这些字段  
                            }
                            featureInfos.Add(new FeatureInfo
                            {
                                Name = featureName,
                                AliasName = featureAliasName,
                                FeatureDatasetName = featureDatasetName,
                                FeatureDatasetAliasName = "",
                                FeatureType = featureType,
                                FieldName = field.Name,
                                FieldAliasName = field.AliasName,
                                FieldType = FieldUtils.GetFieldType(field.Type.ToString()),
                                FieldLength = field.Length,
                                NumberFormat = field.Precision,
                                isNull = field.IsNullable ? "否" : "是"
                            });
                        }


                    } // 若为要素数据集
                    else if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset pESubDataset = pDataset.Subsets;
                        IDataset pSubDataset = pESubDataset.Next();
                        while (pSubDataset != null)
                        {
                            IFeatureClass featureClass = pSubDataset as IFeatureClass;
                            string featureName = pSubDataset.Name; // 要素名称
                            string featureAliasName = featureClass.AliasName; // 要素别名
                            string featureDatasetName = pDataset.Name; // 有要素集的情况
                            string featureDatasetAliasName = pDataset.BrowseName; // 要素集别名
                            string featureType = "";
                            switch (featureClass.ShapeType)
                            {
                                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                                    featureType = "面";
                                    break;
                                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                                    featureType = "线";
                                    break;
                                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                                    featureType = "点";
                                    break;
                            }
                            // 获取字段信息  
                            for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                            {
                                IField field = featureClass.Fields.get_Field(i);
                                // 判断当前字段是否在排除字段数组中  
                                if (excludedFields.Contains(field.Name.ToUpper()))
                                {
                                    continue; // 跳过这些字段  
                                }
                                featureInfos.Add(new FeatureInfo
                                {
                                    Name = featureName,
                                    AliasName = featureAliasName,
                                    FeatureDatasetName = featureDatasetName,
                                    FeatureDatasetAliasName = featureDatasetAliasName,
                                    FeatureType = featureType,
                                    FieldName = field.Name,
                                    FieldAliasName = field.AliasName,
                                    FieldType = FieldUtils.GetFieldType(field.Type.ToString()),
                                    FieldLength = field.Length,
                                    NumberFormat = field.Precision,
                                    isNull = field.IsNullable ? "否" : "是"
                                });
                            }
                            pSubDataset = pESubDataset.Next();
                        }
                    }
                    else if (pDataset.Type == esriDatasetType.esriDTTable) // 具体名称请以版本为准
                    {
                        ITable pTable = pDataset as ITable;
                        string tableName = pDataset.Name;
                        string tableAliasName = tableName; // 表通常没有别名，按需设定
                        string featureDatasetName = ""; // 表通常不属于要素数据集
                        string featureDatasetAliasName = "";
                        string featureType = "表"; // 标识为“表格”

                        // 获取字段信息
                        for (int i = 0; i < pTable.Fields.FieldCount; i++)
                        {
                            IField field = pTable.Fields.get_Field(i);
                            if (excludedFields.Contains(field.Name))
                            {
                                continue;
                            }

                            featureInfos.Add(new FeatureInfo
                            {
                                Name = tableName,
                                AliasName = tableAliasName,
                                FeatureDatasetName = featureDatasetName,
                                FeatureDatasetAliasName = featureDatasetAliasName,
                                FeatureType = featureType,
                                FieldName = field.Name,
                                FieldAliasName = field.AliasName,
                                FieldType = FieldUtils.GetFieldType(field.Type.ToString()),
                                FieldLength = field.Length,
                                NumberFormat = field.Precision,
                                isNull = field.IsNullable ? "否" : "是"
                            });
                        }
                    }
                    pDataset = pEnumDatasets.Next();
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return featureInfos;
        }


        /// <summary>
        /// 创建GDB文件
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <param name="isDelExist"></param>
        public static IWorkspace CreateFileGDB(string gdbPath, bool isDelExist = true)
        {
            IWorkspace workspace = null;
            try
            {
                // 在创建之前检查 GDB 是否存在  
                if (Directory.Exists(gdbPath) && isDelExist)
                {
                    // 删除已有的 GDB  
                    Directory.Delete(gdbPath, true);
                }
                else if (Directory.Exists(gdbPath))
                {
                    workspace.WorkspaceFactory.OpenFromFile(gdbPath, 0);
                    return workspace;
                }

                Type factoryType = Type.GetTypeFromProgID(
                     "esriDataSourcesGDB.FileGDBWorkspaceFactory");

                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance
                    (factoryType);
                string path = System.IO.Path.GetDirectoryName(gdbPath);
                string fileName = System.IO.Path.GetFileName(gdbPath);

                IWorkspaceName workspaceName = workspaceFactory.Create(path, fileName, null, 0);
                IName name = (IName)workspaceName;
                workspace = (IWorkspace)name.Open();
                // 创建 GDB  

                return workspace;

            }
            catch (Exception ex)
            {
                MessageBox.Show("创建 GDB 出错: " + ex.Message);
                return workspace;
            }

        }




    }
}
