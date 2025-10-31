
namespace 质检工具
{
    partial class ToolForm
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
            this.start_btn = new Sunny.UI.UIButton();
            this.addTask_btn = new Sunny.UI.UIButton();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.ok_btn = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // start_btn
            // 
            this.start_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.start_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start_btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.start_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_btn.Location = new System.Drawing.Point(108, 441);
            this.start_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.start_btn.Name = "start_btn";
            this.start_btn.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.start_btn.Size = new System.Drawing.Size(130, 35);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "开始任务";
            this.start_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start_btn.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // addTask_btn
            // 
            this.addTask_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addTask_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addTask_btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.addTask_btn.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.addTask_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addTask_btn.Location = new System.Drawing.Point(780, 441);
            this.addTask_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.addTask_btn.Name = "addTask_btn";
            this.addTask_btn.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.addTask_btn.Size = new System.Drawing.Size(130, 35);
            this.addTask_btn.TabIndex = 2;
            this.addTask_btn.Text = "添加任务";
            this.addTask_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addTask_btn.Click += new System.EventHandler(this.uiButton2_Click);
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
            this.pageHeader1.Size = new System.Drawing.Size(1155, 50);
            this.pageHeader1.TabIndex = 3;
            this.pageHeader1.Text = "pageHeader1";
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.uiSymbolButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.Location = new System.Drawing.Point(307, 295);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(100, 35);
            this.uiSymbolButton1.Symbol = 361617;
            this.uiSymbolButton1.SymbolSize = 35;
            this.uiSymbolButton1.TabIndex = 4;
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.Visible = false;
            // 
            // ok_btn
            // 
            this.ok_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ok_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok_btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.ok_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ok_btn.Location = new System.Drawing.Point(436, 441);
            this.ok_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(179)))));
            this.ok_btn.Size = new System.Drawing.Size(130, 35);
            this.ok_btn.TabIndex = 5;
            this.ok_btn.Text = "确定";
            this.ok_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 505);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.uiSymbolButton1);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.addTask_btn);
            this.Controls.Add(this.start_btn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolForm";
            this.Load += new System.EventHandler(this.ToolForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton start_btn;
        private Sunny.UI.UIButton addTask_btn;
        private AntdUI.PageHeader pageHeader1;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UIButton ok_btn;
    }
}