using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZJZX_ZJAddin.Utils
{
    class FeatureUtils
    {

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="tWorkspace"></param>
        /// <param name="datasetName"></param>
        /// <param name="fcShape"></param>
        /// <param name="fieldsList"></param>
        /// <returns></returns>
        public IFields CreateFields(IWorkspace tWorkspace, string datasetName, string fcShape, ISpatialReference inpSpatialReference = null)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OBJECTID";
            oidFieldEdit.AliasName_2 = "OBJECTID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;

            IGeometryDefEdit pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = pGeoDef as IGeometryDefEdit;

            IFeatureWorkspace fcWorkspace = tWorkspace as IFeatureWorkspace;
            ISpatialReference pSpatialReference = null;
            if (!string.IsNullOrEmpty(datasetName))
            {
                IFeatureDataset tFeatureDataset = fcWorkspace.OpenFeatureDataset(datasetName);
                pSpatialReference = GetSpatialReference(tFeatureDataset);
            }
            else if (inpSpatialReference != null)
            {
                pSpatialReference = inpSpatialReference;
            }
            else
            {
                ISpatialReferenceFactory3 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                // CGCS2000
                IProjectedCoordinateSystem geographicCoordinateSystem = spatialReferenceFactory.CreateProjectedCoordinateSystem(4526);

                ISpatialReference3 spatialReference = geographicCoordinateSystem as ISpatialReference3;
                pSpatialReference = spatialReference;
            }

            pGeoDefEdit.SpatialReference_2 = pSpatialReference;

            switch (fcShape)
            {
                case "esriGeometryPoint":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                    break;
                case "esriGeometryPolyline":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    break;
                case "esriGeometryPolygon":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    break;
                // 适配不同场景
                case "Point":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                    break;
                case "Polyline":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    break;
                case "Polygon":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    break;
                case "点":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                    break;
                case "线":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    break;
                case "面":
                    pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    break;
            }

            pFieldEdit.GeometryDef_2 = pGeoDef;
            fieldsEdit.AddField(pField);
            fields = (IFields)fieldsEdit;

            return fields;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tWorkspace"></param>
        /// <param name="fieldsList"></param>
        /// <returns></returns>
        public IFields CreateTableFields(IWorkspace tWorkspace, ArrayList fieldsList)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OBJECTID";
            oidFieldEdit.AliasName_2 = "OBJECTID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);

            foreach (ArrayList item in fieldsList)
            {
                IField field = new FieldClass();
                IFieldEdit fieldEdit = (IFieldEdit)field;
                fieldEdit.Name_2 = item[0].ToString();
                fieldEdit.AliasName_2 = item[1].ToString();
                fieldEdit.IsNullable_2 = true;

                string fieldType = item[2].ToString();
                switch (fieldType.ToUpper())
                {
                    case "TEXT":
                    case "CHAR":
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                        break;
                    case "FLOAT":
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeSingle;
                        break;
                    case "DOUBLE":
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                        break;
                    case "SHORT":
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                        break;
                    case "LONG":
                    case "INT":
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                        break;
                    case "DATE":
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
                        break;
                }


                if (fieldEdit.Type == esriFieldType.esriFieldTypeString)
                {
                    fieldEdit.Length_2 = int.Parse(item[3].ToString());
                }

                fieldsEdit.AddField(field);
            }

            fields = (IFields)fieldsEdit;
            return fields;
        }

        /// <summary>
        /// 创建数据集
        /// </summary>
        /// <param name="tWorkspace"></param>
        /// <param name="datasetName"></param>
        public void CreateDataset(IWorkspace tWorkspace, string datasetName)
        {
            // 若工作空间不存在数据集时，创建之
            IWorkspace2 tWorkspace2 = tWorkspace as IWorkspace2;
            IFeatureWorkspace fcWorkspace = tWorkspace as IFeatureWorkspace;

            if (!tWorkspace2.get_NameExists(esriDatasetType.esriDTFeatureDataset, datasetName))
            {
                ISpatialReferenceFactory3 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                // CGCS2000
                IProjectedCoordinateSystem geographicCoordinateSystem = spatialReferenceFactory.CreateProjectedCoordinateSystem(4526);

                ISpatialReference3 spatialReference = geographicCoordinateSystem as ISpatialReference3;
                // 黄海1985高程
                IVerticalCoordinateSystem verticalCoordinateSystem = spatialReferenceFactory.CreateVerticalCoordinateSystem((int)esriSRVerticalCSType.esriSRVertCS_YellowSea1985);
                spatialReference.VerticalCoordinateSystem = verticalCoordinateSystem;

                fcWorkspace.CreateFeatureDataset(datasetName, spatialReference);
            }
        }

        /// <summary>
        /// 获取空间坐标系
        /// </summary>
        /// <param name="pDataset"></param>
        /// <returns></returns>
        public static ISpatialReference GetSpatialReference(IFeatureDataset pDataset)
        {
            IGeoDataset pGeoDataset = pDataset as IGeoDataset;
            ISpatialReference pSpatialReference = pGeoDataset.SpatialReference;
            return pSpatialReference;
        }

        /// <summary>  
        /// 获取要素的空间坐标系  
        /// </summary>  
        /// <param name="feature">要素对象</param>  
        /// <returns>ISpatialReference，表示要素的空间坐标系</returns>  
        public static ISpatialReference GetFeatureSpatialReference(IFeature feature)
        {
            // 获取要素的几何对象  
            IGeometry geometry = feature.Shape;

            // 获取几何对象的空间参考  
            ISpatialReference spatialReference = geometry.SpatialReference;

            return spatialReference;
        }


        /// <summary>
        /// 创建要素
        /// </summary>
        /// <param name="tWorkspace"></param>
        /// <param name="datasetName"></param>
        /// <param name="fcName"></param>
        /// <param name="fcAlias"></param>
        /// <param name="fields"></param>
        public IFeatureClass CreateFeatureClass(IWorkspace tWorkspace, string datasetName, string fcName, string fcAlias, string fcType, ISpatialReference inpSpatialReference = null)
        {
            IFeatureWorkspace fcWorkspace = tWorkspace as IFeatureWorkspace;
            IFeatureDataset FeatureDataset = null;
            if (!string.IsNullOrEmpty(datasetName))
            {
                FeatureDataset = fcWorkspace.OpenFeatureDataset(datasetName);
            }
            IFields fields = null;
            if (inpSpatialReference != null)
            {
                fields = CreateFields(tWorkspace, datasetName, fcType, inpSpatialReference);
            }
            else
            {
                fields = CreateFields(tWorkspace, datasetName, fcType);
            }

            // 若已存在对应要素则直接返回
            if ((tWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, fcName))
            {
                return fcWorkspace.OpenFeatureClass(fcName); ;
            }
            if (FeatureDataset != null)
            {
                FeatureDataset.CreateFeatureClass(fcName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            }
            else
            {
                fcWorkspace.CreateFeatureClass(fcName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            }
            // 修改别名
            IFeatureClass featureClass = fcWorkspace.OpenFeatureClass(fcName);
            IClassSchemaEdit tClassSchema = featureClass as IClassSchemaEdit;
            tClassSchema.AlterAliasName(fcAlias);
            return featureClass;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tWorkspace"></param>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <param name="fields"></param>
        public void CreateTable(IWorkspace tWorkspace, string tableName, string tableAlias, IFields fields)
        {
            if ((tWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, tableName))
            {
                return;
            }
            IFeatureWorkspace fcWorkspace = tWorkspace as IFeatureWorkspace;

            IObjectClassDescription ocDescription = new ObjectClassDescriptionClass();
            //IFields fields = ocDescription.RequiredFields;
            //IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

            fcWorkspace.CreateTable(tableName, fields, ocDescription.InstanceCLSID, null, "");

            // 修改别名
            IObjectClass table = fcWorkspace.OpenTable(tableName) as IObjectClass;
            IClassSchemaEdit tClassSchema = table as IClassSchemaEdit;
            tClassSchema.AlterAliasName(tableAlias);
        }


        /// <summary>
        /// 复制要素值
        /// </summary>
        /// <param name="sourceFeatureClass">源要素，即要复制的要素</param>
        /// <param name="targetFeatureClass">目标要素</param>
        public static void InsertFeaturesUsingCursor(IFeatureClass sourceFeatureClass, IFeatureClass targetFeatureClass)
        {
            // 定义不需要的字段名称数组  
            string[] excludedFields = new string[] {   
            "OBJECTID",
            "Objectid",
            "ObjectID",
            "SHAPE_AREA",
            "SHAPE_Area",
            "Shape_Area",
            "SHAPE_LENGTH",
            "SHAPE_Length", 
            "Shape_Length",
            "FID",
            "OID"
            };
            // Create a feature buffer.
            IFeatureBuffer featureBuffer = targetFeatureClass.CreateFeatureBuffer();

            // Create an insert cursor.
            IFeatureCursor insertCursor = targetFeatureClass.Insert(true);

            //IQueryFilter queryFilter = new QueryFilterClass();
            //queryFilter.WhereClause = queryClause;
            IFeatureCursor cursor = sourceFeatureClass.Search(null, true);

            IFeature sourceFeature = cursor.NextFeature();

            while (sourceFeature != null)
            {

                if (sourceFeature.FeatureType == esriFeatureType.esriFTSimpleEdge)
                {
                    //QI至ITopologicalOperator
                    ITopologicalOperator pToplogical = sourceFeature as ITopologicalOperator;
                    //执行Simplify操作
                    pToplogical.Simplify();
                }
                //如果是线或面要素类需要执行下Simplify，这里用的点要素类，不做验证了
                featureBuffer.Shape = sourceFeature.ShapeCopy;

                for (int i = 0; i < sourceFeature.Fields.FieldCount; i++)
                {
                    IField field = sourceFeature.Fields.get_Field(i);
                    if (field.Type != esriFieldType.esriFieldTypeOID && field.Type != esriFieldType.esriFieldTypeGeometry && field.Type != esriFieldType.esriFieldTypeGlobalID && field.Type != esriFieldType.esriFieldTypeGUID)
                    {
                        string fieldName = field.Name;
                        int index = featureBuffer.Fields.FindField(fieldName);
                        if (index > -1 && Array.IndexOf(excludedFields, fieldName) < 0)
                            featureBuffer.set_Value(index, sourceFeature.get_Value(i));
                    }

                }
                insertCursor.InsertFeature(featureBuffer);
                sourceFeature = cursor.NextFeature();
            }

            // Flush the buffer to the geodatabase.
            insertCursor.Flush();
            IFeatureClassManage targetFeatureClassManage = targetFeatureClass as IFeatureClassManage;
            targetFeatureClassManage.UpdateExtent();
        }

        /// <summary>
        /// 要素类属性转表
        /// </summary>
        /// <param name="featureClassName"></param>
        /// <param name="featureWorkspace"></param>
        /// <returns></returns>
        public  DataTable ConvertFeatureClassToDataTable(string featureClassName, IFeatureWorkspace featureWorkspace)
        {
            // 创建一个新的 DataTable 来存放要素结果  
            DataTable dataTable = new DataTable(featureClassName);

            // 获取要素类  
            IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(featureClassName);

            // 添加列到 DataTable  
            for (int i = 0; i < featureClass.Fields.FieldCount; i++)
            {
                IField field = featureClass.Fields.get_Field(i);
                dataTable.Columns.Add(field.Name, typeof(object)); // 使用 object 以适应不同类型  
            }

            // 创建查询游标遍历要素  
            IFeatureCursor cursor = featureClass.Search(null, false);
            IFeature feature;

            // 将要素数据填充到 DataTable  
            while ((feature = cursor.NextFeature()) != null)
            {
                DataRow row = dataTable.NewRow();
                for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                {
                    // 将每个字段的值添加到 DataRow  
                    row[i] = feature.get_Value(i);
                }
                dataTable.Rows.Add(row);
            }

            // 清理游标  
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);

            return dataTable;
        }

        /// <summary>  
        /// 将 数据库 中的表的属性转为 DataTable  
        /// </summary>  
        /// <param name="tableName">要转换的表名</param>  
        /// <param name="featureWorkspace"></param>  
        /// <returns>返回 DataTable，包含表的所有数据及其属性</returns>  
        public DataTable ConvertTableToDataTable(string tableName, IFeatureWorkspace featureWorkspace)
        {

            // 创建一个新的 DataTable 来存放结果  
            DataTable dataTable = new DataTable(tableName);

            // 将工作空间转换为 ITable  
            ITable table = (ITable)((IFeatureWorkspace)featureWorkspace).OpenTable(tableName); // 以表的形式打开  

            // 添加列到 DataTable  
            for (int i = 0; i < table.Fields.FieldCount; i++)
            {
                IField field = table.Fields.get_Field(i);
                dataTable.Columns.Add(field.Name, typeof(object)); // 使用 object 以适应不同类型  
            }

            // 创建查询游标遍历表中的数据  
            ICursor cursor = table.Search(null, false);
            IRow row;

            // 将表数据填充到 DataTable  
            while ((row = cursor.NextRow()) != null)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < table.Fields.FieldCount; i++)
                {
                    // 将每个字段的值添加到 DataRow  
                    dataRow[i] = row.get_Value(i);
                }
                dataTable.Rows.Add(dataRow);
            }

            // 清理游标  
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);

            return dataTable;
        }


    }
}
