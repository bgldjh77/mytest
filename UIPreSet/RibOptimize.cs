using ESRI.ArcGIS.Carto;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using 质检工具.SysFunc;
using System.Drawing;
namespace 质检工具.UIPreSet
{
    internal static class RibOptimize
    {
        /// <summary>
        /// 选择文件夹或gdb并写入配置
        /// </summary>
        public static void SelectAndWritePath(string key, string filter = "", string tip = "请选择文件夹", bool mustEndWithGdb = false)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string path = sf.sf_folder("", tip);
            if (string.IsNullOrEmpty(path) || (mustEndWithGdb && !path.EndsWith(".gdb")))
            {
                //MessageBox.Show($"请选择正确的{(mustEndWithGdb ? "gdb" : "文件夹")}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.Infonotice("设置无效", $"请选择正确的{(mustEndWithGdb ? "gdb" : "文件夹")}");
                return;
            }
            IniFunc.writeString(key, path);
        }

        /// <summary>
        /// 根据按钮文本查找并打开文件
        /// </summary>
        public static void OpenFileByButtonText(string buttonText, string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                if (filename.StartsWith(buttonText))
                {
                    System.Diagnostics.Process.Start(file);
                }
            }
        }

        /// <summary>
        /// 复制图层
        /// </summary>
        public static void CopyLayer(ILayer pLayer, ESRI.ArcGIS.Controls.AxMapControl axMapControl1)
        {
            if (pLayer is IFeatureLayer featureLayer)
            {
                IFeatureLayer clonedLayer = CloneFeatureLayer(featureLayer);
                if (clonedLayer != null)
                {
                    axMapControl1.Map.AddLayer(clonedLayer);
                    axMapControl1.Refresh();
                    MessageBox.Show("图层已复制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("复制失败，图层类型不支持或为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请选择可复制的矢量图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 复制FeatureLayer
        /// </summary>
        public static IFeatureLayer CloneFeatureLayer(IFeatureLayer sourceLayer)
        {
            if (sourceLayer == null)
                return null;
            IFeatureLayer clonedLayer = null;
            try
            {
                clonedLayer = new FeatureLayerClass();
                clonedLayer.FeatureClass = sourceLayer.FeatureClass;
                clonedLayer.Name = sourceLayer.Name;
                clonedLayer.Visible = sourceLayer.Visible;
                clonedLayer.MinimumScale = sourceLayer.MinimumScale;
                clonedLayer.MaximumScale = sourceLayer.MaximumScale;
                if (sourceLayer is IFeatureLayerDefinition sourceLayerDef)
                {
                    IFeatureLayerDefinition clonedLayerDef = (IFeatureLayerDefinition)clonedLayer;
                    clonedLayerDef.DefinitionExpression = sourceLayerDef.DefinitionExpression;
                }
                return clonedLayer;
            }
            catch
            {
                if (clonedLayer != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(clonedLayer);
                }
                throw;
            }
        }

        /// <summary>
        /// 移除图层
        /// </summary>
        public static void RemoveLayer(ILayer pLayer, ESRI.ArcGIS.Controls.AxMapControl axMapControl1)
        {
            if (axMapControl1.LayerCount > 0 && pLayer != null)
            {
                DialogResult result = MessageBox.Show("是否确认移除“" + pLayer.Name + "”？", "确认", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    axMapControl1.Map.DeleteLayer(pLayer);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        /// <summary>
        /// 通用窗体打开
        /// </summary>
        public static void OpenForm<T>() where T : Form, new()
        {
            T form = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (form == null)
            {
                form = new T();
                form.Show();
            }
            else
            {
                // 如果窗体被最小化，则还原
                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }
                // Bring to front并聚焦
                form.Activate();
                form.TopMost = true;
                form.TopMost = false;
            }
        }
        /// <summary>
        /// 刷新主题色并更新所有图标，同时更改页面头部背景色
        /// </summary>
        public static void SetThemeColors(
            Rib ribInstance,
            string[] newColors,
            string headerColorHex,
            string importantButtonHex = null,
            string standardHex = null,
            string fontDownloadHex = null)
        {
            ribInstance.SvgColors = newColors;
            ribInstance.UpdateRibbonIcons(newColors);

            // 主题色
            if (!string.IsNullOrEmpty(headerColorHex))
            {
                Color headerColor = UIPreSet.SvgHelper.HexToColor(headerColorHex);
                ThemeColor.PrimaryTheme = headerColor;

                if (ribInstance.pageHeader1 != null)
                    ribInstance.pageHeader1.BackColor = headerColor;
                if (ribInstance.pageHeader3 != null)
                    ribInstance.pageHeader3.BackColor = headerColor;
            }

            if (!string.IsNullOrEmpty(importantButtonHex))
                ThemeColor.ImportantButton = UIPreSet.SvgHelper.HexToColor(importantButtonHex);

            if (!string.IsNullOrEmpty(standardHex))
                ThemeColor.Standard = UIPreSet.SvgHelper.HexToColor(standardHex);

            if (!string.IsNullOrEmpty(fontDownloadHex))
                ThemeColor.FontDownload = UIPreSet.SvgHelper.HexToColor(fontDownloadHex);

            ribInstance.Refresh();
        }
    }
}