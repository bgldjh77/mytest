
namespace 质检工具
{
    partial class BZCY_Form
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
            this.download_btn = new Sunny.UI.UIButton();
            this.tree1 = new AntdUI.Tree();
            this.selectAll_btn = new Sunny.UI.UIButton();
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
            this.pageHeader1.TabIndex = 4;
            this.pageHeader1.Text = "标准查阅";
            // 
            // download_btn
            // 
            this.download_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.download_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.download_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.download_btn.Location = new System.Drawing.Point(690, 416);
            this.download_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.download_btn.Name = "download_btn";
            this.download_btn.Size = new System.Drawing.Size(110, 33);
            this.download_btn.TabIndex = 7;
            this.download_btn.Text = "批量下载";
            this.download_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.download_btn.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // tree1
            // 
            this.tree1.BlockNode = true;
            this.tree1.Checkable = true;
            this.tree1.Font = new System.Drawing.Font("宋体", 14F);
            this.tree1.Location = new System.Drawing.Point(0, 48);
            this.tree1.Name = "tree1";
            this.tree1.Size = new System.Drawing.Size(800, 362);
            this.tree1.TabIndex = 8;
            this.tree1.Text = "tree1";
            this.tree1.CheckedChanged += new AntdUI.TreeCheckedEventHandler(this.tree1_CheckedChanged);
            this.tree1.NodeMouseClick += new AntdUI.TreeSelectEventHandler(this.tree1_NodeMouseClick);
            // 
            // selectAll_btn
            // 
            this.selectAll_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAll_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selectAll_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.selectAll_btn.Location = new System.Drawing.Point(0, 416);
            this.selectAll_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.selectAll_btn.Name = "selectAll_btn";
            this.selectAll_btn.Size = new System.Drawing.Size(110, 33);
            this.selectAll_btn.TabIndex = 9;
            this.selectAll_btn.Text = "全选";
            this.selectAll_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.selectAll_btn.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // BZCY_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.selectAll_btn);
            this.Controls.Add(this.tree1);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.download_btn);
            this.Name = "BZCY_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BZCY_Form";
            this.Load += new System.EventHandler(this.BZCY_Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private Sunny.UI.UIButton download_btn;
        private AntdUI.Tree tree1;
        private Sunny.UI.UIButton selectAll_btn;
    }
}