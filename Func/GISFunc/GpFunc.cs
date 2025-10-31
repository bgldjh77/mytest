using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ESRI.ArcGIS.Display;
using 质检工具.GpTask;
using 质检工具.GpTask.GpTaskConfig;
using 质检工具.MenuConfig;
using ESRI.ArcGIS.Geometry;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;

namespace 质检工具.Func.GISFunc
{
    class GpFunc
    {
        private ConcurrentDictionary<string, ITrackCancel> trackCancelMap; // 使用线程安全的字典

        public GpFunc()
        {
            trackCancelMap = new ConcurrentDictionary<string, ITrackCancel>();
        }

        public async Task gp_sim_task(string fulltoolpath, List<string>ls, string identifier)
        {
            string result = "";
            Task gpTask = Task.Run(() =>
            {
                //IVariantArray parameters = arglist2IVariantArray(gpBean);
                IVariantArray parameters = new VarArrayClass();
                foreach(string i in ls)
                {
                    parameters.Add(i);
                }
                result = gp_sim(fulltoolpath, parameters, identifier);

            });
            await gpTask;
            //return result;
        }
        public string gp_sim(string fulltoolpath, IVariantArray parameters, string identifier)
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

            //IVariantArray parameters = new VarArrayClass();
            object sev = null;
            string resultstr = "";
            IGeoProcessor2 gp = new GeoProcessorClass();
            try
            {
                gp.SetEnvironmentValue("workspace", globalpara.defaultGdbPath);
                gp.OverwriteOutput = true;
                if (!File.Exists(toolbox)) throw new FileNotFoundException("没有找到对应工具箱");
                

                ITrackCancel trackCancel = new CancelTrackerClass();
                trackCancelMap.TryAdd(identifier, trackCancel); // 添加到字典
                if (parameters.Count > 0) { LogHelper.add2mainlog("参数："); }

                for (int i = 0; i < parameters.Count; i++)
                {
                    LogHelper.add2mainlog(parameters.Element[i] as string);
                }                 

                gp.AddToolbox(toolbox);

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
                if (resultstr == "") resultstr = "工具可能不存在或者参数数量不匹配";
                LogHelper.ErrorLog(toolname + " 执行发生错误",  resultstr+"\n"+ ex.Message );
                gtf.GpTaskStateChange(BSMlist, GpStateEnum.失败);
            }
            finally
            {
                trackCancelMap.TryRemove(identifier, out _); // 移除已完成的任务
                System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(parameters);
            }

            
            Console.WriteLine("结束gp运行！");


            return resultstr;
        }
        
        //gis自带
        public string gp_official(string toolname, List<string> paramer)
        {
            IGeoProcessor2 gp = new GeoProcessorClass();
            gp.OverwriteOutput = true;
            IVariantArray parameters = new VarArrayClass();
            object sev = null;
            string resultstr = "";

            try
            {
                //输入数据  
                foreach (string i in paramer)
                {
                    parameters.Add(i);
                }
                Console.WriteLine("go gp");

                gp.Execute(toolname, parameters, null);//

                Console.WriteLine(gp.GetMessages(ref sev));
            }
            catch (Exception ex)
            {

                Console.WriteLine(gp.GetMessages(ref sev));
                
                for (int i = 0; i < gp.MessageCount; i++)
                {
                    resultstr += gp.GetMessage(i);
                }
                LogHelper.ErrorLog("工具元数据导出", resultstr);
            }
            //释放gp工具及其参数
            System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(parameters);
            Console.WriteLine("结束gp运行！");
            return resultstr;
        }
        private string GetToolNameByIdentifier(string identifier)
        {
            return globalpara.gptasklist.FirstOrDefault(x => x.Key == identifier).Value;
        }

        public void CancelExecution(string identifier)
        {
            string toolName = GetToolNameByIdentifier(identifier);
            Console.WriteLine($"当前字典中的任务数：{trackCancelMap.Count}");
            Console.WriteLine($"尝试取消的任务：{toolName}，ID：{identifier}");
            
            if (string.IsNullOrEmpty(identifier))
            {
                Console.WriteLine("传入的identifier为空");
                return;
            }

            if (trackCancelMap.TryGetValue(identifier, out ITrackCancel trackCancel))
            {
                if (trackCancel != null)
                {
                    trackCancel.Cancel();
                    LogHelper.normallog($"成功取消任务：{toolName}，ID：{identifier}");
                    Console.WriteLine($"成功取消任务：{identifier}");
                }
                else
                {
                    Console.WriteLine("trackCancel对象为null");
                }
            }
            else
            {
                Console.WriteLine($"未找到对应的任务：{identifier}");
                Console.WriteLine("当前活动的任务ID列表：");
                foreach (var key in trackCancelMap.Keys)
                {
                    Console.WriteLine(key);
                }
            }
        }
        private IVariantArray arglist2IVariantArray(GpBean gpBean)
        {
            IVariantArray parameters = new VarArrayClass();
            foreach (ToolArgs args in gpBean.toolArglist)
            {
                if (args.argType == ArgTypeEnum.Coordinate)
                {
                    ISpatialReference spatial_ref = GetSpacialRef.GetSpatialReferenceByDataSourceType(args.nowValue);
                    parameters.Add(spatial_ref);
                }
                else
                {
                    parameters.Add(args.nowValue);
                }
            }
            return parameters;
        }
        public string gp_sim_bk(string fulltoolpath, List<string> paramer, string identifier)
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
            trackCancelMap.TryAdd(identifier, trackCancel); // 添加到字典

            IVariantArray parameters = new VarArrayClass();
            object sev = null;
            string resultstr = "";

            try
            {
                if (paramer.Count > 0) { LogHelper.add2mainlog("参数："); }

                foreach (string i in paramer)
                {
                    LogHelper.add2mainlog(i);
                    parameters.Add(i);
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
                trackCancelMap.TryRemove(identifier, out _); // 移除已完成的任务

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(parameters);
            Console.WriteLine("结束gp运行！");


            return resultstr;
        }
        /// <summary>
        /// 可以验证参数类型是否正确 数字的字符串和数值型可以转换
        /// </summary>
        /// <param name="toolboxPath">工具箱路径（.tbx文件）</param>
        /// <param name="toolName">工具名称</param>
        /// <returns>true表示工具存在，false表示不存在</returns>
        public bool CheckToolValidate(string toolboxPath, string toolName)
        {
            if (string.IsNullOrEmpty(toolboxPath) || string.IsNullOrEmpty(toolName))
                return false;

            if (!File.Exists(toolboxPath))
                return false;
            //object sev = null;
            IGeoProcessor2 gp = null;
            try
            {
                gp = new GeoProcessorClass();

                // 添加工具箱到当前会话
                gp.AddToolbox(toolboxPath);

                // 构建完整的工具路径：工具箱名\工具名
                string toolboxName = System.IO.Path.GetFileNameWithoutExtension(toolboxPath);
                //string fullToolName = $"{toolboxName}\\{toolName}";

                // 使用Validate方法来验证工具是否存在
                // 创建一个空的参数数组进行验证
                IVariantArray parameters = new VarArrayClass();
                parameters.Add("");
                // 尝试验证工具，如果工具不存在会抛出异常
                var result = gp.Validate(toolName, parameters, false);

                return true; // 如果没有抛出异常，说明工具存在
            }
            catch (Exception ex)
            {
                // 如果工具不存在或其他错误，返回false

                //Console.WriteLine(gp.GetMessages(ref sev));
                //if(gp.MessageCount==0) LogHelper.ErrorLog($"工具不存在或者参数类型错误: {toolName}", ex);
                return false;
            }
            finally
            {
                // 清理资源
                if (gp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
                }
            }
        }
        public string gp_getversion()
        {
            IGeoProcessor2 gp = new GeoProcessorClass();
            gp.OverwriteOutput = true;
            gp.AddToolbox(globalpara.sidetoolbox);

          
            IVariantArray parameters = new VarArrayClass();
            object sev = null;
            string resultstr = "";

            try
            {
                ITrackCancel trackCancel = new CancelTrackerClass();
                gp.Execute("GetVersion", parameters, trackCancel);
                resultstr = gp.GetMessages(ref sev);
                
            }
            catch (Exception ex)
            {
                resultstr = gp.GetMessages(ref sev);
            }


            System.Runtime.InteropServices.Marshal.ReleaseComObject(gp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(parameters);
            Console.WriteLine("结束gp运行！");


            return resultstr;
        }


       
        
    }
}
