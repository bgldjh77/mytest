using System;
using System.Collections.Generic;
using System.Linq;

namespace 质检工具.Func.GISFunc.GISMap
{
    /// <summary>
    /// TOC管理器注册表 - 管理多个TOC控件实例
    /// </summary>
    public static class TOCManagerRegistry
    {
        private static readonly Dictionary<string, ITOCManager> _managers = new Dictionary<string, ITOCManager>();
        private static ITOCManager _defaultManager;
        
        /// <summary>
        /// 注册TOC管理器
        /// </summary>
        /// <param name="name">管理器名称</param>
        /// <param name="manager">管理器实例</param>
        /// <param name="setAsDefault">是否设为默认管理器</param>
        public static void Register(string name, ITOCManager manager, bool setAsDefault = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("管理器名称不能为空", nameof(name));
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));
                
            _managers[name] = manager;
            
            if (setAsDefault || _defaultManager == null)
                _defaultManager = manager;
        }
        
        /// <summary>
        /// 获取指定名称的TOC管理器
        /// </summary>
        /// <param name="name">管理器名称</param>
        /// <returns>TOC管理器实例</returns>
        public static ITOCManager Get(string name)
        {
            return _managers.TryGetValue(name, out ITOCManager manager) ? manager : null;
        }
        
        /// <summary>
        /// 获取默认的TOC管理器
        /// </summary>
        /// <returns>默认TOC管理器实例</returns>
        public static ITOCManager GetDefault()
        {
            return _defaultManager;
        }
        
        /// <summary>
        /// 注销TOC管理器
        /// </summary>
        /// <param name="name">管理器名称</param>
        public static void Unregister(string name)
        {
            if (_managers.TryGetValue(name, out ITOCManager manager))
            {
                if (manager == _defaultManager)
                {
                    _defaultManager = _managers.Values.FirstOrDefault(m => m != manager);
                }
                
                if (manager is IDisposable disposable)
                    disposable.Dispose();
                    
                _managers.Remove(name);
            }
        }
        
        /// <summary>
        /// 获取所有注册的管理器名称
        /// </summary>
        /// <returns>管理器名称列表</returns>
        public static IEnumerable<string> GetRegisteredNames()
        {
            return _managers.Keys.ToList();
        }
        
        /// <summary>
        /// 获取所有注册的管理器
        /// </summary>
        /// <returns>管理器实例列表</returns>
        public static IEnumerable<ITOCManager> GetAllManagers()
        {
            return _managers.Values.ToList();
        }
        
        /// <summary>
        /// 清空所有注册的管理器
        /// </summary>
        public static void Clear()
        {
            foreach (var manager in _managers.Values)
            {
                if (manager is IDisposable disposable)
                    disposable.Dispose();
            }
            
            _managers.Clear();
            _defaultManager = null;
        }
    }
}
