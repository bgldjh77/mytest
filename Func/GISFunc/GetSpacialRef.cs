using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;

namespace 质检工具
{
    class GetSpacialRef
    {
        public static int GetWkidFromPrj(string prjFilePath)
        {
            if (!File.Exists(prjFilePath))
                throw new FileNotFoundException("PRJ文件未找到", prjFilePath);

            string content = File.ReadAllText(prjFilePath);

            // 匹配 AUTHORITY["EPSG", XXXX] 或 AUTHORITY["EPSG","XXXX"] 或 AUTHORITY["EPSG", 4491]
            // 支持数字前后是否有引号、空格
            var match = Regex.Match(content, @"AUTHORITY\s*\[\s*""EPSG""\s*,\s*""?(\d+)""?\s*\]",
                          RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            if (match.Success && int.TryParse(match.Groups[1].Value, out int wkid))
            {
                return wkid; // 如 4491
            }

            return 0; // 未找到
        }

        //获取空间参考对象
        public static ISpatialReference GetSpatialReferenceByDataSourceType(string dataSourcePath, string featureClassName = null)
        {
            if (string.IsNullOrEmpty(dataSourcePath))
            {
                Console.WriteLine("错误：数据源路径为空");
                LogHelper.ErrorLog("空间参考错误", "数据源路径为空");
                return null;
            }

            string extension = System.IO.Path.GetExtension(dataSourcePath).ToLower();

            switch (extension)
            {
                case ".lyr":
                    return GetSpatialReferenceFromLyrFile(dataSourcePath);

                case ".shp":
                    return GetSpatialReferenceFromShpFile(dataSourcePath);

                case ".gdb":
                    if (string.IsNullOrEmpty(featureClassName))
                    {
                        Console.WriteLine("错误：要素类名称未提供");
                        LogHelper.ErrorLog("空间参考错误", "要素类名称未提供");
                        return null;
                    }
                    return GetSpatialReferenceFromGdbFeatureClass(dataSourcePath, featureClassName);

                default:
                    Console.WriteLine($"错误：不支持的数据源类型 -> {extension}");
                    LogHelper.ErrorLog("空间参考错误", $"不支持的数据源类型: {extension}");
                    return null;
            }
        }
        //lyr
        public static ISpatialReference GetSpatialReferenceFromLyrFile(string lyrFilePath)
        {
            if (!File.Exists(lyrFilePath))
            {
                Console.WriteLine($"错误：图层文件不存在 -> {lyrFilePath}");
                LogHelper.ErrorLog("空间参考错误", "图层文件不存在");
                return null;
            }

            ILayerFile layerFile = null;
            ILayer layer = null;
            ISpatialReference spatialReference = null;

            try
            {
                layerFile = new LayerFileClass();
                if (!layerFile.get_IsLayerFile(lyrFilePath))
                {
                    Console.WriteLine($"错误：提供的文件不是一个有效的 .lyr 文件 -> {lyrFilePath}");
                    LogHelper.ErrorLog("空间参考错误", "提供的文件不是一个有效的 .lyr 文件");
                    return null;
                }

                layerFile.Open(lyrFilePath);
                layer = layerFile.Layer;

                if (layer is IFeatureLayer featureLayer)
                {
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    if (featureClass != null)
                    {
                        IGeoDataset geoDataset = featureClass as IGeoDataset;
                        spatialReference = geoDataset?.SpatialReference;
                    }
                    else
                    {
                        LogHelper.ErrorLog("空间参考错误", $"无法从 .lyr 文件中获取图层对象");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"打开 .lyr 文件时出错: {ex.Message}");
                LogHelper.ErrorLog("空间参考错误", $"打开 .lyr 文件时出错{ex.Message}");
            }
            finally
            {
                if (layerFile != null) Marshal.ReleaseComObject(layerFile);
                if (layer != null) Marshal.ReleaseComObject(layer);
            }

            return spatialReference;
        }
        //shp
        public static ISpatialReference GetSpatialReferenceFromShpFile(string shpFilePath)
        {
            if (!File.Exists(shpFilePath))
            {
                Console.WriteLine($"错误：Shapefile 文件不存在 -> {shpFilePath}");
                LogHelper.ErrorLog("空间参考错误", "Shapefile 文件不存在");
                return null;
            }

            IWorkspaceFactory workspaceFactory = null;
            IFeatureWorkspace featureWorkspace = null;
            IFeatureClass featureClass = null;
            ISpatialReference spatialReference = null;

            try
            {
                workspaceFactory = new ShapefileWorkspaceFactoryClass();
                string directory = System.IO.Path.GetDirectoryName(shpFilePath);
                string fileName = System.IO.Path.GetFileName(shpFilePath);

                featureWorkspace = (IFeatureWorkspace)workspaceFactory.OpenFromFile(directory, 0);
                featureClass = featureWorkspace.OpenFeatureClass(fileName);

                if (featureClass == null)
                {
                    Console.WriteLine("错误：无法打开 Shapefile 的要素类");
                    LogHelper.ErrorLog("空间参考错误", "无法打开 Shapefile 的要素类");
                    return null;
                }

                IGeoDataset geoDataset = featureClass as IGeoDataset;
                spatialReference = geoDataset?.SpatialReference;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取 Shapefile 时出错: {ex.Message}");
                LogHelper.ErrorLog("空间参考错误", $"读取 Shapefile 时出错: {ex.Message}");
            }
            finally
            {
                if (featureClass != null) Marshal.ReleaseComObject(featureClass);
                if (featureWorkspace != null) Marshal.ReleaseComObject(featureWorkspace);
                if (workspaceFactory != null) Marshal.ReleaseComObject(workspaceFactory);
            }

            return spatialReference;
        }
        //gdb
        public static ISpatialReference GetSpatialReferenceFromGdbFeatureClass(string gdbPath, string featureClassNameOrPath)
        {
            if (!Directory.Exists(gdbPath))
            {
                Console.WriteLine($"错误：Geodatabase 路径不存在 -> {gdbPath}");
                LogHelper.ErrorLog("空间参考错误", "Geodatabase 路径不存在");
                return null;
            }

            IWorkspace workspace = null;
            IFeatureWorkspace featureWorkspace = null;
            IFeatureDataset featureDataset = null;
            IFeatureClass featureClass = null;
            ISpatialReference spatialReference = null;

            try
            {
                // 创建工作空间工厂
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                workspace = workspaceFactory.OpenFromFile(gdbPath, 0);
                

                // 判断是否是要素集内的路径（如 "DatasetName/FeatureClassName"）
                if (featureClassNameOrPath.Contains("/"))
                {
                    string[] parts = featureClassNameOrPath.Split('/');
                    string datasetName = parts[0];
                    string featureName = parts[1];


                    // 获取要素集 
                    featureWorkspace = workspace as IFeatureWorkspace;
                    featureDataset = featureWorkspace.OpenFeatureDataset(datasetName);
                    //要素类
                    featureClass = featureWorkspace.OpenFeatureClass(featureName);

                }
                else
                {
                    // 不在要素集中，直接打开
                    featureClass = featureWorkspace.OpenFeatureClass(featureClassNameOrPath);
                }

                if (featureClass == null)
                {
                    Console.WriteLine($"错误：无法打开要素类 -> {featureClassNameOrPath}");
                    LogHelper.ErrorLog("空间参考错误", $"无法打开要素类: {featureClassNameOrPath}");
                    return null;
                }

                // 获取空间参考
                IGeoDataset geoDataset = featureClass as IGeoDataset;
                spatialReference = geoDataset?.SpatialReference;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取 Geodatabase 要素类时出错: {ex.Message}");
                LogHelper.ErrorLog("空间参考错误", $"读取 Geodatabase 要素类时出错: {ex.Message}");
            }
            finally
            {
                // 释放 COM 对象
                if (featureClass != null) Marshal.ReleaseComObject(featureClass);
                if (featureDataset != null) Marshal.ReleaseComObject(featureDataset);
                if (featureWorkspace != null) Marshal.ReleaseComObject(featureWorkspace);
                if (workspace != null) Marshal.ReleaseComObject(workspace);
            }

            return spatialReference;
        }

        //获取wkt字符串
        public static string GetSpatialReferenceWktFromLyrFile(string lyrFilePath)
        {
            string toolbox = @"C:\Users\Administrator\Desktop\新建文件夹\工具箱.tbx";
            IGeoProcessor2 gp = new GeoProcessorClass();
            object sev = null;
            gp.OverwriteOutput = true;
            gp.AddToolbox(toolbox);
            string wktstring = "";
            try
            {
                IVariantArray parameters = new VarArrayClass();
                parameters.Add(lyrFilePath);
                Console.WriteLine("run gp");
                IGeoProcessorResult result  =gp.Execute("getwkt", parameters, null);
                Console.WriteLine(gp.GetMessages(ref sev));
                wktstring= result.ReturnValue.ToString();


                //Console.WriteLine(result.ReturnValue.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(gp.GetMessages(ref sev));
            }
            finally
            {
                // 释放 COM 对象
                System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
            }
            return wktstring;
        }
        public static void rungp(string lyr,string mdb,string area)
        {
            string toolbox = @"C:\质检中心\upload\zjzx_zjrj\质检工具\质检工具\bin\Debug\data\tool\项目检查\栅格检查.tbx";
            ISpatialReference spatialReference = GetSpatialReferenceFromLyrFile(lyr);
            IGeoProcessor2 gp = new GeoProcessorClass();
            object sev = null;
            gp.OverwriteOutput = true;
            gp.AddToolbox(toolbox);
            try
            {
                IVariantArray parameters = new VarArrayClass();
                parameters.Add(spatialReference);
                parameters.Add(mdb);
                parameters.Add(area);
                parameters.Add("");
                Console.WriteLine("run gp");
                gp.Execute("DOM质检意见处理", parameters, null);
                Console.WriteLine(gp.GetMessages(ref sev));
            }
            catch(Exception ex)
            {
                Console.WriteLine(gp.GetMessages(ref sev));
                Console.WriteLine(ex);
            }

        }
        public static void rungp2(string wkt, string mdb, string area)
        {
            string toolbox = @"C:\质检中心\upload\zjzx_zjrj\质检工具\质检工具\bin\Debug\data\tool\项目检查\栅格检查.tbx";
           
            IGeoProcessor2 gp = new GeoProcessorClass();
            object sev = null;
            gp.OverwriteOutput = true;
            gp.AddToolbox(toolbox);
            try
            {
                IVariantArray parameters = new VarArrayClass();
                parameters.Add(wkt);
                parameters.Add(mdb);
                parameters.Add(area);
                parameters.Add("");
                Console.WriteLine("run gp");
                gp.Execute("DOM质检意见处理", parameters, null);
                Console.WriteLine(gp.GetMessages(ref sev));
            }
            catch (Exception ex)
            {
                Console.WriteLine(gp.GetMessages(ref sev));
            }

        }
        public static string gp_sim(string fulltoolpath, IVariantArray parameters, string identifier)
        {

            GpTaskFunc gtf = new GpTaskFunc();
            List<string> BSMlist = new List<string> { identifier };

            // 更新状态、任务开始时间
            gtf.GpTaskStateChange(BSMlist, GpStateEnum.运行中);


            int lastIndex = fulltoolpath.LastIndexOf(".tbx");
            int lastIndex2 = fulltoolpath.LastIndexOf("\\");
            string part1 = fulltoolpath.Substring(0, lastIndex + 4);
            string part2 = fulltoolpath.Substring(lastIndex2 + 1);
            string toolbox = part1;
            string toolname = part2;

            LogHelper.normallog("当前任务：" + toolname);

            if (!globalpara.gptasklist.ContainsKey(identifier))
            {
                globalpara.gptasklist.Add(identifier, toolname);
            }

            IGeoProcessor2 gp = new GeoProcessorClass();
            gp.OverwriteOutput = true;
            gp.AddToolbox(toolbox);

            ITrackCancel trackCancel = new CancelTrackerClass();
            //trackCancelMap.TryAdd(identifier, trackCancel); // 添加到字典

            //IVariantArray parameters = new VarArrayClass();
            object sev = null;
            string resultstr = "";

            try
            {
                if (parameters.Count > 0) { LogHelper.add2mainlog("参数："); }

                for (int i = 0; i < parameters.Count; i++)
                {
                    LogHelper.add2mainlog(parameters.Element[i] as string);
                }
                    
                LogHelper.add2mainlog("-----开始执行工具-----");
                Console.WriteLine("go gp");
                gp.Execute(toolname, parameters, trackCancel);
                resultstr = gp.GetMessages(ref sev);
                resultstr = LogHelper.gpstrspilter(resultstr);
                Console.WriteLine(gp.GetMessages(ref sev));
                LogHelper.add2mainlog(resultstr);
                LogHelper.add2mainlog(GetNowTime.nowTime() + "已成功执行：" + toolname + "\n");
                LogHelper.Successnotice("已成功执行：" + toolname);

                gtf.GpTaskStateChange(BSMlist, GpStateEnum.已完成);
                LogHelper.add2mainlog("-----结束执行工具-----");
            }
            catch (Exception ex)
            {
                Console.WriteLine(gp.GetMessages(ref sev));
                for (int i = 0; i < gp.MessageCount; i++)
                {
                    resultstr += gp.GetMessage(i);
                }

                LogHelper.ErrorLog(toolname + " 执行发生错误", resultstr);
                gtf.GpTaskStateChange(BSMlist, GpStateEnum.失败);
            }
            finally
            {
                //trackCancelMap.TryRemove(identifier, out _); // 移除已完成的任务

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(parameters);
            Console.WriteLine("结束gp运行！");


            return resultstr;
        }
    
        public static void rungp3(string lyr, string mdb, string area)
        {
            string toolbox = @"C:\质检中心\upload\zjzx_zjrj\质检工具\质检工具\bin\Debug\data\tool\项目检查\栅格检查.tbx\DOM质检意见处理";
            IVariantArray paramer = new VarArrayClass();
            paramer.Add(GetSpatialReferenceFromLyrFile(lyr));
            paramer.Add(mdb);
            paramer.Add(area);
            gp_sim(toolbox, paramer, "test");
        }
    }
}
