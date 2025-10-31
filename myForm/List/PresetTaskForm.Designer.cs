
namespace 质检工具
{
    partial class PresetTaskForm
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
            this.tree1 = new AntdUI.Tree();
            this.AddTask_btn = new Sunny.UI.UIButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openfolder_btn = new Sunny.UI.UIButton();
            this.importfile_btn = new Sunny.UI.UIButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("宋体", 12F);
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(800, 50);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "预设任务";
            // 
            // tree1
            // 
            this.tree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tree1.BlockNode = true;
            this.tree1.Checkable = true;
            this.tree1.Font = new System.Drawing.Font("宋体", 14F);
            this.tree1.Location = new System.Drawing.Point(0, 46);
            this.tree1.Name = "tree1";
            this.tree1.Size = new System.Drawing.Size(800, 364);
            this.tree1.TabIndex = 1;
            this.tree1.Text = "tree1";
            this.tree1.SelectChanged += new AntdUI.TreeSelectEventHandler(this.tree1_SelectChanged);
            this.tree1.CheckedChanged += new AntdUI.TreeCheckedEventHandler(this.tree1_CheckedChanged);
            this.tree1.NodeMouseClick += new AntdUI.TreeSelectEventHandler(this.tree1_NodeMouseClick);
            // 
            // AddTask_btn
            // 
            this.AddTask_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddTask_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddTask_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddTask_btn.Location = new System.Drawing.Point(687, 3);
            this.AddTask_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.AddTask_btn.Name = "AddTask_btn";
            this.AddTask_btn.Size = new System.Drawing.Size(110, 33);
            this.AddTask_btn.TabIndex = 4;
            this.AddTask_btn.Text = "添加任务";
            this.AddTask_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddTask_btn.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.AddTask_btn);
            this.flowLayoutPanel1.Controls.Add(this.importfile_btn);
            this.flowLayoutPanel1.Controls.Add(this.openfolder_btn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 409);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 41);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // openfolder_btn
            // 
            this.openfolder_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openfolder_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openfolder_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.openfolder_btn.Location = new System.Drawing.Point(455, 3);
            this.openfolder_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.openfolder_btn.Name = "openfolder_btn";
            this.openfolder_btn.Size = new System.Drawing.Size(110, 33);
            this.openfolder_btn.TabIndex = 5;
            this.openfolder_btn.Text = "预设文件夹";
            this.openfolder_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.openfolder_btn.Click += new System.EventHandler(this.openfolder_btn_Click);
            // 
            // importfile_btn
            // 
            this.importfile_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importfile_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.importfile_btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importfile_btn.Location = new System.Drawing.Point(571, 3);
            this.importfile_btn.MinimumSize = new System.Drawing.Size(1, 1);
            this.importfile_btn.Name = "importfile_btn";
            this.importfile_btn.Size = new System.Drawing.Size(110, 33);
            this.importfile_btn.TabIndex = 6;
            this.importfile_btn.Text = "添加配置";
            this.importfile_btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importfile_btn.Click += new System.EventHandler(this.importfile_btn_Click);
            // 
            // PresetTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tree1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "PresetTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PresetTaskForm";
            this.Load += new System.EventHandler(this.PresetTaskForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Tree tree1;
        private Sunny.UI.UIButton AddTask_btn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Sunny.UI.UIButton openfolder_btn;
        private Sunny.UI.UIButton importfile_btn;
    }
}