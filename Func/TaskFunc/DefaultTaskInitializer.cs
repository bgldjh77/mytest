using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using �ʼ칤��.Func.SysFunc;
using �ʼ칤��.GpTask;
using �ʼ칤��.GpTask.GpTaskConfig;
using �ʼ칤��.Menu;
using �ʼ칤��.MenuConfig;

namespace �ʼ칤��.Func.TaskFunc
{
    /// <summary>
    /// Ĭ�������ʼ���������ڴ����ͳ�ʼ��Ĭ������
    /// </summary>
    public class DefaultTaskInitializer
    {
        /// <summary>
        /// Ԥ���������ã����ڳ�ʼ��Ĭ������
        /// </summary>
        private static readonly List<DefaultTaskInfo> DefaultTasks = new List<DefaultTaskInfo>
        {
            new DefaultTaskInfo
            {
                FolderName = "��Ŀ���",
                ToolboxName = "դ����",
                ToolsetName = "DOM���",
                ToolName = "���ݳ����ں������",
                SkipArgs = new[] { "����·��", "�����Զ����Ͽ�" }
            },
            // ��������µ���������
            new DefaultTaskInfo
            {
                FolderName = "��Ŀ���",
                ToolboxName = "դ����",
                ToolsetName = "DOM���",  // DOM���
                ToolName = "DOM�ʼ��������",
                SkipArgs = new[] { "����·��" } // ����ʵ�����������Ҫ�����Ĳ���
            },
            // ��Ӻ������������
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "����ͼ���飨�������ַ��",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "�������ݿ�����ʸ���Ա�",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "927����ʸ���Ա�",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "������ʸ�����ԶԱȣ��뺣�����������ݣ�",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "��·��ˮϵ�����߼����",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "��������ͼ��ʸ�����ԶԱȼ��߼���飨���òҪ�أ�",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "�߼����",
                 ToolName = "���ݿ��߼����",
                 SkipArgs = new[] { "����·��" }
             },
             new DefaultTaskInfo
             {
                 FolderName = "��Ŀ���",
                 ToolboxName = "������",
                 ToolsetName = "������",
                 ToolName = "ͳ�Ƽ�����ݿ�",
                 SkipArgs = new[] { "����·��" }
             }
             
             
            // ���Լ�����Ӹ���Ԥ������
        };

        /// <summary>
        /// ��ʼ������Ĭ������
        /// </summary>
        /// <returns>��ʼ���Ƿ�ɹ�</returns>
        public static bool InitializeDefaultDomCheckTask()
        {
            bool anySuccess = false;

            // ��ʼ������DOM���Ԥ������
            foreach (var taskInfo in DefaultTasks)
            {
                if (taskInfo.ToolsetName == "DOM���")
                {
                    bool success = InitializeTask(taskInfo);
                    //LogHelper.normallog($"��ʼ������ {taskInfo.ToolName} {(success ? "�ɹ�" : "ʧ��")}");
                    anySuccess = anySuccess || success;
                }
            }

            return anySuccess;
        }

        /// <summary>
        /// ��ʼ�����к��������Ԥ������
        /// </summary>
        /// <returns>��ʼ���Ƿ�ɹ�</returns>
        public static bool InitializeDefaultCoastalCheckTask()
        {
            bool anySuccess = false;

            // ��ʼ�����к��������Ԥ������
            foreach (var taskInfo in DefaultTasks)
            {
                if (taskInfo.ToolboxName == "������")
                {
                    bool success = InitializeTask(taskInfo);
                    anySuccess = anySuccess || success;
                }
            }

            return anySuccess;
        }

        /// <summary>
        /// ��ʼ������Ĭ�����񣨰���DOM�ͺ�������飩
        /// </summary>
        /// <returns>��ʼ���Ƿ�ɹ�</returns>
        public static bool InitializeAllDefaultTasks()
        {
            bool domSuccess = InitializeDefaultDomCheckTask();
            bool coastalSuccess = InitializeDefaultCoastalCheckTask();

            return domSuccess || coastalSuccess;
        }

        /// <summary>
        /// ����������Ϣ��ʼ��ָ������
        /// </summary>
        /// <param name="taskInfo">������Ϣ</param>
        /// <returns>��ʼ���Ƿ�ɹ�</returns>
        private static bool InitializeTask(DefaultTaskInfo taskInfo)
        {
            try
            {
                // ����Ƿ��Ѵ��ڴ�����
                bool taskExists = globalpara.GpBeanList.Any(gb =>
                    gb.menuBean?.toolName == taskInfo.ToolName &&
                    gb.menuBean?.toolsetName == taskInfo.ToolsetName &&
                    gb.state == GpStateEnum.δ��ʼ);

                if (taskExists)
                {
                    return true;
                }

                // ����MenuBean����
                MenuBean menuBean = new MenuBean
                {
                    folderName = taskInfo.FolderName,
                    toolboxName = taskInfo.ToolboxName,
                    toolsetName = taskInfo.ToolsetName,
                    toolName = taskInfo.ToolName
                };

                // ���ع�������
                csvFunc cf = new csvFunc();
                ToolArgs toolArgsTemplate = new ToolArgs();
                List<string> lines = cf.LoadToolConfig(menuBean, globalpara.toolconfigpath);

                // �����Ҳ������õ����
                if (lines.Count == 0)
                {
                    lines = cf.LoadToolConfig(taskInfo.ToolName);
                    if (lines.Count == 0)
                    {
                        LogHelper.ErrorLog($"��ʼ��Ĭ������ʧ��: {taskInfo.ToolName}", "�޷��ҵ���������");
                        return false;
                    }
                }

                // ���������б�
                List<ToolArgs> ta = new List<ToolArgs>();
                HashSet<string> processedArgs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                HashSet<string> skipArgs = new HashSet<string>(taskInfo.SkipArgs ?? new string[0], StringComparer.OrdinalIgnoreCase);

                foreach (string line in lines)
                {
                    ToolArgs arg = toolArgsTemplate.CreatArgBean(line);

                    // �����ظ�������ָ�������Ĳ���
                    if (processedArgs.Contains(arg.argName) || skipArgs.Contains(arg.argName))
                        continue;

                    // ���Ϊ�Ѵ���
                    processedArgs.Add(arg.argName);

                    // ȷ�������Ĺ���·����menuBeanһ��
                    arg.folderName = menuBean.folderName;
                    arg.toolboxName = menuBean.toolboxName;
                    arg.toolsetName = menuBean.toolsetName;
                    arg.toolName = menuBean.toolName;

                    // ����Ĭ�ϲ���ֵ
                    if (taskInfo.DefaultValues != null && taskInfo.DefaultValues.TryGetValue(arg.argName, out string customValue))
                    {
                        arg.nowValue = customValue;
                    }
                    else
                    {
                        arg.nowValue = arg.defalutValue;
                    }

                    ta.Add(arg);
                }

                if (ta.Count == 0)
                {
                    LogHelper.ErrorLog($"��ʼ��Ĭ������ʧ��: {taskInfo.ToolName}", "δ�ܴ����κβ���");
                    return false;
                }

                // ������� - ���"�Զ�"��ʶ��
                string autoId = "�Զ�_" + Guid.NewGuid().ToString("N"); // ����һ����"�Զ�"ǰ׺��Ψһ��ʶ��
                GpTaskFunc gpTaskFunc = new GpTaskFunc();
                gpTaskFunc.AddGpTask(menuBean, ta, GpStateEnum.δ��ʼ, autoId, false);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog($"��ʼ��Ĭ�������쳣: {taskInfo.ToolName}", ex);
                return false;
            }
        }

        /// <summary>
        /// Ĭ��������Ϣ�࣬��������Ԥ������
        /// </summary>
        private class DefaultTaskInfo
        {
            /// <summary>�ļ�������</summary>
            public string FolderName { get; set; }

            /// <summary>����������</summary>
            public string ToolboxName { get; set; }

            /// <summary>���߼�����</summary>
            public string ToolsetName { get; set; }

            /// <summary>��������</summary>
            public string ToolName { get; set; }

            /// <summary>Ҫ�����Ĳ��������б�</summary>
            public string[] SkipArgs { get; set; }

            /// <summary>�Զ������Ĭ��ֵ����Ϊ��������ֵΪĬ��ֵ</summary>
            public Dictionary<string, string> DefaultValues { get; set; }
        }
    }
}