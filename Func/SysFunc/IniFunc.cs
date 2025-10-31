using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace 质检工具
{
    class IniFunc
    {
        //static string inipath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Zjzx_Config.ini";//ini配置文件路径
        public static string inipath = Application.StartupPath + "\\data\\config\\zjzx_config.ini";
        //获取
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string account,
            string progress_savepath,
            string defval,
            StringBuilder retval,
            int size,
            string filepath);
        //写入
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string section,
            string key,
            string val,
            string filepath);
        //获取方法
        //public static string getString(string section, string key, string def, string filename)
        public static string getString(string key)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString("Information", key, null, sb, 1024, inipath);
            //GetPrivateProfileString(section, key, def, sb, 1024, filename);
            return sb.ToString();
        }
        //写入方法
        //public static void writeString(string section, string key, string val, string filename)
        public static void writeString(string key, string val)
        {
            // WritePrivateProfileString(section, key, val, filename);
            WritePrivateProfileString("Information", key, val, inipath);
        }
    }
}
