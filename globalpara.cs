using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using 质检工具.GpTask.GpTaskConfig;

namespace 质检工具
{
    class globalpara
    {
        public static string errlogpath = Application.StartupPath + @"\log\errlog.log";//错误日志
        public static string mainlogpath= Application.StartupPath + @"\log\mainlog.log";//运行日志
        public static string tmplogpath = Application.StartupPath + @"\log\tmplog.log";//本次日志
        public static string helpdocument = Application.StartupPath + @"\data\用户手册.pdf";//帮助文档
        public static string dbpath= Application.StartupPath + @"\data\config\zjzx_zjrj.db";//软件数据库路径
        public static string savemxdpath = Application.StartupPath + @"\data\config\project.mxd";//软件工程保存的mxd
        public static string taskcachepath = Application.StartupPath + @"\data\config\task.csv";//任务列表
        public static string toolconfigpath = Application.StartupPath + @"\data\tool\config.csv";//工具配置文件
        public static string tooldescpath = Application.StartupPath + @"\data\tool\工具描述.xlsx";//工具描述文件
        public static string updatedespath= Application.StartupPath + @"\data\updateInfo.txt";//版本更新描述文件
        public static string mu_rulepath = Application.StartupPath + @"\data\mu_src\rule";//规则模板文件夹 主页
        public static string mu_markpath= Application.StartupPath + @"\data\mu_src\mark";//评分模板文件夹 模板下载
        public static string mu_standard= Application.StartupPath + @"\data\mu_src\standard";//标准查阅文件夹 暂时是 pdf
        public static string mu_task = Application.StartupPath + @"\data\mu_src\task";//预设任务
        public static string inipath= Application.StartupPath + @"\data\config.ini"; //软件ini配置
        public static string svgpath = Application.StartupPath + @"\data\svg"; //svg图标
        public static string homepagepicpath = Application.StartupPath + @"\data\svg\homepage";//主页图片文件夹
        public static string sidetoolbox = Application.StartupPath + @"\data\sidetool\sidetool.tbx";//其他工具
        public static string translate_xml = Application.StartupPath + @"\data\config\Translator\ARCGIS2ISO19139.xml";//Esri 元数据转换模板 生成工具描述xml
        public static string coordinate_txt_path = Application.StartupPath + @"\data\coordinate";//坐标文件夹
        public static string controlcsvpath = Application.StartupPath + @"\data\plug\数据统计\Control.csv"; // Control.csv 文件路径
        public static string mappingcsvpath = Application.StartupPath + @"\data\plug\数据统计\mapping.csv"; // mapping.csv 文件路径
        public static Dictionary<string, string> gptasklist=new Dictionary<string, string>();
        public static string mainlog = "";//日志
        public static List<GpBean> GpBeanList = new List<GpBean>();//gp任务列表
                                                                  
        //public static bool IsMainFormInitialized = true; // 添加一个静态字段表示主窗体是否完成了初始化
        public static string defaultGdbPath = "";//默认工作空间
        public static bool isArcGisInitialized = false;// GIS控件初始化完成标志

    }
}
