using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具.UIPreSet
{
    class TextFont
    {
        /// <summary>
        /// 设置RibbonPanel的字体
        /// </summary>
        /// <param name="panel">要设置的RibbonPanel</param>
        /// <param name="font">字体</param>
        public void SetRibbonPanelFont(System.Windows.Forms.RibbonPanel panel, Font font)
        {
            if (panel != null)
            {
                // 通过反射设置RibbonPanel的字体属性
                try
                {
                    var fontProperty = panel.GetType().GetProperty("Font");
                    if (fontProperty != null && fontProperty.CanWrite)
                    {
                        fontProperty.SetValue(panel, font);
                    }
                }
                catch
                {
                    // 如果反射失败，尝试直接设置
                    //panel.Font = font;
                }
            }
        }

        /// <summary>
        /// 设置所有RibbonButton的字体
        /// </summary>
        /// <param name="font">字体</param>
        public void SetRibbonButtonsFont(RibbonButton[] buttons, Font font)
        {
            try
            {             
                foreach (var button in buttons)
                {
                    if (button != null)
                    {
                        try
                        {
                            var fontProperty = button.GetType().GetProperty("Font");
                            if (fontProperty != null && fontProperty.CanWrite)
                            {
                                fontProperty.SetValue(button, font);
                            }
                        }
                        catch
                        {
                            // 忽略单个按钮设置失败的情况
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.normallog($"设置RibbonButton字体时出错: {ex.Message}");
            }
        }
    }
}
