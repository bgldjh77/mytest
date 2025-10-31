using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace 质检工具.MenuConfig
{
    class DefaultValue
    {
        static List<string> replacestr = new List<string> { "%成果数据库%", "%模板文件夹%", "%输出数据库%", "%输出文件夹%" };
        public static string getdefaultvalue(string inputstr)
        {
            for(int i=0;i< replacestr.Count;i++)
            {
                if (inputstr.StartsWith(replacestr[i]))
                {
                    string rsp = "";
                    string newstr = "";
                    if (i == 0)
                    {
                        rsp = IniFunc.getString("resultdb");                       
                    }
                    else if (i == 1)
                    {
                        rsp = IniFunc.getString("mufolder");
                    }
                    else if (i == 2)
                    {
                        rsp = IniFunc.getString("outdb");
                    }
                    else if (i == 3)
                    {
                        rsp = IniFunc.getString("outfolder");
                    }
                    if (Directory.Exists(rsp)) 
                    { newstr = inputstr.Replace(replacestr[i], rsp); }
                     
                    return newstr;
                }
            }
            return inputstr;
        }
    }
}
