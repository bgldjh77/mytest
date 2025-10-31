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
    public partial class UpdateInfoForm : AntdUI.Window
    {
        public UpdateInfoForm()
        {
            InitializeComponent();
        }

        private void UpdateInfoForm_Load(object sender, EventArgs e)
        {
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            uiRichTextBox1.ReadOnly = true;
            try
            {
                if (System.IO.File.Exists(globalpara.updatedespath))
                {
                    string content = System.IO.File.ReadAllText(globalpara.updatedespath, Encoding.UTF8);
                    uiRichTextBox1.Text = content;
                }
                else
                {
                    uiRichTextBox1.Text = "未找到更新说明文件";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取更新说明文件失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Rib mf = System.Windows.Forms.Application.OpenForms.OfType<Rib>().FirstOrDefault();
            mf.pageHeader1.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
