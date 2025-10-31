using Svg; // 确保安装了 Svg.NET 库
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace 质检工具.UIPreSet
{
    public static class SvgHelper
    {
        /// <summary>
        /// 从Resources目录加载SVG文件并转换为Bitmap
        /// </summary>
        /// <param name="svgFileName">SVG文件名（例如"标准查询"）</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <param name="colorHex">十六进制颜色代码（例如"66CCFF"）</param>
        /// <returns>转换后的Bitmap</returns>
        public static Bitmap LoadSvgFromResources(string svgFileName, int width = 32, int height = 32, string colorHex = null, int? extendTop1 = null, int? extendBottom1 = null, float? strokeWidth1 = null, string oldColorHex1 = null, string newColorHex1 = null, string oldColorHex2 = null, string newColorHex2 = null)
        {
            // 自动添加.svg扩展名（如果需要）
            if (!svgFileName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            {
                svgFileName += ".svg";
            }

            // 修改为 data/svg 路径
            string svgFilePath = Path.Combine(Application.StartupPath, "data", "svg", svgFileName);

            // 转换颜色（如果提供）
            Color? fillColor = null;
            if (!string.IsNullOrEmpty(colorHex))
            {
                fillColor = HexToColor(colorHex);
            }

            // 加载SVG并转换为Bitmap
            return LoadSvgAsBitmap(svgFilePath, width, height, fillColor, extendTop1, extendBottom1, strokeWidth1, oldColorHex1, newColorHex1, oldColorHex2, newColorHex2);
        }
        /// <summary>
        /// 从SVG文件加载图标并转换为Bitmap
        /// </summary>
        /// <param name="svgFilePath">SVG文件路径</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <param name="fillColor">可选参数，指定填充颜色</param>
        /// <returns>转换后的Bitmap，失败返回null</returns>
        public static Bitmap LoadSvgAsBitmap(
    string svgFilePath,
    int width = 32,
    int height = 32,
    Color? fillColor = null,
    int? extendTop = null,
    int? extendBottom = null,
    float? strokeWidth = null,
    string oldColorHex = null,
    string newColorHex = null,
    string oldColorHex2 = null,
    string newColorHex2 = null)
        {
            try
            {
                if (!File.Exists(svgFilePath))
                {
                    LogHelper.add2mainlog($"SVG文件不存在: {svgFilePath}", Color.Red);
                    return null;
                }

                // 读取SVG文件内容
                string svgContent = File.ReadAllText(svgFilePath);

                // 使用 Svg.NET 库加载和解析 SVG
                var svgDocument = SvgDocument.Open(svgFilePath);
                if (svgDocument == null)
                {
                    LogHelper.add2mainlog("SVG转换失败", Color.Red);
                    return null;
                }

                // 颜色替换（优先于其它操作）
                if (!string.IsNullOrEmpty(oldColorHex) && !string.IsNullOrEmpty(newColorHex))
                {
                    Color oldColor = HexToColor(oldColorHex);
                    Color newColor = HexToColor(newColorHex);
                    ReplaceColorInSvg(svgDocument, oldColor, newColor);
                }
                if (!string.IsNullOrEmpty(oldColorHex2) && !string.IsNullOrEmpty(newColorHex2))
                {
                    Color oldColor2 = HexToColor(oldColorHex2);
                    Color newColor2 = HexToColor(newColorHex2);
                    ReplaceColorInSvg(svgDocument, oldColor2, newColor2);
                }

                // 如果指定了填充颜色，应用到SVG
                if (fillColor.HasValue)
                {
                    ApplyColorToSvg(svgDocument, fillColor.Value);
                }

                // 如果指定了描边宽度，应用到SVG
                if (strokeWidth.HasValue)
                {
                    ApplyStrokeWidthToSvg(svgDocument, strokeWidth.Value);
                }

                // 设置SVG文档大小
                svgDocument.Width = width;
                svgDocument.Height = height;

                // 创建目标Bitmap
                Bitmap bitmap = svgDocument.Draw();

                int top = extendTop.HasValue && extendTop.Value > 0 ? extendTop.Value : 0;
                int bottom = extendBottom.HasValue && extendBottom.Value > 0 ? extendBottom.Value : 0;

                if (top > 0 || bottom > 0)
                {
                    int newHeight = height + top + bottom;
                    Bitmap extendedBitmap = new Bitmap(width, newHeight);
                    using (Graphics g = Graphics.FromImage(extendedBitmap))
                    {
                        g.Clear(Color.Transparent);
                        g.DrawImage(bitmap, 0, top, width, height);
                    }
                    bitmap.Dispose();
                    return extendedBitmap;
                }

                return bitmap;
            }
            catch (Exception ex)
            {
                LogHelper.add2mainlog($"SVG加载错误: {ex.Message}", Color.Red);
                return null;
            }
        }


        public static Bitmap LoadSvgFromResources(
    string svgFileName,
    int width,
    int height,
    string colorHex,
    int? extendTop1,
    int? extendBottom1,
    float? strokeWidth1,
    string[] colorArray)
        {
            // 颜色数组长度检查，防止越界
            string c1 = colorArray != null && colorArray.Length > 0 ? colorArray[0] : null;
            string c2 = colorArray != null && colorArray.Length > 1 ? colorArray[1] : null;
            string c3 = colorArray != null && colorArray.Length > 2 ? colorArray[2] : null;
            string c4 = colorArray != null && colorArray.Length > 3 ? colorArray[3] : null;

            return LoadSvgFromResources(svgFileName, width, height, colorHex, extendTop1, extendBottom1, strokeWidth1, c1, c2, c3, c4);
        }

        // 新增：递归替换SVG元素的指定颜色
        private static void ReplaceColorInSvg(SvgElement element, Color oldColor, Color newColor)
        {
            if (element is SvgVisualElement visual)
            {
                // 替换填充色
                if (visual.Fill is SvgColourServer fillServer && fillServer.Colour.ToArgb() == oldColor.ToArgb())
                {
                    visual.Fill = new SvgColourServer(newColor);
                }
                // 替换描边色
                if (visual.Stroke is SvgColourServer strokeServer && strokeServer.Colour.ToArgb() == oldColor.ToArgb())
                {
                    visual.Stroke = new SvgColourServer(newColor);
                }
            }
            // 递归处理所有子元素
            foreach (var child in element.Children)
            {
                ReplaceColorInSvg(child, oldColor, newColor);
            }
        }

        // 新增：递归设置SVG元素的StrokeWidth
        private static void ApplyStrokeWidthToSvg(SvgElement element, float strokeWidth)
        {
            if (element is SvgPath || element is SvgRectangle || element is SvgCircle ||
                element is SvgEllipse || element is SvgPolygon || element is SvgPolyline)
            {
                var graphicsElement = element as SvgVisualElement;
                if (graphicsElement != null)
                {
                    graphicsElement.StrokeWidth = strokeWidth;
                }
            }

            // 递归处理所有子元素
            foreach (var child in element.Children)
            {
                ApplyStrokeWidthToSvg(child, strokeWidth);
            }
        }

        /// <summary>
        /// 递归地将指定颜色应用于SVG元素及其子元素
        /// </summary>
        /// <param name="element">SVG元素</param>
        /// <param name="color">要应用的颜色</param>
        private static void ApplyColorToSvg(SvgElement element, Color color)
        {
            // 处理填充颜色
            if (element is SvgPath || element is SvgRectangle || element is SvgCircle ||
                element is SvgEllipse || element is SvgPolygon || element is SvgPolyline)
            {
                var graphicsElement = element as SvgVisualElement;
                if (graphicsElement != null && !(graphicsElement.Fill == SvgPaintServer.None))
                {
                    graphicsElement.Fill = new SvgColourServer(color);
                }

                // 只有当描边不是none时才修改描边颜色
                if (graphicsElement != null && !(graphicsElement.Stroke == SvgPaintServer.None) &&
                    graphicsElement.StrokeWidth > 0)
                {
                    graphicsElement.Stroke = new SvgColourServer(color);
                }
            }

            // 递归处理所有子元素
            foreach (var child in element.Children)
            {
                ApplyColorToSvg(child, color);
            }
        }
        /// <summary>
        /// 从预设目录加载SVG文件并转换为Bitmap（只需指定文件名）
        /// </summary>
        /// <param name="svgFileName">SVG文件名（例如"标准查询"）</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <param name="colorHex">十六进制颜色代码（例如"66CCFF"）</param>
        /// <returns>转换后的Bitmap</returns>
        public static Bitmap LoadSvgByName(string svgFileName, int width = 32, int height = 32, string colorHex = null)
        {
            // 自动添加.svg扩展名（如果需要）
            if (!svgFileName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            {
                svgFileName += ".svg";
            }

            // 构建完整的SVG文件路径
            string svgFilePath = Path.Combine(globalpara.svgpath, svgFileName);

            // 转换颜色（如果提供）
            Color? fillColor = null;
            if (!string.IsNullOrEmpty(colorHex))
            {
                fillColor = HexToColor(colorHex);
            }

            // 加载SVG并转换为Bitmap
            return LoadSvgAsBitmap(svgFilePath, width, height, fillColor);
        }

        /// <summary>
        /// 从预设目录加载SVG文件并转换为Bitmap（只需指定文件名）
        /// </summary>
        /// <param name="svgFileName">SVG文件名（例如"标准查询"）</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <param name="fillColor">填充颜色</param>
        /// <returns>转换后的Bitmap</returns>
        public static Bitmap LoadSvgByName(string svgFileName, int width, int height, Color fillColor)
        {
            // 自动添加.svg扩展名（如果需要）
            if (!svgFileName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            {
                svgFileName += ".svg";
            }

            // 构建完整的SVG文件路径
            string svgFilePath = Path.Combine(globalpara.svgpath, svgFileName);

            // 加载SVG并转换为Bitmap
            return LoadSvgAsBitmap(svgFilePath, width, height, fillColor);
        }
        /// <summary>
        /// 将十六进制颜色代码转换为Color对象
        /// </summary>
        /// <param name="hexColor">十六进制颜色代码，例如"#66CCFF"</param>
        /// <returns>Color对象</returns>
        public static Color HexToColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor))
                return Color.Black;

            hexColor = hexColor.TrimStart('#');

            if (hexColor.Length == 6)
            {
                int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
                int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
                int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
                return Color.FromArgb(r, g, b);
            }

            return Color.Black;
        }
        /// <summary>
        /// 此方法仅仅可用于非纯色的SVG图标
        /// 读取SVG文件内容为字符串（用于AntdUI.Button的IconSvg）
        /// </summary>
        /// <param name="svgFileName">SVG文件名（可带或不带.svg）</param>
        /// <param name="fromResource">是否从Resources目录读取，默认true</param>
        /// <returns>SVG字符串，读取失败返回null</returns>
        public static string LoadSvgString(string svgFileName, bool fromResource = true, string replaceFillColor = null)
        {
            if (!svgFileName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
                svgFileName += ".svg";
            string svgFilePath;
            if (fromResource)
                svgFilePath = Path.Combine(Application.StartupPath, "data", "svg", svgFileName);

            else
                svgFilePath = Path.Combine(globalpara.svgpath, svgFileName);

            if (!File.Exists(svgFilePath))
                return null;
            string svgContent = File.ReadAllText(svgFilePath, Encoding.UTF8);

            // 如果需要替换fill颜色
            if (!string.IsNullOrEmpty(replaceFillColor))
            {
                // 替换所有 fill="#xxxxxx" 为 fill="replaceFillColor"
                svgContent = Regex.Replace(svgContent, @"fill=""#([0-9a-fA-F]{3,8})""", $"fill=\"{replaceFillColor}\"");
                // 兼容 fill='#xxxxxx'
                svgContent = Regex.Replace(svgContent, @"fill='#([0-9a-fA-F]{3,8})'", $"fill=\"{replaceFillColor}\"");
            }
            else
            {
                // 自动将 fill="#fff" 或 fill="#ffffff" 替换为 fill="white"
                svgContent = Regex.Replace(svgContent, @"fill=""#fff(f{0,3})""", "fill=\"white\"");
                svgContent = Regex.Replace(svgContent, @"fill='#fff(f{0,3})'", "fill=\"white\"");
            }

            return svgContent;
        }
    }
}