using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.UIPreSet;

namespace 质检工具
{
    public partial class SettingForm : AntdUI.Window
    {
        public SettingForm()
        {
            InitializeComponent();
            InitializeThemeComboBox();
        }

        private void InitializeThemeComboBox()
        {
            // 添加主题选项
            uiComboBox1.Items.AddRange(new string[] { "蓝色", "绿色", "黄色", "紫色", "黑色" });
            uiComboBox1.SelectedIndex = 0; // 默认选择蓝色

            // 绑定事件
            uiComboBox1.SelectedIndexChanged += UnComboBox1_SelectedIndexChanged;
        }
        private void UnComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 根据选择的主题切换颜色
            string selectedTheme = uiComboBox1.SelectedItem.ToString();
            string[] svgColors = null;

            switch (selectedTheme)
            {
                case "蓝色":
                    ThemeColor.ApplyTheme("#66ccff", "#85bf88", "#c9f7ca", "#e1ffff");
                    svgColors = new string[] { "#85bf88", "#66ccff", "#c9f7ca", "#e1ffff" };
                    break;
                case "绿色":
                    ThemeColor.ApplyTheme("#85bf88", "#66ccff", "#c9f7ca", "#e1ffff");
                    svgColors = new string[] { "#85bf88", "#85bf88", "#c9f7ca", "#c9f7ca" };
                    break;
                case "黄色":
                    ThemeColor.ApplyTheme("#f7d358", "#66ccff", "#c9f7ca", "#e1ffff");
                    svgColors = new string[] { "#85bf88", "#b99d2b", "#c9f7ca", "#ffe990" };
                    break;
                case "紫色":
                    ThemeColor.ApplyTheme("#a292d1", "#66ccff", "#c9f7ca", "#e1ffff");
                    svgColors = new string[] { "#85bf88", "#905ebc", "#c9f7ca", "#E7DDFF" };
                    break;
                case "黑色":
                    ThemeColor.ApplyTheme("#333333", "#66ccff", "#c9f7ca", "#e1ffff");
                    svgColors = new string[] { "#85bf88", "#808080", "#c9f7ca", "#d3d3d3" };
                    break;
                default:
                    break;
            }

            // 更新 Ribbon 图标颜色
            if (svgColors != null)
            {
                Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
                if (mf != null)
                {
                    mf.UpdateRibbonIcons(svgColors);
                }
            }
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            InitializeFunc.changebuttoncolor(selectfolder);
            srcfolderpath_input.Text = IniFunc.getString("toolResourcePath"); ;
        }

        private void selectfolder_Click(object sender, EventArgs e)
        {
            SysFunc.SelectFileFunc sf = new SysFunc.SelectFileFunc();
            string srcfodler = sf.sf_folder();
            
            // 检查所选目录下是否存在 config.csv
            if (!string.IsNullOrEmpty(srcfodler) && System.IO.File.Exists(System.IO.Path.Combine(srcfodler, "config.csv")))
            {
                srcfolderpath_input.Text = srcfodler;
            }
            else if (!string.IsNullOrEmpty(srcfodler))
            {
                //MessageBox.Show("所选目录下未找到 config.csv 文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.ErrorLog("资源目录错误", "所选目录下未找到 config.csv 文件！");
            }
            
            this.Focus();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            string configpath = System.IO.Path.Combine(srcfolderpath_input.Text, "config.csv");
            if (System.IO.File.Exists(configpath)) {
                //Properties.Settings.Default.menuconfig = configpath;
                //Properties.Settings.Default.Save();
                globalpara.toolconfigpath = configpath;
                IniFunc.writeString("toolResourcePath",configpath);
                Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
                mf.LoadTreeMenu("通用检查");

            }
            
            this.Close();
        }

        private void SettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
            mf.pageHeader1.Focus();
        }
    }
}
