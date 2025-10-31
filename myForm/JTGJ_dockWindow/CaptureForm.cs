using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ZJZX_ZJAddin.JTGJ
{
    public partial class CaptureForm : Form
    {
        private Point selectionStart;
        private Rectangle selectionRectangle;
        private bool isSelecting;
        private Bitmap fullScreenImage;
        private Point selectionCenter; // 矩形框的中心点 
        private int boxWidth; // 矩形框的宽度  
        private int boxHeight; // 矩形框的高度
        private bool isLimitWH = false;

        public CaptureForm(int width = 0, int height = 0, bool iswh = false)
        {
            // 参数初始化
            boxWidth = width;
            boxHeight = height;
            isLimitWH = iswh;
            this.WindowState = FormWindowState.Maximized; // 全屏  
            this.FormBorderStyle = FormBorderStyle.None;  // 无边框  
            // 捕获全屏  
            CaptureFullScreen();


            // 启用双缓冲  
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && isLimitWH == false)
                {
                    isSelecting = true;
                    selectionStart = e.Location;
                }
            };

            this.MouseMove += (s, e) =>
            {
                if (isSelecting && isLimitWH == false)
                {
                    selectionRectangle = new Rectangle(
                        Math.Min(selectionStart.X, e.X),
                        Math.Min(selectionStart.Y, e.Y),
                        Math.Abs(selectionStart.X - e.X),
                        Math.Abs(selectionStart.Y - e.Y));

                    // 重绘仅选择区域  
                    this.Invalidate(); // 触发重绘  
                }
                if (isLimitWH)
                {
                    isSelecting = false;
                    // 更新鼠标中心点  
                    selectionCenter = e.Location;
                    selectionRectangle = new Rectangle(
                        selectionCenter.X - boxWidth / 2,
                        selectionCenter.Y - boxHeight / 2,
                        boxWidth,
                        boxHeight);

                    // 重绘  
                    this.Invalidate();
                }
            };

            this.MouseUp += (s, e) =>
            {
                if (isSelecting)
                {
                    isSelecting = false;
                    CaptureSelection(); // 捕获选定区域  
                }
                if (isLimitWH)
                {
                    isLimitWH = false;
                    CaptureSelection(); // 捕获选定区域  
                }
            };

        }

        /// <summary>
        /// 检测键盘按下状态
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            // 检测是否按下了 空格 键  
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            // 检测是否按下了 空格 键  
            if (e.KeyCode == Keys.Space && isLimitWH == true)
            {
                CaptureSelection(); // 捕获选定区域  
            }
        }

        /// <summary>
        /// 捕获屏幕
        /// </summary>
        private void CaptureFullScreen()
        {
            Rectangle screenBounds = Screen.GetBounds(Point.Empty);
            fullScreenImage = new Bitmap(screenBounds.Width, screenBounds.Height);

            using (Graphics g = Graphics.FromImage(fullScreenImage))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, screenBounds.Size); // 捕获整个屏幕  
            }
        }

        /// <summary>
        /// 画图界面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 绘制全屏截图（不缩放）  
            if (fullScreenImage != null)
            {
                e.Graphics.DrawImageUnscaled(fullScreenImage, Point.Empty);
            }
            // 添加黑色半透明背景  
            if (!isSelecting && isLimitWH == false)
            {
                using (Brush blackBrush = new SolidBrush(Color.FromArgb(128, Color.Black))) // 128 为透明度  
                {
                    int w = fullScreenImage?.Width ?? this.Width;
                    int h = fullScreenImage?.Height ?? this.Height;
                    e.Graphics.FillRectangle(blackBrush, 0, 0, w, h);
                }
            }


            // 如果正在选择区域  
            if (isSelecting)
            {
                // 计算非选择区域并将该区域"挖空"  
                using (GraphicsPath path = new GraphicsPath())
                {
                    // 添加选择区域路径  
                    path.AddRectangle(selectionRectangle);

                    // 准备图形容器进行剪切  
                    using (Region region = new Region(path))
                    {
                        // 设置剪切区域为选择区域  
                        e.Graphics.ExcludeClip(region);

                        // 用黑色填充剩余的区域  
                        using (Brush fillBrush = new SolidBrush(Color.FromArgb(128, Color.Black))) // 半透明黑色  
                        {
                            int w = fullScreenImage?.Width ?? this.Width;
                            int h = fullScreenImage?.Height ?? this.Height;
                            e.Graphics.FillRectangle(fillBrush, 0, 0, w, h); // 只绘制非选择区域  
                        }

                        // 解除剪切区域  
                        e.Graphics.ResetClip();
                    }
                }

                // 绘制选择区域的边框  
                using (Pen pen = new Pen(Color.Red, 2)) // 红色边框  
                {
                    e.Graphics.DrawRectangle(pen, selectionRectangle); // 绘制选择矩形边框  
                }

                // 显示所选区域的宽和高  
                string sizeText = selectionRectangle.Width + "x" + selectionRectangle.Height;
                using (Font font = new Font("Arial", 12, FontStyle.Bold))
                {
                    using (Brush textBrush = new SolidBrush(Color.Red))
                    {
                        e.Graphics.DrawString(sizeText, font, textBrush, new PointF(selectionRectangle.Location.X, selectionRectangle.Location.Y - 30)); // 显示在左上角  
                    }
                }
            }

            // 绘制选择矩形框  
            if (isLimitWH)
            {
                // 计算非选择区域并将该区域"挖空"  
                using (GraphicsPath path = new GraphicsPath())
                {
                    // 添加选择区域路径  
                    path.AddRectangle(selectionRectangle);

                    // 准备图形容器进行剪切  
                    using (Region region = new Region(path))
                    {
                        // 设置剪切区域为选择区域  
                        e.Graphics.ExcludeClip(region);

                        // 用黑色填充剩余的区域  
                        using (Brush fillBrush = new SolidBrush(Color.FromArgb(128, Color.Black))) // 半透明黑色  
                        {
                            int w = fullScreenImage?.Width ?? this.Width;
                            int h = fullScreenImage?.Height ?? this.Height;
                            e.Graphics.FillRectangle(fillBrush, 0, 0, w, h); // 只绘制非选择区域  
                        }

                        // 解除剪切区域  
                        e.Graphics.ResetClip();
                    }
                }

                // 绘制选择区域的边框  
                using (Pen pen = new Pen(Color.Red, 2)) // 红色边框  
                {
                    e.Graphics.DrawRectangle(pen, selectionRectangle); // 绘制选择矩形边框  
                }

                // 显示所选区域的宽和高  
                string sizeText = boxWidth + "x" + boxHeight;
                using (Font font = new Font("Arial", 12, FontStyle.Bold))
                {
                    using (Brush textBrush = new SolidBrush(Color.Red))
                    {
                        e.Graphics.DrawString(sizeText, font, textBrush, new PointF(selectionRectangle.Location.X, selectionRectangle.Location.Y - 30));  // 显示在选择矩形上方  
                    }
                }
            }

        }

        /// <summary>
        /// 捕获选择
        /// </summary>
        private void CaptureSelection()
        {
            if (selectionRectangle.Width <= 0 || selectionRectangle.Height <= 0)
                return;

            using (Bitmap bitmap = new Bitmap(selectionRectangle.Width, selectionRectangle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // 从全屏截图中获取选择区域  
                    g.DrawImage(fullScreenImage,
                                 new Rectangle(0, 0, selectionRectangle.Width, selectionRectangle.Height),
                                 selectionRectangle,
                                 GraphicsUnit.Pixel);
                }

                PreviewForm previewForm = new PreviewForm(bitmap);
                // 设置预览窗体在选择区域位置  
                previewForm.StartPosition = FormStartPosition.Manual;
                previewForm.Location = PointToScreen(selectionRectangle.Location);
                previewForm.ShowDialog(); // 显示预览窗口   
            }

            this.Close(); // 关闭截图捕获窗口 
        }
    }
}
