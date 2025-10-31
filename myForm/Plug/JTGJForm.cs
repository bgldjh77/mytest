using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具.myForm.Plug
{
    public partial class JTGJForm : AntdUI.Window
    {
        public JTGJForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.JTGJForm_Load);
        }

        private void JTGJForm_Load(object sender, EventArgs e)
        {
            // 设置 pageHeader 的动态背景色
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);      
        }
    }
}
