using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace 质检工具
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 启用 Per-Monitor V2 DPI 感知（失败则回退）
            //EnableDpiAwareness();

            BindExceptionHandler();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppLifecycle.Instance.Reset();//判断初始化状态
            //Application.Run(new AntdUIForm());
            Application.Run(new LoginForm());
            //Application.Run(new Rib());
            
          

        }
        /// <summary>
        /// 启用高 DPI 感知（优先 PerMonitorV2，回退 SetProcessDPIAware）
        /// </summary>
        private static void EnableDpiAwareness()
        {
            try
            {
                // DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = (IntPtr)(-4)
                var PER_MONITOR_V2 = new IntPtr(-4);
                SetProcessDpiAwarenessContext(PER_MONITOR_V2);
                return;
            }
            catch { }

            try
            {
                SetProcessDPIAware();
            }
            catch { }
        }
        /// <summary>
        /// 错误后继续
        /// </summary>
        private static void BindExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        //ui
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogHelper.ErrorLog(null, e.Exception as Exception);
        }
        //处理未捕获的异常
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.ErrorLog(null, e.ExceptionObject as Exception);
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDpiAwarenessContext(System.IntPtr value);
    }
}
