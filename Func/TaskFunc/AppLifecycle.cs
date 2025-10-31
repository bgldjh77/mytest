using System;
using 质检工具.Func.GISFunc;

namespace 质检工具
{
    /// <summary>
    /// 应用程序生命周期管理类，用于控制初始化状态和阶段性任务
    /// </summary>
    public class AppLifecycle
    {
        #region 单例实现
        private static readonly AppLifecycle _instance = new AppLifecycle();

        /// <summary>
        /// 获取应用程序生命周期管理的单例实例
        /// </summary>
        public static AppLifecycle Instance => _instance;

        // 私有构造函数防止外部创建实例
        private AppLifecycle()
        {
            // 重要：确保初始状态正确
            _isInInitializationPhase = true;
           

            
        }
        #endregion

        /// <summary>
        /// 应用程序是否处于初始化阶段
        /// </summary>
        private bool _isInInitializationPhase = true;

        
        /// <summary>
        /// 获取或设置应用程序是否处于初始化阶段
        /// </summary>
        public bool IsInInitializationPhase
        {
            get
            {
                // 添加检查和日志
                var stackTrace = new System.Diagnostics.StackTrace();
                var callingMethod = stackTrace.GetFrame(1)?.GetMethod();
               
                return _isInInitializationPhase;
            }
            private set { _isInInitializationPhase = value; }
        }

       
        /// <summary>
        /// 标记初始化阶段结束
        /// </summary>
        public void EndInitialization()
        {
           
            _isInInitializationPhase = false;
            
        }

        /// <summary>
      

        /// <summary>
        /// 重置生命周期状态（在应用程序启动时调用）
        /// </summary>
        public void Reset()
        {
           
            _isInInitializationPhase = true;
           
        }
    }
}