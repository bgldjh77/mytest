
namespace 质检工具
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.Login_btn = new AntdUI.Button();
            this.input1 = new AntdUI.Input();
            this.input2 = new AntdUI.Input();
            this.resign_btn = new AntdUI.Button();
            this.divider1 = new AntdUI.Divider();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pageHeader2 = new AntdUI.PageHeader();
            this.close_btn = new AntdUI.Button();
            this.label3 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.checkbox1 = new AntdUI.Checkbox();
            this.uiProgressIndicator1 = new Sunny.UI.UIProgressIndicator();
            this.label1 = new AntdUI.Label();
            this.label6 = new AntdUI.Label();
            this.label7 = new AntdUI.Label();
            this.pageHeader1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Login_btn
            // 
            this.Login_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Login_btn.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Login_btn.Location = new System.Drawing.Point(888, 477);
            this.Login_btn.Name = "Login_btn";
            this.Login_btn.Size = new System.Drawing.Size(93, 39);
            this.Login_btn.TabIndex = 2;
            this.Login_btn.Text = "登录";
            this.Login_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // input1
            // 
            this.input1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input1.Location = new System.Drawing.Point(849, 287);
            this.input1.Name = "input1";
            this.input1.PlaceholderText = "账户";
            this.input1.Size = new System.Drawing.Size(307, 42);
            this.input1.TabIndex = 3;
            // 
            // input2
            // 
            this.input2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input2.Location = new System.Drawing.Point(850, 352);
            this.input2.Name = "input2";
            this.input2.PasswordChar = '*';
            this.input2.PlaceholderText = "密码";
            this.input2.Size = new System.Drawing.Size(307, 42);
            this.input2.TabIndex = 4;
            // 
            // resign_btn
            // 
            this.resign_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resign_btn.BorderWidth = 1F;
            this.resign_btn.Location = new System.Drawing.Point(1011, 477);
            this.resign_btn.Name = "resign_btn";
            this.resign_btn.Size = new System.Drawing.Size(93, 39);
            this.resign_btn.TabIndex = 6;
            this.resign_btn.Text = "注册";
            this.resign_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // divider1
            // 
            this.divider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(26)))), ((int)(((byte)(82)))));
            this.divider1.ColorSplit = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(115)))), ((int)(((byte)(246)))));
            this.divider1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.divider1.ForeColor = System.Drawing.Color.White;
            this.divider1.Location = new System.Drawing.Point(849, 197);
            this.divider1.Name = "divider1";
            this.divider1.Size = new System.Drawing.Size(307, 29);
            this.divider1.TabIndex = 7;
            this.divider1.Text = " 用户登录 ";
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.Color.Transparent;
            this.pageHeader1.Controls.Add(this.pictureBox1);
            this.pageHeader1.Controls.Add(this.pageHeader2);
            this.pageHeader1.Controls.Add(this.close_btn);
            this.pageHeader1.Controls.Add(this.label3);
            this.pageHeader1.Controls.Add(this.label2);
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.ForeColor = System.Drawing.Color.White;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(1268, 39);
            this.pageHeader1.TabIndex = 8;
            this.pageHeader1.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::质检工具.Properties.Resources.圆白底图标1;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pageHeader2
            // 
            this.pageHeader2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageHeader2.BackColor = System.Drawing.Color.Transparent;
            this.pageHeader2.ColorScheme = AntdUI.TAMode.Dark;
            this.pageHeader2.Font = new System.Drawing.Font("楷体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.pageHeader2.ForeColor = System.Drawing.Color.Turquoise;
            this.pageHeader2.Location = new System.Drawing.Point(39, 0);
            this.pageHeader2.Mode = AntdUI.TAMode.Dark;
            this.pageHeader2.Name = "pageHeader2";
            this.pageHeader2.Size = new System.Drawing.Size(995, 40);
            this.pageHeader2.TabIndex = 17;
            this.pageHeader2.Text = "粤检易  测绘地理信息成果质检软件(测试版)";
            // 
            // close_btn
            // 
            this.close_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close_btn.BackActive = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(7)))), ((int)(((byte)(11)))));
            this.close_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(7)))), ((int)(((byte)(11)))));
            this.close_btn.DefaultBack = System.Drawing.Color.Transparent;
            this.close_btn.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.close_btn.ForeColor = System.Drawing.Color.White;
            this.close_btn.Location = new System.Drawing.Point(1206, 2);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(60, 39);
            this.close_btn.TabIndex = 17;
            this.close_btn.Text = "×";
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click_1);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(139, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 39);
            this.label3.TabIndex = 12;
            this.label3.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(44, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "label2";
            // 
            // checkbox1
            // 
            this.checkbox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(26)))), ((int)(((byte)(82)))));
            this.checkbox1.ForeColor = System.Drawing.Color.White;
            this.checkbox1.Location = new System.Drawing.Point(855, 417);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(113, 27);
            this.checkbox1.TabIndex = 9;
            this.checkbox1.Text = "记住密码";
            // 
            // uiProgressIndicator1
            // 
            this.uiProgressIndicator1.Active = true;
            this.uiProgressIndicator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiProgressIndicator1.BackColor = System.Drawing.Color.Transparent;
            this.uiProgressIndicator1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiProgressIndicator1.Location = new System.Drawing.Point(853, 235);
            this.uiProgressIndicator1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiProgressIndicator1.Name = "uiProgressIndicator1";
            this.uiProgressIndicator1.Size = new System.Drawing.Size(307, 159);
            this.uiProgressIndicator1.Style = Sunny.UI.UIStyle.Custom;
            this.uiProgressIndicator1.TabIndex = 10;
            this.uiProgressIndicator1.Text = "uiProgressIndicator1";
            this.uiProgressIndicator1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(853, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 338);
            this.label1.TabIndex = 11;
            this.label1.Text = "加载中";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label1.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(351, 676);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(671, 39);
            this.label6.TabIndex = 15;
            this.label6.Text = "由广东省测绘产品质量监督检验中心提供技术支持";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("楷体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(351, 703);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(671, 39);
            this.label7.TabIndex = 16;
            this.label7.Text = "软件交流QQ群：891474122";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::质检工具.Properties.Resources.图片修改_1__1_;
            this.ClientSize = new System.Drawing.Size(1268, 736);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uiProgressIndicator1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input2);
            this.Controls.Add(this.input1);
            this.Controls.Add(this.checkbox1);
            this.Controls.Add(this.resign_btn);
            this.Controls.Add(this.Login_btn);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.divider1);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pageHeader1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.Button Login_btn;
        private AntdUI.Input input1;
        private AntdUI.Input input2;
        private AntdUI.Button resign_btn;
        private AntdUI.Divider divider1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Checkbox checkbox1;
        private Sunny.UI.UIProgressIndicator uiProgressIndicator1;
        private AntdUI.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AntdUI.Label label3;
        private AntdUI.Label label2;
        private AntdUI.Label label6;
        private AntdUI.Label label7;
        private AntdUI.Button close_btn;
        private AntdUI.PageHeader pageHeader2;
    }
}