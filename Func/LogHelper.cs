using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace 质检工具
{
    class LogHelper 
    {
        //static string logpath = Application.StartupPath + Properties.Settings.Default.logpath;
        //static string errlogpath = globalpara.errlogpath;
        static Font font = new Font("Microsoft YaHei UI", 12f);

        public static void ErrorLog(string text, Exception ex)
        {
            string errmsg = ex.Message;          
            if (text == null) { text = "发生错误"; }
            string nowtime = GetNowTime.nowTime();
            Errnotice(text, errmsg);
            text = nowtime + text;
            add2mainlog(text + errmsg, Color.Red);
            writeErrLog( text + errmsg);
        }
        public static void ErrorLog(string text, string errmsg)
        {
            if (text == null) { text = "发生错误"; }
            string nowtime = GetNowTime.nowTime();
            Errnotice(text, errmsg);
            text = nowtime + text;
            add2mainlog(text + errmsg, Color.Red);
            writeErrLog(text + errmsg);
        }
        public static void normallog( string msg)
        {
            //if (text == null) { text = "消息"; }
            string text = "消息";
            string nowtime = GetNowTime.nowTime();
            Infonotice(text, msg);
            add2mainlog(nowtime+ msg);
            writeMainLog( nowtime + text);
        }
        //错误弹窗
        private static void Errnotice(string title, string msg)
        {
            Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
            //AntdUI.Notification.error(mf, title, msg, AntdUI.TAlignFrom.TR);
            if (mf == null) return;
            // 回到主线程显示通知
            mf.BeginInvoke((Action)(() =>
            {
                //if (mf.WindowState == FormWindowState.Minimized)
                //{
                //    mf.WindowState = FormWindowState.Normal;
                //}
                //mf.Activate();
                AntdUI.Notification.error(mf, title, msg, AntdUI.TAlignFrom.BR, font);
            }));
        }
        //消息弹窗
        public static void Infonotice(string title,string msg)
        {
            Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
            mf.BeginInvoke((Action)(() =>
            {              
                AntdUI.Notification.info(mf, title, msg, AntdUI.TAlignFrom.BR, font);
            }));
            
        }
        //成功弹窗
        public static void Successnotice( string msg,int closecount =6)
        {
            string title = "成功";
            Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
            mf.BeginInvoke((Action)(() =>
            {
                AntdUI.Notification.success(mf, title, msg, AntdUI.TAlignFrom.BR, font, closecount);
            }));
            add2mainlog(GetNowTime.nowTime() + msg);
        }
        //处理gp工具消息
        public static string gpstrspilter(string inputstr)
        {
            string newstr = "";
            if (inputstr.Trim() != ""&& !inputstr.Contains("Cancelled script "))
            {
                bool goon = false;
                List<string> resultList = new List<string>(inputstr.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
                for (int i = 0; i < resultList.Count; i++)
                {
                    if (resultList[i].Contains("正在运行脚本 ") | resultList[i].Contains("Running script "))
                    {
                        goon = true;
                    }
                    else if (resultList[i].Contains("Completed script "))
                    {
                        //goon = false;
                        break;
                    }
                    if (goon && !resultList[i].Contains("正在运行脚本 ") && !resultList[i].Contains("Running script "))
                    {
                        newstr += resultList[i] + "\n";
                    }

                }
            }
            return newstr;
        }
        #region//日志文件
        public static void writeErrLog(string inputstr)
        {

            writelogloc(globalpara.errlogpath, inputstr);
        }
        public static void writeMainLog(string inputstr)
        {
            try
            {
                writelogloc(globalpara.mainlogpath, inputstr);
                writelogloc(globalpara.tmplogpath, inputstr);
            }
            catch(Exception ex) { ErrorLog("文件写入错误", ex); }
            
        }

        // 定义一个静态对象作为锁
        private static readonly object fileLock = new object();
        //日志本地存储
        private static void writelogloc(string txtlog, string inputstr)
        {

            if (!File.Exists(txtlog))
            {
                File.Create(txtlog).Close();
            }
            if (inputstr == null) { return; }
            string[] lines = inputstr.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                string newline = line + "\n";
                // 将字符串写入文本文件，每次写入字符串后换行
                lock (fileLock) // 确保同一时间只有一个线程写入文件
                {
                    using (StreamWriter writer = new StreamWriter(txtlog, true))
                    {
                        writer.WriteLine(newline); // 写入字符串并换行
                    }
                }
                    
            }
        }
        //private static void writelogloc(string txtlog, string inputstr)
        //{

        //    if (!File.Exists(txtlog))
        //    {
        //        File.Create(txtlog).Close();
        //    }
        //    if (inputstr == null) { return; }
        //    string[] lines = inputstr.Split(new string[] { "\r\n" }, StringSplitOptions.None);

        //    foreach (string line in lines)
        //    {
        //        string newline = line + "\n";
        //        // 将字符串写入文本文件，每次写入字符串后换行

        //        using (StreamWriter writer = new StreamWriter(txtlog, true))
        //        {
        //            //string text = "Hello, World!"; // 要写入的字符串
        //            writer.WriteLine(newline); // 写入字符串并换行

        //            // 可以继续写入其他字符串，每次写入字符串后都会换行
        //            //writer.WriteLine("This is a new line.");
        //        }
        //    }
        //}
        #endregion


        #region//主日志窗体
        public static void add2mainlog_time(string input)
        {
            add2mainlog(GetNowTime.nowTime()+ input, Color.Black);
        }
        public static void add2mainlog(string input)
        {
            if (input == null) { input = ""; }
            add2mainlog(input, Color.Black);
        }
        public static void add2mainlog(string input,Color color)
        {
            globalpara.mainlog += input;
            MainLogForm mlf = Application.OpenForms.OfType<MainLogForm>().FirstOrDefault();
            if (mlf != null)
            {
                mlf.uiRichTextBox1.Invoke((MethodInvoker)delegate
                {
                    // 追加黑色文本
                    //mlf.uiRichTextBox1.SelectionColor = Color.Black;
                    mlf.uiRichTextBox1.SelectionColor = color;
                    mlf.uiRichTextBox1.AppendText(input + "\n");
                });
            }

            writeMainLog( input);


        }
        #endregion
    }
}
