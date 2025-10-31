using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.MenuConfig;
using 质检工具.SysFunc;

namespace 质检工具.Func.ToolFormFunc
{
    class DynamicTool
    {
        //bool coor_Select = true;//选择下拉后是否更新对应textbox
        /// <summary>
        /// 动态创建控件
        /// </summary>
        /// 
        UIRichTextBox toolDescRichBox = new UIRichTextBox();
        public void creatcontrol(Form thisform, List<ToolArgs> ta, MenuBean menuBean=null)
        {
            int offsety = 0;
            Control ph = thisform.Controls.Find("pageHeader1", true).FirstOrDefault();
            if (ph != null)
            {
                offsety = ph.Height;
            }

            // 创建 SplitterContainer
            SplitContainer splitterContainer = new SplitContainer();
            splitterContainer.Name = "splitContainer1";
            splitterContainer.Dock = DockStyle.Fill;
            splitterContainer.SplitterDistance = thisform.Width -400; // 设置分割位置
            splitterContainer.Location = new Point(0, 0);
            splitterContainer.Height = thisform.Height - 70;
            splitterContainer.Width = thisform.Width;
            splitterContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            thisform.Controls.Add(splitterContainer);

            // 创建右侧的 UIRichTextBox
            
            toolDescRichBox.Name = "Tool_desc_RichBox";
            //toolDescRichBox.Dock = DockStyle.Fill;
            toolDescRichBox.Location = new Point(0, 10+ offsety);
            toolDescRichBox.Height = thisform.Height - 80- offsety;
            toolDescRichBox.Width = splitterContainer.Panel2.Width-10;
            toolDescRichBox.Font = new Font("宋体", 12);
            toolDescRichBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            toolDescRichBox.ReadOnly = true;
            splitterContainer.Panel2.Controls.Add(toolDescRichBox);

            // 在左侧面板中创建控件
            Panel leftPanel = splitterContainer.Panel1;
            
            int yPosition = 10+ offsety; // 起始Y坐标
            const int xLabelPosition = 20; // 标签X坐标
            const int xTextBoxPosition = 20; // 文本框X坐标
            int xbtPosition = splitterContainer.SplitterDistance - 100; // 按钮位置相对于左面板
            const int height = 32;//控件高度
            const int spacing = 32; // 控件间距

            int Argcount = 0;
            foreach (ToolArgs toolArgs in ta)
            {
                // 创建标签
                Label label = new Label();
                label.Text = toolArgs.argName;
                label.Width = leftPanel.Width - 40;
                label.Height = height;
                label.Location = new Point(xLabelPosition, yPosition);
                label.Font = new Font("宋体", 12);
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                leftPanel.Controls.Add(label);

                yPosition += spacing;

                // 创建文本框
                UITextBox textBox = new UITextBox();
                textBox.Location = new Point(xTextBoxPosition, yPosition);
                textBox.Width = leftPanel.Width - 120; // 为按钮留出空间
                textBox.Height = height;
                textBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                string textbox_value = toolArgs.defalutValue;
                if (!string.IsNullOrEmpty(toolArgs.nowValue)) { textbox_value = toolArgs.nowValue; }
                textBox.Text = DefaultValue.getdefaultvalue(textbox_value);
                textBox.Name = "txt_" + toolArgs.argName; // 设置唯一名称以便后续获取
                leftPanel.Controls.Add(textBox);
                textboxEven(toolArgs, textBox);

                //创建下拉选项
                if (toolArgs.argType == ArgTypeEnum.Coordinate)
                {
                    int comboxwidth = 200;
                    //缩短textBox长度
                    textBox.Width = textBox.Width - comboxwidth - 20;

                    UIComboBox comboBox = new UIComboBox();
                    comboBox.Name = "combox" + toolArgs.argName;
                    comboBox.Location = new Point(textBox.Width + xTextBoxPosition + 20, yPosition);
                    comboBox.Height = height;
                    comboBox.Width = comboxwidth;
                    comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    if (!string.IsNullOrEmpty(toolArgs.replaceValue))
                    {
                        comboBox.Items.Add(toolArgs.replaceValue);
                        comboBox.SelectedItem = (toolArgs.replaceValue);
                    }
                    //combox_coor_Even(comboBox, textBox, toolArgs);
                    comboBox.SymbolNormal = 0;
                    comboBox.Enabled = false;
                    leftPanel.Controls.Add(comboBox);
                }

                //创建按钮
                UISymbolButton bt = new UISymbolButton();
                bt.Name = "addDT" + toolArgs.argName;
                bt.Location = new Point(leftPanel.Width - 90, yPosition);
                bt.Height = height;
                bt.Width = 80;
                bt.Text = "";
                bt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                bt.Visible = false;
                buttonEven(bt, toolArgs, textBox, thisform);
                InitializeFunc.changebuttoncolor(bt);
                leftPanel.Controls.Add(bt);

                Argcount++;
                if (Argcount > 5)
                {
                    thisform.Height += height + spacing;
                    splitterContainer.Height = thisform.Height - 80;
                    // 获取屏幕工作区域
                    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

                    // 计算居中位置
                    int x = (workingArea.Width - thisform.Width) / 2 + workingArea.X;
                    int y = (workingArea.Height - thisform.Height) / 2 + workingArea.Y;

                    // 设置窗体位置
                    thisform.Location = new Point(x, y);
                }

                // 更新下一个控件的Y坐标
                yPosition += spacing;
            }

            //调整字体大小
            foreach (Control control in leftPanel.Controls)
            {
                if (control is UITextBox textBox)
                {
                    textBox.Font = new Font("宋体", 12);
                }
            }

            //富文本框
            if (menuBean != null) toolDescRichBox.Text = ReadToolDesc.readToolDesc(menuBean);
            else toolDescRichBox.Text = "";
        }
        //批量执行
        public void creatcontrol(FlowLayoutPanel flowLayoutPanel, List<ToolArgs> ta)//动态创建控件重载，可以在FlowLayoutPanel加载带下拉框的
        {
            const int height = 25; // 控件高度
            const int spacing = 10; // 控件间距

            foreach (ToolArgs toolArgs in ta)
            {
                // 创建标签
                Label label = new Label();
                label.Text = toolArgs.argName;
                label.Width = 950;
                label.Height = height;
                label.Font = new Font("宋体", 12);
                label.TextAlign = ContentAlignment.MiddleLeft;
                flowLayoutPanel.Controls.Add(label);

                // 创建文本框
                UITextBox textBox = new UITextBox();
                textBox.Width = 900;
                textBox.Height = height;
                textBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                string textbox_value = toolArgs.defalutValue;
                if (!string.IsNullOrEmpty(toolArgs.nowValue)) { textbox_value = toolArgs.nowValue; }
                textBox.Text = DefaultValue.getdefaultvalue(textbox_value);
                textBox.Name = "txt_" + toolArgs.argName; // 设置唯一名称以便后续获取
                flowLayoutPanel.Controls.Add(textBox);
                textboxEven(toolArgs, textBox);

                // 创建下拉选项
                if (toolArgs.argType == ArgTypeEnum.Coordinate)
                {
                    UIComboBox comboBox = new UIComboBox();
                    comboBox.Name = "combox" + toolArgs.argName;
                    comboBox.Height = height;
                    comboBox.Width = 200;
                    //缩短textBox长度
                    textBox.Width = textBox.Width - comboBox.Width - 20;
                    comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    //combox_coor_Even(comboBox, textBox, toolArgs);
                    if (!string.IsNullOrEmpty(toolArgs.replaceValue))
                    {
                        comboBox.Items.Add(toolArgs.replaceValue);
                        comboBox.SelectedItem = (toolArgs.replaceValue);
                    }
                    comboBox.SymbolNormal = 0;
                    comboBox.Enabled = false;
                    flowLayoutPanel.Controls.Add(comboBox);
                }

                // 创建按钮
                UISymbolButton bt = new UISymbolButton();
                bt.Name = "addDT" + toolArgs.argName;
                bt.Height = height;
                bt.Width = 80;
                bt.Text = "";
                bt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                bt.Visible = false;
                buttonEven(bt, toolArgs, textBox, flowLayoutPanel.FindForm());
                InitializeFunc.changebuttoncolor(bt);
                flowLayoutPanel.Controls.Add(bt);

                // 添加间距
                flowLayoutPanel.SetFlowBreak(bt, true);
            }
        }
        //按钮动态事件
        private void buttonEven(UISymbolButton bt, ToolArgs toolArgs, UITextBox textBox,Form thisform)
        {
            if (toolArgs.argType == ArgTypeEnum.FeatureClass)
            {
                bt.Visible = true;
                bt.Symbol = 62073;
                bt.SymbolSize = 25;

                // 获取对应的文本框引用
                UITextBox currentTextBox = textBox;

                // 添加点击事件
                bt.Click += (sender, e) =>
                {
                    SelectFileFunc sf = new SelectFileFunc();
                    List<string> fcpathlist = sf.OpenGxDialog_FC();
                    if (fcpathlist.Count > 0)
                    {
                        string fcpt = fcpathlist[0];
                        currentTextBox.Text = fcpt;
                    }
                    thisform.Focus();
                };
            }
            else if (toolArgs.argType == ArgTypeEnum.Folder)
            {
                bt.Visible = true;
                bt.Symbol = 61564;
                bt.SymbolSize = 25;

                // 获取对应的文本框引用
                UITextBox currentTextBox = textBox;

                // 添加点击事件
                bt.Click += (sender, e) =>
                {
                    SelectFileFunc sf = new SelectFileFunc();
                    string foldpt = sf.sf_folder();
                    if (Directory.Exists(foldpt))
                    {
                        currentTextBox.Text = foldpt;
                    }
                    thisform.Focus();
                };

            }
            else if (toolArgs.argType == ArgTypeEnum.File)
            {
                bt.Visible = true;
                bt.Symbol = 61787;
                bt.SymbolSize = 25;

                // 获取对应的文本框引用
                UITextBox currentTextBox = textBox;

                // 添加点击事件
                SelectFileFunc sf = new SelectFileFunc();
                if (toolArgs.direction== DirectionEnum.输入)
                {
                    bt.Click += (sender, e) =>
                    {
                        
                        string filept = sf.sf_selectfile("");
                        if (File.Exists(filept))
                        {
                            currentTextBox.Text = filept;
                        }
                        thisform.Focus();
                    };
                }
                else
                {
                    string extension = toolArgs.fileExtension;
                    bt.Click += (sender, e) =>
                    {
                        string filept = sf.sf_savefile(extension);
                        currentTextBox.Text = filept;
                        thisform.Focus();
                    };
                }
                
            }
            else if (toolArgs.argType == ArgTypeEnum.Workspace)
            {
                bt.Visible = true;
                bt.Symbol = 61888;
                bt.SymbolSize = 25;

                // 获取对应的文本框引用
                UITextBox currentTextBox = textBox;

                // 添加点击事件
                bt.Click += (sender, e) =>
                {
                    SelectFileFunc sf = new SelectFileFunc();
                    string workspace = sf.sf_workspace("请选择工作空间");
                    if (workspace!=null &&( workspace.EndsWith(".mdb") | workspace.EndsWith(".gdb")))
                    {
                        currentTextBox.Text = workspace;
                    }
                    else
                    {
                        LogHelper.normallog("请正确选择工作空间");
                    }
                    thisform.Focus();
                };
            }
        
            else if (toolArgs.argType == ArgTypeEnum.Coordinate)
            {
                bt.Visible = true;
                bt.Symbol = 363394;
                bt.SymbolSize = 25;
                bt.SymbolOffset =new Point( -2,0);

                // 获取对应的文本框引用
                UITextBox currentTextBox = textBox;
                // 添加点击事件
                bt.Click += (sender, e) =>
                {
                    using (SpacialRef_SelectForm pa = new SpacialRef_SelectForm())
                    {
                        DialogResult result = pa.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string prjpath = pa.SelectedFilePath;
                            if (!File.Exists(prjpath)) return;
                            currentTextBox.Text = Path.GetFileNameWithoutExtension(prjpath).Replace(" ", "_");
                            string comboBoxName = "combox" + toolArgs.argName;
                            UIComboBox comboBox = thisform.Controls.Find(comboBoxName, true).OfType<UIComboBox>().FirstOrDefault();
                            if (comboBox != null)
                            {
                                string wkid = GetSpacialRef.GetWkidFromPrj(prjpath).ToString();
                                if (!comboBox.Items.Contains(wkid))
                                {
                                    comboBox.Items.Clear();
                                    comboBox.Items.Add(wkid);
                                    comboBox.SelectedItem = (wkid);
                                }
                            }
                        }
                    }
                        
                        //SelectFileFunc sf = new SelectFileFunc();
                        //string lyrpath = sf.sf_selectfile("prj",globalpara.coordinate_txt_path);
                        //if (File.Exists(lyrpath))
                        //{
                        //    currentTextBox.Text = Path.GetFileNameWithoutExtension(lyrpath).Replace(" ","_");
                        //    string comboBoxName = "combox" + toolArgs.argName;
                        //    UIComboBox comboBox = thisform.Controls.Find(comboBoxName, true).OfType<UIComboBox>().FirstOrDefault();
                        //    if (comboBox != null)
                        //    {
                        //        string wkid = GetSpacialRef.GetWkidFromPrj(lyrpath).ToString();
                        //        if (!comboBox.Items.Contains(wkid))
                        //        {
                        //            comboBox.Items.Add(wkid);
                        //            comboBox.SelectedItem = (wkid);
                        //        }
                        //    }
                        //    // currentTextBox.Text = lyrpath;
                        //    // string comboBoxName = "combox" + toolArgs.argName;
                        //    // UIComboBox comboBox = thisform.Controls.Find(comboBoxName, true)
                        //    //.OfType<UIComboBox>()
                        //    //.FirstOrDefault();
                        //    // if (comboBox != null)
                        //    // {
                        //    //     if (!comboBox.Items.Contains("用户坐标"))
                        //    //     {
                        //    //         comboBox.Items.Add("用户坐标");
                        //    //     }
                        //    //     //coor_Select = false;
                        //    //     comboBox.SelectedItem = "用户坐标";
                        //    // }
                        //}

                        thisform.Focus();
                };
                
            }
            else if (toolArgs.argType == ArgTypeEnum.Table)
            {
                bt.Visible = true;
                bt.Symbol = 61787; // 设置按钮图标
                bt.SymbolSize = 25;

                // 获取对应的文本框引用
                UITextBox currentTextBox = textBox;

                // 添加点击事件
                bt.Click += (sender, e) =>
                {
                    SelectFileFunc sf = new SelectFileFunc();
                    string tablePath = sf.sf_selectTableFromMDB(); // 调用新的方法选择表
                    if (!string.IsNullOrEmpty(tablePath))
                    {
                        currentTextBox.Text = tablePath;
                    }
                    thisform.Focus();
                };
            }
        }
        //文本框事件
        private void textboxEven(ToolArgs toolArgs, UITextBox textBox)
        {
            if (toolArgs.argType == ArgTypeEnum.Int)
            {
                // 限制只能输入整数
                textBox.KeyPress += (sender, e) =>
                {
                    // 允许数字、退格键、负号（只在开头允许）
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' &&
                        !(e.KeyChar == '-' && textBox.Text.Length == 0))
                    {
                        e.Handled = true; // 阻止非法输入
                    }
                };
            }
            else if (toolArgs.argType == ArgTypeEnum.Double)
            {
                // 限制只能输入浮点数（包括小数点和负号）
                textBox.KeyPress += (sender, e) =>
                {
                    // 允许数字、退格键、负号（只在开头）、小数点（只能出现一次）
                    if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' &&
                        !(e.KeyChar == '-' && textBox.Text.Length == 0) &&
                        !(e.KeyChar == '.' && !textBox.Text.Contains('.')))
                    {
                        e.Handled = true;
                    }
                };
            }
            else
            {
                textBox.AllowDrop = true;
                textBox.DragEnter += DragFileFunc.TextBox_DragEnter;
                
                // 统一的拖拽处理事件
                textBox.DragDrop += (sender, e) =>
                {
                    try
                    {
                        // 优先检查是否为文件/文件夹拖拽
                        if (e.Data.GetDataPresent(DataFormats.FileDrop))
                        {
                            // 处理文件/文件夹拖拽
                            DragFileFunc.TextBox_DragDrop(sender, e);
                        }
                        else
                        {
                            // 处理图层拖拽
                            Rib st = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
                            if (st != null && !string.IsNullOrEmpty(st.LayerPath))
                            {
                                textBox.Text = st.LayerPath;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show($"拖拽操作失败: {ex.Message}", "错误", 
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                };
            }
            
            
        }
        //坐标下拉选项
        private void combox_coor_Even(UIComboBox comboBox, UITextBox textBox, ToolArgs toolArgs)
        {
            string[] files = System.IO.Directory.GetFiles(globalpara.coordinate_txt_path);
            foreach (string shp in files)
            {
                if (shp.EndsWith(".shp"))
                {
                    string lyrname = Path.GetFileNameWithoutExtension(shp);
                    comboBox.Items.Add(lyrname);
                }
            }

            // 订阅事件（如果尚未订阅过）
            comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // 避免重复绑定
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            // 使用闭包传递 textBox 到事件中
            void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
            {
                //if (coor_Select)
                //{
                string lyrname = comboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(lyrname)&& lyrname!="用户坐标")
                {
                    
                    string filePath = Path.Combine(globalpara.coordinate_txt_path, lyrname + ".shp");
                    //string showtext = "";
                    //try
                    //{
                    //    showtext=File.ReadAllText(filePath);
                    //}
                    //catch(Exception ex)
                    //{
                    //    LogHelper.ErrorLog("读取坐标配置失败,请手动选择",ex);
                    //}
                    textBox.Text = filePath;
                }
                //}
                //coor_Select = true;
            }

            // 设置默认选中项

            // 设置默认选中项
            if (comboBox.Items.Count > 0)
            {
                // 提取文件名部分（不含扩展名）进行匹配
                string nowValueName = Path.GetFileNameWithoutExtension(toolArgs.nowValue);

                if (!string.IsNullOrEmpty(nowValueName) && comboBox.Items.Contains(nowValueName))
                {
                    comboBox.SelectedItem = nowValueName;
                }
                else if (comboBox.Items.Contains("CGCS2000_3_Degree_GK_CM_114E"))
                {
                    comboBox.SelectedItem = "CGCS2000_3_Degree_GK_CM_114E";
                }
                else
                {
                    comboBox.SelectedIndex = 0;
                }
            }
        }

        //public void show_desc_RichTextBox(MenuBean menuBean)
        //{
        //    if(menuBean!=null) toolDescRichBox.Text = ReadToolDesc.readToolDesc(menuBean);
        //    else toolDescRichBox.Text = "";
        //}

    }
}
