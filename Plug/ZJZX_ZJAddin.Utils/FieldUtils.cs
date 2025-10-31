using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZJZX_ZJAddin.Checker;

namespace ZJZX_ZJAddin.Utils
{
    class FieldUtils
    {

        /// <summary>
        /// 模板规范文字映射字典
        /// </summary>
        public static Dictionary<string, List<string>> fieldTypeMapping = new Dictionary<string, List<string>>  
        {  
            { "String", new List<string> { "text", "string", "文本型", "字符串", "文本","String","Text","TEXT","字符型","STRING"} },  
            { "Long", new List<string> { "long", "Long","LONG","长整型"} },  
            { "Int", new List<string> { "short", "smallinteger", "短整型", "整型","Int","INT"} },  
            { "Float", new List<string> { "float", "single", "浮点型", "单精度浮点型","Float","FLOAT"} },  
            { "Double", new List<string> { "double", "双精度浮点型", "双精度浮点型","Double","DOUBLE"} },  
            { "Date", new List<string> { "date", "日期","Date","DATE","日期型"} },  
            { "OID", new List<string> { "oid", "对象ID","OID"} },  
            { "Geometry", new List<string> { "geometry", "几何类型","Geometry","GEOMETRY"} },  
            { "Blob", new List<string> { "blob", "二进制类型","Blob","BLOB"} },  
            { "Raster", new List<string> { "raster", "栅格型","Raster","RASTER","GRID","IMG"} },  
            { "GUID", new List<string> { "guid", "全局唯一标识符","GUID" } },
            { "面",new List<string> { "面", "Polygon","POLYGON","polygon" }},
            { "线",new List<string> { "线", "Polyline","Line","POLYLINE","polyline"}},
            { "点",new List<string> { "点", "Polypoint" ,"Point","POINT"}},
            { "表",new List<string> { "表", "表格" ,"table","Table","TABLE"}}
        };

        /// <summary>
        /// 进行转换字典值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDicFieldType(string value)
        {
            if (value == "") { throw new Exception("字段类型不能为空。"); }
            string uppervalue = value.ToUpper();
            // 遍历字典  
            foreach (var kvp in fieldTypeMapping)
            {
                // 检查值是否在当前列表中  
                if (kvp.Value.Contains(uppervalue))
                {
                    return kvp.Key; // 返回字典左边的值  
                }
            }

            return ""; // 如果没有找到，返回 null  
        }

        /// <summary>
        /// 字段类型转换描述
        /// </summary>
        /// <param name="esriFieldType"></param>
        /// <returns></returns>
        public static string GetFieldType(string esriFieldType)
        {

            switch (esriFieldType)
            {
                case "esriFieldTypeSmallInteger":
                    return "Int";
                case "esriFieldTypeInteger":
                    return "Int";
                case "esriFieldTypeSingle":
                    return "Float";
                case "esriFieldTypeDouble":
                    return "Double"; ;
                case "esriFieldTypeString":
                    return "String";
                case "esriFieldTypeDate":
                    return "Date";
                case "esriFieldTypeOID":
                    return "OID";
                case "esriFieldTypeGeometry":
                    return "Geometry";
                case "esriFieldTypeBlob":
                    return "Blob";
                case "esriFieldTypeRaster":
                    return "Raster";
                case "esriFieldTypeGUID":
                    return "GUID";
                case "esriFieldTypeGlobalID":
                    return "GID";
                case "esriFieldTypeXML":
                    return "XML";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取要素中所有字段名称
        /// </summary>
        /// <param name="featurePath"></param>
        /// <returns></returns>
        public static List<string> GetFeatureFields(string featurePath)
        {
            List<string> fieldNames = new List<string>();

            // 定义不需要的字段名称数组  
            string[] excludedFields = new string[] {   
            "OBJECTID",
            "Objectid",
            "ObjectID",
            "SHAPE",
            "Shape",   
            "SHAPE_AREA",
            "SHAPE_Area",
            "Shape_Area",
            "SHAPE_LENGTH",
            "SHAPE_Length", 
            "Shape_Length",
            "FID",
            "OID"
            };
            // 获取地理数据库路径，直到 .gdb  
            int gdbIndex = featurePath.LastIndexOf(".gdb");
            if (gdbIndex <= 0)
            {
                throw new Exception("无有效的地理数据库路径。");
            }

            string gdbPath = featurePath.Substring(0, gdbIndex + 4); // 获取到 .gdb 的完整路径
            // 处理路径来提取要素集名称和要素类名称  
            string[] pathParts = featurePath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string featureClassName = pathParts[pathParts.Length - 1]; // 要素类名称

            try
            {
                IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
                IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
                //遍历工作空间下的featureclass
                IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
                IEnumDataset pEnumDatasets = workspace.get_Datasets(esriDatasetType.esriDTAny) as IEnumDataset;
                IDataset pDataset = pEnumDatasets.Next();

                while (pDataset != null)
                {
                    // 若为要素类
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        // 在这里获取字段
                        if (pDataset.Name == featureClassName)
                        {
                            // 获取字段  
                            IFeatureClass featureClass = (IFeatureClass)pDataset;
                            for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                            {
                                IField field = featureClass.Fields.get_Field(i);
                                string fieldName = field.Name;

                                if (Array.IndexOf(excludedFields, fieldName) < 0) // 检查字段是否在排除数组中  
                                {
                                    fieldNames.Add(fieldName); // 添加字段名到列表  
                                }

                            }
                        }
                    } // 若为要素数据集
                    else if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset pESubDataset = pDataset.Subsets;
                        IDataset pSubDataset = pESubDataset.Next();
                        while (pSubDataset != null)
                        {
                            // 在这里获取字段
                            if (pSubDataset.Name == featureClassName)
                            {
                                IFeatureClass featureClass = (IFeatureClass)pSubDataset;
                                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                                {
                                    IField field = featureClass.Fields.get_Field(i);
                                    string fieldName = field.Name;

                                    if (Array.IndexOf(excludedFields, fieldName) < 0) // 检查字段是否在排除数组中  
                                    {
                                        fieldNames.Add(fieldName); // 添加字段名到列表  
                                    }
                                }
                            }
                            pSubDataset = pESubDataset.Next();
                        }
                    }
                    pDataset = pEnumDatasets.Next();
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return fieldNames; // 返回字段名称的列表 
        }

        /// <summary>
        /// shp文件读取要素字段list
        /// </summary>
        /// <param name="shpFilePath"></param>
        /// <returns></returns>
        public static List<string> GetShapefileFields(string shpFilePath)
        {
            List<string> fieldNames = new List<string>(); // 存储字段名称  
            // 定义不需要的字段名称数组  
            string[] excludedFields = new string[] {   
            "OBJECTID",
            "Objectid",
            "ObjectID",
            "SHAPE",
            "Shape",   
            "SHAPE_AREA",
            "SHAPE_Area",
            "Shape_Area",
            "SHAPE_LENGTH",
            "SHAPE_Length", 
            "Shape_Length",
            "FID",
            "OID"
            };

            try
            {
                // 打开 shapefile  
                IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
                IWorkspace workspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(shpFilePath), 0);
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                // 获取 shapefile 名称（包含扩展名），例如 "example.shp"  
                string shapefileName = System.IO.Path.GetFileName(shpFilePath);
                IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(shapefileName);

                // 获取字段  
                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    IField field = featureClass.Fields.get_Field(i);
                    string fieldName = field.Name;

                    // 排除不需要的字段  
                    if (Array.IndexOf(excludedFields, fieldName) < 0) // 检查字段是否在排除数组中  
                    {
                        fieldNames.Add(fieldName); // 添加字段名到列表  
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return fieldNames; // 返回字段名称的列表  
        }


        /// <summary>
        /// 根据已有要素创建字段集
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static IFields CreateFieldsBasedOnFeature(IFeature feature)
        {
            // 创建字段编辑器  
            IFieldsEdit fieldsEdit = new FieldsClass() as IFieldsEdit;

            // 获取现有要素的字段集合  
            IFields existingFields = feature.Fields;

            // 遍历现有字段并复制到新的字段集合  
            for (int i = 0; i < existingFields.FieldCount; i++)
            {
                IField originalField = existingFields.get_Field(i);

                // 创建新字段以添加到字段集合  
                IFieldEdit newField = new FieldClass() as IFieldEdit;
                newField.Name_2 = originalField.Name;
                newField.Type_2 = originalField.Type;
                newField.AliasName_2 = originalField.AliasName;
                newField.IsNullable_2 = originalField.IsNullable;
                newField.DefaultValue_2 = originalField.DefaultValue;

                // 这里可以根据需要设置额外属性，例如长度、精度等  
                // 例如如果字段是字符串类型，可以设置字段长度：  
                if (originalField.Type == esriFieldType.esriFieldTypeString)
                {
                    newField.Length_2 = originalField.Length;
                }

                // 添加新字段到字段集合  
                fieldsEdit.AddField(newField);
            }

            return fieldsEdit as IFields;
        }

        /// <summary>
        /// 对比要素添加（复制）
        /// </summary>
        /// <param name="sourceFeature"></param>
        /// <param name="targetFeatureClass"></param>
        public void CopyFieldsFromFeature(IFeature sourceFeature, IFeatureClass targetFeatureClass)
        {
            // 检查参数是否有效  
            if (sourceFeature == null || targetFeatureClass == null)
            {
                throw new ArgumentNullException("Source feature or target feature class is null.");
            }

            // 获取源要素的字段集合  
            IFields sourceFields = sourceFeature.Fields;

            // 创建字段编辑器  
            IFieldsEdit targetFieldsEdit = targetFeatureClass.Fields as IFieldsEdit;

            // 遍历源要素字段  
            for (int i = 0; i < sourceFields.FieldCount; i++)
            {
                IField sourceField = sourceFields.get_Field(i);

                // 检查目标要素类是否已存在该字段  
                if (targetFieldsEdit.FindField(sourceField.Name) == -1)
                {
                    // 添加字段到目标要素类  
                    AddFieldToFeatureClass(sourceField, targetFeatureClass);

                }
            }
        }

        /// <summary>
        /// 对比要素添加（重新排序）
        /// </summary>
        /// <param name="sourceFeature"></param>
        /// <param name="targetFeatureClass"></param>
        public void CopyFieldsFromFeature(IFeature sourceFeature, IFeatureClass targetFeatureClass, List<string> inFieldList)
        {
            // 检查参数是否有效  
            if (sourceFeature == null || targetFeatureClass == null)
            {
                throw new ArgumentNullException("Source feature or target feature class is null.");
            }

            // 获取源要素的字段集合  
            IFields sourceFields = sourceFeature.Fields;

            // 创建字段编辑器  
            IFieldsEdit targetFieldsEdit = targetFeatureClass.Fields as IFieldsEdit;

            // 遍历源要素字段  
            for (int i = 0; i < inFieldList.Count; i++)
            {
                if (sourceFields.FindField(inFieldList[i]) != -1)
                {
                    int index = sourceFields.FindField(inFieldList[i]);
                    IField sourceField = sourceFields.get_Field(index);
                    // 检查目标要素类是否已存在该字段  
                    if (targetFieldsEdit.FindField(sourceField.Name) == -1)
                    {
                        // 添加字段到目标要素类  
                        AddFieldToFeatureClass(sourceField, targetFeatureClass);

                    }
                }
                else
                {
                    // 传入要素字段没有对应字段
                }

            }
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="sourceField"></param>
        /// <param name="targetFieldsEdit"></param>
        public void AddFieldToFeatureClass(IField sourceField, IFeatureClass targetFeatureClass)
        {
            // 创建新的字段  
            IFieldEdit newField = new FieldClass() as IFieldEdit;
            newField.Name_2 = sourceField.Name;
            newField.AliasName_2 = sourceField.AliasName;
            newField.Type_2 = sourceField.Type;
            newField.IsNullable_2 = sourceField.IsNullable;

            // 根据字段类型设置字段的特定属性  
            switch (sourceField.Type)
            {
                case esriFieldType.esriFieldTypeString:
                    newField.Length_2 = sourceField.Length; // 设置字符串字段的长度  
                    break;
                case esriFieldType.esriFieldTypeDouble:
                    newField.Precision_2 = sourceField.Precision; // 设置双精度字段的精度  
                    newField.Scale_2 = sourceField.Scale; // 设置双精度字段的比例  
                    break;
                // 可添加更多字段类型的处理  
            }

            // 添加新字段到目标字段集合  
            targetFeatureClass.AddField(newField);
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="sourceField"></param>
        /// <param name="targetFieldsEdit"></param>
        public string AddFieldToFeatureClass(IFeatureClass targetFeatureClass, string FieldName, string FieldAliasName, esriFieldType FieldType, bool IsNullable, int FieldLength, int Precision, int Scale)
        {
            int index = targetFeatureClass.FindField(FieldName);
            if (index == -1)
            {
                // 创建新的字段  
                IFieldEdit newField = new FieldClass() as IFieldEdit;
                newField.Name_2 = FieldName;
                newField.AliasName_2 = FieldAliasName;
                newField.Type_2 = FieldType;
                newField.IsNullable_2 = IsNullable;

                // 根据字段类型设置字段的特定属性  
                switch (FieldType)
                {
                    case esriFieldType.esriFieldTypeString:
                        newField.Length_2 = FieldLength; // 设置字符串字段的长度  
                        break;
                    case esriFieldType.esriFieldTypeDouble:
                        newField.Precision_2 = Precision; // 设置双精度字段的精度  
                        newField.Scale_2 = Scale; // 设置双精度字段的比例  
                        break;
                    // 可添加更多字段类型的处理  
                }

                // 添加新字段到目标字段集合  
                targetFeatureClass.AddField(newField);
                return "字段：" + FieldName + "成功添加至要素类：" + targetFeatureClass.AliasName;
            }
            return "字段添加失败";

        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="fieldName"></param>
        public void DeleteFieldFromFeatureClass(IFeatureClass featureClass, string fieldName)
        {
            // 检查输入参数是否有效  
            if (featureClass == null || string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("Feature class or field name is null or empty.");
            }

            // 创建字段集合的编辑版本  
            IFieldsEdit fieldsEdit = featureClass.Fields as IFieldsEdit;

            // 查找要删除的字段索引  
            int fieldIndex = fieldsEdit.FindField(fieldName);
            if (fieldIndex == -1)
            {
                // 未找到对应字段
            }

            // 确定要删除字段  
            IField fieldToRemove = fieldsEdit.get_Field(fieldIndex);

            // 确保字段是可删除的  
            if (fieldToRemove.Type == esriFieldType.esriFieldTypeGeometry ||
                fieldToRemove.Type == esriFieldType.esriFieldTypeOID)
            {
                // 必要字段不可删除
            }

            // 进行删除  
            featureClass.DeleteField(fieldToRemove);


        }

















    }
}
