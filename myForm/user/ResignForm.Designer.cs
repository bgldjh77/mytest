
namespace 质检工具
{
    partial class ResignForm
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
            this.input1 = new AntdUI.Input();
            this.input2 = new AntdUI.Input();
            this.label1 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.select1 = new AntdUI.Select();
            this.label3 = new AntdUI.Label();
            this.button1 = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.SuspendLayout();
            // 
            // input1
            // 
            this.input1.Location = new System.Drawing.Point(266, 130);
            this.input1.Name = "input1";
            this.input1.SelectionStart = 6;
            this.input1.Size = new System.Drawing.Size(275, 43);
            this.input1.TabIndex = 0;
            this.input1.Text = "input1";
            // 
            // input2
            // 
            this.input2.Location = new System.Drawing.Point(263, 204);
            this.input2.Name = "input2";
            this.input2.PasswordChar = '*';
            this.input2.SelectionStart = 6;
            this.input2.Size = new System.Drawing.Size(275, 43);
            this.input2.TabIndex = 1;
            this.input2.Text = "input2";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(155, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "账户";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(155, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            // 
            // select1
            // 
            this.select1.Location = new System.Drawing.Point(266, 275);
            this.select1.Name = "select1";
            this.select1.SelectionStart = 7;
            this.select1.Size = new System.Drawing.Size(272, 38);
            this.select1.TabIndex = 4;
            this.select1.Text = "select1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(155, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "用户类型";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "注册";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(732, 50);
            this.pageHeader1.TabIndex = 7;
            this.pageHeader1.Text = "注册";
            // 
            // ResignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 450);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.select1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input2);
            this.Controls.Add(this.input1);
            this.Name = "ResignForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResignForm";
            this.Load += new System.EventHandler(this.ResignForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input input1;
        private AntdUI.Input input2;
        private AntdUI.Label label1;
        private AntdUI.Label label2;
        private AntdUI.Select select1;
        private AntdUI.Label label3;
        private AntdUI.Button button1;
        private AntdUI.PageHeader pageHeader1;
    }
}