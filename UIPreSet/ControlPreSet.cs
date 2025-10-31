using AntdUI;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具.UIPreSet
{
    class ControlPreSet//控件预设
    {
        /// <summary>
        /// 设置标题行 UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
        /// </summary>
        /// <param name="pageHeader1"></param>
        public static void SetpageHeader(PageHeader pageHeader1)
        {
            float fontSize = 9.0f;
            Font font = new Font("Microsoft YaHei UI", fontSize, FontStyle.Regular);
            int textHeight = TextRenderer.MeasureText("测试文字", font).Height;

            pageHeader1.BackColor = UIPreSet.ThemeColor.PrimaryTheme;
            pageHeader1.UseSystemStyleColor = true;
            pageHeader1.ForeColor = UIPreSet.ThemeColor.WhiteColor;
            pageHeader1.Height = textHeight*2;
            pageHeader1.ShowButton = true;
        }

        public static void SetAntdUIbtn(AntdUI.Button button)
        {
            button.DefaultBack = UIPreSet.ThemeColor.ImportantButton;
            button.BackColor= UIPreSet.ThemeColor.ImportantButton;
            button.ForeColor= UIPreSet.ThemeColor.WhiteColor;
        }

        public static void SetSunnyUIbtn(UISymbolButton bt)
        {
            bt.SymbolColor = Color.FromArgb(166, 188, 255);
            bt.RectColor = Color.Transparent;
            bt.ForeColor = Color.FromArgb(166, 188, 255);
            bt.FillColor = Color.Transparent;
            bt.RectHoverColor = Color.Transparent;
            bt.SymbolHoverColor = Color.FromArgb(204, 213, 235);
            bt.RectPressColor = Color.Transparent;
            bt.ForeHoverColor = Color.Gray;
        }
    }
}
