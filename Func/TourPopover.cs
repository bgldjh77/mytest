using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具.Func
{
    public partial class TourPopover : UserControl
    {
        AntdUI.Tour.Popover popover;
        public TourPopover(AntdUI.Tour.Popover _popover, string title, string text, int step, int max)
        {
            popover = _popover;
            InitializeComponent();
            label1.Text = title;
            label3.Text = text;
            label2.Text = step + " / " + max;
            if (step == max)
            {
                btn_next.LocalizationText = "Finish";
                btn_next.Text = "完成";
            }
            btn_previous.Visible = step > 1;
            if (btn_previous.Visible)
            {
                int w1 = (int)(label1.PSize.Width / AntdUI.Config.Dpi), w = (int)((label2.PSize.Width + btn_previous.PSize.Width + btn_next.PSize.Width) / AntdUI.Config.Dpi);
                Width = w1 > w ? w1 : w;
            }
        }

        private void btn_previous_Click(object sender, EventArgs e) => popover.Tour.Previous();

        private void btn_next_Click(object sender, EventArgs e) => popover.Tour.Next();
    
    

    
    }
}
