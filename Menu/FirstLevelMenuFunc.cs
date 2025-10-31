using AntdUI;
using AntdUI.Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.SysFunc;
using 质检工具.Menu;
using 质检工具.MenuConfig;
using MenuItem = AntdUI.MenuItem;

namespace 质检工具
{
    class FirstLevelMenuFunc
    {
        


        //string configpath = globalpara.toolconfigpath;
        //获取csv第一列不重复值
        public static List<string> GetUniqueFirstColumn(string filePath, int colindex)
        {
            HashSet<string> uniqueValues = new HashSet<string>();

            try
            {
                int count = 0;
                // 使用 ANSI (GB2312) 编码打开文件
                using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("GB2312")))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        
                        if (count > 0)
                        {
                            // 按逗号分割每一行
                            string[] columns = line.Split(',');

                            // 确保至少有一列
                            if (columns.Length > 0)
                            {
                                string firstColumnValue = columns[colindex].Trim(); // 去掉可能的多余空格
                                uniqueValues.Add(firstColumnValue); // 自动去重
                            }
                        }
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"读取文件时出错: {ex.Message}");
                LogHelper.ErrorLog("读取文件时出错",ex);
            }

            // 将HashSet转换为List
            return new List<string>(uniqueValues);
        }
        /// <summary>
        /// 加载一级菜单
        /// </summary>
        /// <param name="segmented1"></param>
        public void loadfirstmenu(Segmented segmented1)
        {

            segmented1.Items.Clear();
            //Application.DoEvents();//强制刷新
            List<string> firstmenu = GetUniqueFirstColumn(globalpara.toolconfigpath, 0);

            for (int i = 0; i < firstmenu.Count; i++)
            {
                SegmentedItem item = new SegmentedItem();
                item.Text = firstmenu[i];
                segmented1.Items.Add(item);
            }
            segmented1.SelectIndex = 0;
        }

        // 用于调整图像大小的方法
        private Image ResizeImage(Image image, int width, int height)
        {
            // 创建一个新的 Bitmap 对象，大小为目标宽度和高度
            Bitmap newImage = new Bitmap(width, height);

            // 使用 Graphics 类绘制原始图像，并调整其大小
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }

            return newImage;
        }
        public List<MenuItem> CreateMenuFromCsv(string firstmenuname)
        {
            string csvPath = globalpara.toolconfigpath;
            var menuItems = new Dictionary<string, MenuItem>();
            var level2Items = new Dictionary<string, MenuItem>();
            var level3Items = new Dictionary<string, MenuItem>();
            // 用于存储每个父节点下的子节点名称
            var nodeChildren = new Dictionary<string, HashSet<string>>();

            // 跳过标题行
            var lines = File.ReadAllLines(csvPath, Encoding.GetEncoding("GB2312")).Skip(1);

            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values[0] == firstmenuname)
                {
                    MenuBeanFunc menuBeanf = new MenuBeanFunc();
                    var bean = menuBeanf.CreatBean(line);
                   
                    // 创建一级菜单
                    if (!menuItems.ContainsKey(bean.folderName))
                    {
                        menuItems[bean.folderName] = new MenuItem(bean.folderName, "FolderOpenOutlined");
                        menuItems[bean.folderName].Font= new System.Drawing.Font("新宋体", (float)16, System.Drawing.FontStyle.Bold);
                    }

                    // 创建二级菜单
                    var level2Key = $"{bean.folderName}_{bean.toolboxName}";
                    if (!level2Items.ContainsKey(level2Key)&& !string.IsNullOrEmpty(bean.toolboxName))
                    {
                        var menuItem2 = new MenuItem(bean.toolboxName) { Expand = bean.toolboxExpand };
                        menuItem2.IconSvg = "MedicineBoxOutlined";
                        menuItem2.Font = new System.Drawing.Font("新宋体", (float)14.25, System.Drawing.FontStyle.Bold);
                        level2Items[level2Key] = menuItem2;
                        menuItems[bean.folderName].Sub.Add(menuItem2);
                    }

                    // 创建工具菜单项
                    var toolMenuItem = new MenuItem(" "+bean.toolName);
                    toolMenuItem.Tag = bean;  // 将bean对象设置为菜单项的Tag
                                              //toolMenuItem.IconSvg = "AppstoreAddOutlined";
                    string customSvgFilePath = globalpara.svgpath + @"\TdesignApp.svg"; // 请替换为您的SVG文件实际路径

                    if (File.Exists(customSvgFilePath))
                    {
                        // 读取SVG文件的全部内容作为字符串
                        string svgContent = File.ReadAllText(customSvgFilePath);
                        toolMenuItem.IconSvg = svgContent;
                    }

                    if (string.IsNullOrEmpty(bean.toolboxName))//二级菜单不存在
                    {

                        menuItems[bean.folderName].Sub.Add(toolMenuItem);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(bean.toolsetName))
                        {
                            // 二级菜单下的去重（使用已定义的level2Key）
                            if (!nodeChildren.ContainsKey(level2Key))
                            {
                                nodeChildren[level2Key] = new HashSet<string>();
                            }
                            if (!nodeChildren[level2Key].Contains(bean.toolName))
                            {
                                nodeChildren[level2Key].Add(bean.toolName);
                                level2Items[level2Key].Sub.Add(toolMenuItem);
                            }
                        }
                        else
                        {
                            // 三级菜单下的去重
                            var level3Key = $"{level2Key}_{bean.toolsetName}";
                            if (!level3Items.ContainsKey(level3Key))
                            {
                                var menuItem3 = new MenuItem(bean.toolsetName) { Expand = bean.toolsetExpand };
                                //menuItem3.Font = new System.Drawing.Font("宋体", (float)12);                                                           
                                level3Items[level3Key] = menuItem3;
                                level2Items[level2Key].Sub.Add(menuItem3);
                                nodeChildren[level3Key] = new HashSet<string>();
                            }
                            if (!nodeChildren[level3Key].Contains(bean.toolName))
                            {
                                nodeChildren[level3Key].Add(bean.toolName);
                                level3Items[level3Key].Sub.Add(toolMenuItem);
                            }
                        }
                    }
                    
                }
            }

            return menuItems.Values.ToList();
        }
        public List<TreeItem> CreateMenuFromCsv2(string firstmenuname)
        {
            string csvPath = globalpara.toolconfigpath;
            var menuItems = new Dictionary<string, TreeItem>();
            var level2Items = new Dictionary<string, TreeItem>();
            var level3Items = new Dictionary<string, TreeItem>();
            // 用于存储每个父节点下的子节点名称
            var nodeChildren = new Dictionary<string, HashSet<string>>();

            // 跳过标题行
            var lines = File.ReadAllLines(csvPath, Encoding.GetEncoding("GB2312")).Skip(1);

            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values[0] == firstmenuname)
                {
                    MenuBeanFunc menuBeanf = new MenuBeanFunc();
                    var bean = menuBeanf.CreatBean(line);

                    // 创建一级菜单
                    //if (!menuItems.ContainsKey(bean.folderName))
                    //{
                    //    //menuItems[bean.folderName] = new TreeItem(bean.folderName, "FolderOpenOutlined");
                    //    menuItems[bean.folderName] = new TreeItem(bean.folderName);
                    //    //menuItems[bean.folderName].Font = new System.Drawing.Font("新宋体", (float)16, System.Drawing.FontStyle.Bold);
                    //}

                    // 创建二级菜单
                    var level2Key = $"{bean.folderName}_{bean.toolboxName}";
                    if (!level2Items.ContainsKey(level2Key) && !string.IsNullOrEmpty(bean.toolboxName))
                    {
                        var menuItem2 = new TreeItem(bean.toolboxName) { Expand = bean.toolboxExpand };
                        menuItem2.IconSvg = "MedicineBoxOutlined";
                        //menuItem2.Font = new System.Drawing.Font("新宋体", (float)14.25, System.Drawing.FontStyle.Bold);
                        level2Items[level2Key] = menuItem2;
    
                        menuItems[bean.toolboxName] = menuItem2;
                    }

                    // 创建工具菜单项
                    var toolMenuItem = new TreeItem("" + bean.toolName);
                    toolMenuItem.Tag = bean;  // 将bean对象设置为菜单项的Tag
                                              //toolMenuItem.IconSvg = "AppstoreAddOutlined";
                    string customSvgFilePath = globalpara.svgpath + @"\TdesignApp.svg"; // 请替换为您的SVG文件实际路径

                    if (File.Exists(customSvgFilePath))
                    {
                        // 读取SVG文件的全部内容作为字符串
                        string svgContent = File.ReadAllText(customSvgFilePath);
                        toolMenuItem.IconSvg = svgContent;
                    }

                    if (string.IsNullOrEmpty(bean.toolboxName))//二级菜单不存在
                    {
                        menuItems[bean.toolName] = toolMenuItem;
                        //menuItems[bean.folderName].Sub.Add(toolMenuItem);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(bean.toolsetName))
                        {
                            // 二级菜单下的去重（使用已定义的level2Key）
                            if (!nodeChildren.ContainsKey(level2Key))
                            {
                                nodeChildren[level2Key] = new HashSet<string>();
                            }
                            if (!nodeChildren[level2Key].Contains(bean.toolName))
                            {
                                nodeChildren[level2Key].Add(bean.toolName);
                                level2Items[level2Key].Sub.Add(toolMenuItem);
                            }
                        }
                        else
                        {
                            // 三级菜单下的去重
                            var level3Key = $"{level2Key}_{bean.toolsetName}";
                            if (!level3Items.ContainsKey(level3Key))
                            {
                                var menuItem3 = new TreeItem(bean.toolsetName) { Expand = bean.toolsetExpand };
                                //menuItem3.Font = new System.Drawing.Font("宋体", (float)12);                                                           
                                level3Items[level3Key] = menuItem3;
                                level2Items[level2Key].Sub.Add(menuItem3);
                                nodeChildren[level3Key] = new HashSet<string>();
                            }
                            if (!nodeChildren[level3Key].Contains(bean.toolName))
                            {
                                nodeChildren[level3Key].Add(bean.toolName);
                                level3Items[level3Key].Sub.Add(toolMenuItem);
                            }
                        }
                    }

                }
            }

            return menuItems.Values.ToList();
        }
        //public List<MenuItem> CreateMenuFromCsv_(string firstmenuname)
        //{
        //    string csvPath = configpath;
        //    var menuItems = new Dictionary<string, MenuItem>();
        //    var level2Items = new Dictionary<string, MenuItem>();
        //    var level3Items = new Dictionary<string, MenuItem>();
        //    // 用于存储每个父节点下的子节点名称
        //    var nodeChildren = new Dictionary<string, HashSet<string>>();

        //    // 跳过标题行
        //    var lines = File.ReadAllLines(csvPath, Encoding.GetEncoding("GB2312")).Skip(1);

        //    foreach (var line in lines)
        //    {
        //        var values = line.Split(',');
        //        if (values[0] == firstmenuname)
        //        {
        //            MenuBean menuBean = new MenuBean();
        //            var bean = menuBean.CreatBean(line);

        //            // 创建一级菜单
        //            if (!menuItems.ContainsKey(bean.folderName))
        //            {
        //                menuItems[bean.folderName] = new MenuItem(bean.folderName);
        //            }

        //            // 创建二级菜单
        //            var level2Key = $"{bean.folderName}_{bean.toolboxName}";
        //            if (!level2Items.ContainsKey(level2Key))
        //            {
        //                var menuItem2 = new MenuItem(bean.toolboxName) { Expand = bean.toolboxExpand };
        //                level2Items[level2Key] = menuItem2;
        //                menuItems[bean.folderName].Sub.Add(menuItem2);
        //            }

        //            // 创建工具菜单项
        //            var toolMenuItem = new MenuItem(bean.toolName);
        //            toolMenuItem.Tag = bean;  // 将bean对象设置为菜单项的Tag

        //            if (string.IsNullOrEmpty(bean.toolsetName))
        //            {
        //                // 二级菜单下的去重（使用已定义的level2Key）
        //                if (!nodeChildren.ContainsKey(level2Key))
        //                {
        //                    nodeChildren[level2Key] = new HashSet<string>();
        //                }
        //                if (!nodeChildren[level2Key].Contains(bean.toolName))
        //                {
        //                    nodeChildren[level2Key].Add(bean.toolName);
        //                    level2Items[level2Key].Sub.Add(toolMenuItem);
        //                }
        //            }
        //            else
        //            {
        //                // 三级菜单下的去重
        //                var level3Key = $"{level2Key}_{bean.toolsetName}";
        //                if (!level3Items.ContainsKey(level3Key))
        //                {
        //                    var menuItem3 = new MenuItem(bean.toolsetName) { Expand = bean.toolsetExpand };
        //                    level3Items[level3Key] = menuItem3;
        //                    level2Items[level2Key].Sub.Add(menuItem3);
        //                    nodeChildren[level3Key] = new HashSet<string>();
        //                }
        //                if (!nodeChildren[level3Key].Contains(bean.toolName))
        //                {
        //                    nodeChildren[level3Key].Add(bean.toolName);
        //                    level3Items[level3Key].Sub.Add(toolMenuItem);
        //                }
        //            }
        //        }
        //    }

        //    return menuItems.Values.ToList();
        //}
        public void loadsubmenu(AntdUI.Menu menu1, string firstmenuname, AntdUI.Tree tree1)
        {

            if (menu1 != null)
                menu1.Items.Clear();
            //var menuItems = CreateMenuFromCsv(firstmenuname);
            var menuItems2= CreateMenuFromCsv2(firstmenuname);

            //// 将menuItems添加到你的主菜单中
            //foreach (var item in menuItems)
            //{
            //    menu1.Items.Add(item);
            //}
            tree1.Items.Clear();
            // 将menuItems添加到你的主菜单中
            foreach (var item in menuItems2)
            {
                tree1.Items.Add(item);
            }

        }

    }
}
