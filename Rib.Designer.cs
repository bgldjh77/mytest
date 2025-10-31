using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
namespace 质检工具
{
    partial class Rib
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
       
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rib));
            AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
            AntdUI.CarouselItem carouselItem1 = new AntdUI.CarouselItem();
            AntdUI.CarouselItem carouselItem2 = new AntdUI.CarouselItem();
            this.ribbon = new System.Windows.Forms.Ribbon();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.homeTab = new System.Windows.Forms.RibbonTab();
            this.settingsPanel = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonSaveProject = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonOpenProject = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonGeneralCheck = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonProjectCheck = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonDataProcessing = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonStatistics = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonDatabaseExport = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonFolderExport = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonResultDatabase = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonTemplateFolder = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel6 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonDatabaseStructureTemplate = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonTopologyCheck = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonDatabaseCreateTemplate = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonDictionaryAssignment = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonTaskList = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonRunLog = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.ribbonLabel1 = new System.Windows.Forms.RibbonLabel();
            this.ribbonPanel7 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonStandardReference = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonTemplateDownload = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonSoftwareInfo = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonSettings = new System.Windows.Forms.RibbonButton();
            this.JTGJbutton = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButtonTourGuide = new System.Windows.Forms.RibbonButton();
            this.ribbonButtonHelpDoc = new System.Windows.Forms.RibbonButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tabs1 = new AntdUI.Tabs();
            this.mainPage_splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tree1 = new AntdUI.Tree();
            this.menuSearchInput = new AntdUI.Input();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new AntdUI.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pageHeader2 = new AntdUI.PageHeader();
            this.carousel1 = new AntdUI.Carousel();
            this.panel1 = new AntdUI.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.arcGisInitButton = new System.Windows.Forms.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.pageHeader3 = new AntdUI.PageHeader();
            this.ICO_PICBOX = new System.Windows.Forms.PictureBox();
            this.ribbonDescriptionMenuItem1 = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonDescriptionMenuItem2 = new System.Windows.Forms.RibbonDescriptionMenuItem();
            this.ribbonButton21 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton22 = new System.Windows.Forms.RibbonButton();
            this.statusStrip.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.tabs1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPage_splitContainer)).BeginInit();
            this.mainPage_splitContainer.Panel1.SuspendLayout();
            this.mainPage_splitContainer.Panel2.SuspendLayout();
            this.mainPage_splitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pageHeader1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ICO_PICBOX)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.BackColor = System.Drawing.Color.White;
            this.ribbon.CaptionBarVisible = false;
            this.ribbon.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ribbon.ForeColor = System.Drawing.Color.Snow;
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Minimized = false;
            this.ribbon.Name = "ribbon";
            // 
            // 
            // 
            this.ribbon.OrbDropDown.BorderRoundness = 8;
            this.ribbon.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon.OrbDropDown.Margin = new System.Windows.Forms.Padding(0);
            this.ribbon.OrbDropDown.Name = "";
            this.ribbon.OrbDropDown.Size = new System.Drawing.Size(0, 72);
            this.ribbon.OrbDropDown.TabIndex = 0;
            this.ribbon.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbon.OrbVisible = false;
            // 
            // 
            // 
            this.ribbon.QuickAccessToolbar.DropDownButtonVisible = false;
            this.ribbon.QuickAccessToolbar.Items.Add(this.ribbonButton1);
            this.ribbon.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon.Size = new System.Drawing.Size(1153, 133);
            this.ribbon.TabIndex = 3;
            this.ribbon.Tabs.Add(this.homeTab);
            this.ribbon.TabSpacing = 4;
            this.ribbon.Text = " ";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.LargeImage")));
            this.ribbonButton1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "ribbonButton1";
            // 
            // homeTab
            // 
            this.homeTab.Name = "homeTab";
            this.homeTab.Panels.Add(this.settingsPanel);
            this.homeTab.Panels.Add(this.ribbonPanel1);
            this.homeTab.Panels.Add(this.ribbonPanel2);
            this.homeTab.Panels.Add(this.ribbonPanel6);
            this.homeTab.Panels.Add(this.ribbonPanel3);
            this.homeTab.Panels.Add(this.ribbonPanel5);
            this.homeTab.Panels.Add(this.ribbonPanel7);
            this.homeTab.Panels.Add(this.ribbonPanel4);
            this.homeTab.Text = "主页";
            // 
            // settingsPanel
            // 
            this.settingsPanel.Items.Add(this.ribbonButtonSaveProject);
            this.settingsPanel.Items.Add(this.ribbonButtonOpenProject);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Text = "项目";
            // 
            // ribbonButtonSaveProject
            // 
            this.ribbonButtonSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSaveProject.Image")));
            this.ribbonButtonSaveProject.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSaveProject.LargeImage")));
            this.ribbonButtonSaveProject.MaximumSize = new System.Drawing.Size(100, 40);
            this.ribbonButtonSaveProject.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonSaveProject.Name = "ribbonButtonSaveProject";
            this.ribbonButtonSaveProject.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSaveProject.SmallImage")));
            this.ribbonButtonSaveProject.Text = "保存工程";
            // 
            // ribbonButtonOpenProject
            // 
            this.ribbonButtonOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonOpenProject.Image")));
            this.ribbonButtonOpenProject.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonOpenProject.LargeImage")));
            this.ribbonButtonOpenProject.MaximumSize = new System.Drawing.Size(100, 40);
            this.ribbonButtonOpenProject.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonOpenProject.Name = "ribbonButtonOpenProject";
            this.ribbonButtonOpenProject.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonOpenProject.SmallImage")));
            this.ribbonButtonOpenProject.Text = "打开工程";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbonButtonGeneralCheck);
            this.ribbonPanel1.Items.Add(this.ribbonButtonProjectCheck);
            this.ribbonPanel1.Items.Add(this.ribbonButtonDataProcessing);
            this.ribbonPanel1.Items.Add(this.ribbonButtonStatistics);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "质检工具";
            // 
            // ribbonButtonGeneralCheck
            // 
            this.ribbonButtonGeneralCheck.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonGeneralCheck.Image")));
            this.ribbonButtonGeneralCheck.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonGeneralCheck.LargeImage")));
            this.ribbonButtonGeneralCheck.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonGeneralCheck.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonGeneralCheck.Name = "ribbonButtonGeneralCheck";
            this.ribbonButtonGeneralCheck.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonGeneralCheck.SmallImage")));
            this.ribbonButtonGeneralCheck.Text = "通用检查";
            this.ribbonButtonGeneralCheck.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            // 
            // ribbonButtonProjectCheck
            // 
            this.ribbonButtonProjectCheck.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonProjectCheck.Image")));
            this.ribbonButtonProjectCheck.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonProjectCheck.LargeImage")));
            this.ribbonButtonProjectCheck.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonProjectCheck.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonProjectCheck.Name = "ribbonButtonProjectCheck";
            this.ribbonButtonProjectCheck.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonProjectCheck.SmallImage")));
            this.ribbonButtonProjectCheck.Text = "项目检查";
            this.ribbonButtonProjectCheck.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            // 
            // ribbonButtonDataProcessing
            // 
            this.ribbonButtonDataProcessing.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDataProcessing.Image")));
            this.ribbonButtonDataProcessing.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDataProcessing.LargeImage")));
            this.ribbonButtonDataProcessing.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonDataProcessing.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonDataProcessing.Name = "ribbonButtonDataProcessing";
            this.ribbonButtonDataProcessing.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDataProcessing.SmallImage")));
            this.ribbonButtonDataProcessing.Text = "数据处理";
            this.ribbonButtonDataProcessing.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            // 
            // ribbonButtonStatistics
            // 
            this.ribbonButtonStatistics.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStatistics.Image")));
            this.ribbonButtonStatistics.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStatistics.LargeImage")));
            this.ribbonButtonStatistics.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonStatistics.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonStatistics.Name = "ribbonButtonStatistics";
            this.ribbonButtonStatistics.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStatistics.SmallImage")));
            this.ribbonButtonStatistics.Text = "数据统计";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.ribbonButtonDatabaseExport);
            this.ribbonPanel2.Items.Add(this.ribbonButtonFolderExport);
            this.ribbonPanel2.Items.Add(this.ribbonButtonResultDatabase);
            this.ribbonPanel2.Items.Add(this.ribbonButtonTemplateFolder);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "参数配置";
            // 
            // ribbonButtonDatabaseExport
            // 
            this.ribbonButtonDatabaseExport.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseExport.Image")));
            this.ribbonButtonDatabaseExport.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseExport.LargeImage")));
            this.ribbonButtonDatabaseExport.MaximumSize = new System.Drawing.Size(200, 100);
            this.ribbonButtonDatabaseExport.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonDatabaseExport.MinimumSize = new System.Drawing.Size(200, 100);
            this.ribbonButtonDatabaseExport.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonDatabaseExport.Name = "ribbonButtonDatabaseExport";
            this.ribbonButtonDatabaseExport.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseExport.SmallImage")));
            this.ribbonButtonDatabaseExport.Text = "输出数据库";
            // 
            // ribbonButtonFolderExport
            // 
            this.ribbonButtonFolderExport.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFolderExport.Image")));
            this.ribbonButtonFolderExport.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFolderExport.LargeImage")));
            this.ribbonButtonFolderExport.MaximumSize = new System.Drawing.Size(100, 40);
            this.ribbonButtonFolderExport.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonFolderExport.Name = "ribbonButtonFolderExport";
            this.ribbonButtonFolderExport.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonFolderExport.SmallImage")));
            this.ribbonButtonFolderExport.Text = "输出文件夹";
            // 
            // ribbonButtonResultDatabase
            // 
            this.ribbonButtonResultDatabase.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonResultDatabase.Image")));
            this.ribbonButtonResultDatabase.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonResultDatabase.LargeImage")));
            this.ribbonButtonResultDatabase.MaximumSize = new System.Drawing.Size(100, 40);
            this.ribbonButtonResultDatabase.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonResultDatabase.Name = "ribbonButtonResultDatabase";
            this.ribbonButtonResultDatabase.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonResultDatabase.SmallImage")));
            this.ribbonButtonResultDatabase.Text = "成果数据库";
            // 
            // ribbonButtonTemplateFolder
            // 
            this.ribbonButtonTemplateFolder.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTemplateFolder.Image")));
            this.ribbonButtonTemplateFolder.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTemplateFolder.LargeImage")));
            this.ribbonButtonTemplateFolder.MaximumSize = new System.Drawing.Size(100, 40);
            this.ribbonButtonTemplateFolder.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonTemplateFolder.Name = "ribbonButtonTemplateFolder";
            this.ribbonButtonTemplateFolder.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTemplateFolder.SmallImage")));
            this.ribbonButtonTemplateFolder.Text = "模板文件夹";
            // 
            // ribbonPanel6
            // 
            this.ribbonPanel6.Items.Add(this.ribbonButtonDatabaseStructureTemplate);
            this.ribbonPanel6.Items.Add(this.ribbonButtonTopologyCheck);
            this.ribbonPanel6.Items.Add(this.ribbonButtonDatabaseCreateTemplate);
            this.ribbonPanel6.Items.Add(this.ribbonButtonDictionaryAssignment);
            this.ribbonPanel6.Name = "ribbonPanel6";
            this.ribbonPanel6.Text = "规则配置";
            // 
            // ribbonButtonDatabaseStructureTemplate
            // 
            this.ribbonButtonDatabaseStructureTemplate.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseStructureTemplate.Image")));
            this.ribbonButtonDatabaseStructureTemplate.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseStructureTemplate.LargeImage")));
            this.ribbonButtonDatabaseStructureTemplate.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonDatabaseStructureTemplate.Name = "ribbonButtonDatabaseStructureTemplate";
            this.ribbonButtonDatabaseStructureTemplate.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseStructureTemplate.SmallImage")));
            this.ribbonButtonDatabaseStructureTemplate.Text = "数据库结构";
            // 
            // ribbonButtonTopologyCheck
            // 
            this.ribbonButtonTopologyCheck.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTopologyCheck.Image")));
            this.ribbonButtonTopologyCheck.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTopologyCheck.LargeImage")));
            this.ribbonButtonTopologyCheck.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonTopologyCheck.Name = "ribbonButtonTopologyCheck";
            this.ribbonButtonTopologyCheck.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTopologyCheck.SmallImage")));
            this.ribbonButtonTopologyCheck.Text = "拓扑规则";
            // 
            // ribbonButtonDatabaseCreateTemplate
            // 
            this.ribbonButtonDatabaseCreateTemplate.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseCreateTemplate.Image")));
            this.ribbonButtonDatabaseCreateTemplate.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseCreateTemplate.LargeImage")));
            this.ribbonButtonDatabaseCreateTemplate.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonDatabaseCreateTemplate.Name = "ribbonButtonDatabaseCreateTemplate";
            this.ribbonButtonDatabaseCreateTemplate.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDatabaseCreateTemplate.SmallImage")));
            this.ribbonButtonDatabaseCreateTemplate.Text = "数据库建立";
            // 
            // ribbonButtonDictionaryAssignment
            // 
            this.ribbonButtonDictionaryAssignment.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDictionaryAssignment.Image")));
            this.ribbonButtonDictionaryAssignment.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDictionaryAssignment.LargeImage")));
            this.ribbonButtonDictionaryAssignment.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonDictionaryAssignment.Name = "ribbonButtonDictionaryAssignment";
            this.ribbonButtonDictionaryAssignment.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonDictionaryAssignment.SmallImage")));
            this.ribbonButtonDictionaryAssignment.Text = "赋值词典";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Items.Add(this.ribbonButtonTaskList);
            this.ribbonPanel3.Items.Add(this.ribbonButtonRunLog);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "质检任务";
            // 
            // ribbonButtonTaskList
            // 
            this.ribbonButtonTaskList.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTaskList.Image")));
            this.ribbonButtonTaskList.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTaskList.LargeImage")));
            this.ribbonButtonTaskList.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonTaskList.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonTaskList.Name = "ribbonButtonTaskList";
            this.ribbonButtonTaskList.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTaskList.SmallImage")));
            this.ribbonButtonTaskList.Text = "任务列表";
            // 
            // ribbonButtonRunLog
            // 
            this.ribbonButtonRunLog.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonRunLog.Image")));
            this.ribbonButtonRunLog.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonRunLog.LargeImage")));
            this.ribbonButtonRunLog.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonRunLog.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonRunLog.Name = "ribbonButtonRunLog";
            this.ribbonButtonRunLog.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonRunLog.SmallImage")));
            this.ribbonButtonRunLog.Text = "运行日志";
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.Items.Add(this.ribbonLabel1);
            this.ribbonPanel5.Name = "ribbonPanel5";
            this.ribbonPanel5.Text = "GIS工具";
            // 
            // ribbonLabel1
            // 
            this.ribbonLabel1.LabelWidth = 120;
            this.ribbonLabel1.Name = "ribbonLabel1";
            this.ribbonLabel1.Text = "        ";
            // 
            // ribbonPanel7
            // 
            this.ribbonPanel7.Items.Add(this.ribbonButtonStandardReference);
            this.ribbonPanel7.Items.Add(this.ribbonButtonTemplateDownload);
            this.ribbonPanel7.Items.Add(this.ribbonButtonSoftwareInfo);
            this.ribbonPanel7.Items.Add(this.ribbonButtonSettings);
            this.ribbonPanel7.Items.Add(this.JTGJbutton);
            this.ribbonPanel7.Name = "ribbonPanel7";
            this.ribbonPanel7.Text = "其他";
            // 
            // ribbonButtonStandardReference
            // 
            this.ribbonButtonStandardReference.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStandardReference.Image")));
            this.ribbonButtonStandardReference.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStandardReference.LargeImage")));
            this.ribbonButtonStandardReference.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonStandardReference.Name = "ribbonButtonStandardReference";
            this.ribbonButtonStandardReference.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonStandardReference.SmallImage")));
            this.ribbonButtonStandardReference.Text = "标准查阅";
            // 
            // ribbonButtonTemplateDownload
            // 
            this.ribbonButtonTemplateDownload.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTemplateDownload.Image")));
            this.ribbonButtonTemplateDownload.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTemplateDownload.LargeImage")));
            this.ribbonButtonTemplateDownload.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonTemplateDownload.Name = "ribbonButtonTemplateDownload";
            this.ribbonButtonTemplateDownload.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTemplateDownload.SmallImage")));
            this.ribbonButtonTemplateDownload.Text = "模板下载";
            // 
            // ribbonButtonSoftwareInfo
            // 
            this.ribbonButtonSoftwareInfo.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSoftwareInfo.Image")));
            this.ribbonButtonSoftwareInfo.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSoftwareInfo.LargeImage")));
            this.ribbonButtonSoftwareInfo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonSoftwareInfo.Name = "ribbonButtonSoftwareInfo";
            this.ribbonButtonSoftwareInfo.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSoftwareInfo.SmallImage")));
            this.ribbonButtonSoftwareInfo.Text = "软件信息";
            // 
            // ribbonButtonSettings
            // 
            this.ribbonButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSettings.Image")));
            this.ribbonButtonSettings.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSettings.LargeImage")));
            this.ribbonButtonSettings.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.ribbonButtonSettings.Name = "ribbonButtonSettings";
            this.ribbonButtonSettings.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonSettings.SmallImage")));
            this.ribbonButtonSettings.Text = "设置";
            // 
            // JTGJbutton
            // 
            this.JTGJbutton.Image = ((System.Drawing.Image)(resources.GetObject("JTGJbutton.Image")));
            this.JTGJbutton.LargeImage = ((System.Drawing.Image)(resources.GetObject("JTGJbutton.LargeImage")));
            this.JTGJbutton.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.JTGJbutton.Name = "JTGJbutton";
            this.JTGJbutton.SmallImage = ((System.Drawing.Image)(resources.GetObject("JTGJbutton.SmallImage")));
            this.JTGJbutton.Text = "截图工具";
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.Items.Add(this.ribbonButtonTourGuide);
            this.ribbonPanel4.Items.Add(this.ribbonButtonHelpDoc);
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Text = "帮助";
            // 
            // ribbonButtonTourGuide
            // 
            this.ribbonButtonTourGuide.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTourGuide.Image")));
            this.ribbonButtonTourGuide.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTourGuide.LargeImage")));
            this.ribbonButtonTourGuide.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonTourGuide.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonTourGuide.Name = "ribbonButtonTourGuide";
            this.ribbonButtonTourGuide.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonTourGuide.SmallImage")));
            this.ribbonButtonTourGuide.Text = "开始引导";
            // 
            // ribbonButtonHelpDoc
            // 
            this.ribbonButtonHelpDoc.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButtonHelpDoc.Image")));
            this.ribbonButtonHelpDoc.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonHelpDoc.LargeImage")));
            this.ribbonButtonHelpDoc.MaximumSize = new System.Drawing.Size(100, 100);
            this.ribbonButtonHelpDoc.MinimumSize = new System.Drawing.Size(80, 80);
            this.ribbonButtonHelpDoc.Name = "ribbonButtonHelpDoc";
            this.ribbonButtonHelpDoc.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButtonHelpDoc.SmallImage")));
            this.ribbonButtonHelpDoc.Text = "帮助文档";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 607);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1153, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.ForeColor = System.Drawing.Color.Black;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(92, 17);
            this.statusLabel.Text = "质检工具已就绪";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.tabs1);
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 133);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1153, 474);
            this.mainPanel.TabIndex = 2;
            // 
            // tabs1
            // 
            this.tabs1.BackColor = System.Drawing.Color.White;
            this.tabs1.Controls.Add(this.mainPage_splitContainer);
            this.tabs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs1.Location = new System.Drawing.Point(0, 0);
            this.tabs1.Name = "tabs1";
            this.tabs1.Size = new System.Drawing.Size(1133, 474);
            this.tabs1.Style = styleLine1;
            this.tabs1.TabIndex = 2;
            this.tabs1.Text = "tabs1";
            // 
            // mainPage_splitContainer
            // 
            this.mainPage_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPage_splitContainer.Location = new System.Drawing.Point(3, 3);
            this.mainPage_splitContainer.Name = "mainPage_splitContainer";
            // 
            // mainPage_splitContainer.Panel1
            // 
            this.mainPage_splitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // mainPage_splitContainer.Panel2
            // 
            this.mainPage_splitContainer.Panel2.Controls.Add(this.tabControl1);
            this.mainPage_splitContainer.Size = new System.Drawing.Size(1127, 468);
            this.mainPage_splitContainer.SplitterDistance = 220;
            this.mainPage_splitContainer.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tree1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuSearchInput, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 468);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tree1
            // 
            this.tree1.BackColor = System.Drawing.Color.White;
            this.tree1.BlockNode = true;
            this.tree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tree1.Gap = 10;
            this.tree1.HandDragFolder = false;
            this.tree1.Location = new System.Drawing.Point(3, 38);
            this.tree1.Name = "tree1";
            this.tree1.Size = new System.Drawing.Size(214, 427);
            this.tree1.TabIndex = 1;
            this.tree1.Text = "tree1";
            this.tree1.SelectChanged += new AntdUI.TreeSelectEventHandler(this.tree1_SelectChanged);
            // 
            // menuSearchInput
            // 
            this.menuSearchInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuSearchInput.IconRatio = 1F;
            this.menuSearchInput.Location = new System.Drawing.Point(3, 3);
            this.menuSearchInput.Name = "menuSearchInput";
            this.menuSearchInput.PlaceholderText = "搜索工具";
            this.menuSearchInput.PrefixSvg = "SearchOutlined";
            this.menuSearchInput.Size = new System.Drawing.Size(214, 29);
            this.menuSearchInput.TabIndex = 100;
            this.menuSearchInput.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(903, 468);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(895, 442);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "地图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(889, 436);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.Location = new System.Drawing.Point(35, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 80);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(895, 442);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "主页";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.pageHeader2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.carousel1);
            this.splitContainer3.Size = new System.Drawing.Size(889, 436);
            this.splitContainer3.SplitterDistance = 84;
            this.splitContainer3.TabIndex = 9;
            // 
            // pageHeader2
            // 
            this.pageHeader2.BackColor = System.Drawing.Color.White;
            this.pageHeader2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader2.Description = "对地理信息数据进行空间位置、几何特征、拓扑关系等多维质量检查";
            this.pageHeader2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageHeader2.DragMove = false;
            this.pageHeader2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.pageHeader2.HandCursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader2.LocalizationDescription = "Tabs.Description";
            this.pageHeader2.LocalizationText = "Tabs.Text";
            this.pageHeader2.Location = new System.Drawing.Point(0, 0);
            this.pageHeader2.Name = "pageHeader2";
            this.pageHeader2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pageHeader2.Size = new System.Drawing.Size(889, 84);
            this.pageHeader2.SubText = "";
            this.pageHeader2.TabIndex = 8;
            this.pageHeader2.Text = "地理数据检查";
            this.pageHeader2.UseTitleFont = true;
            // 
            // carousel1
            // 
            this.carousel1.BackColor = System.Drawing.Color.Transparent;
            this.carousel1.DotMargin = 10;
            this.carousel1.DotPosition = AntdUI.TAlignMini.Bottom;
            this.carousel1.DotSize = new System.Drawing.Size(58, 15);
            this.carousel1.HandCursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            this.carousel1.HandDragFolder = false;
            carouselItem1.Img = global::质检工具.Properties.Resources.示例1;
            carouselItem2.Img = global::质检工具.Properties.Resources.示例2;
            this.carousel1.Image.Add(carouselItem1);
            this.carousel1.Image.Add(carouselItem2);
            this.carousel1.Location = new System.Drawing.Point(3, 3);
            this.carousel1.Name = "carousel1";
            this.carousel1.Padding = new System.Windows.Forms.Padding(40, 80, 40, 80);
            this.carousel1.Size = new System.Drawing.Size(883, 342);
            this.carousel1.TabIndex = 7;
            this.carousel1.Text = "carousel1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1133, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 474);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.arcGisInitButton);
            this.panel2.Location = new System.Drawing.Point(635, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(122, 91);
            this.panel2.TabIndex = 8;
            // 
            // arcGisInitButton
            // 
            this.arcGisInitButton.BackColor = System.Drawing.Color.White;
            this.arcGisInitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arcGisInitButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.arcGisInitButton.FlatAppearance.BorderSize = 0;
            this.arcGisInitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.arcGisInitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.arcGisInitButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(52)))), ((int)(((byte)(58)))));
            this.arcGisInitButton.Location = new System.Drawing.Point(0, 0);
            this.arcGisInitButton.Name = "arcGisInitButton";
            this.arcGisInitButton.Size = new System.Drawing.Size(122, 91);
            this.arcGisInitButton.TabIndex = 0;
            this.arcGisInitButton.Text = "点击加载地图控件";
            this.arcGisInitButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.arcGisInitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.arcGisInitButton.UseVisualStyleBackColor = false;
            this.arcGisInitButton.Click += new System.EventHandler(this.arcGisInitButton_Click);
            // 
            // pageHeader1
            // 
            this.pageHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pageHeader1.Controls.Add(this.pageHeader3);
            this.pageHeader1.Controls.Add(this.ICO_PICBOX);
            this.pageHeader1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader1.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold);
            this.pageHeader1.ForeColor = System.Drawing.Color.Snow;
            this.pageHeader1.Gap = 0;
            this.pageHeader1.HandCursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader1.HandDragFolder = false;
            this.pageHeader1.LocalizationDescription = "Tabs.Description";
            this.pageHeader1.LocalizationText = "Tabs.Text";
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(1153, 28);
            this.pageHeader1.SubFont = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold);
            this.pageHeader1.SubGap = 0;
            this.pageHeader1.SubText = "";
            this.pageHeader1.TabIndex = 14;
            this.pageHeader1.Text = " ";
            this.pageHeader1.UseLeftMargin = false;
            // 
            // pageHeader3
            // 
            this.pageHeader3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pageHeader3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.pageHeader3.Cursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader3.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold);
            this.pageHeader3.ForeColor = System.Drawing.Color.Snow;
            this.pageHeader3.Gap = 0;
            this.pageHeader3.HandCursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader3.HandDragFolder = false;
            this.pageHeader3.LocalizationDescription = "Tabs.Description";
            this.pageHeader3.LocalizationText = "Tabs.Text";
            this.pageHeader3.Location = new System.Drawing.Point(456, 0);
            this.pageHeader3.Name = "pageHeader3";
            this.pageHeader3.Size = new System.Drawing.Size(83, 28);
            this.pageHeader3.SubFont = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Bold);
            this.pageHeader3.SubGap = 0;
            this.pageHeader3.SubText = "";
            this.pageHeader3.TabIndex = 11;
            this.pageHeader3.Text = "粤检易";
            this.pageHeader3.UseLeftMargin = false;
            this.pageHeader3.UseSystemStyleColor = true;
            // 
            // ICO_PICBOX
            // 
            this.ICO_PICBOX.Image = global::质检工具.Properties.Resources.logo;
            this.ICO_PICBOX.Location = new System.Drawing.Point(0, 0);
            this.ICO_PICBOX.Name = "ICO_PICBOX";
            this.ICO_PICBOX.Size = new System.Drawing.Size(27, 28);
            this.ICO_PICBOX.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ICO_PICBOX.TabIndex = 13;
            this.ICO_PICBOX.TabStop = false;
            // 
            // ribbonDescriptionMenuItem1
            // 
            this.ribbonDescriptionMenuItem1.DescriptionBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ribbonDescriptionMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonDescriptionMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.Image")));
            this.ribbonDescriptionMenuItem1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.LargeImage")));
            this.ribbonDescriptionMenuItem1.Name = "ribbonDescriptionMenuItem1";
            this.ribbonDescriptionMenuItem1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem1.SmallImage")));
            this.ribbonDescriptionMenuItem1.Text = "ribbonDescriptionMenuItem1";
            // 
            // ribbonDescriptionMenuItem2
            // 
            this.ribbonDescriptionMenuItem2.DescriptionBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ribbonDescriptionMenuItem2.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonDescriptionMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem2.Image")));
            this.ribbonDescriptionMenuItem2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem2.LargeImage")));
            this.ribbonDescriptionMenuItem2.Name = "ribbonDescriptionMenuItem2";
            this.ribbonDescriptionMenuItem2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonDescriptionMenuItem2.SmallImage")));
            this.ribbonDescriptionMenuItem2.Text = "ribbonDescriptionMenuItem2";
            // 
            // ribbonButton21
            // 
            this.ribbonButton21.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton21.Image")));
            this.ribbonButton21.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton21.LargeImage")));
            this.ribbonButton21.Name = "ribbonButton21";
            this.ribbonButton21.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton21.SmallImage")));
            // 
            // ribbonButton22
            // 
            this.ribbonButton22.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton22.Image")));
            this.ribbonButton22.LargeImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton22.LargeImage")));
            this.ribbonButton22.Name = "ribbonButton22";
            this.ribbonButton22.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton22.SmallImage")));
            // 
            // Rib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 629);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.ribbon);
            this.ForeColor = System.Drawing.Color.Snow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Rib";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "质检工具";
            this.Load += new System.EventHandler(this.Rib_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.tabs1.ResumeLayout(false);
            this.mainPage_splitContainer.Panel1.ResumeLayout(false);
            this.mainPage_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPage_splitContainer)).EndInit();
            this.mainPage_splitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pageHeader1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ICO_PICBOX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon;
        private System.Windows.Forms.RibbonTab homeTab;
        private System.Windows.Forms.RibbonPanel settingsPanel;
        private System.Windows.Forms.RibbonButton ribbonButtonSaveProject;
        private System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.RibbonButton ribbonButtonOpenProject;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbonButtonGeneralCheck;
        private System.Windows.Forms.RibbonButton ribbonButtonProjectCheck;
        private System.Windows.Forms.RibbonButton ribbonButtonDataProcessing;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton ribbonButtonTaskList;
        private System.Windows.Forms.RibbonButton ribbonButtonRunLog;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton ribbonButtonTourGuide;
        private System.Windows.Forms.RibbonButton ribbonButtonHelpDoc;
        private AntdUI.Tabs tabs1;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonDescriptionMenuItem ribbonDescriptionMenuItem1;
        private System.Windows.Forms.RibbonDescriptionMenuItem ribbonDescriptionMenuItem2;
        private System.Windows.Forms.RibbonButton ribbonButtonDatabaseExport;
        private System.Windows.Forms.RibbonButton ribbonButtonResultDatabase;
        private System.Windows.Forms.RibbonButton ribbonButtonTemplateFolder;
        private System.Windows.Forms.RibbonButton ribbonButtonFolderExport;
        private System.Windows.Forms.RibbonButton ribbonButton21;
        private System.Windows.Forms.RibbonButton ribbonButton22;
        private AntdUI.Tree tree1;
        private RibbonPanel ribbonPanel6;
        private RibbonButton ribbonButtonDatabaseStructureTemplate;
        private RibbonButton ribbonButtonTopologyCheck;
        private RibbonButton ribbonButtonDictionaryAssignment;
        private RibbonButton ribbonButtonDatabaseCreateTemplate;
        private SplitContainer splitContainer1;
        private RibbonPanel ribbonPanel7;
        private RibbonButton ribbonButtonStandardReference;
        private RibbonButton ribbonButtonSoftwareInfo;
        private RibbonButton ribbonButtonSettings;
        private System.Windows.Forms.Panel panel2;
        private RibbonPanel ribbonPanel5;
        private RibbonLabel ribbonLabel1;
        private AntdUI.Input menuSearchInput;
        private TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public Carousel carousel1;
        public PageHeader pageHeader2;
        private System.Windows.Forms.TabPage tabPage2;
        private SplitContainer splitContainer3;
        private AntdUI.Panel panel1;
        private RibbonButton ribbonButtonTemplateDownload;
        private PictureBox ICO_PICBOX;
        public PageHeader pageHeader1;
        public PageHeader pageHeader3;
        private System.Windows.Forms.Button arcGisInitButton;
        private SplitContainer mainPage_splitContainer;
        private TableLayoutPanel tableLayoutPanel1;
        private AntdUI.Label label3;
        private RibbonButton ribbonButtonStatistics;
        private RibbonButton JTGJbutton;
    }
}