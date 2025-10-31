
namespace 质检工具
{
    partial class MuDownloadForm
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
            this.table1 = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.panel2 = new AntdUI.Panel();
            this.download_btn = new Sunny.UI.UIButton();
            this.pagination1 = new AntdUI.Pagination();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.pageHeader1.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(139)))), ((int)(((byte)(218)))));
            this.pageHeader1.DividerShow = true;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("宋体", 12F);
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(800, 50);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "检查表格与模板下载";
            // 
            // table1
            // 
            this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table1.Location = new System.Drawing.Point(0, 0);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(800, 367);
            this.table1.TabIndex = 1;
            this.table1.Text = "table1";
            this.table1.CellButtonClick += new AntdUI.Table.ClickButtonEventHandler(this.Table1_CellButtonClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.table1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 400);
            this.panel1.TabIndex = 3;
            this.panel1.Text = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.download_btn);
            this.panel2.Controls.Add(this.pagination1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 367);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 33);
            this.panel2.TabIndex = 3;
            this.panel2.Text = "panel2";
            // 
            // download_btn
            // 
            this.download_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.download_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.download_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.download_btn.Location = new System.Drawing.Point(690, 0);
            this.download_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.download_btn.Name = "download_btn";
            this.download_btn.Size = new System.Drawing.Size(110, 33);
            this.download_btn.TabIndex = 3;
            this.download_btn.Text = "批量下载";
            this.download_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.download_btn.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // pagination1
            // 
            this.pagination1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pagination1.Current = 0;
            this.pagination1.Location = new System.Drawing.Point(0, -4);
            this.pagination1.Name = "pagination1";
            this.pagination1.PageSize = 3;
            this.pagination1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pagination1.ShowSizeChanger = true;
            this.pagination1.Size = new System.Drawing.Size(691, 37);
            this.pagination1.TabIndex = 2;
            this.pagination1.Text = "pagination1";
            // 
            // MuDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "MuDownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板下载";
            this.Load += new System.EventHandler(this.MuDownloadForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Table table1;
        private AntdUI.Panel panel1;
        private AntdUI.Pagination pagination1;
        private AntdUI.Panel panel2;
        private Sunny.UI.UIButton download_btn;
    }
}