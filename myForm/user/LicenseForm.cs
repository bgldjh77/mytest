using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.LicenseFunc;
using 质检工具.SysFunc;

namespace 质检工具
{
    public partial class LicenseForm : AntdUI.Window
    {
        bool license = false;
        public LicenseForm()
        {
            InitializeComponent();
        }

        private void LicenseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Rib form = Application.OpenForms.OfType<Rib>().FirstOrDefault();
            if (form != null)
            {
                if (!license) { form.Close(); }
                else {
                    form.Enabled = true;
                    form.Focus(); };
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            license = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择授权文件";
            openFileDialog.Filter = "授权文件 (*.lic)|*.lic";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.licText.Text = openFileDialog.FileName;
            }
        }

        private void LicenseForm_Load(object sender, EventArgs e)
        {
            licText.Text = "";
            this.input2.ReadOnly = true;

            input2.Text = AuthManger.GetMachineCode() + "@" + AuthManger.GetEncryUserInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string licPath = this.licText.Text;
            if (licPath.Equals(""))
            {
                MessageBox.Show("请选择授权文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (AuthManger.Verify(this.licText.Text))
            {
                license = true;
                    this.Close();
            }
            else
            {
                MessageBox.Show("授权失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(this.input2.Text);
            LogHelper.Infonotice("提示","机器码已复制");
        }
    }
}
