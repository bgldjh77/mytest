
namespace 质检工具
{
    partial class LicenseForm
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
            this.licText = new AntdUI.Input();
            this.button2 = new AntdUI.Button();
            this.button3 = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.label1 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.button1 = new AntdUI.Button();
            this.input2 = new AntdUI.Input();
            this.SuspendLayout();
            // 
            // licText
            // 
            this.licText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.licText.Location = new System.Drawing.Point(81, 141);
            this.licText.Name = "licText";
            this.licText.SelectionStart = 6;
            this.licText.Size = new System.Drawing.Size(411, 46);
            this.licText.TabIndex = 1;
            this.licText.Text = "input1";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(511, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 46);
            this.button2.TabIndex = 2;
            this.button2.Text = "选择许可";
            this.button2.Type = AntdUI.TTypeMini.Primary;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(226, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 46);
            this.button3.TabIndex = 3;
            this.button3.Text = "授权";
            this.button3.Type = AntdUI.TTypeMini.Primary;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("宋体", 12F);
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(608, 50);
            this.pageHeader1.TabIndex = 4;
            this.pageHeader1.Text = "软件授权";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(13, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 46);
            this.label1.TabIndex = 5;
            this.label1.Text = "许可";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(13, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 46);
            this.label2.TabIndex = 8;
            this.label2.Text = "机器码";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(511, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 46);
            this.button1.TabIndex = 7;
            this.button1.Text = "复制";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // input2
            // 
            this.input2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input2.Location = new System.Drawing.Point(81, 78);
            this.input2.Name = "input2";
            this.input2.SelectionStart = 6;
            this.input2.Size = new System.Drawing.Size(411, 46);
            this.input2.TabIndex = 6;
            this.input2.Text = "input2";
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 291);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.input2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.licText);
            this.Name = "LicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LicenseForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LicenseForm_FormClosed);
            this.Load += new System.EventHandler(this.LicenseForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input licText;
        private AntdUI.Button button2;
        private AntdUI.Button button3;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Label label1;
        private AntdUI.Label label2;
        private AntdUI.Button button1;
        private AntdUI.Input input2;
    }
}