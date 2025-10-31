
namespace 质检工具.Func
{
    partial class TourPopover
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
            this.label1 = new AntdUI.Label();
            this.panel1 = new AntdUI.Panel();
            this.btn_previous = new AntdUI.Button();
            this.btn_next = new AntdUI.Button();
            this.label3 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_previous);
            this.panel1.Controls.Add(this.btn_next);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 39);
            this.panel1.TabIndex = 1;
            this.panel1.Text = "panel1";
            // 
            // btn_previous
            // 
            this.btn_previous.AutoSizeMode = AntdUI.TAutoSize.Width;
            this.btn_previous.BorderWidth = 1F;
            this.btn_previous.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_previous.Location = new System.Drawing.Point(41, 0);
            this.btn_previous.Name = "btn_previous";
            this.btn_previous.Size = new System.Drawing.Size(70, 39);
            this.btn_previous.TabIndex = 2;
            this.btn_previous.Text = "上一页";
            this.btn_previous.Click += new System.EventHandler(this.btn_previous_Click);
            // 
            // btn_next
            // 
            this.btn_next.AutoSizeMode = AntdUI.TAutoSize.Width;
            this.btn_next.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_next.Location = new System.Drawing.Point(111, 0);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(70, 39);
            this.btn_next.TabIndex = 1;
            this.btn_next.Text = "下一页";
            this.btn_next.Type = AntdUI.TTypeMini.Primary;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 71);
            this.label3.TabIndex = 2;
            this.label3.Text = "";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "";
            // 
            // TourPopover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "TourPopover";
            this.Size = new System.Drawing.Size(181, 146);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label label1;
        private AntdUI.Panel panel1;
        private AntdUI.Button btn_previous;
        private AntdUI.Button btn_next;
        private AntdUI.Label label3;
        private AntdUI.Label label2;
    }
}