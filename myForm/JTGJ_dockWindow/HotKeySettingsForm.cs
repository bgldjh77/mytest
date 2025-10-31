using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZJZX_ZJAddin.UIForm
{
    public partial class HotKeySettingsForm : Form
    {
        public Keys SelectedHotKey { get; private set; }

        public HotKeySettingsForm(Keys currentKey)
        {
            InitializeComponent();
            SelectedHotKey = currentKey; // 设置当前热键  

            // 显示当前热键  
            Label currentKeyLabel = new Label
            {
                Text = "当前快捷键: Ctrl + " + currentKey + "（仅支持修改该键）\n",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(currentKeyLabel);

            // 提示用户按下新快捷键  
            Label instructionLabel = new Label
            {
                Text = "请按下新的快捷键（无需重复按Ctrl）:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(instructionLabel);

            // 设定窗口其他参数  
            this.KeyPreview = true; // 确保窗口捕获按键  
            this.FormClosing += HotKeySettingsForm_FormClosing; // 处理关闭事件  
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 检查按下的键  
            if (e.KeyCode != Keys.Control) // 忽略 Ctrl 键本身  
            {
                SelectedHotKey = e.KeyCode; // 更新选择的热键  
                this.DialogResult = DialogResult.OK; // 设定为 OK 结果  
                this.Close(); // 关闭窗口  
            }

            base.OnKeyDown(e); // 确保基类处理  
        }

        private void HotKeySettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                // 可以在这里添加确认用户未选择新热键的提示  
            }
        }

        // 确保是窗体需求的设计部分  
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HotKeySettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotKeySettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置快捷键";
            this.ResumeLayout(false);

        }
    }
}
