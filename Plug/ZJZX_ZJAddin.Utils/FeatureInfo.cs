using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJZX_ZJAddin.Utils
{
    /// <summary>
    /// 要素信息类
    /// </summary>
    class FeatureInfo
    {
        // 要素名称
        public string Name { get; set; }
        // 要素别名
        public string AliasName { get; set; }
        // 要素集名称
        public string FeatureDatasetName { get; set; }
        // 要素集别名
        public string FeatureDatasetAliasName { get; set; }
        // 要素类型
        public string FeatureType { get; set; }
        // 字段名
        public string FieldName { get; set; }
        // 字段别名
        public string FieldAliasName { get; set; }
        // 字段数据类型
        public string FieldType { get; set; }
        // 字段长度
        public long FieldLength { get; set; }
        // 小数位数
        public int NumberFormat { get; set; }
        // 是否必填
        public string isNull { get; set; }
        // 是否检查字段长度
        public string isCheckNum { get; set; }
    }
}
