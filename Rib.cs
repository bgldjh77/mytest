
using AntdUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZJZX_ZJAddin.JTGJ;
using 质检工具.Func;
using 质检工具.Func.GISFunc;
using 质检工具.Func.GISFunc.GISMap;
using 质检工具.Func.GISFunc.toolbar;
using 质检工具.Func.SaveProject;
using 质检工具.GISFunc;
using 质检工具.GpTask;
using 质检工具.myForm;
using 质检工具.myForm.Plug;
using 质检工具.SysFunc;
using 质检工具.UIPreSet;

namespace 质检工具
{


    public partial class Rib : AntdUI.Window
    {
        private FirstLevelMenuFunc firstLevelMenuFunc;// 一级菜单功能管理器
        public string[] SvgColors = { "#85bf88", "#66ccff", "#c9f7ca", "#e1ffff" };//初始主题色
        //蓝色"#85bf88", "#66ccff", "#c9f7ca", "#e1ffff"
        //紫色"#85bf88", "#905ebc", "#c9f7ca", "#E7DDFF"
        //黑色"#85bf88", "#808080", "#c9f7ca", "#d3d3d3"
        //绿色"#85bf88", "#85bf88", "#c9f7ca", "#c9f7ca"
        //黄色"#85bf88", "#b99d2b", "#c9f7ca", "#ffe990"
        //初版色"#85bf88", "#4d4dff", "#c9f7ca", "#d3deff"
        private 质检工具.Menu.MenuSearchFunc2 menuSearchFunc2;// 菜单搜索功能
        private ILayer pSelectLayer; // 左键选中的图层
        private ILayer pLayer;       // 右键选中的图层
        public string LayerPath;//选中的图层路径
        private System.Windows.Forms.ContextMenuStrip maplayermenu;// 地图图层右键菜单
        public ESRI.ArcGIS.Controls.AxMapControl axMapControl1;// ArcGIS地图控件
        public ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;// ArcGIS目录控件
        public ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;// ArcGIS工具栏控件
        public ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl2;// ArcGIS工具栏控件
        private bool isArcGisInitialized = false;//判断初始化是否完成
        private ToolTip panel2ToolTip; // panel2的悬浮提示

        AntdUI.TourForm tourForm;//引导教程
        public Rib()
        {
            InitializeComponent();
        }
        //窗体载入事件
        private void Rib_Load(object sender, EventArgs e)
        {
            //string svgIconColor = "6b9a9f";//已弃用

            InitializeFunc.toolResourcePath();//工具资源路径
            this.Resize += Rib_Resize;

            // 定义悬浮按钮，设置颜色
            var btns = new AntdUI.FloatButton.ConfigBtn[]
            {
                 new AntdUI.FloatButton.ConfigBtn("open", "切换"){
                    IconSvg = 质检工具.UIPreSet.SvgHelper.LoadSvgString("切换", true, "#85bf88")
                 }
            };

            // 配置悬浮按钮，设置更大的尺寸
            var config = new AntdUI.FloatButton.Config(this, btns, btn =>
            {
                if (tabControl1.SelectedIndex == 0)
                    tabControl1.SelectedIndex = 1;
                else
                    tabControl1.SelectedIndex = 0;

            })
            {
                Control = tabControl1,      // 指定停靠的控件
                Align = TAlign.BL,     // 右下角（可选：TL, TR, BL, BR, Center等）
                MarginX = 16,          // 距离控件右侧的距离
                MarginY = 16,           // 距离控件底部的距离
                Size = 50 // 按钮大小
            };
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);

            // 打开悬浮按钮
            var floatBtn = AntdUI.FloatButton.open(config);
            
            //动态设置图标
            UpdateRibbonIcons(SvgColors);

            // 设置窗体大小和状态
            this.WindowState = FormWindowState.Maximized;

            // 加载菜单
            firstLevelMenuFunc = new FirstLevelMenuFunc();

            changeTextSize((float)8);
            // 初始化tree1为第一个菜单（如“通用检查”）
            LoadTreeMenu("通用检查");
            arcGisInitButton.FlatStyle = FlatStyle.Flat;
            arcGisInitButton.FlatAppearance.BorderSize = 0;
            arcGisInitButton.Image = 质检工具.UIPreSet.SvgHelper.LoadSvgFromResources("地球", 50, 50, null, 0, 10, 0.5f);//arcgis控件初始化按钮
            // 绑定按钮事件
            ribbonButtonGeneralCheck.Click += (s, ev) => LoadTreeMenu("通用检查");
            ribbonButtonProjectCheck.Click += (s, ev) => LoadTreeMenu("项目检查");
            ribbonButtonDataProcessing.Click += (s, ev) => LoadTreeMenu("数据处理与评分统计");
            ribbonButtonDatabaseExport.Click += ribbonButton4_Click;// 数据库输出
            ribbonButtonFolderExport.Click += ribbonButton8_Click;// 文件夹输出
            ribbonButtonTemplateDownload.Click += ribbonButton14_Click;// 模板下载
            ribbonButtonTaskList.Click += ribbonButton15_Click;// 任务列表
            ribbonButtonRunLog.Click += ribbonButton16_Click;// 运行日志
            ribbonButtonStandardReference.Click += ribbonButton23_Click;// 标准查阅
            ribbonButtonSoftwareInfo.Click += ribbonButton24_Click;// 软件信息
            ribbonButtonSettings.Click += ribbonButton25_Click;// 设置
            ribbonButtonResultDatabase.Click += ribbonButton5_Click;// 成果数据库
            ribbonButtonTemplateFolder.Click += ribbonButton6_Click; // 模板文件夹
            ribbonButtonDatabaseStructureTemplate.Click += ribbonButton2_Click;//数据库结构检查规则模板
            ribbonButtonTopologyCheck.Click += ribbonButton3_Click; // 拓扑检查模板
            ribbonButtonDatabaseCreateTemplate.Click += ribbonButton13_Click; // 数据库建立模板
            ribbonButtonDictionaryAssignment.Click += ribbonButton12_Click; // 赋值词典
            ribbonButtonHelpDoc.Click += ribbonButton18_Click; // 帮助文档
            ribbonButtonOpenProject.Click += ribbonButton7_Click;// 打开工程
            ribbonButtonSaveProject.Click += settingsButton_Click;// 保存工程
            ribbonButtonTourGuide.Click += ribbonButton17_Click;// 引导教程
            ribbonButtonStatistics.Click += ribbonButton19_Click;//数据统计
            JTGJbutton.Click+= JTGJbutton_Click;//截图工具
            this.FormClosing += Rib_FormClosing;//确保关闭
            this.StartPosition = FormStartPosition.CenterScreen;
            // 延迟调用resize_picPanel，确保控件完全初始化
            this.BeginInvoke(new Action(() => {
                resize_picPanel();
            }));

            menuSearchFunc2 = new 质检工具.Menu.MenuSearchFunc2(tree1, menuSearchInput);// 菜单搜索功能初始化
            menuSearchInput.TextChanged += menuSearchFunc2.onTextChanged;// 绑定搜索框文本变化事件
            tabControl1.SelectedIndex = 1;// 默认打开为广告页
            InitializeFunc.loadmainlog();//加载日志
            InitializeFunc.checkDefaultWorkspace();//检查默认工作空间
            
            质检工具.Func.TaskFunc.DefaultTaskInitializer.InitializeAllDefaultTasks();//任务初始化

        }
        //窗体右上角关闭事件
        private void Rib_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 关闭登录窗体
                LoginForm loginForm = System.Windows.Forms.Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
                if (loginForm != null)
                {
                    loginForm.Close();
                }

                // 中断gp工具（可根据实际需要补充）
                // cmdFunc cf = new cmdFunc();
                // string appname = AppDomain.CurrentDomain.FriendlyName;
                // cf.usecmd("taskkill /F /IM " + appname, out string print);
                // mf1.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #region//gis控件
        //动态加载arcgis控件
        private void InitializeArcGisControls()
        {
            if (axMapControl1 == null)
            {
                axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
                ((System.ComponentModel.ISupportInitialize)(axMapControl1)).BeginInit();
                axMapControl1.Name = "axMapControl1";
                axMapControl1.Location = new System.Drawing.Point(0, 0);
                axMapControl1.Size = new System.Drawing.Size(800, 600);
                axMapControl1.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(axMapControl1);
                ((System.ComponentModel.ISupportInitialize)(axMapControl1)).EndInit();
                axMapControl1.BringToFront();
            }
            if (axTOCControl1 == null)
            {
                axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
                ((System.ComponentModel.ISupportInitialize)(axTOCControl1)).BeginInit();
                axTOCControl1.Name = "axTOCControl1";
                axTOCControl1.Location = new System.Drawing.Point(0, 0);
                axTOCControl1.Size = new System.Drawing.Size(200, 600);
                axTOCControl1.Dock = DockStyle.Fill;
                axTOCControl1.AllowDrop = true;
                splitContainer1.Panel1.Controls.Add(axTOCControl1);
                ((System.ComponentModel.ISupportInitialize)(axTOCControl1)).EndInit();
                axTOCControl1.BringToFront();
            }
            if (axToolbarControl2 == null)
            {
                axToolbarControl2 = new ESRI.ArcGIS.Controls.AxToolbarControl();
                ((System.ComponentModel.ISupportInitialize)(axToolbarControl2)).BeginInit();
                axToolbarControl2.Name = "axToolbarControl2";
                axToolbarControl2.Dock = DockStyle.None; // 取消自动停靠
                axToolbarControl2.Size = new System.Drawing.Size(274, 28);
                axToolbarControl2.Location = new System.Drawing.Point(0, panel2.Height/2-28); // 距离panel2顶端20像素
                panel2.Controls.Add(axToolbarControl2);
                ((System.ComponentModel.ISupportInitialize)(axToolbarControl2)).EndInit();
                axToolbarControl2.BringToFront();
                axToolbarControl2.OnMouseDown += axToolbarControl2_OnMouseDown;
                axToolbarControl2.OnItemClick += axToolbarControl2_OnItemClick;
            }
            if (axToolbarControl1 == null)
            {
                axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
                ((System.ComponentModel.ISupportInitialize)(axToolbarControl1)).BeginInit();
                axToolbarControl1.Name = "axToolbarControl1";
                axToolbarControl1.Dock = DockStyle.None; // 取消自动停靠
                axToolbarControl1.Size = new System.Drawing.Size(274, 28);

                // 距离底端10像素
                int bottomMargin = 10;
                int y = panel2.Height - axToolbarControl1.Height - bottomMargin;
                axToolbarControl1.Location = new System.Drawing.Point(0, panel2.Height / 2 );

                panel2.Controls.Add(axToolbarControl1);
                ((System.ComponentModel.ISupportInitialize)(axToolbarControl1)).EndInit();
                axToolbarControl1.BringToFront();
                axToolbarControl1.OnMouseDown += axToolbarControl1_OnMouseDown;
                axToolbarControl1.OnItemClick += axToolbarControl1_OnItemClick;
            }
            axToolbarControl2.SetBuddyControl(axMapControl1);
            axToolbarControl1.SetBuddyControl(axMapControl1);
            axTOCControl1.SetBuddyControl(axMapControl1);

            // 绑定地图相关事件
            axTOCControl1.OnMouseDown += axTOCControl1_OnMouseDown;
            AddMapInquiryToToolbar();
            // 添加保存按钮等
            AddSaveButtonToToolbar();
            // 绑定事件、设置Buddy等后续逻辑...
            // 右键菜单初始化（已移到此处）
            maplayermenu = new System.Windows.Forms.ContextMenuStrip();
            var removeLayerMenuItem = new ToolStripMenuItem("移除图层", null, removelayer_Click);
            var copyLayerMenuItem = new ToolStripMenuItem("复制图层", null, copylayer_Click);
            maplayermenu.Items.Add(removeLayerMenuItem);
            maplayermenu.Items.Add(copyLayerMenuItem);

            //拖拽事件
            axTOCControl1.EnableLayerDragDrop = true;
            var tocManager1 = new TOCManager();
            TOCManagerRegistry.Register("MainTOC", tocManager1, true);
            tocManager1.Initialize(axTOCControl1);
            tocManager1.LayerSelected += OnLayerSelected;
            // 设置初始化完成标志
            globalpara.isArcGisInitialized = true;
        }
        //加载gis地图控件
        private void arcGisInitButton_Click(object sender, EventArgs e)
        {
            // 禁用按钮，防止重复点击
            arcGisInitButton.Enabled = false;

            // 创建一个临时加载窗体
            Form tempLoadingForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(220, 100),
                TopMost = true,
                ShowInTaskbar = false,
                ControlBox = false,
                Text = "加载中"
            };

            var tipLabel = new System.Windows.Forms.Label
            {
                Text = "正在加载，请稍候...",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("宋体", 14)
            };
            tempLoadingForm.Controls.Add(tipLabel);

            // 显示临时窗体
            tempLoadingForm.Show();

            Task.Run(() =>
            {
                // 主线程加载控件
                this.Invoke(new Action(() =>
                {
                    InitializeArcGisControls();
                }));

                // 加载完成后关闭临时窗体并移除按钮
                this.Invoke(new Action(() =>
                {
                    tempLoadingForm.Close();
                    tempLoadingForm.Dispose();
                    if (arcGisInitButton != null)
                    {
                        arcGisInitButton.Parent?.Controls.Remove(arcGisInitButton);
                        arcGisInitButton.Dispose();
                    }
                    ribbonLabel1.LabelWidth = 280;
                    panel2.Width = 250;
                    // 调用 Rib_Resize 方法刷新位置
                    Rib_Resize(this, EventArgs.Empty);
                }));
                axToolbarControl1.BackColor = ColorTranslator.FromHtml("#ffffff");
                axToolbarControl2.BackColor = ColorTranslator.FromHtml("#ffffff");

            });

        }
        /// <summary>
        /// 刷新布局，更新arcGisInitButton位置
        /// </summary>
        private void RefreshLayout()
        {
            try
            {
                // 强制ribbon重新布局
                ribbon?.Invalidate();
                ribbon?.Update();

                // 延迟调用位置调整，确保ribbon布局完成
                this.BeginInvoke(new Action(() =>
                {
                    // 调用Rib_Resize方法重新计算位置
                    Rib_Resize(this, EventArgs.Empty);
                }));
            }
            catch (Exception ex)
            {
                LogHelper.normallog($"刷新布局时出错: {ex.Message}");
            }
        }
        private void OnLayerSelected(object sender, LayerSelectedEventArgs e)
        {
            TOCManagerRegistry.Get("MainTOC");
        }
        //为axToolbarControl添加工具
        private void AddMapInquiryToToolbar()
        {
            // 地图查询（Identify）工具
            ICommand identifyCmd = new ControlsMapIdentifyTool();
            axToolbarControl1.AddItem(identifyCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 添加数据（Add Data）工具
            ICommand addDataCmd = new ControlsAddDataCommand();
            axToolbarControl1.AddItem(addDataCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 放大工具
            ICommand zoomInCmd = new ControlsMapZoomInTool();
            axToolbarControl1.AddItem(zoomInCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 缩小工具
            ICommand zoomOutCmd = new ControlsMapZoomOutTool();
            axToolbarControl1.AddItem(zoomOutCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            // 平移工具
            ICommand panCmd = new ControlsMapPanTool();
            axToolbarControl1.AddItem(panCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 全图显示
            ICommand fullExtentCmd = new ControlsMapFullExtentCommand();
            axToolbarControl1.AddItem(fullExtentCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 地图旋转
            ICommand rotateCmd = new ControlsMapRotateTool();
            axToolbarControl1.AddItem(rotateCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 超链接工具（Hyperlink Tool）
            ICommand hyperlinkCmd = new ControlsMapHyperlinkTool();
            axToolbarControl1.AddItem(hyperlinkCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //等值线
            ICommand threedCmd = new Controls3DAnalystContourTool();
            axToolbarControl2.AddItem(threedCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //漫游显示
            ICommand environmentCmd = new ControlsMapRoamTool();
            axToolbarControl2.AddItem(environmentCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            // 选择工具
            ICommand selectCmd = new ControlsSelectFeaturesTool();
            axToolbarControl2.AddItem(selectCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            //测量工具
            ICommand measureCmd = new ControlsMapMeasureTool();
            axToolbarControl2.AddItem(measureCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

            // 清除选择
            ICommand clearSelectionCmd = new ControlsClearSelectionCommand();
            axToolbarControl2.AddItem(clearSelectionCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            //放缩栏
            ICommand ZoomToolCommand = new ControlsMapZoomToolControl();
            axToolbarControl2.AddItem(ZoomToolCommand, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            
            // 查询工具
            ICommand findCmd = new ControlsMapFindCommand();
            axToolbarControl2.AddItem(findCmd, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);

        }

        //复制图层
        private void copylayer_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.CopyLayer(pLayer, axMapControl1);
        }

        //删除图层
        private void removelayer_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.RemoveLayer(pLayer, axMapControl1);
        }
        //axToolbarControl1点击事件
        private void axToolbarControl1_OnItemClick(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnItemClickEvent e)
        {
            int itemIndex = e.index;
            // 根据itemIndex判断点击了哪个工具

        }
        //axToolbarControl2点击事件
        private void axToolbarControl2_OnItemClick(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnItemClickEvent e)
        {
            int itemIndex = e.index;
        }

        private void axToolbarControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEvent e)
        {

        }

        private void axToolbarControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEvent e)
        {

        }
        // axTOCControl1鼠标点击事件
        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            object unk = null;
            object data = null;
            ILayer pLayer2 = null;

            if (e.button == 1) // 左键
            {
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref pLayer2, ref unk, ref data);
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (pLayer2 is IAnnotationSublayer) return;
                    else
                    {
                        pSelectLayer = pLayer2;
                        LayerPath = 质检工具.Func.GISFunc.GISMap.LayerPathHelper.GetLayerFullPath(pLayer2);
                    }
                }
            }
            else if (e.button == 2) // 右键
            {
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref pLayer2, ref unk, ref data);
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    pLayer = pLayer2;
                    maplayermenu.Show(axTOCControl1, e.x, e.y);
                }
            }
        }

        //为axToolbarControl添加保存按钮
        private void AddSaveButtonToToolbar()
        {
            ICommand command = new Save2MXD(axMapControl1);
            //axToolbarControl2.AddItem(command, 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(command, 0, -1, false, 0, esriCommandStyles.esriCommandStyleIconOnly);
        }


        #endregion
        #region//引导教程
        //引导教程事件
        private void ribbonButton17_Click(object sender, EventArgs e)
        {
            OpenTour();
        }
        //引导教程
        void OpenTour()
        {
            List<string> tourname = new List<string> { "设置", "一级菜单", "工具菜单", "搜索", "介绍", "参数设置", "规则配置", "页面切换" };
            List<string> tourdesc = new List<string> { "可更改软件工具读取路径","点击切换一级工具菜单","点击工具集展开工具，点击工具进入工具窗口","根据关键字匹配工具",
            "GIS基础工具","工具默认值参数设置","模板文件设置","切换为地图视图"};
            if (tourForm == null)
            {
                Form popover = null;
                tourForm = AntdUI.Tour.open(new AntdUI.Tour.Config(this, item =>
                {
                    switch (item.Index)
                    {
                        case 0:
                            // 定位到 ribbonButton25 所在的区域
                            var btnRect = ribbonButtonSettings.Bounds;
                            var ribbonLocation = ribbon.PointToScreen(Point.Empty);
                            var btnLocation = new Point(ribbonLocation.X + btnRect.X, ribbonLocation.Y + btnRect.Y);
                            btnLocation = this.PointToClient(btnLocation);
                            item.Set(new Rectangle(btnLocation, btnRect.Size));
                            break;
                        case 1:
                            // 定位到 ribbonPanel1 所在的区域
                            var btnRect1 = ribbonPanel1.Bounds;
                            var ribbonLocation1 = ribbon.PointToScreen(Point.Empty);
                            var btnLocation1 = new Point(ribbonLocation1.X + btnRect1.X, ribbonLocation1.Y + btnRect1.Y);
                            btnLocation = this.PointToClient(btnLocation1);
                            item.Set(new Rectangle(btnLocation, btnRect1.Size));
                            break;
                        case 2:
                            item.Set(tree1);
                            break;
                        case 3:
                            item.Set(menuSearchInput);
                            break;
                        case 4:
                            var btnRect5 = ribbonPanel5.Bounds;
                            var ribbonLocation5 = ribbon.PointToScreen(Point.Empty);
                            var btnLocation5 = new Point(ribbonLocation5.X + btnRect5.X, ribbonLocation5.Y + btnRect5.Y);
                            btnLocation = this.PointToClient(btnLocation5);
                            item.Set(new Rectangle(btnLocation, btnRect5.Size));
                            break;
                        case 5:
                            var btnRect3 = ribbonPanel2.Bounds;
                            var ribbonLocation3 = ribbon.PointToScreen(Point.Empty);
                            var btnLocation3 = new Point(ribbonLocation3.X + btnRect3.X, ribbonLocation3.Y + btnRect3.Y);
                            btnLocation = this.PointToClient(btnLocation3);
                            item.Set(new Rectangle(btnLocation, btnRect3.Size));
                            break;
                        case 6:
                            var btnRect4 = ribbonPanel6.Bounds;
                            var ribbonLocation4 = ribbon.PointToScreen(Point.Empty);
                            var btnLocation4 = new Point(ribbonLocation4.X + btnRect4.X, ribbonLocation4.Y + btnRect4.Y);
                            btnLocation = this.PointToClient(btnLocation4);
                            item.Set(new Rectangle(btnLocation, btnRect4.Size));
                            break;
                        case 7:
                            label3.Location = new Point(16, splitContainer1.Panel1.Height- 120);
                            item.Set(label3);
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
        #region//菜单栏按钮
        #region//四个路径设置
        //数据库输出
        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.SelectAndWritePath("outdb", "gdb", "请选择gdb", true);
        }
        //成果数据库
        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.SelectAndWritePath("resultdb", "gdb", "请选择gdb", true);
        }
        //模板文件夹
        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.SelectAndWritePath("mufolder", "", "请选择文件夹", false);
        }
        //文件夹输出
        private void ribbonButton8_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.SelectAndWritePath("outfolder", "", "请选择文件夹", false);
        }
        #endregion
        //菜单栏 更多选项弹窗类 暂时隐藏
        public class TempPopupForm : Form
        {
            public TempPopupForm(Rib parent)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.Size = new Size(180, 40);
                this.BackColor = Color.WhiteSmoke;
                this.TopMost = true;
                this.ShowInTaskbar = false;

                // 第一个按钮：图标+文字
                var btn1 = new System.Windows.Forms.Button();
                btn1.Text = "引导";
                btn1.Size = new Size(80, 28);
                btn1.Location = new Point(10, 6);
                btn1.TextAlign = ContentAlignment.MiddleLeft;
                btn1.ImageAlign = ContentAlignment.MiddleLeft;
                btn1.TextImageRelation = TextImageRelation.ImageBeforeText;

                // 使用SvgHelper动态加载SVG为Bitmap
                btn1.Image = 质检工具.UIPreSet.SvgHelper.LoadSvgFromResources(
                    "开始引导2", // SVG文件名（不带.svg后缀）
                    20,    // 宽度
                    20,    // 高度
                    null // 填充色，可自定义
                );

                btn1.Click += (s, ev) =>
                {
                    parent.OpenTour();
                    this.Close();
                };
                this.Controls.Add(btn1);

                // 第二个按钮：点击事件为空但也关闭弹窗
                var btn2 = new System.Windows.Forms.Button();
                btn2.Text = "空按钮";
                btn2.Size = new Size(80, 28);
                btn2.Location = new Point(90, 6);
                btn2.Click += (s, ev) =>
                {
                    this.Close();
                };
                this.Controls.Add(btn2);

                // 延迟关闭，防止点击时弹窗提前销毁
                this.Deactivate += (s, e) =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (!this.IsDisposed)
                            this.Close();
                    }));
                };
            }
        }
        // 1. 打开工程（原 button1_Click 逻辑）
        private void ribbonButton7_Click(object sender, EventArgs e)
        {
            try
            {
                SelectFileFunc sf = new SelectFileFunc();
                string projectfile = sf.sf_selectfile("ini");
                if (!System.IO.File.Exists(projectfile)) { return; }

                ProjectFunc pjf = new ProjectFunc();
                pjf.ExtractFiles(projectfile, System.IO.Path.GetDirectoryName(globalpara.taskcachepath));

                GpTaskFunc gtf = new GpTaskFunc();
                gtf.addTaskByCache(globalpara.taskcachepath);

                if (isArcGisInitialized)
                {
                    MXDFunc mxdfunc = new MXDFunc();
                    mxdfunc.addformMXD(globalpara.savemxdpath);
                }

                // 添加成功日志
                LogHelper.normallog("工程文件打开成功：" + projectfile);

                // 切换主窗体地图tab页
                //if (System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault() is Rib mainForm)
                //{
                //  mainForm.tabs1_selectcache();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开工程失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 2. 保存工程（原 button2_Click 逻辑）
        private void settingsButton_Click(object sender, EventArgs e)
        {
            MainFormFunc.SaveProject();
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                //MessageBox.Show(this, "执行保存操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainFormFunc.SaveProject();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //数据统计（plug）
        private void ribbonButton19_Click(object sender, EventArgs e)
        {
            // 检查是否已经有 Statistics 窗体打开
            CaculateForm statisticsForm = Application.OpenForms.OfType<CaculateForm>().FirstOrDefault();
            if (statisticsForm == null)
            {
                // 如果没有打开，则创建并显示新的 Statistics 窗体
                statisticsForm = new CaculateForm();
                statisticsForm.Show();
            }
            else
            {
                // 如果已经打开，则激活该窗体
                statisticsForm.Activate();
            }
        }
        //帮助文档
        private void ribbonButton18_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(globalpara.helpdocument);
        }
        //赋值词典
        private void ribbonButton12_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.OpenFileByButtonText(ribbonButtonDictionaryAssignment.Text, globalpara.mu_rulepath);
        }
        //数据库建立模板
        private void ribbonButton13_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.OpenFileByButtonText(ribbonButtonDatabaseCreateTemplate.Text, globalpara.mu_rulepath);
        }
        //拓扑检查模板
        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.OpenFileByButtonText(ribbonButtonTopologyCheck.Text, globalpara.mu_rulepath);
        }
        //数据库结构检查规则模板
        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            质检工具.UIPreSet.RibOptimize.OpenFileByButtonText(ribbonButtonDatabaseStructureTemplate.Text, globalpara.mu_rulepath);
        }
        //模板下载
        private void ribbonButton14_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<MuDownloadForm>();
        }
        //任务列表
        private void ribbonButton15_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<TaskListForm>();
            TaskListForm tf = Application.OpenForms.OfType<TaskListForm>().FirstOrDefault();
            if (tf != null)
            {
                tf.LoadPageData();
            }

            AppLifecycle.Instance.EndInitialization();
        }
        //运行日志
        private void ribbonButton16_Click(object sender, EventArgs e)
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
        //截图工具
        private void JTGJbutton_Click(object sender, EventArgs e)
        {
            // 创建一个新的窗体
            JTGJForm dockWindowForm = new JTGJForm { };

            dockWindowForm.Size = new Size(320, 630);
            // 实例化 JTGJ_dockWindow 并设置为窗体的子控件
            JTGJ_dockWindow dockWindow = new JTGJ_dockWindow(null)
            {
                Dock = DockStyle.Fill // 填充整个窗体
            };

            // 将 JTGJ_dockWindow 添加到窗体
            dockWindowForm.Controls.Add(dockWindow);

            // 显示窗体
            dockWindowForm.Show();
        }
        //标准查阅
        private void ribbonButton23_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<BZCY_Form>();
        }
        //软件信息
        private void ribbonButton24_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<UpdateInfoForm>();
        }
        // 设置
        private void ribbonButton25_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<SettingForm>();
        }
        #endregion
        #region//工具树
        //加载工具树
        public void LoadTreeMenu(string menuName)
        {
            firstLevelMenuFunc.loadsubmenu(null, menuName, tree1);
        }
        //工具树操作
        private void tree1_SelectChanged(object sender, AntdUI.TreeSelectEventArgs e)
        {
            // 展开/收起节点
            tree1.SelectItem.Expand = !tree1.SelectItem.Expand;

            // 只有叶子节点（无子节点）才弹窗
            if (tree1.SelectItem.Sub.Count == 0)
            {
                // 取出 MenuBean
                MenuBean selectbean = e.Item.Tag as MenuBean;
                if (selectbean == null || string.IsNullOrWhiteSpace(selectbean.folderName))
                {
                    return;
                }

                ToolForm form = new ToolForm(selectbean);
                form.Owner = this;
                form.Show();
                // 检查是否已有 ToolForm 打开

            }
        }
        #endregion
        
        

        //根据 ribbonPanel5 的大小调整 panel2（axtoolbar） 的位置和宽度（加载gis控件的按钮）
        private void Rib_Resize(object sender, EventArgs e)
        {
            pageHeader1.Width = this.Width;
            pageHeader3.Location = new Point((int)(this.Width / 2) - 50, 0);
            pageHeader3.Height = pageHeader1.Height;
            ICO_PICBOX.Size = new Size(pageHeader1.Height, pageHeader1.Height);
            // 获取 ribbonPanel5 在 ribbon 控件中的位置和宽度
            var panelRect = ribbonPanel5.Bounds;

            // ribbonPanel5.Bounds 是相对于 ribbon 的，需要转换到窗体坐标
            var ribbonLocation = ribbon.PointToScreen(Point.Empty);
            var panelLocation = new Point(ribbonLocation.X + panelRect.X + 1, ribbonLocation.Y + panelRect.Y);

            // 转换到窗体客户区坐标
            panelLocation = this.PointToClient(panelLocation);

            // 只同步 X 和 Width，Y 保持设计器设置
            panel2.Location = new Point(panelLocation.X, panelLocation.Y + Convert.ToInt32(pageHeader1.Height * 0.5));
            panel2.Width = panelRect.Width - 2;

            // 计算文字高度并从panel高度中减去
            float fontSize = 9.0f;
            Font font = new Font("Microsoft YaHei UI", fontSize, FontStyle.Regular);
            int textHeight = TextRenderer.MeasureText("测试文字", font).Height;
            panel2.Height = panelRect.Height - textHeight - Convert.ToInt32(pageHeader1.Height * 0.5);
            //主页图片
            resize_picPanel();
        }
        //滚动图片大小
        private void resize_picPanel()
        {
            //主页图片
            int width = Convert.ToInt32(splitContainer3.Width / 10 * 8);
            carousel1.Width = width;
            carousel1.Height = Convert.ToInt32(width / 16 * 9);
            int posy = Convert.ToInt32((splitContainer3.Panel2.Height / 2) - (carousel1.Height * 0.5));
            carousel1.Location = new Point(Convert.ToInt32(splitContainer3.Width * 0.5 - width * 0.5), posy);
        }

        /// <summary>
        /// 批量刷新所有Ribbon按钮图标
        /// </summary>
        public void UpdateRibbonIcons(string[] svgColors)
        {
            ribbonButtonSaveProject.Image = UIPreSet.SvgHelper.LoadSvgFromResources("保存工程2", 35, 35, null, 0, 0, 1, svgColors);
            ribbonButtonSaveProject.SmallImage = ribbonButtonSaveProject.Image;

            ribbonButtonOpenProject.Image = UIPreSet.SvgHelper.LoadSvgFromResources("打开工程2", 35, 35, null, 0, 0, 1, svgColors);
            ribbonButtonOpenProject.SmallImage = ribbonButtonOpenProject.Image;

            ribbonButtonGeneralCheck.Image = UIPreSet.SvgHelper.LoadSvgFromResources("通用检查2", 50, 50, null, 10, 5, 0.5f, svgColors);
            ribbonButtonGeneralCheck.SmallImage = UIPreSet.SvgHelper.LoadSvgFromResources("通用检查2", 30, 30, null, 10, 5, 0.5f, svgColors);
            ribbonButtonGeneralCheck.LargeImage = ribbonButtonGeneralCheck.Image;

            ribbonButtonProjectCheck.Image = UIPreSet.SvgHelper.LoadSvgFromResources("项目检查2", 50, 50, null, 10, 5, 0.5f, svgColors);
            ribbonButtonProjectCheck.SmallImage = UIPreSet.SvgHelper.LoadSvgFromResources("项目检查2", 30, 30, null, 10, 5, 0.5f, svgColors);
            ribbonButtonProjectCheck.LargeImage = ribbonButtonProjectCheck.Image;

            ribbonButtonDataProcessing.Image = UIPreSet.SvgHelper.LoadSvgFromResources("数据处理与评分2", 50, 50, null, 10, 5, 0.5f, svgColors);
            ribbonButtonDataProcessing.SmallImage = UIPreSet.SvgHelper.LoadSvgFromResources("数据处理与评分2", 30, 30, null, 10, 5, 0.5f, svgColors);
            ribbonButtonDataProcessing.LargeImage = ribbonButtonDataProcessing.Image;

            ribbonButtonDatabaseExport.Image = UIPreSet.SvgHelper.LoadSvgFromResources("数据库输出2", 30, 30, null, 5, 5, 0.5f, svgColors);
            ribbonButtonDatabaseExport.SmallImage = ribbonButtonDatabaseExport.Image;

            ribbonButtonFolderExport.Image = UIPreSet.SvgHelper.LoadSvgFromResources("文件夹输出2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonFolderExport.SmallImage = ribbonButtonFolderExport.Image;

            ribbonButtonDatabaseCreateTemplate.Image = UIPreSet.SvgHelper.LoadSvgFromResources("数据库建立模板2", 30, 30, null, 5, 5, 0.5f, svgColors);
            ribbonButtonDatabaseCreateTemplate.SmallImage = ribbonButtonDatabaseCreateTemplate.Image;

            ribbonButtonDatabaseStructureTemplate.Image = UIPreSet.SvgHelper.LoadSvgFromResources("数据库检查2", 30, 30, null, 5, 5, 0.5f, svgColors);
            ribbonButtonDatabaseStructureTemplate.SmallImage = ribbonButtonDatabaseStructureTemplate.Image;

            ribbonButtonResultDatabase.Image = UIPreSet.SvgHelper.LoadSvgFromResources("成果数据库2", 30, 30, null, 5, 5, 0.5f, svgColors);
            ribbonButtonResultDatabase.SmallImage = ribbonButtonResultDatabase.Image;

            ribbonButtonTemplateFolder.Image = UIPreSet.SvgHelper.LoadSvgFromResources("模板文件夹2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonTemplateFolder.SmallImage = ribbonButtonTemplateFolder.Image;

            ribbonButtonTopologyCheck.Image = UIPreSet.SvgHelper.LoadSvgFromResources("拓扑检查2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonTopologyCheck.SmallImage = ribbonButtonTopologyCheck.Image;

            ribbonButtonDictionaryAssignment.Image = UIPreSet.SvgHelper.LoadSvgFromResources("赋值词典2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonDictionaryAssignment.SmallImage = ribbonButtonDictionaryAssignment.Image;

            ribbonButtonStandardReference.Image = UIPreSet.SvgHelper.LoadSvgFromResources("标准查阅2", 30, 30, null, 5, 5, 0.5f, svgColors);
            ribbonButtonStandardReference.SmallImage = ribbonButtonStandardReference.Image;

            ribbonButtonSoftwareInfo.Image = UIPreSet.SvgHelper.LoadSvgFromResources("软件信息2", 30, 30, null, 5, 5, 0.5f, svgColors);
            ribbonButtonSoftwareInfo.SmallImage = ribbonButtonSoftwareInfo.Image;

            ribbonButtonSettings.Image = UIPreSet.SvgHelper.LoadSvgFromResources("设置2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonSettings.SmallImage = ribbonButtonSettings.Image;

            ribbonButtonTemplateDownload.Image = UIPreSet.SvgHelper.LoadSvgFromResources("模板下载2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonTemplateDownload.SmallImage = ribbonButtonTemplateDownload.Image;

            ribbonButtonTaskList.Image = UIPreSet.SvgHelper.LoadSvgFromResources("任务列表2", 50, 50, null, 10, 5, 0.5f, svgColors);
            ribbonButtonTaskList.LargeImage = ribbonButtonTaskList.Image;
            ribbonButtonTaskList.SmallImage = UIPreSet.SvgHelper.LoadSvgFromResources("任务列表2", 30, 30, null, 10, 5, 0.5f, svgColors);

            ribbonButtonRunLog.Image = UIPreSet.SvgHelper.LoadSvgFromResources("运行日志2", 50, 50, null, 10, 5, 0.5f, svgColors);
            ribbonButtonRunLog.LargeImage = ribbonButtonRunLog.Image;
            ribbonButtonRunLog.SmallImage = UIPreSet.SvgHelper.LoadSvgFromResources("运行日志2", 30, 30, null, 10, 5, 0.5f, svgColors);

            ribbonButtonTourGuide.Image = UIPreSet.SvgHelper.LoadSvgFromResources("开始引导2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonTourGuide.SmallImage = ribbonButtonTourGuide.Image;
            ribbonButtonTourGuide.LargeImage = UIPreSet.SvgHelper.LoadSvgFromResources("开始引导2", 50, 50, null, 10, 5, 0.5f, svgColors);

            ribbonButtonHelpDoc.Image = UIPreSet.SvgHelper.LoadSvgFromResources("帮助文档2", 30, 30, null, 0, 0, 0.5f, svgColors);
            ribbonButtonHelpDoc.SmallImage = ribbonButtonHelpDoc.Image;
            ribbonButtonHelpDoc.LargeImage = UIPreSet.SvgHelper.LoadSvgFromResources("帮助文档2", 50, 50, null, 10, 5, 0.5f, svgColors);

            ribbonButtonStatistics.Image = UIPreSet.SvgHelper.LoadSvgFromResources("数据统计2", 50, 50, null, 10, 5, 0.5f, svgColors);
            ribbonButtonStatistics.SmallImage = UIPreSet.SvgHelper.LoadSvgFromResources("数据统计2", 30, 30, null, 10, 5, 0.5f, svgColors);
            ribbonButtonStatistics.LargeImage = ribbonButtonStatistics.Image;


            JTGJbutton.Image = UIPreSet.SvgHelper.LoadSvgFromResources("截图2", 30, 30, null, 5, 5, 0.5f, svgColors);
            JTGJbutton.SmallImage = JTGJbutton.Image;
        }

        /// <summary>
        /// 修改Ribbon控件的文字大小
        /// </summary>
        /// <param name="fontSize">字体大小，默认为9.0f</param>
        public void changeTextSize(float fontSize = 9.0f)
        {
            // 设置ribbon控件的字体大小
            Font ribbonFont = new Font("Microsoft YaHei UI", fontSize, FontStyle.Regular);
            
            try
            {
                // 设置ribbon主控件字体
                if (ribbon != null)
                {
                    ribbon.Font = ribbonFont;
                }
                TextFont textFont = new TextFont();
                // 设置所有ribbonpanel的字体
                textFont.SetRibbonPanelFont(ribbonPanel1, ribbonFont);
                textFont.SetRibbonPanelFont(ribbonPanel2, ribbonFont);
                textFont.SetRibbonPanelFont(ribbonPanel3, ribbonFont);
                textFont.SetRibbonPanelFont(ribbonPanel4, ribbonFont);
                textFont.SetRibbonPanelFont(ribbonPanel5, ribbonFont);
                textFont.SetRibbonPanelFont(ribbonPanel6, ribbonFont);
                textFont.SetRibbonPanelFont(ribbonPanel7, ribbonFont);
                textFont.SetRibbonPanelFont(settingsPanel, ribbonFont);

                // 设置所有ribbon按钮的字体
                RibbonButton[] buttons = new System.Windows.Forms.RibbonButton[]
                {
                    ribbonButtonGeneralCheck, ribbonButtonProjectCheck, ribbonButtonDataProcessing, ribbonButtonStatistics,
                    ribbonButtonDatabaseExport, ribbonButtonFolderExport, ribbonButtonResultDatabase, ribbonButtonTemplateFolder,
                    ribbonButtonDatabaseStructureTemplate, ribbonButtonTopologyCheck, ribbonButtonDatabaseCreateTemplate, ribbonButtonDictionaryAssignment,
                    ribbonButtonTaskList, ribbonButtonRunLog,
                    ribbonButtonStandardReference, ribbonButtonTemplateDownload, ribbonButtonSoftwareInfo, ribbonButtonSettings, JTGJbutton,
                    ribbonButtonTourGuide, ribbonButtonHelpDoc,
                    ribbonButtonSaveProject, ribbonButtonOpenProject
                };
                textFont.SetRibbonButtonsFont(buttons, ribbonFont);
                
                // 刷新布局，确保arcGisInitButton位置正确
                RefreshLayout();
            }
            catch (Exception ex)
            {
                LogHelper.normallog($"设置Ribbon字体大小时出错: {ex.Message}");
            }
        }
            
        
        



    }
}