
namespace 质检工具
{
    partial class TaskListForm
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
            this.Back_panel1 = new AntdUI.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.step_run_btn = new AntdUI.Button();
            this.start_btn = new AntdUI.Button();
            this.stopTask_btn = new AntdUI.Button();
            this.delete_btn = new AntdUI.Button();
            this.importTask_btn = new AntdUI.Button();
            this.startDrag_btn = new AntdUI.Button();
            this.List_panel = new System.Windows.Forms.Panel();
            this.zhedie_collapse1 = new AntdUI.Collapse();
            this.collapseItem1 = new AntdUI.CollapseItem();
            this.custom_panel = new System.Windows.Forms.Panel();
            this.custom_Tasktable = new AntdUI.Table();
            this.pagination1 = new AntdUI.Pagination();
            this.collapseItem2 = new AntdUI.CollapseItem();
            this.DOM_panel = new System.Windows.Forms.Panel();
            this.DOM_table = new AntdUI.Table();
            this.DOM_pagination = new AntdUI.Pagination();
            this.collapseItem3 = new AntdUI.CollapseItem();
            this.HAD_panel = new System.Windows.Forms.Panel();
            this.HAD_table = new AntdUI.Table();
            this.HAD_pagination = new AntdUI.Pagination();
            this.Back_panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.List_panel.SuspendLayout();
            this.zhedie_collapse1.SuspendLayout();
            this.collapseItem1.SuspendLayout();
            this.custom_panel.SuspendLayout();
            this.collapseItem2.SuspendLayout();
            this.DOM_panel.SuspendLayout();
            this.collapseItem3.SuspendLayout();
            this.HAD_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.pageHeader1.DividerColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.pageHeader1.DividerShow = true;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("宋体", 12F);
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(1050, 50);
            this.pageHeader1.TabIndex = 1;
            this.pageHeader1.Text = "任务列表";
            // 
            // Back_panel1
            // 
            this.Back_panel1.Controls.Add(this.tableLayoutPanel1);
            this.Back_panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Back_panel1.Location = new System.Drawing.Point(0, 50);
            this.Back_panel1.Name = "Back_panel1";
            this.Back_panel1.Size = new System.Drawing.Size(1050, 550);
            this.Back_panel1.TabIndex = 4;
            this.Back_panel1.Text = "panel1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.List_panel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1050, 550);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.step_run_btn);
            this.flowLayoutPanel1.Controls.Add(this.start_btn);
            this.flowLayoutPanel1.Controls.Add(this.stopTask_btn);
            this.flowLayoutPanel1.Controls.Add(this.delete_btn);
            this.flowLayoutPanel1.Controls.Add(this.importTask_btn);
            this.flowLayoutPanel1.Controls.Add(this.startDrag_btn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1050, 38);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // step_run_btn
            // 
            this.step_run_btn.Location = new System.Drawing.Point(930, 0);
            this.step_run_btn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.step_run_btn.Name = "step_run_btn";
            this.step_run_btn.Size = new System.Drawing.Size(110, 38);
            this.step_run_btn.TabIndex = 10;
            this.step_run_btn.Text = "分步执行";
            this.step_run_btn.Type = AntdUI.TTypeMini.Primary;
            this.step_run_btn.Click += new System.EventHandler(this.step_run_btn_Click);
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(800, 0);
            this.start_btn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(110, 38);
            this.start_btn.TabIndex = 6;
            this.start_btn.Text = "批量执行";
            this.start_btn.Type = AntdUI.TTypeMini.Primary;
            this.start_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopTask_btn
            // 
            this.stopTask_btn.Location = new System.Drawing.Point(670, 0);
            this.stopTask_btn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.stopTask_btn.Name = "stopTask_btn";
            this.stopTask_btn.Size = new System.Drawing.Size(110, 38);
            this.stopTask_btn.TabIndex = 8;
            this.stopTask_btn.Text = "停止";
            this.stopTask_btn.Type = AntdUI.TTypeMini.Warn;
            this.stopTask_btn.Click += new System.EventHandler(this.stopTask_btn_Click);
            // 
            // delete_btn
            // 
            this.delete_btn.Location = new System.Drawing.Point(540, 0);
            this.delete_btn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.delete_btn.Name = "delete_btn";
            this.delete_btn.Size = new System.Drawing.Size(110, 38);
            this.delete_btn.TabIndex = 5;
            this.delete_btn.Text = "删除";
            this.delete_btn.Type = AntdUI.TTypeMini.Error;
            this.delete_btn.Click += new System.EventHandler(this.delete_btn_Click);
            // 
            // importTask_btn
            // 
            this.importTask_btn.Location = new System.Drawing.Point(410, 0);
            this.importTask_btn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.importTask_btn.Name = "importTask_btn";
            this.importTask_btn.Size = new System.Drawing.Size(110, 38);
            this.importTask_btn.TabIndex = 7;
            this.importTask_btn.Text = "导入任务";
            this.importTask_btn.Type = AntdUI.TTypeMini.Success;
            this.importTask_btn.Click += new System.EventHandler(this.importTask_btn_Click);
            // 
            // startDrag_btn
            // 
            this.startDrag_btn.Location = new System.Drawing.Point(280, 0);
            this.startDrag_btn.Margin = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.startDrag_btn.Name = "startDrag_btn";
            this.startDrag_btn.Size = new System.Drawing.Size(110, 38);
            this.startDrag_btn.TabIndex = 9;
            this.startDrag_btn.Text = "排序";
            this.startDrag_btn.Type = AntdUI.TTypeMini.Primary;
            this.startDrag_btn.Click += new System.EventHandler(this.button2_Click);
            // 
            // List_panel
            // 
            this.List_panel.AutoScroll = true;
            this.List_panel.Controls.Add(this.zhedie_collapse1);
            this.List_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List_panel.Location = new System.Drawing.Point(3, 41);
            this.List_panel.Name = "List_panel";
            this.List_panel.Size = new System.Drawing.Size(1044, 506);
            this.List_panel.TabIndex = 3;
            // 
            // zhedie_collapse1
            // 
            this.zhedie_collapse1.BorderWidth = 0F;
            this.zhedie_collapse1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zhedie_collapse1.Dock = System.Windows.Forms.DockStyle.Top;
            this.zhedie_collapse1.Font = new System.Drawing.Font("宋体", 12F);
            this.zhedie_collapse1.Gap = 5;
            this.zhedie_collapse1.HandDragFolder = false;
            this.zhedie_collapse1.Items.Add(this.collapseItem1);
            this.zhedie_collapse1.Items.Add(this.collapseItem2);
            this.zhedie_collapse1.Items.Add(this.collapseItem3);
            this.zhedie_collapse1.Location = new System.Drawing.Point(0, 0);
            this.zhedie_collapse1.Name = "zhedie_collapse1";
            this.zhedie_collapse1.Size = new System.Drawing.Size(1044, 400);
            this.zhedie_collapse1.TabIndex = 3;
            this.zhedie_collapse1.Text = "collapse1";
            this.zhedie_collapse1.Unique = true;
            // 
            // collapseItem1
            // 
            this.collapseItem1.AutoScroll = true;
            this.collapseItem1.Controls.Add(this.custom_panel);
            this.collapseItem1.Location = new System.Drawing.Point(-1006, -198);
            this.collapseItem1.Name = "collapseItem1";
            this.collapseItem1.Size = new System.Drawing.Size(1006, 198);
            this.collapseItem1.TabIndex = 0;
            this.collapseItem1.Text = "自定义任务列表";
            // 
            // custom_panel
            // 
            this.custom_panel.Controls.Add(this.custom_Tasktable);
            this.custom_panel.Controls.Add(this.pagination1);
            this.custom_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_panel.Location = new System.Drawing.Point(0, 0);
            this.custom_panel.Name = "custom_panel";
            this.custom_panel.Size = new System.Drawing.Size(1006, 198);
            this.custom_panel.TabIndex = 3;
            // 
            // custom_Tasktable
            // 
            this.custom_Tasktable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.custom_Tasktable.Location = new System.Drawing.Point(0, 0);
            this.custom_Tasktable.Name = "custom_Tasktable";
            this.custom_Tasktable.Size = new System.Drawing.Size(1006, 166);
            this.custom_Tasktable.TabIndex = 1;
            this.custom_Tasktable.Text = "table1";
            this.custom_Tasktable.CellButtonClick += new AntdUI.Table.ClickButtonEventHandler(this.table1_CellButtonClick);
            // 
            // pagination1
            // 
            this.pagination1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagination1.Location = new System.Drawing.Point(0, 166);
            this.pagination1.Name = "pagination1";
            this.pagination1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pagination1.ShowSizeChanger = true;
            this.pagination1.Size = new System.Drawing.Size(1006, 32);
            this.pagination1.TabIndex = 2;
            this.pagination1.Text = "pagination1";
            // 
            // collapseItem2
            // 
            this.collapseItem2.Controls.Add(this.DOM_panel);
            this.collapseItem2.Location = new System.Drawing.Point(-1006, -257);
            this.collapseItem2.Name = "collapseItem2";
            this.collapseItem2.Size = new System.Drawing.Size(1006, 257);
            this.collapseItem2.TabIndex = 0;
            this.collapseItem2.Text = "DOM检查";
            // 
            // DOM_panel
            // 
            this.DOM_panel.Controls.Add(this.DOM_table);
            this.DOM_panel.Controls.Add(this.DOM_pagination);
            this.DOM_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DOM_panel.Location = new System.Drawing.Point(0, 0);
            this.DOM_panel.Name = "DOM_panel";
            this.DOM_panel.Size = new System.Drawing.Size(1006, 257);
            this.DOM_panel.TabIndex = 4;
            // 
            // DOM_table
            // 
            this.DOM_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DOM_table.Location = new System.Drawing.Point(0, 0);
            this.DOM_table.Name = "DOM_table";
            this.DOM_table.Size = new System.Drawing.Size(1006, 225);
            this.DOM_table.TabIndex = 1;
            this.DOM_table.Text = "table1";
            // 
            // DOM_pagination
            // 
            this.DOM_pagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DOM_pagination.Location = new System.Drawing.Point(0, 225);
            this.DOM_pagination.Name = "DOM_pagination";
            this.DOM_pagination.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DOM_pagination.ShowSizeChanger = true;
            this.DOM_pagination.Size = new System.Drawing.Size(1006, 32);
            this.DOM_pagination.TabIndex = 2;
            this.DOM_pagination.Text = "pagination2";
            // 
            // collapseItem3
            // 
            this.collapseItem3.AutoScroll = true;
            this.collapseItem3.Controls.Add(this.HAD_panel);
            this.collapseItem3.Location = new System.Drawing.Point(-1006, -242);
            this.collapseItem3.Name = "collapseItem3";
            this.collapseItem3.Size = new System.Drawing.Size(1006, 242);
            this.collapseItem3.TabIndex = 1;
            this.collapseItem3.Text = "海岸带检查";
            // 
            // HAD_panel
            // 
            this.HAD_panel.Controls.Add(this.HAD_table);
            this.HAD_panel.Controls.Add(this.HAD_pagination);
            this.HAD_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HAD_panel.Location = new System.Drawing.Point(0, 0);
            this.HAD_panel.Name = "HAD_panel";
            this.HAD_panel.Size = new System.Drawing.Size(1006, 242);
            this.HAD_panel.TabIndex = 5;
            // 
            // HAD_table
            // 
            this.HAD_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HAD_table.Location = new System.Drawing.Point(0, 0);
            this.HAD_table.Name = "HAD_table";
            this.HAD_table.Size = new System.Drawing.Size(1006, 210);
            this.HAD_table.TabIndex = 1;
            this.HAD_table.Text = "table1";
            // 
            // HAD_pagination
            // 
            this.HAD_pagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HAD_pagination.Location = new System.Drawing.Point(0, 210);
            this.HAD_pagination.Name = "HAD_pagination";
            this.HAD_pagination.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.HAD_pagination.ShowSizeChanger = true;
            this.HAD_pagination.Size = new System.Drawing.Size(1006, 32);
            this.HAD_pagination.TabIndex = 2;
            this.HAD_pagination.Text = "pagination2";
            // 
            // TaskListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 600);
            this.Controls.Add(this.Back_panel1);
            this.Controls.Add(this.pageHeader1);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Name = "TaskListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务列表";
            this.Load += new System.EventHandler(this.TaskListForm_Load);
            this.SizeChanged += new System.EventHandler(this.TaskListForm_SizeChanged);
            this.Back_panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.List_panel.ResumeLayout(false);
            this.zhedie_collapse1.ResumeLayout(false);
            this.collapseItem1.ResumeLayout(false);
            this.custom_panel.ResumeLayout(false);
            this.collapseItem2.ResumeLayout(false);
            this.DOM_panel.ResumeLayout(false);
            this.collapseItem3.ResumeLayout(false);
            this.HAD_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Panel Back_panel1;
        private AntdUI.Table custom_Tasktable;
        private AntdUI.Pagination pagination1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private AntdUI.Button delete_btn;
        private AntdUI.Button start_btn;
        private AntdUI.Button importTask_btn;
        private AntdUI.Button stopTask_btn;
        private AntdUI.Button startDrag_btn;
        private AntdUI.Collapse zhedie_collapse1;
        private AntdUI.CollapseItem collapseItem1;
        private AntdUI.CollapseItem collapseItem2;
        private AntdUI.CollapseItem collapseItem3;
        private System.Windows.Forms.Panel List_panel;
        private System.Windows.Forms.Panel custom_panel;
        private System.Windows.Forms.Panel DOM_panel;
        private AntdUI.Table DOM_table;
        private AntdUI.Pagination DOM_pagination;
        private System.Windows.Forms.Panel HAD_panel;
        private AntdUI.Table HAD_table;
        private AntdUI.Pagination HAD_pagination;
        private AntdUI.Button step_run_btn;
    }
}