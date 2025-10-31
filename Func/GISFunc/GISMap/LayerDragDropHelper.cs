using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace 质检工具.Func.GISFunc.GISMap
{
    /// <summary>
    /// 图层拖拽辅助类 - 重构后的LayerDataObject功能
    /// </summary>
    public static class LayerDragDropHelper
    {
        /// <summary>
        /// 为TextBox控件启用从指定TOC管理器拖放图层的功能
        /// </summary>
        /// <param name="textBox">目标TextBox控件</param>
        /// <param name="tocManagerName">TOC管理器名称，为空则使用默认管理器</param>
        public static void EnableLayerDragDrop(TextBox textBox, string tocManagerName = null)
        {
            if (textBox == null)
                throw new ArgumentNullException(nameof(textBox));

            // 启用TextBox的拖放功能
            textBox.AllowDrop = true;
            
            // 存储TOC管理器名称到Tag中
            textBox.Tag = tocManagerName;
            
            // 绑定拖放事件
            textBox.DragEnter += TextBox_DragEnter;
            textBox.DragDrop += TextBox_DragDrop;
        }

        /// <summary>
        /// 为多个TextBox控件批量启用拖放功能
        /// </summary>
        /// <param name="textBoxes">TextBox控件数组</param>
        /// <param name="tocManagerName">TOC管理器名称，为空则使用默认管理器</param>
        public static void EnableLayerDragDropForMultiple(TextBox[] textBoxes, string tocManagerName = null)
        {
            if (textBoxes == null)
                throw new ArgumentNullException(nameof(textBoxes));

            foreach (TextBox textBox in textBoxes)
            {
                EnableLayerDragDrop(textBox, tocManagerName);
            }
        }

        /// <summary>
        /// 手动获取选中图层的路径并设置到TextBox
        /// </summary>
        /// <param name="textBox">目标TextBox</param>
        /// <param name="tocManagerName">TOC管理器名称，为空则使用默认管理器</param>
        public static void SetSelectedLayerPath(TextBox textBox, string tocManagerName = null)
        {
            if (textBox == null)
                throw new ArgumentNullException(nameof(textBox));

            try
            {
                ITOCManager tocManager = GetTOCManager(tocManagerName);
                if (tocManager == null)
                {
                    MessageBox.Show("未找到指定的TOC管理器", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string layerPath = LayerPathHelper.GetSelectedLayerPath(tocManager);
                if (!string.IsNullOrEmpty(layerPath))
                {
                    textBox.Text = layerPath;
                }
                else
                {
                    MessageBox.Show("请先在TOC控件中选择一个图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取图层路径失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 处理拖拽进入事件
        /// </summary>
        private static void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            // TOC控件拖拽时formats可能为空，直接允许拖拽
            // 在DragDrop事件中通过TOC管理器获取选中图层
            e.Effect = DragDropEffects.Copy;
        }
        
        /// <summary>
        /// 检查是否包含ESRI相关数据
        /// </summary>
        private static bool HasESRIData(IDataObject dataObject)
        {
            string[] formats = dataObject.GetFormats();
            foreach (string format in formats)
            {
                if (format.ToLower().Contains("esri") || 
                    format.ToLower().Contains("layer") ||
                    format.ToLower().Contains("arcgis"))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 处理拖拽放下事件
        /// </summary>
        private static void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                if (textBox == null) return;

                // TOC控件拖拽时formats数组为空，直接使用TOC管理器获取选中图层
                string tocManagerName = textBox.Tag as string;
                ITOCManager tocManager = GetTOCManager(tocManagerName);
                
                if (tocManager?.SelectedLayer != null)
                {
                    // 获取图层路径
                    string layerPath = LayerPathHelper.GetLayerFullPath(tocManager.SelectedLayer);
                    textBox.Text = layerPath ?? "无法获取图层路径";
                }
                else
                {
                    textBox.Text = "请先在TOC中选择图层";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"拖放操作失败: {ex.Message}\n堆栈跟踪: {ex.StackTrace}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 从拖拽数据中提取图层对象
        /// </summary>
        /// <param name="draggedItem">拖拽的项目</param>
        /// <returns>图层对象</returns>
        private static ILayer ExtractLayerFromDragData(object draggedItem)
        {
            try
            {
                MessageBox.Show($"拖拽数据类型: {draggedItem.GetType().FullName}", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 尝试直接转换为图层
                if (draggedItem is ILayer layer)
                {
                    return layer;
                }
                
                // 如果是其他类型，可能需要进一步处理
                // 这里可以根据实际的拖拽数据结构进行调整
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取TOC管理器
        /// </summary>
        /// <param name="managerName">管理器名称</param>
        /// <returns>TOC管理器实例</returns>
        private static ITOCManager GetTOCManager(string managerName)
        {
            if (string.IsNullOrEmpty(managerName))
            {
                return TOCManagerRegistry.GetDefault();
            }
            else
            {
                return TOCManagerRegistry.Get(managerName);
            }
        }

        /// <summary>
        /// 禁用TextBox的拖放功能
        /// </summary>
        /// <param name="textBox">目标TextBox控件</param>
        public static void DisableLayerDragDrop(TextBox textBox)
        {
            if (textBox == null) return;

            textBox.AllowDrop = false;
            textBox.DragEnter -= TextBox_DragEnter;
            textBox.DragDrop -= TextBox_DragDrop;
            textBox.Tag = null;
        }
    }
}
