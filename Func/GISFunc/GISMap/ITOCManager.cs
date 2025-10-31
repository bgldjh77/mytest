using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;

namespace 质检工具.Func.GISFunc.GISMap
{
    /// <summary>
    /// TOC控件管理器接口
    /// </summary>
    public interface ITOCManager
    {
        /// <summary>
        /// 获取当前选中的图层
        /// </summary>
        ILayer SelectedLayer { get; }
        
        /// <summary>
        /// TOC控件实例
        /// </summary>
        AxTOCControl TOCControl { get; }
        
        /// <summary>
        /// 选中图层变化事件
        /// </summary>
        event EventHandler<LayerSelectedEventArgs> LayerSelected;
        
        /// <summary>
        /// 初始化TOC控件
        /// </summary>
        /// <param name="tocControl">TOC控件实例</param>
        void Initialize(AxTOCControl tocControl);
        
        /// <summary>
        /// 获取指定索引的图层
        /// </summary>
        /// <param name="index">图层索引</param>
        /// <returns>图层对象</returns>
        ILayer GetLayer(int index);
        
        /// <summary>
        /// 获取图层数量
        /// </summary>
        int LayerCount { get; }
    }
    
    /// <summary>
    /// 图层选中事件参数
    /// </summary>
    public class LayerSelectedEventArgs : EventArgs
    {
        public ILayer Layer { get; set; }
        public string LayerName { get; set; }
        public ITOCManager TOCManager { get; set; }
        
        public LayerSelectedEventArgs(ILayer layer, ITOCManager tocManager)
        {
            Layer = layer;
            LayerName = layer?.Name;
            TOCManager = tocManager;
        }
    }
}
