
namespace 质检工具
{
    partial class popover_saveProject
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
            this.open_btn = new AntdUI.Button();
            this.save_btn = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // open_btn
            // 
            this.open_btn.Location = new System.Drawing.Point(3, 3);
            this.open_btn.Name = "open_btn";
            this.open_btn.Size = new System.Drawing.Size(117, 47);
            this.open_btn.TabIndex = 0;
            this.open_btn.Text = "打开工程";
            this.open_btn.Type = AntdUI.TTypeMini.Primary;
            this.open_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(126, 3);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(117, 47);
            this.save_btn.TabIndex = 1;
            this.save_btn.Text = "保存工程";
            this.save_btn.Type = AntdUI.TTypeMini.Primary;
            this.save_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // popover_saveProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.open_btn);
            this.Name = "popover_saveProject";
            this.Size = new System.Drawing.Size(245, 55);
            this.Load += new System.EventHandler(this.popover_saveProject_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button open_btn;
        private AntdUI.Button save_btn;
    }
}