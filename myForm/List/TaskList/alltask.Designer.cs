namespace 质检工具.myForm.List.TaskList
{
    partial class alltask
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
            this.pageHeader1 = new AntdUI.PageHeader();
            this.addTask_btn = new Sunny.UI.UIButton();
            this.flowLayoutPanel1 = new AntdUI.In.FlowLayoutPanel();
            this.saveallbtn = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(191)))), ((int)(((byte)(136)))));
            this.pageHeader1.DividerShow = true;
            this.pageHeader1.DividerThickness = 3F;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageHeader1.ForeColor = System.Drawing.Color.White;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(1429, 25);
            this.pageHeader1.TabIndex = 3;
            this.pageHeader1.Text = "批量任务设置";
            // 
            // addTask_btn
            // 
            this.addTask_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addTask_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addTask_btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.addTask_btn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.addTask_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addTask_btn.Location = new System.Drawing.Point(900, 726);
            this.addTask_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.addTask_btn.Name = "addTask_btn";
            this.addTask_btn.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.addTask_btn.Size = new System.Drawing.Size(130, 35);
            this.addTask_btn.TabIndex = 2;
            this.addTask_btn.Text = "开始执行";
            this.addTask_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addTask_btn.Click += new System.EventHandler(this.addTask_btn_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1405, 689);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // saveallbtn
            // 
            this.saveallbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveallbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveallbtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.saveallbtn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.saveallbtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveallbtn.Location = new System.Drawing.Point(326, 726);
            this.saveallbtn.MinimumSize = new System.Drawing.Size(1, 1);
            this.saveallbtn.Name = "saveallbtn";
            this.saveallbtn.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.saveallbtn.Size = new System.Drawing.Size(130, 35);
            this.saveallbtn.TabIndex = 5;
            this.saveallbtn.Text = "暂存";
            this.saveallbtn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveallbtn.Click += new System.EventHandler(this.saveAll_Click);
            // 
            // alltask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 773);
            this.Controls.Add(this.saveallbtn);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.addTask_btn);
            this.Name = "alltask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolForm";
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.PageHeader pageHeader1;
        private Sunny.UI.UIButton addTask_btn;
        private AntdUI.In.FlowLayoutPanel flowLayoutPanel1;
        private Sunny.UI.UIButton saveallbtn;
    }
}