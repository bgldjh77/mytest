using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJZX_ZJAddin.IEnum
{
    class FeatureShpeType
    {
        public static string GetFeatureType(string fcShape)
        {
            switch (fcShape)
            {
                case "esriGeometryPoint":
                    return "点";
                case "esriGeometryPolyline":
                    return "线";
                case "esriGeometryPolygon":
                    return "面";
                default:
                    return "";
            }
        }
    }
}
