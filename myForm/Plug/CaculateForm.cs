using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZJZX_ZJAddin.Utils;
using 质检工具.Func.SysFunc;
using 质检工具.Func.ToolFormFunc;
using 质检工具.MenuConfig;
using 质检工具.SysFunc;
using 质检工具.UIPreSet;
using System.Threading.Tasks;
namespace 质检工具.myForm.Plug
{
    public partial class CaculateForm : AntdUI.Window
    {
        private List<ToolArgs> toolArgsList;
        private DynamicTool dynamicTool; // 声明 dynamicTool

        public CaculateForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.CaculateForm_Load);
            dynamicTool = new DynamicTool(); // 初始化 dynamicTool
        }

        private void CaculateForm_Load(object sender, EventArgs e)
        {
            // 设置 pageHeader 的动态背景色
            UIPreSet.ControlPreSet.SetpageHeader(pageHeader1);
            // 加载工具配置
            csvFunc csvLoader = new csvFunc();
            string csvPath = globalpara.controlcsvpath;

            if (!File.Exists(csvPath))
            {
                MessageBox.Show("未找到 Control.csv 文件，请检查路径是否正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 预处理 CSV 文件，确保每一行都有 nowValue 列
            csvLoader.PreprocessCsv(csvPath);

            // 加载工具参数
            toolArgsList = csvLoader.LoadTaskCache(csvPath);

            if (toolArgsList == null || toolArgsList.Count == 0)
            {
                MessageBox.Show("未能加载任何工具参数，请检查 Control.csv 文件内容是否正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 动态生成控件
            dynamicTool.creatcontrol(this, toolArgsList);
            
            // 加载并显示 mapping.csv 数据
            LoadAndDisplayMappingData();

            move2newlayout();

        }
       
        private void move2newlayout()
        {
            var splitContainer = this.Controls.Find("splitContainer1", true).FirstOrDefault() as SplitContainer;
            if (splitContainer != null && splitContainer.Panel2 != null)
            {
                this.Controls.Remove(datagridpanel);
                splitContainer.Panel2.Controls.Add(datagridpanel);
                datagridpanel.Location = new Point(0,pageHeader1.Height);
                datagridpanel.Size = new Size(splitContainer.Panel2.Width-2, splitContainer.Panel2.Height- pageHeader1.Height);
                datagridpanel.BringToFront();
            }
        }
        private void LoadAndDisplayMappingData()//加载映射关系数据
        {
            // 使用 globalpara.mappingcsvpath 获取 Mapping.csv 的路径
            string mappingCsvPath = globalpara.mappingcsvpath;

            // 检查文件是否存在
            if (!File.Exists(mappingCsvPath))
            {
                MessageBox.Show("未找到 Mapping.csv 文件，请检查路径是否正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 创建 DataTable 来存储 CSV 数据
                DataTable dataTable = new DataTable();

                // 读取 CSV 文件的内容
                using (StreamReader reader = new StreamReader(mappingCsvPath, Encoding.GetEncoding("GB2312")))
                {
                    string line;
                    bool isHeader = true;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');

                        if (isHeader)
                        {
                            // 添加列名
                            foreach (string column in columns)
                            {
                                dataTable.Columns.Add(column.Trim());
                            }
                            isHeader = false;
                        }
                        else
                        {
                            // 添加数据行
                            DataRow row = dataTable.NewRow();
                            for (int i = 0; i < columns.Length; i++)
                            {
                                row[i] = columns[i].Trim();
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                }

                // 将 DataTable 绑定到 DataGridView
                uiDataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载 Mapping.csv 文件时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addTask_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取 Rib 实例
                Rib mainForm = Application.OpenForms.OfType<Rib>().FirstOrDefault();
                if (mainForm != null)
                {
                    // 更新 statusLabel 的文本为“开始进行数据统计”
                    mainForm.Invoke(new Action(() =>
                    {
                        mainForm.statusLabel.Text = "开始进行数据统计";
                    }));
                }

                // 获取 mdb 路径和 excel 路径
                UITextBox mdbTextBox = this.Controls.Find("txt_" + toolArgsList[0].argName, true).FirstOrDefault() as UITextBox;
                string mdbPath = mdbTextBox.Text;
                UITextBox excelTextBox = this.Controls.Find("txt_" + toolArgsList[1].argName, true).FirstOrDefault() as UITextBox;
                string excelPath = excelTextBox.Text;
                // 读取 mapping.csv
                string mappingCsvPath = globalpara.mappingcsvpath;
                // 解析映射关系
                var mappingList = new List<(string mdbTable, string excelSheet, string indexCol)>();
                using (var reader = new StreamReader(mappingCsvPath, Encoding.GetEncoding("GB2312")))
                {
                    string line;
                    bool isHeader = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isHeader) { isHeader = false; continue; }
                        var arr = line.Split(',');
                        if (arr.Length >= 3)
                            mappingList.Add((arr[0].Trim(), arr[1].Trim(), arr[2].Trim()));
                    }
                }

                // 后台处理
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (s, args) =>
                {
                    try
                    {
                        FeatureUtils featureUtils = new FeatureUtils();
                        IFeatureWorkspace ws = WorkspaceUtils.OpenMDBFile(mdbPath);

                        foreach (var map in mappingList)
                        {
                            DataTable dt;
                            // 先尝试按要素类读取，否则按表读取
                            try
                            {
                                dt = featureUtils.ConvertFeatureClassToDataTable(map.mdbTable, ws);
                            }
                            catch
                            {
                                dt = featureUtils.ConvertTableToDataTable(map.mdbTable, ws);
                            }

                            if (dt == null || dt.Rows.Count == 0)
                                continue;

                            string[] columns = ExcelUtils.GetDataTableColumnNames(dt);
                            string[,] arr = ExcelUtils.ConvertDataTableToStringArray(dt, columns);

                            // 检查是否已存在
                            if (ExcelUtils.IsDataAlreadyInWorksheet(excelPath, map.excelSheet, arr))
                                continue;

                            // 查找索引列位置
                            Tuple<int, int> idx = ExcelUtils.FindValueInWorksheet(excelPath, map.excelSheet, map.indexCol);
                            // 验证返回值是否有效
                            if (idx.Item1 == -1 || idx.Item2 == -1)
                            {
                                LogHelper.normallog($"未找到索引列 '{map.indexCol}'，跳过工作表 {map.excelSheet} 的插入操作。");
                                continue;
                            }
                            // 插入数据
                            ExcelUtils.InsertDataIntoExcel(excelPath, map.excelSheet, arr, idx?.Item1 ?? 1, idx?.Item2 ?? 1);
                        }

                        // 检查是否勾选了“自动删除中间表”
                        if (!checkBox1.Checked)
                        {
                            // 备份 Excel 文件
                            string backupPath = Path.Combine(Path.GetDirectoryName(excelPath), Path.GetFileNameWithoutExtension(excelPath) + "_未删除" + Path.GetExtension(excelPath));
                            File.Copy(excelPath, backupPath, true);
                        }

                        // 进行删除操作
                        Tuple<int, int> clxIndex1 = ExcelUtils.FindValueInWorksheet(excelPath, "1评分总表", "处理项");
                        ExcelUtils.DeleteRowsWithValue(excelPath, "1评分总表", clxIndex1.Item2, "删除");
                        Tuple<int, int> clxIndex2 = ExcelUtils.FindValueInWorksheet(excelPath, "2影像质量评分表", "处理项");
                        ExcelUtils.DeleteRowsWithValue(excelPath, "2影像质量评分表", clxIndex2.Item2, "删除");
                        Tuple<int, int> clxIndex3 = ExcelUtils.FindValueInWorksheet(excelPath, "3平面精度检测记录表", "处理项");
                        ExcelUtils.DeleteRowsWithValue(excelPath, "3平面精度检测记录表", clxIndex3.Item2, "删除");
                        Tuple<int, int> clxIndex4 = ExcelUtils.FindValueInWorksheet(excelPath, "4检查记录表", "处理项");
                        ExcelUtils.DeleteRowsWithValue(excelPath, "4检查记录表", clxIndex4.Item2, "删除");

                        LogHelper.Successnotice("数据统计处理完成！");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.ErrorLog("运行过程中发生错误", ex);
                    }
                    finally
                    {
                        // 更新 statusLabel 的文本为“数据统计已完成”
                        if (mainForm != null)
                        {
                            mainForm.Invoke(new Action(() =>
                            {
                                mainForm.statusLabel.Text = "数据统计已完成";
                            }));
                        }
                    }
                };
                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("运行过程中发生错误", ex);

                // 更新 statusLabel 的文本为“数据统计已完成”
                Rib mainForm = Application.OpenForms.OfType<Rib>().FirstOrDefault();
                if (mainForm != null)
                {
                    mainForm.Invoke(new Action(() =>
                    {
                        mainForm.statusLabel.Text = "数据统计已完成";
                    }));
                }
            }
        }

        private void mappingbutton_Click(object sender, EventArgs e)//打开映射关系文件
        {
            // 获取 Mapping.csv 的路径
            string mappingCsvPath = globalpara.mappingcsvpath;

            // 检查文件是否存在
            if (!File.Exists(mappingCsvPath))
            {
                MessageBox.Show("未找到 Mapping.csv 文件，请检查路径是否正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 使用默认程序打开文件
                System.Diagnostics.Process.Start(mappingCsvPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开 Mapping.csv 文件时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CaculateForm_Resize(object sender, EventArgs e)
        {
            resize_layout();
        }
        private void resize_layout()
        {
            //mappingbutton.Location = new Point(uiDataGridView1.Location.X, this.Height - mappingbutton.Height);
            
            var splitContainer = this.Controls.Find("splitContainer1", true).FirstOrDefault() as SplitContainer;
            if (splitContainer != null && splitContainer.Panel2 != null)
            {
                mappingbutton.Location = new Point(splitContainer.SplitterDistance+2, addTask_btn.Location.Y);

            }
                mappingbutton.Width = uiDataGridView1.Width;                    
        }

        private void datagridpanel_Resize(object sender, EventArgs e)
        {
            resize_layout();
        }
    }
}