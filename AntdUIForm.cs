using AntdUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.GISFunc;
using 质检工具.Func.SysFunc;
using 质检工具.Menu;
using 质检工具.MenuConfig;
using 质检工具.SysFunc;
using AntdUI.Svg;
using 质检工具.GISFunc;
using 质检工具.Func;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using 质检工具.Func.GISFunc.toolbar;
using 质检工具.Func.SaveProject;
using 质检工具.Func.LicenseFunc;

namespace 质检工具
{
    public partial class AntdUIForm : AntdUI.Window
    {
        FirstLevelMenuFunc firstLevelMenuFunc;

        public MenuSearchFunc2 menuSearch;

        public AntdUIForm()
        {
            //InitializeFunc.InitializeGIS();
            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            //IAoInitialize aoinitialize = new AoInitialize();
            //esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            //licenseStatus = aoinitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
            //if (licenseStatus == esriLicenseStatus.esriLicenseNotInitialized)
            //{
            //    MessageBox.Show("没有ArcInfo许可！");
            //    Application.Exit();
            //}

            InitializeComponent();
        }
        

        private void AntdUIForm_Load(object sender, EventArgs e)
        {
            uionload();
            firstLevelMenuFunc = new FirstLevelMenuFunc();

            loadfirstmenu();//加载一级菜单
            InitializeFunc ilf = new InitializeFunc();
            //ilf.mainformload(this);   //状态初始化                  
            AddSaveButtonToToolbar();//arcgis toolbar控件添加
            //ilf.checkversion(this);//arcgis版本验证 启用和禁用地图功能
            this.PerformLayout();
            lincense_run();//验证证书
        }

        
        
        /// <summary>
        /// 最大化窗体\ui布局颜色
        /// </summary>
        private void uionload()
        {

            this.WindowState = FormWindowState.Maximized;//最大化
            //munesplitter.SplitterDistance = 250;
            munesplitter.SplitterSize = 0;

            InitializeFunc.changebuttoncolor(mainlog_btn);
            InitializeFunc.changebuttoncolor(mu_download_btn);
            InitializeFunc.changebuttoncolor(tasklist_btn);
            InitializeFunc.changebuttoncolor(resultdb);
            InitializeFunc.changebuttoncolor(mufolder);
            InitializeFunc.changebuttoncolor(outdb);
            InitializeFunc.changebuttoncolor(outfolder);
            
            munesplitter.SplitterDistance = 320;
            //AntdUI.Config.TextRenderingHighQuality = true;
            AntdUI.Config.ShadowEnabled = true;
            Refresh();
        }
        #region//菜单
        //切换一级菜单
        private bool segmented1_SelectIndexChanging(object sender, IntEventArgs e)
        {
            if (segmented1.Items[e.Value].Text == "标准查阅")
            {
                InitializeFunc.ShowForm<BZCY_Form>();
            }
            else { firstLevelMenuFunc.loadsubmenu(menu1, segmented1.Items[e.Value].Text, tree1); }
            return true;
        }

        //选择工具
        private void tree1_SelectChanged(object sender, TreeSelectEventArgs e)
        {
            tree1.SelectItem.Expand = !tree1.SelectItem.Expand;
            if (tree1.SelectItem.Sub.Count == 0)
            {
                MenuBean selectbean = (MenuBean)e.Item.Tag;

                if (String.IsNullOrWhiteSpace(selectbean.folderName))
                {
                    return;
                }
                ToolForm form = Application.OpenForms.OfType<ToolForm>().FirstOrDefault();
                if (form == null)
                {
                    form = new ToolForm(selectbean);
                    form.Show();
                }
                else
                {
                    form = new ToolForm(selectbean);
                    form.Show();
                    //DialogResult result = MessageBox.Show("是否关闭上一个工具窗体？", "提示",
                    //    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    //if (result == DialogResult.OK)
                    //{
                    //    form.Close();
                    //    form = new ToolForm(selectbean);
                    //    form.Show();
                    //}
                    //else
                    //{
                    //    form = new ToolForm(selectbean);
                    //    form.Show();
                    //}
                }
            }
        }
        //选择工具(弃用)
        private void menu1_SelectChanged(object sender, MenuSelectEventArgs e)
        {
            //if (globalpara.toolnamelist.Contains(e.Value.Text))
            //{

            MenuBean selectbean = (MenuBean)e.Value.Tag;

            if (String.IsNullOrWhiteSpace(selectbean.folderName))
            {
                return;
            }
            ToolForm form = Application.OpenForms.OfType<ToolForm>().FirstOrDefault();
            if (form == null)
            {
                form = new ToolForm(selectbean);
                form.Show();
            }
            else
            {
                form = new ToolForm(selectbean);
                form.Show();
                //DialogResult result = MessageBox.Show("是否关闭上一个工具窗体？", "提示",
                //    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                //if (result == DialogResult.OK)
                //{
                //    form.Close();
                //    form = new ToolForm(selectbean);
                //    form.Show();
                //}
                //else
                //{
                //    form = new ToolForm(selectbean);
                //    form.Show();
                //}
            }
            //}
        }
        private FormWindowState _previousWindowState; // 用于保存上一次的窗体状态
        //次级菜单栏宽度
        private void AntdUIForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != _previousWindowState)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    Console.WriteLine("窗体已最大化");
                    //菜单栏                    
                    munesplitter.SplitterDistance = 320;
                }
                else if (_previousWindowState == FormWindowState.Maximized && this.WindowState == FormWindowState.Normal)
                {
                    Console.WriteLine("窗体已取消最大化");
                    //菜单栏
                    munesplitter.SplitterDistance = 320;
                }

                // 更新上一次的窗体状态
                _previousWindowState = this.WindowState;
            }
            //主页图片
            int width = panel1.Width;
            carousel1.Width = width;
            carousel1.Height = Convert.ToInt32(width / 16 * 9);
            int posy = Convert.ToInt32((panel1.Height / 2) - (width / 16 * 9 * 0.5));
            carousel1.Location = new Point(0, posy);
        }
        /// <summary>
        /// 加载工具一级菜单目录
        /// </summary>
        public void loadfirstmenu()
        {
            firstLevelMenuFunc.loadfirstmenu(segmented1);
            menuSearch = new MenuSearchFunc2(tree1, menuSearchInput);//绑定搜索
            firstLevelMenuFunc.loadsubmenu(menu1, segmented1.Items[0].Text, tree1);//加载第一个一级菜单
        }
        #endregion
        #region//搜索框
        //搜索框输入事件
        private void menuSearchInput_TextChanged(object sender, EventArgs e)
        {
            menuSearch.onTextChanged(sender, e);
        }
        //回车
        private void menuSearchInput_KeyDown(object sender, KeyEventArgs e)
        {
            // 检测是否按下了回车键
            if (e.KeyCode == Keys.Enter)
            {
                // 调用公共逻辑
                menuSearchInput_TextChanged(sender, e);

                // 防止回车键触发其他默认行为（如按钮点击）
                //e.SuppressKeyPress = true;
            }
        }
        #endregion
        #region //主页功能
        
        #region//参数默认值设置

        //成果数据库
        private void resultdb_Click(object sender, EventArgs e)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string resultdb = sf.sf_folder("", "请选择gdb");
            if (resultdb == null || !resultdb.EndsWith(".gdb"))
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "提示", "请选择正确目录", AntdUI.TType.Warn));
                return;
            }
            IniFunc.writeString("resultdb", resultdb);
        }
        //模板文件夹
        private void mufolder_Click(object sender, EventArgs e)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string mufolder = sf.sf_folder("");
            IniFunc.writeString("mufolder", mufolder);
        }
        //输出位置(gdb)
        private void outdb_Click(object sender, EventArgs e)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string outdb = sf.sf_folder("", "请选择gdb");
            if (outdb == null || !outdb.EndsWith(".gdb"))
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "提示", "请选择正确目录", AntdUI.TType.Warn));
                return;
            }
            IniFunc.writeString("outdb", outdb);
        }
        //输出位置(文件夹)
        private void outfolder_Click(object sender, EventArgs e)
        {
            SelectFileFunc sf = new SelectFileFunc();
            string outfolder = sf.sf_folder("");
            IniFunc.writeString("outfolder", outfolder);
        }

        #endregion

        //帮助文档
        private void openHelpDoc_btn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(globalpara.helpdocument);
        }
        //打开模板表格
        private void label_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Label clickedLabel = sender as System.Windows.Forms.Label;

            if (clickedLabel == null) return;

            // 获取 Label 的文本
            string labelText = clickedLabel.Text;
            string[] files = Directory.GetFiles(globalpara.mu_rulepath);
            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                if (filename.StartsWith(labelText))
                {
                    System.Diagnostics.Process.Start(file);
                }
            }
        }
        //取消聚焦
        private void Cancelfocus(object sender, EventArgs e)
        {
            pageHeader1.Focus();
        }
        //切换tab页
        private void tabs1_SelectedIndexChanged(object sender, IntEventArgs e)
        {
            IniFunc.writeString("MainMapTab",tabs1.SelectedIndex.ToString());
        }
        public void tabs1_selectcache()
        {
            string str = IniFunc.getString("MainMapTab");
            if (str != "") { tabs1.SelectedIndex = Convert.ToInt32(str); }
        }
        #endregion
        #region//主页引导
        AntdUI.TourForm tourForm;
        private void openTour_btn_Click(object sender, EventArgs e)
        {
            OpenTour(setting_btn);
        }
        void OpenTour(AntdUI.Button btn)
        {
            List<string> tourname = new List<string> { "设置", "一级菜单", "工具菜单", "搜索", "介绍", "参数设置", "规则配置", "页面切换" };
            List<string> tourdesc = new List<string> { "可更改软件工具读取路径","点击切换一级工具菜单","点击工具集展开工具，点击工具进入工具窗口","根据关键字匹配工具",
            "工具介绍","工具默认值参数设置","模板文件设置","切换为地图视图"};
            if (tourForm == null)
            {
                Form popover = null;
                tourForm = AntdUI.Tour.open(new AntdUI.Tour.Config(this, item =>
                {
                    switch (item.Index)
                    {
                        case 0:
                            item.Set(btn);
                            break;
                        case 1:
                            item.Set(segmented1);
                            break;
                        case 2:
                            item.Set(menu1);
                            break;
                        case 3:
                            item.Set(menuSearchInput);
                            break;
                        case 4:
                            item.Set(pageHeader1);
                            break;
                        case 5:
                            item.Set(cspz_uiPanel);
                            break;
                        case 6:
                            item.Set(gzpz_uiPanel);
                            break;
                        case 7:
                            item.Set(label2);
                            break;
                        //case 8:
                        //    var rect = form.ClientRectangle;
                        //item.Set(new Rectangle(rect.X + (rect.Width - 200) / 2, rect.Y + (rect.Height - 200) / 2, 200, 200));
                        //break;
                        default:
                            item.Close();
                            tourForm = null;
                            break;
                    }
                }, info =>
                {
                    popover?.Close();
                    popover = null;
                    if (info.Rect.HasValue)
                    {
                        if (tourname.Count >= info.Index)
                        {
                            var tourpos = TAlign.Bottom;
                            if (info.Index == 2)
                            {
                                tourpos = TAlign.Right;
                            }
                            else if (info.Index == 5 | info.Index == 6)
                            {
                                tourpos = TAlign.Left;
                            }
                            else if (info.Index == 7)
                            {
                                tourpos = TAlign.Top;
                            }
                            popover = AntdUI.Popover.open(new AntdUI.Popover.Config(info.Form, new TourPopover(info, tourname[info.Index], tourdesc[info.Index], (info.Index + 1), 8)) { Offset = info.Rect.Value, Focus = false, ArrowAlign = tourpos });
                        }
                        //if (info.Index == 0)
                        //{
                        //    popover = AntdUI.Popover.open(new AntdUI.Popover.Config(info.Form, new TourPopover(info, "设置", "设置软件工具读取路径", (info.Index + 1), 9)) { Offset = info.Rect.Value, Focus = false });
                        //}
                        //popover = AntdUI.Popover.open(new AntdUI.Popover.Config(info.Form, new TourPopover(info, info.Index == 8 ? "DIV Rectangle" : "Button " + (info.Index + 1), "Tour Step " + (info.Index + 1), (info.Index + 1), 9)) { Offset = info.Rect.Value, Focus = false });
                    }
                }));
            }
            else tourForm.Next();
        }
        #endregion
        #region//软件功能
        //保存软件工程
        private void save_btn_Click(object sender, EventArgs e)
        {
            AntdUI.Popover.open(new AntdUI.Popover.Config(save_btn, new popover_saveProject(this) { Size = new Size(245, 55) }) { });
        }
        //软件信息
        private void info_btn_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<UpdateInfoForm>();
        }
        //设置
        private void setting_btn_Click_1(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<SettingForm>();
        }
        //日志
        private void mainlog_btn_Click(object sender, EventArgs e)
        {
            MainLogForm mlf = System.Windows.Forms.Application.OpenForms.OfType<MainLogForm>().FirstOrDefault();
            if (mlf != null)
            {
                mlf.Opacity = 1;
                mlf.ShowInTaskbar = true;
                mlf.Visible = true;
                mlf.Focus();
            }
        }
        //模板下载
        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<MuDownloadForm>();
        }
        //任务管理
        private void tasklist_btn_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<TaskListForm>();
            TaskListForm tf = Application.OpenForms.OfType<TaskListForm>().FirstOrDefault();
            tf.LoadPageData();
        }
        #endregion
        #region//地图
        //添加工具栏工具
        private void AddSaveButtonToToolbar()
        {
            ICommand command = new Save2MXD(axMapControl1);
            axToolbarControl1.AddItem(command, 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
        }
        //地图控件
        private void axToolbarControl1_OnItemClick(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnItemClickEvent e)
        {
            // 获取点击的工具索引
            int itemIndex = e.index;
            // 根据工具索引判断用户选择的是哪个工具
            if (itemIndex == 1)//保存地图
            {

            }
        }     
        //移除图层
        private void removelayer_Click(object sender, EventArgs e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                if (pLayer != null)
                {
                    DialogResult result = MessageBox.Show("是否确认移除“" + pLayer.Name + "”？", "确认", MessageBoxButtons.OKCancel);

                    // 根据用户的响应进行处理
                    if (result == DialogResult.OK)
                    {                        
                        axMapControl1.Map.DeleteLayer(pLayer);
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                    }
                }
            }
        }
        ILayer pSelectLayer;//左键
        ILayer pLayer;//右键
        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer pLayer2 = null;

            //图层拖动事件的参数
            IBasicMap pBasMap = null;

            //图层符号化的参数
            object unk = null;
            object data = null;
            //左击事件
            if (e.button == 1)
            {
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pBasMap, ref pLayer2, ref unk, ref data);
                ////图层拖动事件
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (pLayer2 is IAnnotationSublayer) return;//如果是注记图层则返回
                    else
                    {
                        pSelectLayer = pLayer2;
                        //pSelectLayer=(ILayer)clonefc(pLayer2 as IFeatureLayer);
                    }

                }
                if (pSelectLayer != null)
                {
                    if (pSelectLayer.Name != null)
                    {
                        // uiLabel1.Text = pSelectLayer.Name;
                    }
                }

                //图层符号化
                //if (pItem == esriTOCControlItem.esriTOCControlItemLegendClass)
                //{
                //    if (pLayer2 is IFeatureLayer)
                //    {
                //        IFeatureLayer pFeatureLayer = pLayer2 as IFeatureLayer;
                //        if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                //        {
                //            ILegendGroup pLegendGroup = unk as ILegendGroup;
                //            ILegendClass pLegendClass = pLegendGroup.get_Class((int)data);
                //            frmMarkerSymbol frm = new frmMarkerSymbol(axMapControl1.Object as IMapControlDefault, pLayer2, pLegendClass);
                //            frm.ShowDialog();
                //        }
                //        else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                //        {
                //            ILegendGroup pLegendGroup = unk as ILegendGroup;
                //            ILegendClass pLegendClass = pLegendGroup.get_Class((int)data);
                //            frmLineSymbol frm = new frmLineSymbol(axMapControl1.Object as IMapControlDefault, pLayer2, pLegendClass);
                //            frm.ShowDialog();
                //        }
                //        else if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                //        {
                //            ILegendGroup pLegendGroup = unk as ILegendGroup;
                //            ILegendClass pLegendClass = pLegendGroup.get_Class((int)data);
                //            frmFillSymbol frm = new frmFillSymbol(axMapControl1.Object as IMapControlDefault, pLayer2, pLegendClass);
                //            frm.ShowDialog();
                //        }
                //    }
                //}

            }
            //右击事件
            else if (e.button == 2)
            {
                esriTOCControlItem item = new esriTOCControlItem();
                IBasicMap map = new Map() as IBasicMap;
                object o = new object();
                ILayer pLayer3 = null;
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref pLayer3, ref o, ref o);

                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    maplayermenu.Show(axTOCControl1, e.x, e.y);
                }
                else if (item == esriTOCControlItem.esriTOCControlItemMap)
                {
                    //addmapmenu.Show(axTOCControl1, e.x, e.y);

                }
                pLayer = pLayer3;


            }
        }
        //打开属性表
        private void opentable_Click(object sender, EventArgs e)
        {
            ILayer clonelayer= (ILayer)clonefc(pLayer as IFeatureLayer);
            TableForm tableform = new TableForm(clonelayer);
            tableform.Show();
        }
        //复制图层
        private IFeatureLayer clonefc(IFeatureLayer sourceLayer)
        {

            if (sourceLayer == null)
                return null;

            IFeatureLayer clonedLayer = null;

            try
            {
                clonedLayer = new FeatureLayerClass();

                // 复制 FeatureClass
                clonedLayer.FeatureClass = sourceLayer.FeatureClass;

                // 复制图层名称和其他基本属性
                clonedLayer.Name = sourceLayer.Name;
                clonedLayer.Visible = sourceLayer.Visible;
                clonedLayer.MinimumScale = sourceLayer.MinimumScale;
                clonedLayer.MaximumScale = sourceLayer.MaximumScale;

                // 如果原图层有定义查询，也复制它
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
        #endregion

        //窗体关闭事件
        private void AntdUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                
                LoginForm loginForm = System.Windows.Forms.Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
                if (loginForm == null) return;
                loginForm.Close();

                //中断gp工具


                //cmdFunc cf = new cmdFunc();
                //string appname = AppDomain.CurrentDomain.FriendlyName;
                //cf.usecmd("taskkill /F /IM " + appname, out string print);
                //mf1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
       
        private void lincense_run()
        {
            if (!AuthManger.Verify())
            {
                LicenseForm af = new LicenseForm();
                this.Enabled = false;

                af.Show();
                af.TopMost = true;
            }

        }
        private async void uiButton2_Click(object sender, EventArgs e)//测试按钮
        {
            //生成工具帮助xml
            string csvPath = globalpara.toolconfigpath;
            List<string> toolpathlist = new List<string>();
            GpFunc gp = new GpFunc();
            var lines = File.ReadAllLines(csvPath, Encoding.GetEncoding("GB2312")).Skip(1);
            foreach (var line in lines)
            {
                var values = line.Split(',');
                string toolpath = Path.Combine(Path.GetDirectoryName(globalpara.toolconfigpath),
                    values[0], values[1] + ".tbx", values[5]);
                if (!toolpathlist.Contains(toolpath)) { toolpathlist.Add(toolpath); }
            }
            foreach (string toolapth in toolpathlist)
            {
                Console.WriteLine(toolapth);
                List<string> ls = new List<string>();
                ls.Add(toolapth);
                ls.Add(globalpara.translate_xml);
                ls.Add(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(toolapth)), Path.GetFileNameWithoutExtension(toolapth) + ".xml"));
                Task gpTask = Task.Run(() =>
                {
                    gp.gp_official("ESRITranslator", ls);
                });
                await gpTask;            
            }


        }
    }
}
