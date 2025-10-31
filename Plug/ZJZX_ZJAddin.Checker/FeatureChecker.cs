using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ZJZX_ZJAddin.Checker.IChecker;
using ZJZX_ZJAddin.IEnum;
using ZJZX_ZJAddin.Utils;

namespace ZJZX_ZJAddin.Checker
{
    class FeatureChecker
    {

        private static readonly IApplication _application;

        /// <summary>
        /// 读取图层中的要素路径
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadLayerFeaturePaths()  
        {  
            List<string> featurePaths = new List<string>();  

            // 获取当前地图文档  
            IMxDocument mxDocument = (IMxDocument)_application.Document;  
            IMap map = mxDocument.FocusMap;

            // 遍历图层  
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);

                // 检查图层是否为 IFeatureLayer  
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer != null)
                {
                    // 获取要素类  
                    IFeatureClass featureClass = featureLayer.FeatureClass;

                    // 检查特征图层和工作空间  
                    IGeoFeatureLayer geoFeatureLayer = featureLayer as IGeoFeatureLayer;
                    IDataset dataset = featureClass as IDataset;

                    if (geoFeatureLayer != null && dataset != null)
                    {
                        // 添加路径到列表  
                        featurePaths.Add(dataset.Workspace.PathName);
                    }

                    // 遍历要素以获得 OID  
                    IFeatureCursor featureCursor = featureClass.Search(null, false);
                    IFeature feature;
                    while ((feature = featureCursor.NextFeature()) != null)
                    {
                        // 可以在此处处理每一要素  
                    }

                    Marshal.ReleaseComObject(featureCursor); // 释放资源  
                }
            }
            return featurePaths; // 返回列表  
        }  

        /// <summary>
        /// 获取workspace下的所有要素类名
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public static List<string> GetFeatureClassByWorkspace(IWorkspace workspace)
        {
            List<string> listFeaClass = new List<string>();
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
                        listFeaClass.Add(pDataset.Name);
                    } // 若为要素数据集
                    else if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset pESubDataset = pDataset.Subsets;
                        IDataset pSubDataset = pESubDataset.Next();
                        while (pSubDataset != null)
                        {
                            listFeaClass.Add(pSubDataset.Name);
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

            return listFeaClass;
        }


        /// <summary>
        /// 获取GDB中的所有要素类名
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetFeatureClassByWorkspace(string gdbPath)
        {
            Dictionary<string, string> DicFeaClass = new Dictionary<string, string>();
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
                        DicFeaClass.Add(pDataset.Name, gdbPath + "\\" + pDataset.Name);
                    } // 若为要素数据集
                    else if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset pESubDataset = pDataset.Subsets;
                        IDataset pSubDataset = pESubDataset.Next();
                        while (pSubDataset != null)
                        {
                            DicFeaClass.Add(pSubDataset.Name, gdbPath + "\\" + pDataset.Name + "\\" + pSubDataset.Name);
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
            return DicFeaClass;
        }

        /// <summary>
        /// 模板创建数据库
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public ArrayList CreateFeatureByModel(String gdbPath, DataTable dt)
        {
            ArrayList resultList = new ArrayList();
            FeatureUtils FeatureUtils = new FeatureUtils();
            resultList.Add("模板创建数据库路径输出为：" + gdbPath);
            Dictionary<string, List<string>> ModelFieldNameDic = new Dictionary<string, List<string>>();
            List<FeatureInfo> ModelfeatureInfo_List = new List<FeatureInfo>();
            List<string> InListModel = new List<string>();
            List<string> InListModelDataset = new List<string>();
            FieldUtils FieldUtils = new FieldUtils();

            // 将模板数据进行转换方便进行比较
            foreach (DataRow row in dt.Rows)
            {
                FeatureInfo FeatureInfo = new FeatureInfo();
                if (!string.IsNullOrEmpty(row["要素类名称"].ToString()))
                {
                    InListModel.Add(row["要素类名称"].ToString());
                    InListModelDataset.Add(row["要素数据集"].ToString());
                    ModelfeatureInfo_List.Add(new FeatureInfo
                    {
                        Name = row["要素类名称"].ToString(),
                        AliasName = row["要素类别名"].ToString(),
                        FeatureDatasetName = row["要素数据集"].ToString(),
                        FeatureDatasetAliasName = row["要素数据集别名"].ToString(),
                        // 字典映射转换
                        FeatureType = FieldUtils.GetDicFieldType(row["要素类几何类型"].ToString()),
                        FieldType = FieldUtils.GetDicFieldType(row["字段类型"].ToString()),
                        FieldName = row["字段名"].ToString(),
                        FieldAliasName = row["别名"].ToString(),
                        FieldLength = string.IsNullOrEmpty(row["字段长度"].ToString()) ? 0 : Convert.ToInt32(row["字段长度"].ToString()),
                        NumberFormat = string.IsNullOrEmpty(row["小数位数"].ToString()) ? 0 : Convert.ToInt32(row["小数位数"].ToString()),
                        isNull = row["是否必填"].ToString()
                    });
                }
            }

            // 使用HashSet去重  
            HashSet<string> uniqueModelFc = new HashSet<string>(InListModel);
            // 为每个去重后的字段名赋值  
            foreach (var Fc in uniqueModelFc)
            {
                ModelFieldNameDic[Fc] = new List<string>(); // 可以根据需要填充该列表  
            }

            // 使用HashSet去重  
            HashSet<string> uniqueModelDatset = new HashSet<string>(InListModelDataset);
            

            // 进行创建操作
            IWorkspace workspace = GDBFileUtils.CreateFileGDB(gdbPath);
            if (workspace != null)
            {
                foreach (var Datset in uniqueModelDatset)
                {
                    // 创建数据集
                    FeatureUtils.CreateDataset(workspace, Datset);
                }
                foreach (var Fc in uniqueModelFc)
                {
                    bool isCreated = false;
                    foreach (FeatureInfo ModelFc in ModelfeatureInfo_List)
                    {

                        if (ModelFc.Name == Fc)
                        {
                            if (!isCreated)
                            {
                                FeatureUtils.CreateFeatureClass(workspace, ModelFc.FeatureDatasetName, Fc, ModelFc.AliasName, ModelFc.FeatureType);
                                isCreated = true;
                                resultList.Add("要素类："+Fc+"已创建");
                                IFeatureWorkspace fcWorkspace = workspace as IFeatureWorkspace;
                                IFeatureClass featureClass = fcWorkspace.OpenFeatureClass(Fc);

                                // 创建新的字段  
                                IFieldEdit newField = new FieldClass() as IFieldEdit;
                                newField.Name_2 = ModelFc.FieldName;
                                newField.AliasName_2 = ModelFc.FieldAliasName;
                                newField.Type_2 = FieldTypeRule.GetFieldType(ModelFc.FieldType);
                                if (ModelFc.isNull == "是")
                                {
                                    newField.IsNullable_2 = false;
                                }
                                else
                                {
                                    newField.IsNullable_2 = true;
                                }
                                // 根据字段类型设置字段的特定属性  
                                switch (newField.Type)
                                {
                                    case esriFieldType.esriFieldTypeString:
                                        newField.Length_2 = Convert.ToInt32(ModelFc.FieldLength); // 设置字符串字段的长度  
                                        break;
                                    case esriFieldType.esriFieldTypeDouble:
                                        newField.Precision_2 = Convert.ToInt32(ModelFc.NumberFormat); // 设置双精度字段的精度  
                                        break;
                                    // 可添加更多字段类型的处理  
                                }
                                // 添加新字段到目标字段集合  
                                featureClass.AddField(newField);
                                resultList.Add("要素类：" + Fc + "字段：" + ModelFc.FieldName + "已创建");
                            }
                            else
                            {
                                IFeatureWorkspace fcWorkspace = workspace as IFeatureWorkspace;
                                IFeatureClass featureClass = fcWorkspace.OpenFeatureClass(Fc);

                                // 创建新的字段  
                                IFieldEdit newField = new FieldClass() as IFieldEdit;
                                newField.Name_2 = ModelFc.FieldName;
                                newField.AliasName_2 = ModelFc.FieldAliasName;
                                newField.Type_2 = FieldTypeRule.GetFieldType( ModelFc.FieldType);
                                if (ModelFc.isNull == "是")
                                {
                                    newField.IsNullable_2 = true;
                                }
                                else
                                {
                                    newField.IsNullable_2 = false;
                                }
                                // 根据字段类型设置字段的特定属性  
                                switch (newField.Type)
                                {
                                    case esriFieldType.esriFieldTypeString:
                                        newField.Length_2 = Convert.ToInt32(ModelFc.FieldLength); // 设置字符串字段的长度  
                                        break;
                                    case esriFieldType.esriFieldTypeDouble:
                                        newField.Precision_2 = Convert.ToInt32(ModelFc.NumberFormat); // 设置双精度字段的精度  
                                        break;
                                    // 可添加更多字段类型的处理  
                                }
                                // 添加新字段到目标字段集合  
                                featureClass.AddField(newField);
                                resultList.Add("要素类：" + Fc + "字段：" + ModelFc.FieldName + "已创建");
                            }
                        }
                        resultList.Add("要素类：" + Fc + "创建完成");
                    }
                }
            }
            else
            {
                MessageBox.Show("新建GDB数据库失败");
                resultList.Add("新建GDB数据库失败");
            }
            return resultList;
        }

        /// <summary>
        /// 数据库要素和模板对比
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <param name="dt"></param>
        /// <param name="featureInfoList"></param>
        /// <returns></returns>
        public ArrayList DetectFeatureInfo(String gdbPath, DataTable dt, List<FeatureInfo> featureInfoList)
        {
            ArrayList resultList = new ArrayList();
            resultList.Add("数据库结构检查情况,检查数据库路径为：" + gdbPath);
            List<FeatureInfo> ModelfeatureInfo_List = new List<FeatureInfo>();
            List<string> InListModel = new List<string>();
             // 提取 Name 属性值为 List  
            List<string> InListGDB = featureInfoList.Select(f => f.Name).ToList();
            Dictionary<string, List<string>> ModelFieldNameDic = new Dictionary<string,List<string>>();
            Dictionary<string, List<string>> GDBFieldNameDic = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> CommonFieldNameDic = new Dictionary<string, List<string>>();

            // 将模板数据进行转换方便进行比较
            foreach (DataRow row in dt.Rows)
            {
                FeatureInfo FeatureInfo = new FeatureInfo();
                if (!string.IsNullOrEmpty(row["要素类名称"].ToString()))
                {
                    InListModel.Add(row["要素类名称"].ToString());
                    ModelfeatureInfo_List.Add(new FeatureInfo
                    {
                        Name = row["要素类名称"].ToString(),
                        AliasName = row["要素类别名"].ToString(),
                        FeatureDatasetName = row["要素数据集"].ToString(),
                        FeatureDatasetAliasName = row["要素数据集别名"].ToString(),
                        // 字典映射转换
                        FeatureType = FieldUtils.GetDicFieldType(row["要素类几何类型"].ToString()),
                        FieldType = FieldUtils.GetDicFieldType(row["字段类型"].ToString()),
                        FieldName = row["字段名"].ToString(),
                        FieldAliasName = row["别名"].ToString(),
                        FieldLength = string.IsNullOrEmpty(row["字段长度"].ToString()) ? 0 : Convert.ToInt32(row["字段长度"].ToString()),
                        NumberFormat = string.IsNullOrEmpty(row["小数位数"].ToString()) ? 0 : Convert.ToInt32(row["小数位数"].ToString()),
                        isNull = row["是否必填"].ToString(),
                        isCheckNum = row["是否进行小数位数检查"].ToString()
                    }); 
                }
            }


            // 使用HashSet去重  
            HashSet<string> uniqueModelFields = new HashSet<string>(InListModel);
            // 为每个去重后的字段名赋值  
            foreach (var field in uniqueModelFields)
            {
                ModelFieldNameDic[field] = new List<string>(); // 可以根据需要填充该列表  
            }

            // 使用HashSet去重  
            HashSet<string> uniqueGDBFields = new HashSet<string>(InListGDB);
            // 为每个去重后的字段名赋值  
            foreach (var field in uniqueGDBFields)
            {
                GDBFieldNameDic[field] = new List<string>(); // 可以根据需要填充该列表  
            }  
            
            // 使用HashSet去重  
            HashSet<string> uniqueFields = new HashSet<string>(InListGDB);
            // 为每个去重后的字段名赋值  
            foreach (var field in uniqueFields)
            {
                  
                CommonFieldNameDic[field] = new List<string>(); // 可以根据需要填充该列表  
            }  


            // 找到模板中存在但是不在 数据库 中的缺失元素  
            List<string> onlyInListWord = InListModel.Except(InListGDB).ToList();
            // 找到数据库中存在但是不在 模板 中的多余元素   
            List<string> onlyInListGDB = InListGDB.Except(InListModel).ToList();

            foreach (string inwordfc in onlyInListWord)
            {
                resultList.Add("要素类【" + inwordfc + "】在数据库中缺失");
            }

            foreach (string inGDBfc in onlyInListGDB)
            {
                resultList.Add("要素类【" + inGDBfc + "】在数据库中多余");
            }


            // 检查要素数据集，存储字段数组
            foreach (FeatureInfo FCI in ModelfeatureInfo_List)
            {
                if (!onlyInListWord.Contains(FCI.Name))
                {
                    ModelFieldNameDic[FCI.Name].Add(FCI.FieldName);  
                    foreach (FeatureInfo GDBFCI in featureInfoList)
                    {
                        if (FCI.Name == GDBFCI.Name)
                        {
                            GDBFieldNameDic[GDBFCI.Name].Add(GDBFCI.FieldName);
                            // 检查数据集
                            if (FCI.FeatureDatasetName != GDBFCI.FeatureDatasetName)
                            {
                                resultList.Add("要素类【" + FCI.Name + "】应在数据集【" + FCI.FeatureDatasetName + "】中,实际在数据集【" + GDBFCI.FeatureDatasetName + "】中");
                            }
                            // 检查数据集别名
                            if (FCI.FeatureDatasetAliasName != GDBFCI.FeatureDatasetAliasName)
                            {
                                resultList.Add("要素类【" + FCI.Name + "】数据集别名应为【" + FCI.FeatureDatasetAliasName + "】中,实际为【" + GDBFCI.FeatureDatasetAliasName + "】中");
                            }
                            // 检查要素类型
                            if (FCI.FeatureType != GDBFCI.FeatureType)
                            {
                                resultList.Add("要素类【" + FCI.Name + "】几何类型应为【" + FCI.FeatureType + "】中,实为【" + GDBFCI.FeatureType + "】中");
                            }
                            // 要素类别名
                            if (FCI.AliasName != GDBFCI.AliasName)
                            {
                                resultList.Add("要素类【" + FCI.Name + "】别名应为【" + FCI.AliasName + "】中,实为【" + GDBFCI.AliasName + "】中");
                            }
                        }
                    }
                }
            }


            // 多余重复字段检查
            foreach (var mkey in ModelFieldNameDic.Keys)
            {
                
                foreach (var gkey in GDBFieldNameDic.Keys)
                {
                    if (gkey == mkey)
                    {
                        List<string> MFields = ModelFieldNameDic[mkey];
                        List<string> GFields = GDBFieldNameDic[gkey];

                        List<string> onlyInModelFelds = MFields.Except(GFields).ToList();
                        // 找到数据库中存在但是不在 模板 中的多余元素   
                        List<string> onlyInGDBFields = GFields.Except(MFields).ToList();
                        // 获取两个列表中相同的且去重的元素  
                        List<string> commonElements = MFields.Intersect(GFields).ToList();
                        CommonFieldNameDic[mkey] = commonElements;


                        foreach (string inmodelf in onlyInModelFelds)
                        {
                            resultList.Add("要素类【" + mkey + "】在数据库中缺失字段【" + inmodelf + "】");
                        }

                        foreach (string inGDBf in onlyInGDBFields)
                        {
                            resultList.Add("要素类【" + gkey + "】在数据库中存在多余字段【" + inGDBf + "】");
                        }

                    }
                }
                
            }

             // 检查要素字段属性
            foreach (FeatureInfo FCI in ModelfeatureInfo_List)
            {
                if (!onlyInListWord.Contains(FCI.Name))
                {
                    foreach (FeatureInfo GDBFCI in featureInfoList)
                    {
                        if (FCI.Name == GDBFCI.Name)
                        {
                            // 匹配字段，比较字段属性
                            if (CommonFieldNameDic[GDBFCI.Name].Contains(GDBFCI.FieldName))
                            {
                                if (GDBFCI.FieldName == FCI.FieldName)
                                {
                                    // 检查字段类型
                                    if (GDBFCI.FieldType != FCI.FieldType)
                                    {
                                        resultList.Add("要素类【" + FCI.Name + "】在数据库中字段【" + GDBFCI.FieldName + "】类型出错 应为【" + FCI.FieldType + "】实为【" + GDBFCI.FieldType + "】");
                                    }
                                    // 检查字段别名
                                    if (GDBFCI.FieldAliasName != FCI.FieldAliasName)
                                    {
                                        resultList.Add("要素类【" + FCI.Name + "】在数据库中字段【" + GDBFCI.FieldName + "】别名出错 应为【" + FCI.FieldAliasName + "】实为【" + GDBFCI.FieldAliasName + "】");
                                    }
                                    // 检查字段长度
                                    if (GDBFCI.FieldLength != FCI.FieldLength && FCI.FieldType=="String")
                                    {
                                        resultList.Add("要素类【" + FCI.Name + "】在数据库中字段【" + GDBFCI.FieldName + "】长度出错 应为【" + FCI.FieldLength + "】实为【" + GDBFCI.FieldLength + "】");
                                    }
                                    // 检查字段小数位数
                                    if (GDBFCI.NumberFormat != FCI.NumberFormat && FCI.FieldType != "String" && FCI.isCheckNum == "是")
                                    {
                                        resultList.Add("要素类【" + FCI.Name + "】在数据库中字段【" + GDBFCI.FieldName + "】小数位数出错 应为【" + FCI.NumberFormat + "】实为【" + GDBFCI.NumberFormat + "】");
                                    }
                                    // 检查是否必填
                                    if (GDBFCI.isNull != FCI.isNull)
                                    {
                                        if (GDBFCI.isNull == "否")
                                        {
                                            resultList.Add("要素类【" + FCI.Name + "】在数据库中字段【" + GDBFCI.FieldName + "】是否必填项出错 应为 【必填】实为 【选填】");
                                        }
                                        else
                                        {
                                            resultList.Add("要素类【" + FCI.Name + "】在数据库中字段【" + GDBFCI.FieldName + "】是否必填项出错 应为 【选填】实为 【必填】");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
 
            }
            
            // 使用 HashSet 去重  
            HashSet<object> hashSet = new HashSet<object>(resultList.Cast<object>());
            // 将 HashSet 转换为 List，然后用它初始化 ArrayList  
            ArrayList distinctArrayList = new ArrayList(hashSet.ToList());

            distinctArrayList.Insert(1, "检测完成，存在属性结构问题的要素类共【" + (distinctArrayList.Count - 1).ToString() + "】条。");
            distinctArrayList.Insert(2, "************************************************************");
          return distinctArrayList;
        }

        /// <summary>
        /// 要素字段顺序调整
        /// </summary>
        /// <param name="gdbPath"></param>
        /// <param name="datasetName"></param>
        /// <param name="fcName"></param>
        /// <param name="resultgdb"></param>
        /// <param name="FieldsList"></param>
        public void alterFeatureFieldsIndex(string gdbPath,string datasetName,string fcName,string resultgdb,List<string> FieldsList)
        {
            FeatureUtils FeatureUtils = new FeatureUtils();
            FieldUtils FieldUtils = new FieldUtils();
            IWorkspaceFactory worFact = new FileGDBWorkspaceFactory();
            IWorkspace workspace = worFact.OpenFromFile(gdbPath, 0);
            
            // 这里进行要素坐标系获取，读取gdb中的要素集继续赋值，否则会出现面积为0无图形的情况
            IEnumDataset pEnumDatasets = workspace.get_Datasets(esriDatasetType.esriDTFeatureClass) as IEnumDataset;
            IDataset pDataset = pEnumDatasets.Next();
            while (pDataset == null)
            {
                pDataset = pEnumDatasets.Next();
            }
            ISpatialReference pSpatialReference = null;
            if (pDataset != null)
            {
                IGeoDataset pGeoDataset = pDataset as IGeoDataset;
                pSpatialReference = pGeoDataset.SpatialReference;
            }
            
            IFeatureWorkspace fcWorkspace = workspace as IFeatureWorkspace;
            IFeatureClass fclass =  fcWorkspace.OpenFeatureClass(fcName);
            IFeatureCursor featureCursor = fclass.Search(null, false);  
            IFeature feature = featureCursor.NextFeature();
            string fcType = null;

            if (feature != null)
            {

                fcType = fclass.ShapeType.ToString(); 
            }
            else
            {
                MessageBox.Show("原要素不存在数据");
                return;
            }
            IWorkspace resultworkspace = worFact.OpenFromFile(resultgdb, 0);
            // 建立要素
            IFeatureClass TargetFeature = FeatureUtils.CreateFeatureClass(resultworkspace, datasetName, fcName + "_顺序调整", fcName + "_顺序调整", fcType, pSpatialReference);
            // 复制且对字段重新进行排序
            FieldUtils.CopyFieldsFromFeature(feature, TargetFeature, FieldsList);
            // 复制对应字段值
            FeatureUtils.InsertFeaturesUsingCursor(fclass,TargetFeature);

        }
    }
}
