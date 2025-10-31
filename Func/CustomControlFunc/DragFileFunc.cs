using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 质检工具.Func
{
    class DragFileFunc
    {
        public static void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths == null) return;
            if (paths.Length > 0)
            {
                string path = paths[0]; // 取第一个项目

                // 只要路径存在（不管是文件还是文件夹），就填入 TextBox
                if (File.Exists(path) || Directory.Exists(path))
                {
                    if (sender is UITextBox textBox)
                    {
                        textBox.Text = path;
                    }
                }
            }
        }
        public static void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            // 判断拖放的数据是否包含文件/文件夹路径
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

                // 只要至少有一个路径，并且该路径是存在的文件或文件夹，就允许拖放
                if (paths.Length > 0 && (File.Exists(paths[0]) || Directory.Exists(paths[0])))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                //e.Effect = DragDropEffects.None;
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
