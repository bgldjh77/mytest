using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;

namespace 质检工具.Func.GISFunc.GISMap
{
    /// <summary>
    /// TOC控件管理器实现类
    /// </summary>
    public class TOCManager : ITOCManager
    {
        private AxTOCControl _tocControl;
        private ILayer _selectedLayer;
        
        public ILayer SelectedLayer => _selectedLayer;
        public AxTOCControl TOCControl => _tocControl;
        
        public event EventHandler<LayerSelectedEventArgs> LayerSelected;
        
        public int LayerCount => _tocControl?.ActiveView?.FocusMap?.LayerCount ?? 0;
        
        /// <summary>
        /// 初始化TOC控件
        /// </summary>
        /// <param name="tocControl">TOC控件实例</param>
        public void Initialize(AxTOCControl tocControl)
        {
            if (tocControl == null)
                throw new ArgumentNullException(nameof(tocControl));
                
            _tocControl = tocControl;
            _tocControl.OnMouseDown += OnTOCMouseDown;
        }
        
        /// <summary>
        /// 获取指定索引的图层
        /// </summary>
        /// <param name="index">图层索引</param>
        /// <returns>图层对象</returns>
        public ILayer GetLayer(int index)
        {
            if (_tocControl?.ActiveView?.FocusMap == null || index < 0 || index >= LayerCount)
                return null;
                
            return _tocControl.ActiveView.FocusMap.get_Layer(index);
        }
        
        /// <summary>
        /// TOC控件鼠标点击事件处理
        /// </summary>
        private void OnTOCMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 1) // 只处理左键点击
                return;
                
            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer pLayer = null;
            IBasicMap pBasMap = null;
            object unk = null;
            object data = null;
            
            _tocControl.HitTest(e.x, e.y, ref pItem, ref pBasMap, ref pLayer, ref unk, ref data);
            
            if (pItem == esriTOCControlItem.esriTOCControlItemLayer && pLayer != null)
            {
                // 排除注记图层
                if (pLayer is IAnnotationSublayer)
                    return;
                    
                _selectedLayer = pLayer;
                
                // 触发图层选中事件
                LayerSelected?.Invoke(this, new LayerSelectedEventArgs(_selectedLayer, this));
            }
        }
        
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_tocControl != null)
            {
                _tocControl.OnMouseDown -= OnTOCMouseDown;
                _tocControl = null;
            }
            _selectedLayer = null;
        }
    }
}
