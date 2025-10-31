using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具
{
    public partial class SpacialRef_SelectForm : AntdUI.Window
    {
        public string SelectedFilePath { get; private set; } = string.Empty;
        public SpacialRef_SelectForm()
        {
            InitializeComponent();
            
        }

        private void SpacialRef_SelectForm_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            // 加载坐标文件树
            loadSpacialFileTree();
            //展开部分节点
            expandTree();
            // 初始化计时器
            init(); 
        }
        //展开部分节点
        private void expandTree()
        {
            foreach(var item in spcaialRefTree.Items)
            {
                item.Expand = true;
                foreach(var subitem in item.Sub)
                {
                    subitem.Expand = true;
                }
            }
        }
        // 加载坐标文件树
        private void loadSpacialFileTree()
        {
            try
            {
                spcaialRefTree.Items.Clear();

                // 检查目录是否存在
                if (!Directory.Exists(globalpara.coordinate_txt_path))
                {
                    MessageBox.Show($"坐标目录不存在: {globalpara.coordinate_txt_path}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 创建根节点
                var rootNode = new AntdUI.TreeItem("坐标系统");

                // 递归加载目录
                LoadDirectory(globalpara.coordinate_txt_path, rootNode);

                // 添加根节点到树控件
                if (rootNode.Sub.Count > 0)
                {
                    spcaialRefTree.Items.Add(rootNode);
                }
                else
                {
                    rootNode.Text = "未找到坐标文件";
                    spcaialRefTree.Items.Add(rootNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载坐标文件树时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDirectory(string directoryPath, AntdUI.TreeItem parentNode)
        {
            try
            {
                // 获取当前目录下的所有文件
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    // 只显示特定扩展名的文件（可选）
                    // if (Path.GetExtension(file).ToLower() == ".prj" || Path.GetExtension(file).ToLower() == ".txt")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        var fileNode = new AntdUI.TreeItem(fileName)
                        {
                            Tag = file // 保存完整路径到Tag中
                        };
                        parentNode.Sub.Add(fileNode);
                    }
                }

                // 递归处理子目录
                foreach (var dir in Directory.GetDirectories(directoryPath))
                {
                    string dirName = new DirectoryInfo(dir).Name;
                    var dirNode = new AntdUI.TreeItem(dirName);
                    LoadDirectory(dir, dirNode);

                    // 只有子节点有内容才添加到树中
                    if (dirNode.Sub.Count > 0)
                    {
                        parentNode.Sub.Add(dirNode);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 忽略无权限访问的目录
            }
            catch (Exception ex)
            {
                // 记录其他异常
                Console.WriteLine($"Error loading directory {directoryPath}: {ex.Message}");
            }
        }

        private void spcaialRefTree_SelectChanged(object sender, TreeSelectEventArgs e)
        {
            spcaialRefTree.SelectItem.Expand = !spcaialRefTree.SelectItem.Expand;
        }
        #region//搜索
        private Timer timer;
        private AntdUI.TreeItemCollection originalTreeItems; // 保存原始树节点

        private void SearchInput_TextChanged(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
        }

        private void init()
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += onTimerTick;
            
            // 保存原始树结构
            if (spcaialRefTree.Items != null && spcaialRefTree.Items.Count > 0)
            {
                originalTreeItems =  CloneTreeItems(spcaialRefTree.Items);
            }
        }

        public void onTimerTick(object sender, EventArgs e)
        {
            SearchTree();
            timer.Stop();
        }

        private void SearchTree()
        {
            string searchText = SearchInput.Text.Trim().ToLower();
            
            // 如果搜索框为空，恢复原始树
            if (string.IsNullOrEmpty(searchText))
            {
                spcaialRefTree.Items.Clear();
                foreach (AntdUI.TreeItem item in originalTreeItems)
                {
                    if (item != null)
                    {
                        spcaialRefTree.Items.Add(item);
                    }
                }
                return;
            }

            // 创建新的树结构用于显示搜索结果
            var resultTree = new AntdUI.TreeItemCollection(new Tree());

            // 遍历原始树进行搜索
            foreach (AntdUI.TreeItem rootItem in originalTreeItems)
            {
                var matchedRoot = SearchInSubtree(rootItem, searchText);
                if (matchedRoot != null)
                {
                    resultTree.Add(matchedRoot);
                }
            }

            // 清空现有项并添加新的搜索结果
            spcaialRefTree.Items.Clear();
            foreach (AntdUI.TreeItem item in resultTree)
            {
                spcaialRefTree.Items.Add(item);
            }
        }

        private AntdUI.TreeItem SearchInSubtree(AntdUI.TreeItem node, string searchText)
        {
            // 检查当前节点是否匹配
            bool isMatch = node.Text.ToLower().Contains(searchText);

            // 检查子节点是否有匹配
            var matchedChildren = new List<AntdUI.TreeItem>();
            if (!isMatch)
            {
                foreach (var child in node.Sub)
                {
                    var matchedChild = SearchInSubtree(child, searchText);
                    if (matchedChild != null)
                    {
                        matchedChildren.Add(matchedChild);
                    }
                }
            }

            // 如果当前节点或子节点有匹配，则保留该节点
            if (isMatch || matchedChildren.Count > 0)
            {
                var resultNode = new AntdUI.TreeItem(node.Text)
                {
                    Tag = node.Tag,
                    Expand = true // 展开匹配的节点
                };

                // 添加匹配的子节点
                foreach (var child in matchedChildren)
                {
                    resultNode.Sub.Add(child);
                }

                return resultNode;
            }

            return null;
        }

        // 深度克隆树节点
        private AntdUI.TreeItemCollection CloneTreeItems(AntdUI.TreeItemCollection items)
        {
            var clonedItems = new AntdUI.TreeItemCollection(new Tree());
            foreach (AntdUI.TreeItem item in items)
            {
                clonedItems.Add(CloneTreeItem(item));
            }
            return clonedItems;
        }

        private AntdUI.TreeItem CloneTreeItem(AntdUI.TreeItem item)
        {
            var clonedItem = new AntdUI.TreeItem(item.Text)
            {
                Tag = item.Tag,
                Expand = item.Expand
            };

            if (item.Sub != null && item.Sub.Count > 0)
            {
                foreach (AntdUI.TreeItem subItem in item.Sub)
                {
                    clonedItem.Sub.Add(CloneTreeItem(subItem));
                }
            }

            return clonedItem;
        }

        #endregion

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (spcaialRefTree.SelectItem.Sub.Count == 0)
            {
                SelectedFilePath = (string)spcaialRefTree.SelectItem.Tag;
            }
            else { SelectedFilePath = ""; }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
