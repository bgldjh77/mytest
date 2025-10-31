using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace 质检工具.SysFunc
{
    class SelectFileFunc
    {
        public string sf_savefile(string extension )
        {
            string filepath = "";
            // 创建 SaveFileDialog 实例
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // 设置对话框的标题
            saveFileDialog.Title = "保存文件";

            // 设置默认的文件扩展名
            saveFileDialog.DefaultExt = extension;

            // 设置过滤器，指定可保存的文件类型
            saveFileDialog.Filter = $"文件 (*.{extension})|*.{extension}|所有文件 (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string projectfile = saveFileDialog.FileName;
                return projectfile;
            }

            return filepath;
        }
        public string sf_selectTableFromMDB()
        {
            // 打开文件选择对话框，选择 MDB 文件
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择 MDB 文件",
                Filter = "Access 数据库 (*.mdb)|*.mdb",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string mdbPath = openFileDialog.FileName;

                // 打开 MDB 文件工作空间
                IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactory();
                IWorkspace workspace = workspaceFactory.OpenFromFile(mdbPath, 0);
                IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;

                // 获取所有表的名称
                List<string> tableNames = new List<string>();
                IEnumDataset enumDataset = workspace.get_Datasets(esriDatasetType.esriDTTable);
                IDataset dataset;
                while ((dataset = enumDataset.Next()) != null)
                {
                    tableNames.Add(dataset.Name);
                }

                // 显示表选择对话框
                using (Form selectTableForm = new Form())
                {
                    selectTableForm.Text = "选择表";
                    selectTableForm.Width = 400;
                    selectTableForm.Height = 300;

                    ListBox listBox = new ListBox
                    {
                        Dock = DockStyle.Fill,
                        DataSource = tableNames
                    };
                    selectTableForm.Controls.Add(listBox);

                    Button okButton = new Button
                    {
                        Text = "确定",
                        Dock = DockStyle.Bottom
                    };
                    selectTableForm.Controls.Add(okButton);

                    string selectedTable = null;
                    okButton.Click += (s, e) =>
                    {
                        selectedTable = listBox.SelectedItem?.ToString();
                        selectTableForm.DialogResult = DialogResult.OK;
                        selectTableForm.Close();
                    };

                    if (selectTableForm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(selectedTable))
                    {
                        return $"{mdbPath}\\{selectedTable}"; // 返回完整路径
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// 文件
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string sf_selectfile(string extension,string lastFolderPath="")
        {
            string scfile = "";
            OpenFileDialog openFielDialog = new OpenFileDialog();
            string filterstr = "(*." + extension + ")| *." + extension;
            if (extension == "doc" | extension == "docx")
            {
                filterstr = "(*.doc,*.docx)| *.doc;*.docx";
            }
            else if (extension == "")
            {
                filterstr = "文件 (*.*)|*.*";
            }
            if (!string.IsNullOrEmpty(lastFolderPath) && Directory.Exists(lastFolderPath))
            {
                openFielDialog.InitialDirectory = lastFolderPath;
            }
            openFielDialog.Title = "请选择文件";
            openFielDialog.Filter = filterstr;
            openFielDialog.Multiselect = false;
            openFielDialog.RestoreDirectory = true;
            if (openFielDialog.ShowDialog() == DialogResult.OK)
            {
                scfile = openFielDialog.FileName;


            }
            return scfile;
        }
        /// <summary>
        /// 文件夹
        /// </summary>
        /// <param name="lastFolderPath"></param>
        /// <returns></returns>
        //public string sf_folder(string lastFolderPath="",string notice= "请选择文件夹")
        //{
        //    string path = null;
            
        //    // 使用传统的FolderBrowserDialog来避免DPI缩放问题
        //    // CommonOpenFileDialog在非100%缩放下会触发DPI重新感知
        //    using (FolderBrowserDialog dialog = new FolderBrowserDialog())
        //    {
        //        dialog.Description = notice;
        //        dialog.ShowNewFolderButton = true;
                
        //        if (!string.IsNullOrEmpty(lastFolderPath) && Directory.Exists(lastFolderPath))
        //        {
        //            dialog.SelectedPath = lastFolderPath;
        //        }
                
        //        if (dialog.ShowDialog() == DialogResult.OK)
        //        {
        //            path = dialog.SelectedPath;
        //        }
        //    }
            
        //    return path;
        //}
        public string sf_folder(string lastFolderPath = "", string notice = "请选择文件夹")
        {
            string path = null;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog(notice);
            if (lastFolderPath != null | lastFolderPath != "")
            {
                dialog.InitialDirectory = lastFolderPath;
            }
            dialog.IsFolderPicker = true; //选择文件还是文件夹（true:选择文件夹，false:选择文件）
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                path = dialog.FileName;
                //MessageBox.Show(path);
            }
            return path;
        }
        //工作空间 mdb gdb
        public string sf_workspace( string notice = "请选择工作空间")
        {
            // 1. 创建 GxDialog 实例
            IGxDialog gxDialog = new GxDialogClass();
            gxDialog.Title = notice;
            gxDialog.AllowMultiSelect = false; // 只允许单选

            // 2. 创建并设置对象过滤器，这是最关键的一步
            // 我们需要一个过滤器集合来同时容纳 GDB 和 MDB 两种类型
            IGxObjectFilterCollection filterCollection = gxDialog as IGxObjectFilterCollection;

            // 添加文件地理数据库过滤器 (.gdb)
            filterCollection.AddFilter(new GxFilterFileGeodatabasesClass(), false);

            // 添加个人地理数据库过滤器 (.mdb)
            filterCollection.AddFilter(new GxFilterPersonalGeodatabasesClass(), false);

            // 将集合赋给对话框的过滤器属性
            //gxDialog.ObjectFilter = filterCollection as IGxObjectFilter;

            // 3. 显示对话框并获取用户的选择
            IEnumGxObject selectedObjects = null;
            // DoModalOpen 的第一个参数是父窗口句柄，0 表示没有父窗口
            if (!gxDialog.DoModalOpen(0, out selectedObjects))
            {
                // 用户点击了“取消”按钮
                return null;
            }

            // 4. 处理返回结果
            if (selectedObjects == null)
            {
                return null;
            }

            // 因为我们设置了 AllowMultiSelect = false，所以只取第一个结果
            IGxObject selectedObject = selectedObjects.Next();

            if (selectedObject != null)
            {
                // IGxObject.FullName 属性提供了我们需要的完整路径
                return selectedObject.FullName;
            }

            return null;
        }


        //选择要素类
        public List<string> OpenGxDialog_FC()
        {
            List<string> fc_fullpathlist = new List<string>();

            // 1. 创建 GxDialog 实例
            IGxDialog gxDialog = new GxDialogClass();
            gxDialog.Title = "请选择一个或多个要素类";
            gxDialog.AllowMultiSelect = true;

            // 2.【关键修改】设置对象过滤器，使其只接受要素类
            // 这将从UI层面限制用户的选择，只能选择要素类。
            gxDialog.ObjectFilter = new GxFilterFeatureClassesClass();

            // 3. 打开对话框
            IEnumGxObject selectedItems;
            if (gxDialog.DoModalOpen(0, out selectedItems) == true)
            {
                if (selectedItems == null)
                {
                    return fc_fullpathlist; // 如果没有选择任何内容，则返回空列表
                }

                // 4. 遍历选择的对象并获取其完整路径
                selectedItems.Reset();
                IGxObject gxObject = selectedItems.Next();
                while (gxObject != null)
                {
                    // 【简化路径获取】
                    // 因为我们已经使用了过滤器，所以可以确信 gxObject 是一个要素类。
                    // IGxObject.FullName 属性直接提供了我们需要的完整路径，
                    // 无需像原代码那样手动拼接工作空间、要素数据集和要素类名称。
                    fc_fullpathlist.Add(gxObject.FullName);

                    Console.WriteLine("已选择要素类: " + gxObject.FullName);

                    gxObject = selectedItems.Next();
                }
            }
            else
            {
                Console.WriteLine("用户取消了选择。");
            }

            return fc_fullpathlist;
        }
        public List<string> OpenGxDialog_FC_bk()
        {
            List<string> fc_fullpathlist = new List<string>();
            // 创建 GxDialog 实例

            //Type type = typeof(GxDialogClass);
            //object obj = Activator.CreateInstance(type);
            //IGxDialog gxDialog = obj as IGxDialog;
            IGxDialog gxDialog = new GxDialogClass();


            // 设置对话框标题
            gxDialog.Title = "请选择文件或数据集";

            // 允许多选
            gxDialog.AllowMultiSelect = true;
            IEnumGxObject selectedItems;
            // 打开对话框，用户选择文件或目录后返回
            if (gxDialog == null)
            {
                MessageBox.Show("s");

            }



            //Console.WriteLine(gxDialog.DoModalOpen(0, out selectedItems));

            if (gxDialog != null && gxDialog.DoModalOpen(0, out selectedItems) == true)
            {
                // 获取选择的项
                selectedItems.Reset();
                IGxObject gxObject = selectedItems.Next();
                while (gxObject != null)
                {
                    // 判断对象是否为要素类
                    if (gxObject is IGxDataset gxDataset)
                    {
                        // 检查数据集类型是否为要素类
                        if (gxDataset.Dataset is IFeatureClass)
                        {
                            // 获取数据集的工作空间
                            IWorkspace workspace = gxDataset.Dataset.Workspace;
                            if (workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                            {

                                // 文件系统工作空间
                                IWorkspaceName workspaceName = ((IDataset)workspace).FullName as IWorkspaceName;
                                string gdb = workspaceName.PathName;
                                string featureClassPath = gdb;
                                IFeatureClass featureClass = gxDataset.Dataset as IFeatureClass;
                                string datasetname = "";
                                if (featureClass.FeatureDataset != null)
                                {
                                    datasetname = featureClass.FeatureDataset.BrowseName;
                                }

                                if (datasetname != "")
                                {
                                    featureClassPath += "\\" + datasetname + "\\" + gxDataset.Dataset.Name;
                                }
                                else
                                {
                                    featureClassPath += "\\" + gxDataset.Dataset.Name;
                                }
                                fc_fullpathlist.Add(featureClassPath);
                                Console.WriteLine(featureClassPath);
                                //return featureClassPath;
                            }
                        }
                    }
                    // 输出选择的对象的路径或名称
                    //Console.WriteLine(gxObject.Name);
                    gxObject = selectedItems.Next();
                }
            }
            else
            {
                Console.WriteLine("用户取消了选择。");
            }
            return fc_fullpathlist;
        }
    }
}
