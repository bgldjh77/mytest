using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace 质检工具.UIPreSet
{
    class ThemeColor//软件颜色
    {
        //主题色
        public static Color PrimaryTheme = Color.FromArgb(102, 204, 255);

        // 重要按钮颜色
        public static Color ImportantButton = Color.FromArgb(54, 85, 179);

        // 标准色
        public static Color Standard = Color.FromArgb(102, 204, 255);

        // 字体色 - 下载类
        public static Color FontDownload = Color.FromArgb(102, 204, 255);

        //白色
        public static Color WhiteColor = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 应用主题色到整个软件
        /// </summary>
        public static void ApplyTheme(string primaryColorHex, string importantButtonHex, string standardHex, string fontDownloadHex)
        {
            // 更新主题色
            PrimaryTheme = ColorTranslator.FromHtml(primaryColorHex);
            ImportantButton = ColorTranslator.FromHtml(importantButtonHex);
            Standard = ColorTranslator.FromHtml(standardHex);
            FontDownload = ColorTranslator.FromHtml(fontDownloadHex);

            // 更新所有打开的窗体
            foreach (Form form in Application.OpenForms)
            {
                if (form is AntdUI.Window window)
                {
                    // 更新页面头部背景色
                    if (window.Controls.OfType<AntdUI.PageHeader>().FirstOrDefault() is AntdUI.PageHeader pageHeader)
                    {
                        pageHeader.BackColor = PrimaryTheme;
                    }
                    // 如果窗体是 Rib，更新 pageHeader3 的背景色
                    if (form is 质检工具.Rib rib && rib.pageHeader3 != null)
                    {
                        rib.pageHeader3.BackColor = PrimaryTheme;
                    }
                    // 刷新窗体
                    window.Refresh();
                }
            }
        }
    }
    
}
