using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 质检工具.Func.GISFunc;
using static 质检工具.Func.AntdTableFunc.FeatureTable;

namespace 质检工具
{
    public partial class TableForm : AntdUI.Window
    {
        ILayer layer;
        DataTable dt;
        public TableForm(ILayer _layer)
        {
            layer = _layer;

            InitializeComponent();
        }
        private void getTable(ILayer _layer)
        {
            DataTypeChange dtc = new DataTypeChange();
            IFeatureLayer featureLayer = (IFeatureLayer)_layer;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            ITable table = (ITable)featureClass;
            dt = dtc.ToDataTable(table);

            // 设置分页参数
            pagination1.Total = dt.Rows.Count;
            pagination1.PageSize = 100;
            pagination1.Current = 1;

            // 初始加载第一页数据
            LoadPageData(pagination1.Current, pagination1.PageSize);

            // 添加分页事件处理
            pagination1.ValueChanged += Pagination1_ValueChanged;
        }
        //private void creatTable()
        //{
        //    var columns = new AntdUI.ColumnCollection();
            
        //    // 根据DataTable的列创建表格列
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        columns.Add(new AntdUI.Column(col.ColumnName, col.ColumnName, AntdUI.ColumnAlign.Center)
        //            .SetLocalizationTitleID("Table.Column."));
        //    }
            
        //    table1.Columns = columns;

        //    // 设置分页参数
        //    pagination1.Total = dt.Rows.Count;
        //    pagination1.PageSize = 10;
        //    pagination1.Current = 1;

        //    // 初始加载第一页数据
        //    //LoadPageData(pagination1.Current, pagination1.PageSize);

        //    // 添加分页事件处理
        //    pagination1.ValueChanged += Pagination1_ValueChanged;
        //}
        //private void creatTable2()
        //{
        //    IFeatureLayer featureLayer = (IFeatureLayer)layer;
        //    IFeatureClass featureClass = featureLayer.FeatureClass;
        //    ITable mTable = (ITable)featureClass;

        //    // 创建列
        //    var columns = new AntdUI.ColumnCollection();
        //    for (int i = 0; i < mTable.Fields.FieldCount; i++)
        //    {
        //        string alistname = mTable.Fields.get_Field(i).AliasName;
        //        string fieldname = mTable.Fields.get_Field(i).Name;

        //        if (alistname == "Shape_Length" | alistname == "SHAPE_Length")
        //        {
        //            alistname = "长度  ";
        //        }
        //        else if (alistname == "Shape_Area" | alistname == "SHAPE_Area")
        //        {
        //            alistname = "面积  ";
        //        }

        //        columns.Add(new AntdUI.Column(fieldname, alistname, AntdUI.ColumnAlign.Center)
        //            .SetLocalizationTitleID("Table.Column."));
        //    }
        //    table1.Columns = columns;

        //    // 设置分页参数
        //    pagination1.Total = featureClass.FeatureCount(null);
        //    pagination1.PageSize = 10;
        //    pagination1.Current = 1;

        //    // 加载第一页数据
        //    LoadFeatureData(pagination1.Current, pagination1.PageSize);

        //    // 添加分页事件处理
        //    pagination1.ValueChanged += (sender, e) => 
        //    {
        //        LoadFeatureData(e.Current, e.PageSize);
        //    };
        //}

        //private void LoadFeatureData(int current, int pageSize)
        //{
        //    IFeatureLayer featureLayer = (IFeatureLayer)layer;
        //    IFeatureClass featureClass = featureLayer.FeatureClass;
        //    ITable mTable = (ITable)featureClass;

        //    var list = new List<Dictionary<string, object>>();
        //    int startIndex = (current - 1) * pageSize;

        //    // 使用游标获取数据
        //    ICursor cursor = mTable.Search(null, false);
        //    IRow row = null;
        //    int count = 0;

        //    // 跳过前面的记录
        //    while (count < startIndex && (row = cursor.NextRow()) != null)
        //    {
        //        count++;
        //        //row.Dispose();
        //    }

        //    // 获取当前页的数据
        //    count = 0;
        //    while (count < pageSize && (row = cursor.NextRow()) != null)
        //    {
        //        var rowData = new Dictionary<string, object>();
                
        //        for (int i = 1; i < mTable.Fields.FieldCount; i++)
        //        {
        //            string fieldname = mTable.Fields.get_Field(i).Name;
        //            object value = row.get_Value(i);
        //            rowData[fieldname] = value ?? DBNull.Value;
        //        }

        //        list.Add(rowData);
        //        count++;
        //        //row.Dispose();
        //    }

        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);

        //    table1.DataSource = list;
        //}
        private void Pagination1_ValueChanged(object sender, AntdUI.PagePageEventArgs e)
        {
            LoadPageData(e.Current, e.PageSize);
            //LoadFeatureData(e.Current, e.PageSize);
        }
        private void creatTable()
        {
            
        }
        private void TableForm_Load(object sender, EventArgs e)
        {
            pageHeader1.Text = layer.Name;
            getTable(layer);
            
            //creatTable2();
        }
        private void LoadPageData(int current, int pageSize)
        {
            // 计算当前页的起始索引
            int startIndex = (current - 1) * pageSize;
            
            // 创建一个新的DataTable来存储当前页的数据
            DataTable pageData = dt.Clone();

            // 获取当前页的数据行并添加到新的DataTable中
            var pageRows = dt.Rows.Cast<DataRow>()
                            .Skip(startIndex)
                            .Take(pageSize);

            foreach (DataRow row in pageRows)
            {
                pageData.ImportRow(row);
            }

            // 更新表格数据源
            uiDataGridView1.DataSource = pageData;
        }
    }
}
