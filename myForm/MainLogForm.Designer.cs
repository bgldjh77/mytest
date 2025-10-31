
namespace 质检工具
{
    partial class MainLogForm
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
            this.uiRichTextBox1 = new Sunny.UI.UIRichTextBox();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.panel1 = new AntdUI.Panel();
            this.rightmouseMenu = new Sunny.UI.UIContextMenuStrip();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.rightmouseMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiRichTextBox1
            // 
            this.uiRichTextBox1.ContextMenuStrip = this.rightmouseMenu;
            this.uiRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRichTextBox1.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.uiRichTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox1.Name = "uiRichTextBox1";
            this.uiRichTextBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox1.ReadOnly = true;
            this.uiRichTextBox1.ShowText = false;
            this.uiRichTextBox1.Size = new System.Drawing.Size(800, 409);
            this.uiRichTextBox1.TabIndex = 2;
            this.uiRichTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.Color.White;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("宋体", 12F);
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(800, 41);
            this.pageHeader1.TabIndex = 3;
            this.pageHeader1.Text = "日志";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiRichTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 409);
            this.panel1.TabIndex = 4;
            this.panel1.Text = "panel1";
            // 
            // rightmouseMenu
            // 
            this.rightmouseMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.rightmouseMenu.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rightmouseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem});
            this.rightmouseMenu.Name = "rightmouseMenu";
            this.rightmouseMenu.Size = new System.Drawing.Size(109, 26);
            this.rightmouseMenu.Text = "清空";
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // MainLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "MainLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志";
            this.Load += new System.EventHandler(this.MainLogForm_Load);
            this.panel1.ResumeLayout(false);
            this.rightmouseMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public Sunny.UI.UIRichTextBox uiRichTextBox1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Panel panel1;
        private Sunny.UI.UIContextMenuStrip rightmouseMenu;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
    }
}