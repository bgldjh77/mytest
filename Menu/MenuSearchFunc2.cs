using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具.Menu
{
    public class MenuSearchFunc2//tree方法
    {
        AntdUI.Tree mymenu;
        AntdUI.Input textBox;

        Timer timer;
        //AntdUI.MenuItem originmenuItem;
        AntdUI.Tree combineMenu;

        public void onTextChanged(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
        }

        public MenuSearchFunc2(AntdUI.Tree _mymenu, AntdUI.Input _textBox)
        {
            mymenu = _mymenu;
            textBox = _textBox;
            init();
        }
        private void init()
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += onTimerTick;
            getcombine();

            //CreateMenuFromCsv
        }

        //多个一级目录内容合并到一起方便搜索
        private void getcombine()
        {
            FirstLevelMenuFunc fmf = new FirstLevelMenuFunc();
            List<string> firstmenu = FirstLevelMenuFunc.GetUniqueFirstColumn(globalpara.toolconfigpath, 0);
            combineMenu = new AntdUI.Tree();
            combineMenu.Items.Add(new AntdUI.TreeItem("搜索"));
            //Application.DoEvents();
            foreach (string foldername in firstmenu)
            {
                var menuItems = fmf.CreateMenuFromCsv2(foldername);
                foreach (var item in menuItems)
                {
                    combineMenu.Items[0].Sub.Add(item);
                }
            }

        }

        private string getLastText()
        {
            if (textBox != null) return textBox.Text;
            return "";
        }
        public void search()
        {
            string lastText = getLastText();
            if (lastText.Equals(""))
            {
                mymenu.Items.Clear();
                foreach (var item in combineMenu.Items)
                {
                    mymenu.Items.Add(item);
                }
                mymenu.Items[0].Expand = true;
            }
            else
            {
                mymenu.Items.Clear();
                var searchRoot = new AntdUI.TreeItem("搜索结果");
                mymenu.Items.Add(searchRoot);
                SearchMenuItems(combineMenu.Items[0], lastText, searchRoot);
                mymenu.ExpandAll();
            }
        }
        public void onTimerTick(object sender, EventArgs e)
        {
            search();
            timer.Stop();
        }

        private void SearchMenuItems(AntdUI.TreeItem sourceNode, string searchText, AntdUI.TreeItem targetParent)
        {
            
                foreach (AntdUI.TreeItem item in sourceNode.Sub)
                {
                    bool shouldAdd = false;
                    AntdUI.TreeItem newItem = null;

                    // 检查当前节点是否匹配
                    if (item.Text.ToUpper().Contains(searchText.ToUpper()))
                    {
                        shouldAdd = true;
                    }

                    // 检查子节点是否匹配
                    if (item.Sub.Count > 0)
                    {
                        // 复制所有属性创建新节点
                        newItem = new AntdUI.TreeItem(item.Text)
                        {
                            Tag = item.Tag,
                            //Font = item.Font,
                            //ForeColor = item.ForeColor,
                            //BackColor = item.BackColor,
                            //Expand = item.Expand,
                            IconSvg = item.IconSvg,
                            //SymbolSize = item.SymbolSize
                        };
                        SearchMenuItems(item, searchText, newItem);
                        if (newItem.Sub.Count > 0)
                        {
                            shouldAdd = true;
                        }
                    }

                    // 添加匹配的节点
                    if (shouldAdd)
                    {
                        if (newItem == null)
                        {
                            // 复制所有属性创建新节点
                            newItem = new AntdUI.TreeItem(item.Text)
                            {
                                Tag = item.Tag,
                                //Font = item.Font,
                                //ForeColor = item.ForeColor,
                                //BackColor = item.BackColor,
                                //Expand = item.Expand,
                                IconSvg = item.IconSvg,
                                //SymbolSize = item.SymbolSize
                            };
                        }
                        targetParent.Sub.Add(newItem);
                    }
                }
            
            
        }
    }
}
