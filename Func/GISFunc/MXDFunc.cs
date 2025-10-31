using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace 质检工具.GISFunc
{
    class MXDFunc
    {
        //保存软件工程时候使用
        public void SaveMapToMXD_noskip()
        {
            try
            {
                //int layerCount = axMapControl1.LayerCount;
                //if (layerCount == 0) { return; }
                 Rib  mainform = Application.OpenForms.OfType<Rib>().FirstOrDefault();
                if (mainform == null) { return; }
                AxMapControl axMapControl1 = mainform.axMapControl1;
                string mxdPath = globalpara.savemxdpath;
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.New(mxdPath);
                mapDoc.ReplaceContents(axMapControl1.Map as IMxdContents);
                mapDoc.Save(mapDoc.UsesRelativePaths, true);
                mapDoc.Close();
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("保存地图文档失败", ex);
            }
        }
        public void SaveMapToMXD(AxMapControl axMapControl1)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Map Document (*.mxd)|*.mxd";
                saveFileDialog.DefaultExt = "mxd";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string mxdPath = saveFileDialog.FileName;
                    IMapDocument mapDoc = new MapDocumentClass();
                    mapDoc.New(mxdPath);
                    mapDoc.ReplaceContents(axMapControl1.Map as IMxdContents);
                    mapDoc.Save(mapDoc.UsesRelativePaths, true);
                    mapDoc.Close();
                    //MessageBox.Show("地图文档保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("地图文档保存成功！是否打开该文档？", "提示",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(mxdPath);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"保存地图文档失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.ErrorLog("保存地图文档失败",ex);
            }
        }
        public void addformMXD(string mxdpath)
        {
            if (File.Exists(mxdpath))
            {
                Rib mainform = Application.OpenForms.OfType<Rib>().FirstOrDefault();
                if (mainform == null) { return; }
                //检查地图文档有效性
                if (mainform.axMapControl1.CheckMxFile(mxdpath))
                {
                    //**************加载文件************
                    //axMapControl.LoadMxFile(datapath);
                    mainform.axMapControl1.ClearLayers();
                    add_data_from_mxd(mainform.axMapControl1, mxdpath);
                }
                else
                {
                    //MessageBox.Show(mxdpath + "是无效的地图文档!", "信息提示");
                    LogHelper.normallog(mxdpath + "是无效的地图文档!");
                    return;
                }
            }
        }
        //从mxd追加地图要素
        public void add_data_from_mxd(AxMapControl axMapControl1, string mxdpath)
        {
            IMapControl2 mapControl = (IMapControl2)axMapControl1.Object;
            IMapControl3 mapControl3 = (IMapControl3)axMapControl1.Object;

            IMapDocument mapDoc = new MapDocument();
            mapDoc.Open(mxdpath, string.Empty);

            // 获取 MXD 中的第一个地图
            IMap map = mapDoc.get_Map(0);

            // 将 MXD 中的图层逐一添加到现有地图中
            for (int i = 0; i < map.LayerCount; i++)
            {
                mapControl.AddLayer(map.get_Layer(i));
            }

            mapControl3.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
    }
}
