using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Sunny.UI;
using System.Drawing;
using 质检工具.Func.SysFunc;
using ESRI.ArcGIS.esriSystem;
using AntdUI;
using 质检工具.Func.GISFunc;
using 质检工具.Func.GISFunc.GISMap;
using ESRI.ArcGIS.Controls;

namespace 质检工具
{
    class InitializeFunc
    {
        //检查默认工作控件
        public static void checkDefaultWorkspace()
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string defaultGdbPath = Path.Combine(myDocuments, "ArcGIS", "Default.gdb");
            if (!Directory.Exists(defaultGdbPath))
            {
                GDB_Func.creatGDB(defaultGdbPath);
            }
            globalpara.defaultGdbPath = defaultGdbPath;
        }

        //修改工具资源路径
        public static void toolResourcePath()
        {
           string toolRecPt=  IniFunc.getString("toolResourcePath");
            if (File.Exists(toolRecPt))
            {
                globalpara.toolconfigpath = toolRecPt;
            }
        }
        
        #region//版本检查
        string version = "";
        bool versionchecking = false;
        private System.Windows.Forms.Timer gisTimer;
        //arcgis版本检查
        public void checkversion(Rib form)
        {
            if (gisTimer == null)
            {
                gisTimer = new System.Windows.Forms.Timer();
                gisTimer.Interval = 500; // 0.5秒
                gisTimer.Tick += (s, args) =>
                {
                    if (version == "10.8.1\n")
                    {
                        gisTimer.Stop();
                        form.axTOCControl1.Enabled = true;
                        form.axToolbarControl1.Enabled = true;
                        form.axToolbarControl2.Enabled = true;
                        form.axMapControl1.Enabled = true;
                        versionchecking = false;
                    }
                };
            }
            if (!versionchecking)
            {
                GpFunc gpFunc = new GpFunc();
                Task.Run(() =>
                {
                    version = LogHelper.gpstrspilter(gpFunc.gp_getversion());
                });
                versionchecking = true;
                gisTimer.Start();
            }
        }
       
        #endregion
        // 用于调整图像大小的方法
        public Image ResizeImage(Image image, int width, int height)
        {
            // 创建一个新的 Bitmap 对象，大小为目标宽度和高度
            Bitmap newImage = new Bitmap(width, height);

            // 使用 Graphics 类绘制原始图像，并调整其大小
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }

            return newImage;
        }

      
        /// <summary>
        /// 隐形加载日志
        /// </summary>
        public static void loadmainlog()
        {
            MainLogForm mlf = new MainLogForm();
            //mlf.ShowInTaskbar = false;  // 不在任务栏显示
            mlf.Opacity = 0;  // 设置透明度为 0
            mlf.Show();
            mlf.Hide();
        }


        /// <summary>
        /// 工具箱配置文件
        /// </summary>
        //public static void toolconfig()
        //{
        //    string toolconfigpath =  Properties.Settings.Default.menuconfig;
        //    if (File.Exists(toolconfigpath))
        //    {
        //        globalpara.toolconfigpath = toolconfigpath;
        //        //修改工具描述文件路径
        //        string dscpt = Path.GetDirectoryName(toolconfigpath) + Path.GetFileName(globalpara.tooldescpath);
        //        if (File.Exists(dscpt)) { globalpara.tooldescpath = dscpt; }

        //    }
        //}
        public static void InitializeGIS()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            IAoInitialize aoinitialize = new AoInitialize();
            esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            licenseStatus = aoinitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
            if (licenseStatus == esriLicenseStatus.esriLicenseNotInitialized)
            {
                MessageBox.Show("没有ArcGIS许可！");
                Application.Exit();
            }
            //LogHelper.normallog("GIS功能已启用");
        }

        // ShowForm<AntdUIForm>();//打开 AntdUIForm
        // ShowForm<SettingForm>(); //打开 SettingForm
        /// <summary>
        /// 一个通用的窗体检查和打开方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void ShowForm<T>() where T : Form, new()
        {
            var existingForm = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (existingForm != null)
            {
                existingForm.Show();
                existingForm.Activate();
            }
            else
            {
                var newForm = new T();
                newForm.Show();
            }
        }

        /// <summary>
        /// sunnyui按钮颜色
        /// </summary>
        /// <param name="bt"></param>
        public static void changebuttoncolor(UISymbolButton bt)
        {
            bt.SymbolColor = Color.FromArgb(80, 160, 255);
            bt.RectColor = Color.Transparent;
            bt.ForeColor = Color.FromArgb(80, 160, 255);
            bt.FillColor = Color.Transparent;
            bt.RectHoverColor = Color.Transparent;
            bt.SymbolHoverColor = Color.FromArgb(204, 213, 235);
            bt.RectPressColor = Color.Transparent;
            bt.ForeHoverColor = Color.Gray;
        }
        
    }
}
