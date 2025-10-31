using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具.Func.GISFunc
{
    class DataTypeChange
    {
        //gdb要素转IFeatureClass
        public IFeatureClass GetGDB_IFeatureClass(string fullpath)
        {
            GDB_Func gf = new GDB_Func();
            string forpath = System.IO.Path.GetDirectoryName(fullpath);//上一级目录

            if (gf.IsFeatureDataset(forpath)) //要素集内
            {
                string feature_name = System.IO.Path.GetFileNameWithoutExtension(fullpath);//要素类名称
                string gdbpath = System.IO.Path.GetDirectoryName(forpath);//gdb路径
                string datasetname = System.IO.Path.GetFileName(forpath);//要素集名
                // 打开工作空间
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace workspace = workspaceFactory.OpenFromFile(gdbpath, 0);
                // 获取要素集 
                IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
                IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(datasetname);
                // 获取要素类
                try
                {
                    IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(feature_name);
                    return featureClass;
                }
                catch (Exception ex)
                {
                    string exstr = ex.ToString();
                    LogHelper.ErrorLog("GDB要素获取错误", ex);
                    Console.WriteLine("gdf_fc2FC2" + exstr);
                    return null;
                }

                // 创建要素图层
                //IFeatureLayer featureLayer = new FeatureLayer();
                //return featureLayer.FeatureClass = featureClass;

            }
            else//直接获取要素类
            {
                Console.WriteLine("直接获取要素类");
                return GetGDB_IFeatureClass_sim(fullpath);
            }

        }
        private IFeatureClass GetGDB_IFeatureClass_sim(string fullpath)
        {
            try
            {
                string gdbPath = System.IO.Path.GetDirectoryName(fullpath);
                string feature_name = System.IO.Path.GetFileNameWithoutExtension(fullpath);
                FileGDBWorkspaceFactoryClass pFileGDBWorkspaceFactoryClass = new FileGDBWorkspaceFactoryClass();
                //IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pFileGDBWorkspaceFactoryClass.OpenFromFile(gdbPath, 0);
                IFeatureClass pFC = pFeatureWorkspace.OpenFeatureClass(feature_name);
                IFeatureLayer pFLayer = new FeatureLayerClass();
                return pFLayer.FeatureClass = pFC;
            }
            catch(Exception ex)
            {
                LogHelper.ErrorLog("GDB要素获取错误", ex);
            }
            return null;

        }
        //ITable转DataTable
        public System.Data.DataTable ToDataTable(ITable mTable)
        {
            IRow pRrow = null;
            ICursor pCursor = null;
            try
            {
                System.Data.DataTable pTable = new System.Data.DataTable();
                DataColumn col0 = new DataColumn(mTable.Fields.get_Field(0).Name, typeof(int));
                pTable.Columns.Add(col0);
                for (int i = 1; i < mTable.Fields.FieldCount; i++)
                {

                    string alistname = mTable.Fields.get_Field(i).AliasName;
                    string fieldname = mTable.Fields.get_Field(i).Name;

                    if (alistname == "Shape_Length" | alistname == "SHAPE_Length")
                    {
                        alistname = "长度  ";
                    }
                    else if (alistname == "Shape_Area" | alistname == "SHAPE_Area")
                    {
                        alistname = "面积  ";
                    }


                    string typename = mTable.Fields.get_Field(i).Type.ToString();
                    Console.WriteLine(typename);
                    if (typename == "esriFieldTypeDouble" | typename == "esriFieldTypeSingle")
                    {
                        //Console.WriteLine("double");
                        DataColumn col1 = new DataColumn(fieldname, typeof(double));
                        col1.Caption = alistname; // 设置列的别名
                        pTable.Columns.Add(col1);
                    }
                    else if (typename == "esriFieldTypeSmallInteger" | typename == "esriFieldTypeInteger")
                    {
                        //Console.WriteLine("int");
                        DataColumn col1 = new DataColumn(fieldname, typeof(int));
                        col1.Caption = alistname; // 设置列的别名
                        pTable.Columns.Add(col1);
                    }
                    else if (typename == "esriFieldTypeDate")
                    {
                        //Console.WriteLine("datetime");
                        DataColumn col1 = new DataColumn(fieldname, typeof(string));
                        col1.Caption = alistname; // 设置列的别名
                        pTable.Columns.Add(col1);
                    }
                    else
                    {
                        //Console.WriteLine("string");
                        DataColumn col1 = new DataColumn(fieldname, typeof(string));
                        col1.Caption = alistname; // 设置列的别名
                        pTable.Columns.Add(col1);
                    }

                    //pTable.Columns.Add(alistname);

                    //pTable.Columns.Add(mTable.Fields.get_Field(i).Name);
                }
                pCursor = mTable.Search(null, false);
                pRrow = pCursor.NextRow();
                while (pRrow != null)
                {
                    DataRow pRow = pTable.NewRow();
                    string[] StrRow = new string[pRrow.Fields.FieldCount];
                    for (int i = 0; i < pRrow.Fields.FieldCount; i++)
                    {
                        string typename = pRrow.Fields.Field[i].Type.ToString();
                        if (pRrow.get_Value(i).ToString() == "")
                        {
                            //UIMessageBox.Show("空");
                            //UIMessageBox.Show(pRrow.Fields.Field[i].Name);
                            //UIMessageBox.Show(typename);
                            //StrRow[i] = "0";
                            //if (typename == "esriFieldTypeSmallInteger" | typename == "esriFieldTypeInteger"| typename == "esriFieldTypeDouble" | typename == "esriFieldTypeSingle")
                            //{
                            //    StrRow[i] = "0";
                            //}
                        }
                        else
                        {
                            StrRow[i] = pRrow.get_Value(i).ToString();
                        }

                        //Console.WriteLine(StrRow[i]);
                    }

                    pRow.ItemArray = StrRow;
                    pTable.Rows.Add(pRow);
                    pRrow = pCursor.NextRow();
                }
                pTable.Columns.RemoveAt(1);//删除shape列
                return pTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if (ex.Message.Contains("OutOfMemory")) { LogHelper.ErrorLog("数据内存占用过大",ex); }
                return null;
            }
            finally
            {
                if (pRrow != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pRrow);
                    pRrow = null;
                }

                if (pCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
                    pCursor = null;
                }

                // 强制垃圾回收，确保内存回收及时进行
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
