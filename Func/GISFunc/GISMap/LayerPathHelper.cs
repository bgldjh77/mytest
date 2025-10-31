using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;

namespace 质检工具.Func.GISFunc.GISMap
{
    /// <summary>
    /// 图层路径帮助类 - 重构后的GetLayerPath功能
    /// </summary>
    public static class LayerPathHelper
    {
        /// <summary>
        /// 根据图层对象获取完整路径
        /// </summary>
        /// <param name="layer">图层对象</param>
        /// <returns>图层完整路径</returns>
        public static string GetLayerFullPath(ILayer layer)
        {
            if (layer == null)
                return null;

            try
            {
                // 处理要素图层
                if (layer is IFeatureLayer featureLayer)
                {
                    return GetFeatureLayerPath(featureLayer);
                }
                
                // 处理栅格图层
                if (layer is IRasterLayer rasterLayer)
                {
                    return GetRasterLayerPath(rasterLayer);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取图层路径时发生错误: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// 获取要素图层路径
        /// </summary>
        /// <param name="featureLayer">要素图层</param>
        /// <returns>要素图层路径</returns>
        public static string GetFeatureLayerPath(IFeatureLayer featureLayer)
        {
            if (featureLayer?.FeatureClass == null)
                return null;
                
            string datasetName = "";
            if (featureLayer.FeatureClass.FeatureDataset != null)
            {
                datasetName = featureLayer.FeatureClass.FeatureDataset.BrowseName;
            }

            IDataset dataset = featureLayer.FeatureClass as IDataset;
            if (dataset?.Workspace == null)
                return null;
                
            IWorkspace workspace = dataset.Workspace;
            
            // 处理本地数据库工作空间 (GDB)
            if (workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
                IWorkspaceName workspaceName = ((IDataset)workspace).FullName as IWorkspaceName;
                string gdbPath = workspaceName.PathName;
                
                string featureClassPath = gdbPath;
                if (!string.IsNullOrEmpty(datasetName))
                {
                    featureClassPath += "\\" + datasetName + "\\" + dataset.Name;
                }
                else
                {
                    featureClassPath += "\\" + dataset.Name;
                }
                
                Console.WriteLine($"获取GDB要素图层路径: {featureClassPath}");
                return featureClassPath;
            }
            // 处理文件系统工作空间 (SHP等)
            else if (workspace.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                IWorkspaceName workspaceName = ((IDataset)workspace).FullName as IWorkspaceName;
                string workspacePath = workspaceName.PathName;
                
                // 对于Shapefile，路径是工作空间路径 + 数据集名称 + .shp
                string shpPath = workspacePath + "\\" + dataset.Name + ".shp";
                
                Console.WriteLine($"获取SHP要素图层路径: {shpPath}");
                return shpPath;
            }
            
            return null;
        }
        
        /// <summary>
        /// 获取栅格图层路径
        /// </summary>
        /// <param name="rasterLayer">栅格图层</param>
        /// <returns>栅格图层路径</returns>
        private static string GetRasterLayerPath(IRasterLayer rasterLayer)
        {
            if (rasterLayer == null)
                return null;
                
            string rasterPath = rasterLayer.FilePath;
            Console.WriteLine($"获取栅格图层路径: {rasterPath}");
            return rasterPath;
        }
        
        /// <summary>
        /// 从TOC管理器获取选中图层的路径
        /// </summary>
        /// <param name="tocManager">TOC管理器</param>
        /// <returns>选中图层的路径</returns>
        public static string GetSelectedLayerPath(ITOCManager tocManager)
        {
            if (tocManager?.SelectedLayer == null)
                return null;
                
            return GetLayerFullPath(tocManager.SelectedLayer);
        }
        
        /// <summary>
        /// 从指定TOC管理器获取指定索引图层的路径
        /// </summary>
        /// <param name="tocManager">TOC管理器</param>
        /// <param name="layerIndex">图层索引</param>
        /// <returns>图层路径</returns>
        public static string GetLayerPathByIndex(ITOCManager tocManager, int layerIndex)
        {
            if (tocManager == null)
                return null;
                
            ILayer layer = tocManager.GetLayer(layerIndex);
            return GetLayerFullPath(layer);
        }
    }
}
