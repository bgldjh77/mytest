using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.GISFunc
{
    class GetDataType
    {
        //是否为面
        public static bool ispolognarea(string datapath)
        {
            bool tf = false;
            DataTypeChange dtc = new DataTypeChange();
            IFeatureClass featureClass = dtc.GetGDB_IFeatureClass(datapath);
            // 获取要素类的几何定义
            // 获取要素类的字段集合
            if (featureClass != null)
            {
                IFields fields = featureClass.Fields;

                // 遍历字段查找包含几何信息的字段
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (field.GeometryDef != null)
                    {
                        //esriGeometryPolyline
                        //esriGeometryPolygon
                        //esriGeometryPoint
                        Console.WriteLine(field.GeometryDef.GeometryType);
                        if (field.GeometryDef.GeometryType.ToString() == "esriGeometryPolygon")
                        {
                            return true;
                        }
                        else if (field.GeometryDef.GeometryType.ToString() == "esriGeometryPolyline")
                        {
                            //UIPage up = new UIPage();
                            //up.ShowInfoNotifier(datapath + "是线状矢量数据", false, 10000);
                            LogHelper.normallog(datapath + "是线状矢量数据");
                        }
                        else if (field.GeometryDef.GeometryType.ToString() == "esriGeometryPoint")
                        {
                            //UIPage up = new UIPage();
                            //up.ShowInfoNotifier(datapath + "是点状矢量数据", false, 10000);
                            LogHelper.normallog(datapath + "是点状矢量数据");
                        }
                        // 判断几何类型是否为面状几何
                        //if (field.GeometryDef.GeometryType == esriGeometryType.esriGeometryPolygon)
                        //{
                        //    Console.WriteLine("这是面状矢量数据");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("这不是面状矢量数据");
                        //}
                        //break;
                    }
                }
            }
            return tf;
        }
        //返回类型字符串 Polygon Polyline Point
        public static string pologntype(string datapath)
        {
            DataTypeChange dtc = new DataTypeChange();
            IFeatureClass featureClass = dtc.GetGDB_IFeatureClass(datapath);
            // 获取要素类的几何定义
            // 获取要素类的字段集合
            if (featureClass != null)
            {
                IFields fields = featureClass.Fields;

                // 遍历字段查找包含几何信息的字段
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (field.GeometryDef != null)
                    {
                        //esriGeometryPolyline
                        //esriGeometryPolygon
                        //esriGeometryPoint
                        Console.WriteLine(field.GeometryDef.GeometryType);
                        if (field.GeometryDef.GeometryType.ToString() == "esriGeometryPolygon")
                        {
                            return "Polygon";
                        }
                        else if (field.GeometryDef.GeometryType.ToString() == "esriGeometryPolyline")
                        {
                            return "Polyline";
                        }
                        else if (field.GeometryDef.GeometryType.ToString() == "esriGeometryPoint")
                        {
                            return "Point";
                        }

                    }
                }
            }
            return "";
        }
        //判断矢量、栅格、属性表 （仅限gdb）
        public static string getdatatype(string fullpath)
        {
            //string fullpath = @"C:\data\土壤二普测试\基础地理数据.gdb\tppp\Clip";

            //string gdb = @"C:\data\土壤二普测试\基础地理数据.gdb";
            string gdb = System.IO.Path.GetDirectoryName(fullpath);
            GDB_Func gf = new GDB_Func();
            string forpath = System.IO.Path.GetDirectoryName(fullpath);//上一级目录
            string dataname = System.IO.Path.GetFileNameWithoutExtension(fullpath);
            //Console.WriteLine(forpath);
            //Console.WriteLine(gf.IsFeatureDataset(forpath));
            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
            IWorkspace workspace;
            IFeatureWorkspace featureWorkspace;
            if (gf.IsFeatureDataset(forpath)) //要素集内
            {

                string feature_name = System.IO.Path.GetFileNameWithoutExtension(fullpath);//要素类名称
                gdb = System.IO.Path.GetDirectoryName(forpath);//gdb路径
                string datasetname = System.IO.Path.GetFileName(forpath);//要素集名
                                                                         // 打开工作空间
                                                                         //workspaceFactory = new FileGDBWorkspaceFactoryClass();
                workspace = workspaceFactory.OpenFromFile(gdb, 0);
                // 获取要素集 
                featureWorkspace = workspace as IFeatureWorkspace;
                IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(datasetname);


            }
            else { workspace = workspaceFactory.OpenFromFile(gdb, 0); }

            // 打开文件地理数据库
            //IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactory();
            //IWorkspace workspace = workspaceFactory.OpenFromFile(gdb, 0);

            // 获取数据集
            featureWorkspace = (IFeatureWorkspace)workspace;
            IFeatureClass featureClass = null;
            IRasterDataset rasterDataset = null;
            ITable table = null;

            Console.WriteLine(fullpath);
            Console.WriteLine("开始检查数据类型");
            try
            {// 尝试打开栅格数据集
             //rasterDataset = (IRasterDataset)workspace.OpenRasterDataset("bhtbpoint");
                IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();//1
                IRasterWorkspace rasterWorkspace = pWorkspaceFactory.OpenFromFile(gdb, 0) as IRasterWorkspace;
                // 打开栅格数据集
                rasterDataset = rasterWorkspace.OpenRasterDataset(dataname);

            }
            catch
            {
                Console.WriteLine("不是栅格数据");
                try
                {
                    // 尝试打开要素类
                    featureClass = featureWorkspace.OpenFeatureClass(dataname);
                }
                catch
                {
                    Console.WriteLine("打开要素类失败");
                    // 打开表
                    try { table = featureWorkspace.OpenTable(dataname); }
                    catch { Console.WriteLine("打开表失败"); }

                }
            }
            finally
            {
                Console.WriteLine("此处结束检测数据类型");
            }

            // 判断数据类型
            if (featureClass != null)
            {
                // 栅格数据集
                Console.WriteLine("数据类型: 矢量数据");
                return "矢量数据";
            }
            else if (rasterDataset != null)
            {
                // 矢量数据
                Console.WriteLine("数据类型:栅格数据");
                return "栅格数据";
            }
            else if (table != null)
            {
                // 表
                Console.WriteLine("数据类型: 表数据");
                return "表数据";
            }
            else
            {
                Console.WriteLine("未知数据类型");
                return "未知数据类型";
            }
        }
    }
}
