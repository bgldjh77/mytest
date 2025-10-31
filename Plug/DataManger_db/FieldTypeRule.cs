using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJZX_ZJAddin.IEnum
{
    class FieldTypeRule
    {
        /// <summary>
        /// 字段类型转换
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static esriFieldType GetFieldType(string FieldType)
        {
            switch (FieldType)
            {
                // 以下为多要素规则  
                case "String":
                    return esriFieldType.esriFieldTypeString; // 字符串类型  
                case "Integer":
                    return esriFieldType.esriFieldTypeInteger; // 整型  
                case "Double":
                    return esriFieldType.esriFieldTypeDouble; // 双精度浮点型  
                case "Date":
                    return esriFieldType.esriFieldTypeDate; // 日期类型  
                case "Blob":
                    return esriFieldType.esriFieldTypeBlob; // 二进制大对象  
                case "Raster":
                    return esriFieldType.esriFieldTypeRaster; // 栅格数据  
                case "Geometry":
                    return esriFieldType.esriFieldTypeGeometry; // 几何类型  
                case "GlobalID":
                    return esriFieldType.esriFieldTypeGlobalID; // 全局唯一标识符  
                case "GUID":
                    return esriFieldType.esriFieldTypeGUID; // 全局唯一标识符 (GUID)  
                case "OID":
                    return esriFieldType.esriFieldTypeOID; // 对象ID类型  
                default:
                    return esriFieldType.esriFieldTypeString;
            }
        }
    }
}
