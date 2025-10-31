
namespace 质检工具
{
    partial class SpacialRef_SelectForm
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
            this.ok_btn = new Sunny.UI.UIButton();
            this.spcaialRefTree = new AntdUI.Tree();
            this.SearchInput = new AntdUI.Input();
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
            this.pageHeader1.TabIndex = 5;
            this.pageHeader1.Text = "坐标选择";
            // 
            // ok_btn
            // 
            this.ok_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ok_btn.Location = new System.Drawing.Point(690, 413);
            this.ok_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(110, 33);
            this.ok_btn.TabIndex = 8;
            this.ok_btn.Text = "确定";
            this.ok_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // spcaialRefTree
            // 
            this.spcaialRefTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcaialRefTree.BlockNode = true;
            this.spcaialRefTree.Font = new System.Drawing.Font("宋体", 14F);
            this.spcaialRefTree.Location = new System.Drawing.Point(0, 43);
            this.spcaialRefTree.Name = "spcaialRefTree";
            this.spcaialRefTree.Size = new System.Drawing.Size(800, 364);
            this.spcaialRefTree.TabIndex = 9;
            this.spcaialRefTree.Text = "tree1";
            this.spcaialRefTree.SelectChanged += new AntdUI.TreeSelectEventHandler(this.spcaialRefTree_SelectChanged);
            // 
            // SearchInput
            // 
            this.SearchInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchInput.IconRatio = 1F;
            this.SearchInput.Location = new System.Drawing.Point(0, 404);
            this.SearchInput.Name = "SearchInput";
            this.SearchInput.PlaceholderText = "搜索坐标";
            this.SearchInput.PrefixSvg = "SearchOutlined";
            this.SearchInput.Size = new System.Drawing.Size(375, 42);
            this.SearchInput.TabIndex = 101;
            this.SearchInput.TabStop = false;
            this.SearchInput.TextChanged += new System.EventHandler(this.SearchInput_TextChanged);
            // 
            // SpacialRef_SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SearchInput);
            this.Controls.Add(this.spcaialRefTree);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.pageHeader1);
            this.Name = "SpacialRef_SelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpacialRef_SelectForm";
            this.Load += new System.EventHandler(this.SpacialRef_SelectForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private Sunny.UI.UIButton ok_btn;
        private AntdUI.Tree spcaialRefTree;
        private AntdUI.Input SearchInput;
    }
}