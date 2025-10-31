using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
//using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using System.Text.RegularExpressions;
using System.IO;
//using ZJZX_ZJAddin.Checker;
using 质检工具.MenuConfig;
using 质检工具;
using System.Threading.Tasks;

namespace ZJZX_ZJAddin.Checker
{
    class FieldChecker
    {

        /// <summary>
        ///  检查空图层
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public ArrayList DetectNull(String gdbPath)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("检测数据库路径为：" + gdbPath);
            resultList.Add("空图层名称：");
            //IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
            //IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
            IWorkspace workspace = Getworkspace(gdbPath);

            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);
            bool countfc = false;
            // 统计要素个数
            foreach (var fc in listFeaClass)
            {
                IFeatureClass featureClass = pFeatureWorkspace.OpenFeatureClass(fc);
                IQueryFilter pQueryFilter = new QueryFilter();
                int count = featureClass.FeatureCount(pQueryFilter);

                // 要素个数为0则存入resultList
                if (count == 0)
                {
                    resultList.Add(fc);
                    countfc = true;
                }

            }
            if (!countfc)
            {
                resultList.Add("未检测到空图层");
            }

            return resultList;
        }


        /// <summary>
        ///  中文字符判断
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Boolean IsChineseChar(Char c)
        {
            if ((int)c > 127)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        ///  检查别名是否为中文
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public ArrayList DetectAlias(String gdbPath)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("图层/字段别名情况,检查数据库路径为：" + gdbPath);

            IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
            IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);

            foreach (var fc in listFeaClass)
            {
                resultList.Add(fc);
                bool flag = true;
                IFeatureClass featureClass = pFeatureWorkspace.OpenFeatureClass(fc);

                // 判断图层别名
                if (!IsChineseChar(featureClass.AliasName[0]))
                {
                    flag = false;
                    resultList.Add("未修改图层别名");
                }

                // 判断字段别名
                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    List<String> defaultFields = new List<String> { "OBJECTID", "SHAPE", "SHAPE_AREA", "SHAPE_LENGTH", "FID" };
                    IField field = featureClass.Fields.get_Field(i);

                    if (!defaultFields.Contains(field.Name.ToUpper()))
                    {
                        if (!IsChineseChar(field.AliasName[0]))
                        {
                            flag = false;
                            resultList.Add("未修改别名字段【" + field.Name + "】");
                        }
                    }
                }

                // 若未检测到别名异常情况，删除要素类名称
                if (flag)
                {
                    resultList.RemoveAt(resultList.Count - 1);
                }
                else
                {
                    resultList.Add("************************************************************");
                }
            }

            resultList.Insert(1, "检测完成，需修改别名的图层/字段共【" + (resultList.Count - 1).ToString() + "】项。");
            resultList.Insert(2, "************************************************************");
            return resultList;
        }


        /// <summary>
        ///  检查坐标系
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <param name="pSpatialReferenceName"></param>
        /// <returns></returns>
        public ArrayList DetectCoordinateSystem(String gdbPath, String pSpatialReferenceName)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("要素类坐标系情况,检查数据库路径为：" + gdbPath);

            IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
            IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);

            foreach (var fc in listFeaClass)
            {
                IFeatureClass featureClass = pFeatureWorkspace.OpenFeatureClass(fc);

                IGeoDataset pGeoDataset = featureClass as IGeoDataset;
                ISpatialReference pSpatialReference = pGeoDataset.SpatialReference;
                if (pSpatialReference.Name != pSpatialReferenceName)
                {
                    resultList.Add(fc);
                    resultList.Add(pSpatialReference.Name + "\t" + pSpatialReference.Abbreviation + "\t" + pSpatialReference.Alias);
                    resultList.Add("************************************************************");
                }
            }

            resultList.Insert(1, "检测完成，坐标系统不规范的要素类【" + (resultList.Count - 1).ToString() + "】个。");
            resultList.Insert(2, "************************************************************");
            return resultList;
        }


        /// <summary>
        ///  检查字段冗余情况
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public ArrayList FindRedundantFields(String gdbPath)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("要素类字段冗余情况,检查数据库路径为：" + gdbPath);

            //IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
            //IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
            IWorkspace workspace = Getworkspace(gdbPath);
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);

            foreach (var fc in listFeaClass)
            {
                resultList.Add(fc);
                IFeatureClass featureClass = pFeatureWorkspace.OpenFeatureClass(fc);
                // 检查字段名称中任意字符不包括下划线及数字，例"test_123"，这个表达式会匹配到 "test"，而不包含_123，来检查是否生成了重复字段
                string pattern = @"[.\\s\\S]*?(?=(_\d+))";
                bool flag = true;

                // 遍历字段
                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    IField field = featureClass.Fields.get_Field(i);
                    bool result = System.Text.RegularExpressions.Regex.IsMatch(field.Name, pattern);
                    if (result)
                    {
                        flag = false;
                        resultList.Add("疑似冗余字段【" + field.Name + "】");
                    }
                }
                // 不输出无冗余字段要素类 
                if (flag)
                {
                    resultList.RemoveAt(resultList.Count - 1);
                }
                else
                {
                    resultList.Add("************************************************************");
                }
            }

            resultList.Insert(1, "检测完成，冗余字段共有【" + (resultList.Count - 1).ToString() + "】项。");
            resultList.Insert(2, "************************************************************");
            return resultList;
        }

        /// <summary>
        ///  递归复制目录,适用于GDB文件数据库
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destDir"></param> 
        static void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string fileName = System.IO.Path.GetFileName(file);
                string destFile = System.IO.Path.Combine(destDir, fileName);
                File.Copy(file, destFile, true);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string dirName = System.IO.Path.GetFileName(dir);
                string destSubDir = System.IO.Path.Combine(destDir, dirName);
                CopyDirectory(dir, destSubDir);
            }
        }

        /// <summary>
        ///  添加字段方法  
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="fieldLength"></param>
        static bool AddField(IFeatureClass featureClass, string fieldName, esriFieldType fieldType, int fieldLength = 100, string fieldAliasName = "", bool isNULL = true)
        {
            if (featureClass.Fields.FindField(fieldName) == -1) // 检查字段是否已存在  
            {
                IFieldEdit newField = new FieldClass() as IFieldEdit;
                // 设置新字段的属性  
                newField.Name_2 = fieldName;                    // 字段名称  
                newField.Type_2 = fieldType;                    // 字段类型  
                newField.IsNullable_2 = isNULL;                   // 是否允许为 NULL  
                newField.AliasName_2 = fieldAliasName;          // 字段别名  
                newField.Length_2 = fieldLength;                       // 对于字符串类型，设置字段长度（可根据需要调整）  
                featureClass.AddField(newField);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  检查非法字段
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public ArrayList FindIllegalCharFieldValue(String gdbPath, String outputresultDirPath, string illegalPattern = @"[~!@#￥%^&* /\n\s]")
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("要素类非法字段值情况,检查数据库路径为【" + gdbPath+" 】检查的非法字符为："+illegalPattern+" ");
            string gdbName = System.IO.Path.GetFileName(gdbPath);
            // 拼接输出完整的地理数据库路径  
            String outputGdbPath = System.IO.Path.Combine(outputresultDirPath, "Check_" + gdbName);

            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactory();

            // 创建输出工作空间
            // 检查GDB是否存在 
            try
            {
                if (Directory.Exists(outputGdbPath))
                {
                    // 如果存在，删除 GDB  
                    Directory.Delete(outputGdbPath, true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("输出路径下【" + outputGdbPath + "】已存在，请删除后重新运行该工具");
                resultList.Add("输出路径下【" + outputGdbPath + "】已存在，请删除后重新运行该工具");
                return resultList;
            }

            // 复制GDB
            CopyDirectory(gdbPath, outputGdbPath);
            //workspaceFactory.Create(outputresultDirPath, "Check_" + gdbName, null, 0);
            IWorkspace outputWorkspace = workspaceFactory.OpenFromFile(outputGdbPath, 0);
            IFeatureWorkspace outputFeatureWorkspace = outputWorkspace as IFeatureWorkspace;
            List<string> featureClassList = FeatureChecker.GetFeatureClassByWorkspace(outputWorkspace);


            foreach (var featureClassName in featureClassList)
            {
                resultList.Add(featureClassName);
                IFeatureClass featureClass = outputFeatureWorkspace.OpenFeatureClass(featureClassName);

                bool hasIllegalFields_all = false;

                // 创建问题描述字段
                AddField(featureClass, "wtms", esriFieldType.esriFieldTypeString, 1000, "问题描述");

                IFeatureCursor featureCursor = featureClass.Update(null, false);
                IFeature feature;
                // 遍历字段，检查每个要素的字段值  
                while ((feature = featureCursor.NextFeature()) != null)
                {
                    bool hasIllegalFields = false;
                    string issueDescription = "";

                    // 检查每个字段的值  
                    for (int j = 0; j < featureClass.Fields.FieldCount; j++)
                    {
                        IField field = featureClass.Fields.get_Field(j);
                        object fieldValue = feature.get_Value(j);

                        // 检查字段值是否包含非法字符  
                        if (field.Type == esriFieldType.esriFieldTypeString && fieldValue != null && Regex.IsMatch(fieldValue.ToString(), illegalPattern))
                        {
                            hasIllegalFields = true;
                            hasIllegalFields_all = true;
                            issueDescription += field.Name + "存在非法字段，";
                        }
                    }

                    // 如果有非法字段，将该要素的wtms字段更新并记录信息  
                    if (hasIllegalFields)
                    {
                        // 在问题描述中去掉最后的逗号  
                        issueDescription = issueDescription.TrimEnd('，');
                        // 更新wtms字段  
                        feature.set_Value(feature.Fields.FindField("wtms"), issueDescription); // 根据实际需求设置wtms字段的值  
                        featureCursor.UpdateFeature(feature); // 更新特征  
                    }
                }

                if (hasIllegalFields_all)
                {
                    resultList.RemoveAt(resultList.Count - 1); // 移除要素类名称  
                    resultList.Add("疑似存在非法字段【" + featureClassName + "】");
                }
                else
                {
                    resultList.RemoveAt(resultList.Count - 1); // 移除要素类名称  
                }
            }

            resultList.Insert(1, "检测完成，存在非法字段的共有【" + (resultList.Count - 1).ToString() + "】项。");
            resultList.Insert(2, "************************************************************");
            return resultList;

        }

        public static void FindIllegalCharFieldValue(List<ToolArgs> TaskArgs)
        {
            string toolname = TaskArgs[0].toolName;
            List<string> arglist = new List<string>();
            foreach (ToolArgs toolArgs in TaskArgs)
            {
                arglist.Add(toolArgs.nowValue);
            }
            string gdbPath = arglist[0];
            string illegalPattern = arglist[1];
            string outputresultDirPath = arglist[2];
            FieldChecker fc = new FieldChecker();
            ArrayList arrayList = new ArrayList();

            arrayList = fc.FindIllegalCharFieldValue(gdbPath, outputresultDirPath, illegalPattern);

            string combinestr = "";
            foreach(string res in arrayList)
            {
                combinestr += res + "\n";
            }
            combinestr += "已成功执行：" + toolname + "\n";
            LogHelper.Successnotice(combinestr);
        }
        public static void FindTinyGeometry( List<ToolArgs> TaskArgs)
        {
            string toolname = TaskArgs[0].toolName;
            List<string> arglist = new List<string>();
            foreach (ToolArgs toolArgs in TaskArgs)
            {
                arglist.Add(toolArgs.nowValue);
            }
            string gdbPath = arglist[0];
            string shapelength = arglist[1];
            string shapearea = arglist[2];
            FieldChecker fc = new FieldChecker();
            ArrayList arrayList = new ArrayList();

            arrayList = fc.CountTinyGeometry(gdbPath, Convert.ToDouble(shapelength), Convert.ToDouble(shapearea));
            string combinestr = "";
            foreach (string res in arrayList)
            {
                combinestr += res + "\n";
            }
            combinestr += "已成功执行：" + toolname + "\n";
            LogHelper.Successnotice(combinestr);

        }
        /// <summary>
        ///  获取目标空间参考（此处提供示例，您可以自行选择适当的空间参考） 
        /// </summary>
        /// <param name="gcsType"></param>
        /// <returns></returns> 
        private ISpatialReference GetTargetSpatialReference(int gcsType)
        {
            // 创建空间参考工厂  
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironment();
            // 返回一个适合的投影坐标系统，例如 UTM  
            return spatialReferenceFactory.CreateGeographicCoordinateSystem(gcsType);
        }

        /// <summary>
        /// 计算线要素投影长度
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private double CalculateProjectedLength(IFeature feature, int gcsType)
        {
            // 确保 feature 不是 null  
            if (feature == null)
                throw new ArgumentNullException();

            // 获取几何形状  
            IGeometry geometry = feature.Shape;

            // 检查几何类型是否为线
            if (geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                IPolyline polyline = geometry as IPolyline;
                if (polyline != null)
                {
                    // 获取目标空间参考  
                    ISpatialReference targetSpatialReference = GetTargetSpatialReference(gcsType);
                    if (polyline.SpatialReference != targetSpatialReference)
                    {
                        // 投影线几何到目标空间参考  
                        polyline.Project(targetSpatialReference);
                    }
                    // 计算长度  
                    double length = polyline.Length; // 单位取决于投影坐标系统  
                    return length;
                }
                else
                {
                    return 0;
                }
            }

            if (geometry.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IPolygon polygon = geometry as IPolygon;
                if (polygon != null)
                {
                    // 获取目标空间参考  
                    ISpatialReference targetSpatialReference = GetTargetSpatialReference(gcsType);
                    if (polygon.SpatialReference != targetSpatialReference)
                    {
                        // 投影多边形到目标空间参考  
                        polygon.Project(targetSpatialReference);
                    }

                    // 通过 IArea 接口获取面积  
                    IArea area = (IArea)polygon;
                    return area.Area; // 单位取决于目标投影坐标系统  
                }
                else
                {
                    return 0;
                }
            }
            return 0;


        }

        /// <summary>
        ///  检查细斑细线个数
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public ArrayList CountTinyGeometry(String gdbPath,double shapelength, double shapearea)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("要素类细斑细线数量,检查数据库路径为：" + gdbPath);
            IWorkspace workspace = Getworkspace(gdbPath);
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);
            foreach (var fc in listFeaClass)
            {
                IFeatureClass featureClass = pFeatureWorkspace.OpenFeatureClass(fc);

                // 判断要素类类型
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    // 创建游标以查找所有的多段线要素  
                    IFeatureCursor featureCursor = featureClass.Search(null, false);
                    IFeature feature;
                    int count = 0;
                    // 迭代所有要素  
                    while ((feature = featureCursor.NextFeature()) != null)
                    {
                        try
                        {
                            // 计算每个要素的投影长度，4514为北京54编号
                            double projectedLength = CalculateProjectedLength(feature, 4490);
                            // 检查投影长度是否小于 2  
                            if (projectedLength < shapelength)
                            {
                                count++;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                            // 加入到错误日志中
                        }
                    }

                    if (count > 0)
                    {
                        resultList.Add(fc);
                        resultList.Add("细线数量：" + count.ToString());
                        resultList.Add("************************************************************");
                    }
                }
                // 判断要素类类型
                else if (featureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                {
                    // 创建游标以查找所有的面要素  
                    IFeatureCursor featureCursor = featureClass.Search(null, false);
                    IFeature feature;
                    int count = 0;
                    // 迭代所有要素  
                    while ((feature = featureCursor.NextFeature()) != null)
                    {
                        try
                        {
                            // 计算每个要素的投影面积 
                            double projecteArea = CalculateProjectedLength(feature, 4490);
                            // 检查投影长度是否小于 2  
                            if (projecteArea < shapearea)
                            {
                                count++;
                            }
                        }
                        catch (Exception ex)
                        {
                            // 加入到错误日志中
                        }
                    }
                    if (count > 0)
                    {
                        resultList.Add(fc);
                        resultList.Add("细斑数量：" + count.ToString());
                        resultList.Add("************************************************************");
                    }
                }
            }
            resultList.Insert(1, "检测完成，疑似需修复几何的要素类共【" + (resultList.Count - 1).ToString() + "】个。");
            resultList.Insert(2, "************************************************************");
            return resultList;
        }


        /// <summary>
        ///  查询必填字段是否填写
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public ArrayList DetectRequired(String gdbPath)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("必填字段填写情况,检查数据库路径为：" + gdbPath);
            //IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
            //IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);

            IWorkspace workspace = Getworkspace(gdbPath);
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);

            foreach (var fc in listFeaClass)
            {
                resultList.Add(fc);
                IFeatureClass featureClass = pFeatureWorkspace.OpenFeatureClass(fc);
                bool flag = true;

                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    IField field = featureClass.Fields.get_Field(i);
                    List<String> defaultFields = new List<String> { "OBJECTID", "SHAPE", "SHAPE_AREA", "SHAPE_LENGTH", "FID" };

                    if (!field.IsNullable && !defaultFields.Contains(field.Name.ToUpper()))
                    {
                        IQueryFilter queryFilter = new QueryFilter();
                        queryFilter.WhereClause = field.Name + " IS NULL";

                        if (field.Type == esriFieldType.esriFieldTypeString)
                        {
                            queryFilter.WhereClause = field.Name + " IS NULL OR " + field.Name + "=''";
                        }

                        int count = featureClass.FeatureCount(queryFilter);
                        if (count > 0)
                        {
                            flag = false;
                            resultList.Add("必填字段【" + field.Name + "】存在空值");
                        }
                    }
                }

                // 不输出不存在必填字段未填情况的要素类
                if (flag)
                {
                    resultList.RemoveAt(resultList.Count - 1);
                }
                else
                {
                    resultList.Add("************************************************************");
                }
            }

            resultList.Insert(1, "检测完成，必填字段为空的字段共【" + (resultList.Count - 1).ToString() + "】项。");
            resultList.Insert(2, "************************************************************");
            return resultList;
        }


        /// <summary>
        ///  检查几何修复
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        //public ArrayList DetectGeometry(String gdbPath)
        //{
        //    ArrayList resultList = new ArrayList();
        //    resultList.Add("几何错误情况,检查数据库路径为：" + gdbPath);

        //    IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
        //    IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
            
        //    IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
        //    List<string> listFeaClass = FeatureChecker.GetFeatureClassByWorkspace(workspace);

        //    Geoprocessor geoProcessor = new Geoprocessor();
        //    geoProcessor.OverwriteOutput = true;
        //    CheckGeometry checkGeometryTool = new CheckGeometry();

        //    foreach (var fc in listFeaClass)
        //    {
        //        checkGeometryTool.in_features = gdbPath + @"\" + fc;
        //        checkGeometryTool.out_table = gdbPath + @"\checkgeometry_table";
        //        geoProcessor.Execute(checkGeometryTool, null);

        //        ITable table = pFeatureWorkspace.OpenTable(@"checkgeometry_table");
        //        IQueryFilter pQueryFilter = new QueryFilter();
        //        int count = table.RowCount(pQueryFilter);

        //        if (count > 0)
        //        {
        //            resultList.Add("图层【" + fc + "】需修复几何");
        //        }
        //    }

        //    ITable pTable = pFeatureWorkspace.OpenTable(@"checkgeometry_table");
        //    IDataset dataset = pTable as IDataset;
        //    dataset.Delete();

        //    resultList.Insert(1, "检测完成，存在几何错误问题的要素类共【" + (resultList.Count - 1).ToString() + "】条。");
        //    resultList.Insert(2, "************************************************************");
        //    return resultList;
        //}


        /// <summary>
        /// 根据路径选择文件
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        public IWorkspace Getworkspace(string inputPath)
        {
            IWorkspace workspace = null;
            try
            {
                // 根据扩展名来判断是 MDB 还是 GDB  
                string extension = System.IO.Path.GetExtension(inputPath).ToLower();
                IWorkspaceFactory workspaceFactory = null;

                if (extension == ".mdb")
                {
                    workspaceFactory = new AccessWorkspaceFactory();
                    workspace = workspaceFactory.OpenFromFile(inputPath, 0);
                    return workspace;
                }
                else if (extension == ".gdb")
                {
                    workspaceFactory = new FileGDBWorkspaceFactory();
                    workspace = workspaceFactory.OpenFromFile(inputPath, 0);
                    return workspace;
                }
                else
                {
                    return workspace;
                }
            }
            catch (Exception e)
            {
                throw;
            }


        }
    }

        
    
}
