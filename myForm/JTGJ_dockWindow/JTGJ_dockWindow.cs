using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZJZX_ZJAddin.UIForm;
using ZJZX_ZJAddin.Utils;

namespace ZJZX_ZJAddin.JTGJ
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
   
       
    public partial class JTGJ_dockWindow : UserControl
    {
        private KeyboardHook _keyboardHook;
        private Keys _currentHotKey = Keys.D; // 默认热键 
        private bool iswh = false;

        public JTGJ_dockWindow(object hook)
        {
            InitializeComponent();
            _keyboardHook = new KeyboardHook();
            _keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
            this.textBox1.Text = "Ctrl + D";
            this.Hook = hook;
        }

        private void KeyboardHook_KeyPressed(object sender, Keys key)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && key == _currentHotKey)
            {
                if (iswh)
                {
                    int boxwidth = Convert.ToInt32(this.width.Text);
                    int boxheight = Convert.ToInt32(this.height.Text);
                    CaptureForm capForm = new CaptureForm(boxwidth, boxheight, iswh);
                    capForm.Show();
                }
                else
                {
                    CaptureForm capForm = new CaptureForm();
                    capForm.Show();
                }
            }
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private JTGJ_dockWindow m_windowUI;
            public AddinImpl()
            {
            }
            internal JTGJ_dockWindow UI
            {
                get { return m_windowUI; }
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new JTGJ_dockWindow(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void JTGJ_dockWindow_Load(object sender, EventArgs e)
        {

        }

       

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                _keyboardHook.Hook(); // 注册钩子  
            }
            else
            {
                _keyboardHook.Unhook(); // 注销钩子  
            }
        }

       

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.width.Text) || string.IsNullOrEmpty(this.height.Text))
            {
                MessageBox.Show("请先填写好指定参数（截图高度/截图宽度）");
                this.iswh = false;
            }
            else
            {
                this.iswh = this.checkBox2.Checked;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg2 = new FolderBrowserDialog();
            dlg2.Description = "请选择截图输出文件路径";
            string resultSaveDir = null;
            if (dlg2.ShowDialog() == DialogResult.OK)
            {
                resultSaveDir = dlg2.SelectedPath;
            }
            if (resultSaveDir != null)
            {
                this.SaveImgPath.Text = resultSaveDir;
            }
            else
            {
                MessageBox.Show("请正确输入结果保存位置！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (iswh)
            {
                int boxwidth = Convert.ToInt32(this.width.Text);
                int boxheight = Convert.ToInt32(this.height.Text);
                CaptureForm capForm = new CaptureForm(boxwidth, boxheight, iswh);
                capForm.Show();
            }
            else
            {
                CaptureForm capForm = new CaptureForm();
                capForm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new HotKeySettingsForm(_currentHotKey))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // 更新当前热键并重置钩子  
                    _currentHotKey = settingsForm.SelectedHotKey;
                    _keyboardHook.Unhook(); // 注销旧的钩子 
                    MessageBox.Show("快捷键已更新为: " + settingsForm.SelectedHotKey, "快捷键更新", MessageBoxButtons.OK);
                    this.textBox1.Text = "Ctrl + " + settingsForm.SelectedHotKey.ToString();
                }
            }
        }
    }
}
