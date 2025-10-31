using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 质检工具.Func.ToolFormFunc
{
    class ReadToolDesc
    {
        public static string readToolDesc(MenuBean menuBean)
        {
            string xml_path = Path.Combine(Path.GetDirectoryName(globalpara.toolconfigpath), "工具描述", menuBean.folderName,
                menuBean.toolName + ".xml");
            return printdesc(xml_path);
        }
        private static string printdesc(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);

                // 定义命名空间
                XNamespace gmd = "http://www.isotc211.org/2005/gmd";
                XNamespace gco = "http://www.isotc211.org/2005/gco";

                // 查找 <abstract> 元素下的 <CharacterString>
                var abstractElement = doc.Descendants(gmd + "abstract")
                                         .FirstOrDefault();

                if (abstractElement != null)
                {
                    var content = abstractElement.Element(gco + "CharacterString")?.Value;
                    Console.WriteLine("提取的 Abstract 内容：");
                    Console.WriteLine(content);
                    return content;
                }
                else
                {
                    Console.WriteLine("<abstract> 标签未找到。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取或解析 XML 时出错：");
                Console.WriteLine(ex.Message);
            }
            return "";
        }
    }
}
