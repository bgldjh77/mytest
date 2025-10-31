namespace ZJZX_ZJAddin.JTGJ
{
    partial class JTGJ_dockWindow
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Storagepathbutton = new AntdUI.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.FeatureName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.截图存放路径 = new System.Windows.Forms.Label();
            this.SaveImgPath = new System.Windows.Forms.TextBox();
            this.width = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button2 = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Storagepathbutton);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.FeatureName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.截图存放路径);
            this.panel1.Controls.Add(this.SaveImgPath);
            this.panel1.Controls.Add(this.width);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.height);
            this.panel1.Location = new System.Drawing.Point(18, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 340);
            this.panel1.TabIndex = 2;
            // 
            // Storagepathbutton
            // 
            this.Storagepathbutton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Storagepathbutton.BackgroundImage = global::质检工具.Properties.Resources.打开工程;
            this.Storagepathbutton.BorderWidth = 2F;
            this.Storagepathbutton.Icon = global::质检工具.Properties.Resources.打开工程大;
            this.Storagepathbutton.IconGap = 1F;
            this.Storagepathbutton.IconRatio = 2F;
            this.Storagepathbutton.Location = new System.Drawing.Point(181, 146);
            this.Storagepathbutton.Name = "Storagepathbutton";
            this.Storagepathbutton.Size = new System.Drawing.Size(62, 41);
            this.Storagepathbutton.TabIndex = 25;
            this.Storagepathbutton.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(80, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "自动截图参数栏";
            // 
            // FeatureName
            // 
            this.FeatureName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FeatureName.FormattingEnabled = true;
            this.FeatureName.Location = new System.Drawing.Point(18, 283);
            this.FeatureName.Name = "FeatureName";
            this.FeatureName.Size = new System.Drawing.Size(223, 20);
            this.FeatureName.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(14, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "截图要素名称:";
            // 
            // 截图存放路径
            // 
            this.截图存放路径.AutoSize = true;
            this.截图存放路径.Font = new System.Drawing.Font("宋体", 11F);
            this.截图存放路径.Location = new System.Drawing.Point(14, 157);
            this.截图存放路径.Name = "截图存放路径";
            this.截图存放路径.Size = new System.Drawing.Size(105, 15);
            this.截图存放路径.TabIndex = 5;
            this.截图存放路径.Text = "截图存放路径:";
            // 
            // SaveImgPath
            // 
            this.SaveImgPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveImgPath.Location = new System.Drawing.Point(18, 193);
            this.SaveImgPath.Name = "SaveImgPath";
            this.SaveImgPath.Size = new System.Drawing.Size(225, 21);
            this.SaveImgPath.TabIndex = 4;
            // 
            // width
            // 
            this.width.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.width.Location = new System.Drawing.Point(140, 94);
            this.width.Multiline = true;
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(103, 30);
            this.width.TabIndex = 3;
            this.width.Text = "400";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(14, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "截图宽度:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(14, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "截图高度:";
            // 
            // height
            // 
            this.height.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.height.Location = new System.Drawing.Point(140, 43);
            this.height.Multiline = true;
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(103, 30);
            this.height.TabIndex = 0;
            this.height.Text = "500";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(18, 460);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "是否启用快捷键";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(185, 454);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(95, 21);
            this.textBox1.TabIndex = 6;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(18, 547);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(132, 16);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "是否指定截图的长宽";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BorderWidth = 2F;
            this.button2.Icon = global::质检工具.Properties.Resources.快捷键;
            this.button2.IconGap = 1F;
            this.button2.IconRatio = 2F;
            this.button2.Location = new System.Drawing.Point(18, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(262, 36);
            this.button2.TabIndex = 27;
            this.button2.Text = "修改快捷键";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BorderWidth = 2F;
            this.button1.Icon = global::质检工具.Properties.Resources.截图2;
            this.button1.IconGap = 1F;
            this.button1.IconRatio = 2F;
            this.button1.Location = new System.Drawing.Point(18, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(262, 36);
            this.button1.TabIndex = 26;
            this.button1.Text = "手动截图";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // JTGJ_dockWindow
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Name = "JTGJ_dockWindow";
            this.Size = new System.Drawing.Size(300, 596);
            this.Load += new System.EventHandler(this.JTGJ_dockWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox width;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox height;
        private System.Windows.Forms.Label 截图存放路径;
        private System.Windows.Forms.TextBox SaveImgPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FeatureName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private AntdUI.Button Storagepathbutton;
        private AntdUI.Button button1;
        private AntdUI.Button button2;
    }
}
