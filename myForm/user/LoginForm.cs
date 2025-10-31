using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.SqlFunc;
using 质检工具.UIPreSet;
using 质检工具.User.UserConfig;


namespace 质检工具
{
    public partial class LoginForm : AntdUI.Window
    {
        public static UserBean userBean;
        
        // 保存原始设计的控件位置和大小
        private Dictionary<Control, Rectangle> originalBounds = new Dictionary<Control, Rectangle>();
        private Dictionary<Control, Font> originalFonts = new Dictionary<Control, Font>();
        
        public LoginForm()
        {
            InitializeComponent();
            
            // 保存所有控件的原始位置和大小
            SaveOriginalControlBounds();
        }
        
        /// <summary>
        /// 保存所有控件的原始位置和大小
        /// </summary>
        private void SaveOriginalControlBounds()
        {
            // 递归保存所有控件的原始位置和大小
            SaveControlBounds(this);
        }
        
        /// <summary>
        /// 递归保存控件及其子控件的原始位置和大小
        /// </summary>
        private void SaveControlBounds(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // 保存位置和大小
                originalBounds[control] = new Rectangle(control.Location, control.Size);
                
                // 保存字体
                if (control.Font != null)
                {
                    originalFonts[control] = new Font(control.Font, control.Font.Style);
                }
                
                // 递归处理子控件
                if (control.HasChildren)
                {
                    SaveControlBounds(control);
                }
            }
        }
        
        /// <summary>
        /// 恢复所有控件到原始设计的位置和大小
        /// </summary>
        private void RestoreOriginalControlBounds()
        {

            int designWidth = 1268;   // 设计时的宽度
            int designHeight = 736;  // 设计时的高度

            // 禁用窗体大小调整
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.MaximizeBox = false;
            //this.MinimizeBox = true;

            // 设置固定大小
            this.Size = new Size(designWidth, designHeight);
            this.MinimumSize = new Size(designWidth, designHeight);
            this.MaximumSize = new Size(designWidth, designHeight);

            // 手动计算居中位置
            this.StartPosition = FormStartPosition.Manual;

            // 获取屏幕工作区域
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

            // 计算居中位置
            int x = (workingArea.Width - designWidth) / 2 + workingArea.X;
            int y = (workingArea.Height - designHeight) / 2 + workingArea.Y;

            // 设置窗体位置
            this.Location = new Point(x, y);
            //RecalculateControlPositions();
            this.SuspendLayout();
            
            try
            {
                foreach (var kvp in originalBounds)
                {
                    Control control = kvp.Key;
                    Rectangle originalRect = kvp.Value;
                    
                    // 恢复位置和大小
                    control.Location = originalRect.Location;
                    control.Size = originalRect.Size;
                    
                    // 恢复字体
                    if (originalFonts.ContainsKey(control))
                    {
                        control.Font = new Font(originalFonts[control], originalFonts[control].Style);
                    }
                }
            }
            finally
            {
                this.ResumeLayout(true);
            }
        }
        
      
        /// <summary>
        /// 调整控件字体大小以适应DPI缩放
        /// </summary>
        private void AdjustFontSizes(float scaleX, float scaleY)
        {
            // 使用较小的缩放比例来调整字体，避免字体过大
            float fontScale = Math.Min(scaleX, scaleY);
            
            
            
            // 调整分隔线字体
            if (divider1.Font != null)
            {
                divider1.Font = new Font(divider1.Font.FontFamily, 10, divider1.Font.Style);
            }
            
            // 调整复选框字体
            if (checkbox1.Font != null)
            {
                float newSize = checkbox1.Font.Size * fontScale;
                checkbox1.Font = new Font(checkbox1.Font.FontFamily, 7, checkbox1.Font.Style);
            }
            
            // 调整标签字体
            if (label1.Font != null)
            {
                float newSize = label1.Font.Size * fontScale;
                label1.Font = new Font(label1.Font.FontFamily, Math.Max(8, newSize), label1.Font.Style);
            }
            
            if (label6.Font != null)
            {
                float newSize = label6.Font.Size * fontScale;
                label6.Font = new Font(label6.Font.FontFamily, Math.Max(10, newSize), label6.Font.Style);
            }
            
            if (label7.Font != null)
            {
                float newSize = label7.Font.Size * fontScale;
                label7.Font = new Font(label7.Font.FontFamily, 1, label7.Font.Style);
            }
            
            // 调整页眉字体
            if (pageHeader2.Font != null)
            {
                //float newSize = pageHeader2.Font.Size * fontScale;
                pageHeader2.Font = new Font(pageHeader2.Font.FontFamily, 14, pageHeader2.Font.Style);
            }

            // 调整关闭按钮字体
            close_btn.TextAlign = ContentAlignment.MiddleCenter;
            if (close_btn.Font != null)
            {
                float newSize = close_btn.Font.Size * fontScale;
                close_btn.Font = new Font(close_btn.Font.FontFamily, 12, close_btn.Font.Style);
            }
        }
        /// <summary>
        /// 根据控件大小动态调整字体大小
        /// </summary>
        private void AdjustFontSizes2()
        {
            pageHeader2.Font = new Font(pageHeader2.Font.FontFamily, 17, pageHeader2.Font.Style);
            divider1.Font = new Font(divider1.Font.FontFamily, 10, divider1.Font.Style);
            //checkbox1.Font = new Font(checkbox1.Font.FontFamily, 8, checkbox1.Font.Style);
            // 根据控件高度计算合适的字体大小
            //AdjustControlFont(pageHeader2, 0.4f); // 页眉字体占控件高度的60%
            //AdjustControlFont(divider1, 0.6f);    // 分隔线字体占控件高度的70%
            //AdjustControlFont(checkbox1, 0.4f);   // 复选框字体占控件高度的60%
            //AdjustControlFont(input1, 0.5f);      // 输入框字体占控件高度的50%
            //AdjustControlFont(input2, 0.5f);      // 输入框字体占控件高度的50%
            //AdjustControlFont(Login_btn, 0.4f);   // 按钮字体占控件高度的40%
            //AdjustControlFont(resign_btn, 0.4f);  // 按钮字体占控件高度的40%
            //AdjustControlFont(label1, 0.5f);      // 标签字体占控件高度的50%
            //AdjustControlFont(label6, 0.6f);      // 标签字体占控件高度的60%
            //AdjustControlFont(label7, 0.6f);      // 标签字体占控件高度的60%
            //AdjustControlFont(button1, 0.5f);     // 关闭按钮字体占控件高度的50%
        }
        
        /// <summary>
        /// 根据控件大小调整单个控件的字体
        /// </summary>
        /// <param name="control">要调整的控件</param>
        /// <param name="heightRatio">字体大小相对于控件高度的比例</param>
        private void AdjustControlFont(Control control, float heightRatio)
        {
            if (control?.Font != null && control.Height > 0)
            {
                // 根据控件高度计算字体大小
                float fontSize = control.Height * heightRatio;
                
                // 设置最小和最大字体大小限制
                //fontSize = Math.Max(6, Math.Min(fontSize, 72));
                
                try
                {
                    control.Font = new Font(control.Font.FontFamily, fontSize, control.Font.Style);
                }
                catch
                {
                    // 如果字体创建失败，使用默认字体
                    //control.Font = new Font("Microsoft YaHei", fontSize, FontStyle.Regular);
                }
            }
        }
        bool resignGIS = false;
        bool loading = false;
        bool isInstallGIS = false;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ControlPreSet.SetAntdUIbtn(Login_btn);
            
            // 恢复所有控件到原始设计的位置和大小
            RestoreOriginalControlBounds();
            AdjustFontSizes2();
            //pageHeader1.Text = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "");
            //InitializeFunc.InitializeGIS();

            getlastlogin();
            isInstallGIS = IsArcGISDesktopInstalled();

            if (isInstallGIS)
            {
                Task.Run(() =>
                {
                    InitializeFunc.InitializeGIS();
                    resignGIS = true;
                });
            }
           

        }

        string GetValueAsString(object value)
        {
            return value == null || value == DBNull.Value ? "" : Convert.ToString(value);
        }
        private System.Windows.Forms.Timer gisTimer;

        private void button1_Click(object sender, EventArgs e)
        {
            string phone = input1.Text;
            string password = input2.Text;
            string remindpass = checkbox1.Checked.ToString();
            if (!string.IsNullOrWhiteSpace(input1.Text)&& !string.IsNullOrWhiteSpace(input2.Text))
            {
                string noticemessage = "";
                if (GetUser.hasaccount(input1.Text))
                {
                    if(GetUser.updatelogin( phone,  password,  remindpass))//验证密码 正确则更新登录时间
                    {
                        Dictionary<string, object> res = GetUser.getlastlogin();
                        if (res != null)
                        {
                            userBean = new UserBean();
                            userBean.name = res["name"].ToString();
                            userBean.phone = res["phone"].ToString();
                            userBean.type =  res["usertype"].ToString();
                        }
                        skip2main();
                    }
                    else
                    {
                        noticemessage="\n密码错误";
                    }
                }
                else { noticemessage = "\n没有该账户"; }
                if (noticemessage == "") { return; }
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, "登录", noticemessage, AntdUI.TType.None)
                {
                    OnButtonStyle = (id, btn) =>
                    {
                        btn.BackExtend = "135, #6253E1, #04BEFE";
                    },
                    CancelText = null,
                    OkText = "知道了"
                });
            }
        }
        private void skip2main()
        {
            label1.Visible = true;
            uiProgressIndicator1.Visible = true;
            uiProgressIndicator1.BringToFront();
            this.Enabled = false;
            if (gisTimer == null&&isInstallGIS)
            {
                gisTimer = new System.Windows.Forms.Timer();
                gisTimer.Interval = 500; // 0.5秒
                gisTimer.Tick += (s, args) =>
                {
                    if (resignGIS)
                    {
                        gisTimer.Stop();
                        Cursor = Cursors.Default; // 恢复鼠标样式
                       
                        this.Visible = false;
                        Rib af = new Rib();
                        af.Show();
                       
                    }
                };
            }
            Cursor = Cursors.WaitCursor; // 设置鼠标为加载样式
            Login_btn.Cursor = Cursors.WaitCursor;
            gisTimer.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            InitializeFunc.ShowForm<ResignForm>();
        }

        //获取上次登录
        private void getlastlogin()
        {
            Dictionary<string, object> res = GetUser.getlastlogin();
            if (res != null)
            {
                input1.Text = res["phone"].ToString();
                if (res["remindpassword"].ToString() == "True")
                {
                    checkbox1.Checked = true;
                    input2.Text = res["password"].ToString();
                }
                else { input2.Text = ""; }
                
            }
            else
            {
                input1.Text = "";
                input2.Text = "";
            }
        }

        private void close_btn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public static bool IsArcGISDesktopInstalled()
        {
            // ArcGIS Desktop 的注册表路径（32位和64位系统）
            string[] registryPaths = new string[]
            {
                @"SOFTWARE\ESRI\ArcGIS",           // 64位系统主路径
                @"SOFTWARE\WOW6432Node\ESRI\ArcGIS" // 32位程序在64位系统上的路径
            };

            foreach (string path in registryPaths)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(path))
                {
                    if (key != null)
                    {
                        object versionObj = key.GetValue("RealVersion");
                        if (versionObj != null)
                        {
                            Console.WriteLine($"ArcGIS Desktop 已安装，版本: {versionObj}");
                            if(versionObj.ToString().Substring(0, 4) != "10.8")
                            {
                                MessageBox.Show("ArcGIS版本不是10.8，软件可能无法正常使用");
                            }
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ArcGIS可能未安装，软件无法正常使用");
                    }
                }
            }

            return false;
        }
    }
}
