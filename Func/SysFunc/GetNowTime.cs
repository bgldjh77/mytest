using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 质检工具
{
    class GetNowTime
    {
        public static string nowTime()
        {
            DateTime currentTime = DateTime.Now;
            // 使用自定义格式字符串将日期转换为带有0占位的字符串
            string formattedDate = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            string nt = formattedDate.ToString() + " | ";
            //nt = nt.Replace("/","-");
            return nt;
            //Console.WriteLine(currentTime.ToString());
        }
        public static string nowtime2()
        {
            DateTime currentTime = DateTime.Now;
            // 使用自定义格式字符串将日期转换为带有0占位的字符串
            string formattedDate = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            return formattedDate;
        }
        public static string runtimestr(TimeSpan elapsedTime)
        {
            string str = elapsedTime.ToString().Substring(0, 10);

            if (elapsedTime.TotalMinutes < 1)
            {
                string sub6 = str.Substring(6);
                if (sub6.StartsWith("0"))
                {
                    sub6 = sub6.Substring(1);
                }
                //return str.Substring(6)+"秒";
                return sub6 + "秒";
            }
            else { str = str.Split('.')[0]; }
            return str;
        }
    }
}
