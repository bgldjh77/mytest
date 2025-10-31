using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具
{
    public partial class MainLogForm : AntdUI.Window
    {
        public MainLogForm()
        {
            InitializeComponent();
        }
        //重写关闭窗口按钮方法
        protected override void OnClosing(CancelEventArgs e)
        {

            this.Visible = false;
            //取消退出
            e.Cancel = true;
            
        }

        private void MainLogForm_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uiRichTextBox1.Clear();
        }
    }
}
