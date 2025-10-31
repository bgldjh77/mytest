using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZJZX_ZJAddin.JTGJ
{
    public partial class PreviewForm : Form
    {

        private Bitmap screenshot;
        private ToolStrip toolStrip;
        private bool isDrawing = false; // 控制涂鸦状态  
        private Point lastPoint;
        private Color penColor = Color.Red; // 默认涂鸦笔颜色  
        private float penWidth = 2; // 默认涂鸦笔宽度 
        private Stack<Bitmap> undoStack; // 用于存储撤回的历史
        private bool isAddingText = false; // 添加文字状态
        private bool isDrawingRectangle = false; // 新增标志用以识别画矩形状态 
        private bool isDrawingArrow = false; // 标志用以识别画箭头状态
        private Point rectangleStart; // 矩形起始位置  
        private Point rectangleEnd; // 矩形结束位置 
        private Point arrowStart; // 箭头起始位置  
        private Point arrowEnd; // 箭头结束位置 


        public PreviewForm(Bitmap bitmap)
        {
            this.screenshot = new Bitmap(bitmap);
            this.undoStack = new Stack<Bitmap>(); // 初始化撤回栈  


            // 设置窗体标题和大小  
            this.Text = "截图预览";
            this.FormBorderStyle = FormBorderStyle.None;  // 无边框 
            this.ClientSize = new Size(screenshot.Width, screenshot.Height + 40); // 留出工具栏的空间  
            this.DoubleBuffered = true; // 开启双缓冲，减少闪烁  

            // 初始化工具栏  
            InitializeToolStrip();

            // 窗体的绘制事件  
            this.Paint += PreviewForm_Paint;

            // 添加鼠标事件处理  
            this.MouseDown += PreviewForm_MouseDown;
            this.MouseMove += PreviewForm_MouseMove;
            this.MouseUp += PreviewForm_MouseUp;

            // 设置窗体可接收键盘输入  
            this.KeyPreview = true; // 让窗体优先接收键盘事件 
        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void InitializeToolStrip()
        {
            toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Bottom;
            

            // 完成按钮  
            var FinButton = new ToolStripButton("完成");
            FinButton.Click += FinishButton_Click;
            toolStrip.Items.Add(FinButton);

            // 取消按钮  
            var cancelButton = new ToolStripButton("取消");
            cancelButton.Click += (s, e) => this.Close(); // 关闭预览窗口  
            toolStrip.Items.Add(cancelButton);

            // 保存截图按钮  
            var saveButton = new ToolStripButton("保存");
            saveButton.Click += SaveButton_Click;
            toolStrip.Items.Add(saveButton);


            // 撤回按钮  
            var undoButton = new ToolStripButton("撤回");
            undoButton.Click += UndoButton_Click; // 处理撤回事件  
            undoButton.Enabled = false; // 初始时禁用撤回按钮  

            // 添加文字按钮  
            var addTextButton = new ToolStripButton("添加文字");
            addTextButton.Click += (s, e) =>
            {
                if (isDrawing == false && isDrawingRectangle == false && isDrawingArrow == false)
                {
                    isAddingText = !isAddingText; // 切换添加文字状态  
                    addTextButton.Checked = isAddingText; // 高亮显示添加文字按钮  
                    this.Cursor = isAddingText ? Cursors.IBeam : Cursors.Default; // 更改鼠标光标  
                }
            };
            toolStrip.Items.Add(addTextButton);

            // 涂鸦按钮  
            var doodleButton = new ToolStripButton("涂鸦");
            doodleButton.Click += (s, e) =>
            {
                if (isAddingText == false && isDrawingRectangle == false && isDrawingArrow == false)
                {
                    isDrawing = !isDrawing; // 切换涂鸦状态  
                    doodleButton.Checked = isDrawing; // 高亮显示涂鸦按钮  
                    this.Cursor = isDrawing ? Cursors.Cross : Cursors.Default; // 更改鼠标光标  

                    if (isDrawing)
                    {
                        // 保存当前状态到撤回栈  
                        SaveToUndoStack();

                        // 选择涂鸦颜色和笔宽  
                        SelectPenSettings();

                        // 启用撤回按钮  
                        undoButton.Enabled = true;
                    }
                    else
                    {
                        // 关闭涂鸦时禁用撤回按钮  
                        undoButton.Enabled = false;
                    }
                }
            };
            toolStrip.Items.Add(doodleButton);

            // 画矩形按钮  
            var rectangleButton = new ToolStripButton("画矩形");
            rectangleButton.Click += (s, e) =>
            {
                if (isDrawing == false && isAddingText == false && isDrawingArrow == false)
                {
                    isDrawingRectangle = !isDrawingRectangle; // 切换画矩形状态  
                    rectangleButton.Checked = isDrawingRectangle;
                    this.Cursor = isDrawingRectangle ? Cursors.Cross : Cursors.Default; // 根据状态更改光标  
                }
            };
            toolStrip.Items.Add(rectangleButton);

            // 画箭头按钮  
            var arrowButton = new ToolStripButton("画箭头");
            arrowButton.Click += (s, e) =>
            {
                if (isDrawing == false && isAddingText == false && isDrawingRectangle == false)
                {
                    isDrawingArrow = !isDrawingArrow; // 切换画箭头状态  
                    arrowButton.Checked = isDrawingArrow;
                    this.Cursor = isDrawingArrow ? Cursors.Cross : Cursors.Default; // 根据状态更改光标  
                }
            };
            toolStrip.Items.Add(arrowButton);



            toolStrip.Items.Add(undoButton);

            // 添加工具栏到窗体  
            this.Controls.Add(toolStrip);
        }

        /// <summary>
        /// 完成按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishButton_Click(object sender, EventArgs e)
        {
            // 保存选定区域的截图  
            using (Bitmap bitmap = new Bitmap(screenshot.Width, screenshot.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(screenshot, 0, 0);
                }
                Clipboard.SetImage(bitmap);
                this.Close();
            }
        }


        /// <summary>
        /// 检测键盘按下状态
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            // 检测是否按下了 空格 键  
            if (e.KeyCode == Keys.Space)
            {
                // 保存选定区域的截图  
                using (Bitmap bitmap = new Bitmap(screenshot.Width, screenshot.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(screenshot, 0, 0);
                    }
                    Clipboard.SetImage(bitmap);
                    this.Close();
                }
            }
            // 检测是否按下了 空格 键  
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }  


        /// <summary>
        /// 截图绘制预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewForm_Paint(object sender, PaintEventArgs e)
        {
            // 绘制截图  
            e.Graphics.DrawImage(screenshot, 0, 0); // 绘制截图
            if (isDrawingRectangle && rectangleStart != new Point(0, 0))
            {
                // 如果正在绘制矩形，实时绘制矩形的轮廓  
                using (Pen pen = new Pen(Color.Red, 2)) // 设置矩形可视化的颜色和宽度  
                {
                    e.Graphics.DrawRectangle(pen, GetRectangle(rectangleStart, rectangleEnd));
                }
            }
            // 实时绘制箭头  
            if (isDrawingArrow && arrowStart != new Point(0, 0))
            {
                using (Pen pen = new Pen(Color.Red, 2)) // 设置箭头可视化的颜色和宽度  
                {
                    DrawArrow(e.Graphics, arrowStart, arrowEnd, pen); // 绘制箭头的预览  
                }
            }
        }

        /// <summary>
        /// 保存状态
        /// </summary>
        private void SaveToUndoStack()
        {
            // 将当前状态保存到撤回栈  
            undoStack.Push((Bitmap)screenshot.Clone()); // 深拷贝当前截图  
        }

        /// <summary>
        /// 撤回按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoButton_Click(object sender, EventArgs e)
        {
            // 从撤回栈中弹出最后一个状态并更新截图  
            if (undoStack.Count > 0)
            {
                screenshot.Dispose(); // 释放当前位图  
                screenshot = undoStack.Pop(); // 获取之前的状态  
                Invalidate(); // 刷新窗体以更新  
            }

            // 如果撤回栈为空，禁用撤回按钮  
            int undo_index = ((ToolStrip)Controls[0]).Items.Count - 1;
            var undoButton = (ToolStripButton)((ToolStrip)Controls[0]).Items[undo_index];
            if (undoStack.Count == 0)
            {
                undoButton.Enabled = false; // 禁用撤回按钮  
            }

        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg",
                Title = "保存截图",
                FileName = "截图_"+DateTime.Now.ToString("yyyyMMddHHmmss")+".png" // 设置默认文件名  
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 保存选定区域的截图  
                using (Bitmap bitmap = new Bitmap(screenshot.Width, screenshot.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(screenshot, 0, 0);
                    }
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Png); // 保存为 PNG 文件  
                    MessageBox.Show("截图已保存！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 鼠标单击事件记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewForm_MouseDown(object sender, MouseEventArgs e)
        {
            // 这里控制按钮状态
            int undo_index = ((ToolStrip)Controls[0]).Items.Count - 1;
            var undoButton = (ToolStripButton)((ToolStrip)Controls[0]).Items[undo_index];
            int text_index = ((ToolStrip)Controls[0]).Items.Count - 5;// 画箭头按钮顺序索引号
            var textButton = (ToolStripButton)((ToolStrip)Controls[0]).Items[text_index];
            if (isDrawing && !isAddingText)
            {
                // 保存当前状态到撤回栈  
                SaveToUndoStack();
                undoButton.Enabled = true; // 开启撤回按钮  
                lastPoint = e.Location; // 记录涂鸦起始点  
            }
            else if (isAddingText && e.Button == MouseButtons.Left)
            {
                // 保存当前状态到撤回栈 
                SaveToUndoStack();
                // 当添加文字模式激活，点击时弹出输入框  
                using (var inputForm = new Form())
                {
                    inputForm.Size = new Size(150, 20);
                    inputForm.Opacity = 0.5;
                    inputForm.StartPosition = FormStartPosition.Manual; // 手动设置位置  
                    inputForm.Location = PointToScreen(new Point(e.X, e.Y)); // 设定在点击位置  
                    inputForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    var textfont = new Font("Arial", 12f, FontStyle.Bold);
                    var textBox = new TextBox { Dock = DockStyle.Fill, Multiline = true, Font = textfont };

                    // 处理 KeyDown 事件  
                    textBox.KeyDown += (senderKey, args) =>
                    {
                        if (args.KeyCode == Keys.Enter)
                        {
                            // 获取用户输入的文本  
                            string inputText = textBox.Text;

                            // 在截图上绘制文字  
                            using (Graphics g = Graphics.FromImage(screenshot))
                            {
                                using (Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel))
                                {
                                    // 在鼠标点击的位置绘制文字  
                                    g.DrawString(inputText, font, Brushes.Red, new PointF(e.X, e.Y));
                                }
                            }

                            // 关闭输入框  
                            inputForm.Close();
                            this.Invalidate(); // 刷新窗体以显示更改
                            undoButton.Enabled = true; // 开启撤回按钮 
                            isAddingText = false;
                            textButton.Checked = isAddingText;
                            this.Cursor = isAddingText ? Cursors.IBeam : Cursors.Default; // 更改鼠标光标  
                        }
                    };

                    inputForm.Controls.Add(textBox);
                    inputForm.ShowDialog(); // 显示输入框  
                }
            }
            else if (isDrawingRectangle && e.Button == MouseButtons.Left)
            {
                SaveToUndoStack();
                // 记住矩形的起始点  
                rectangleStart = e.Location;
                undoButton.Enabled = true; // 开启撤回按钮 
            }
            else if (isDrawingArrow && e.Button == MouseButtons.Left)
            {
                // 保存当前状态到撤回栈  
                SaveToUndoStack();
                // 记录箭头的起始点  
                arrowStart = e.Location;
                undoButton.Enabled = true; // 开启撤回按钮 
            }

        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    // 画涂鸦线  
                    using (Pen pen = new Pen(penColor, penWidth)) // 使用用户选择的颜色和宽度  
                    {
                        g.DrawLine(pen, lastPoint, e.Location);
                    }
                }
                lastPoint = e.Location;
                this.Invalidate(); // 刷新窗体  
            }
            else if (isDrawingRectangle)
            {
                rectangleEnd = e.Location; // 更新矩形的结束点  

                // 重新绘制窗体以显示矩形  
                this.Invalidate(); // 标记窗体为无效状态，以触发重绘  
            }
            else if (isDrawingArrow)
            {
                arrowEnd = e.Location; // 更新箭头的结束点  
                this.Invalidate(); // 标记窗体为无效状态以触发重绘  
            }

        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewForm_MouseUp(object sender, MouseEventArgs e)
        {
            int Rectangle_index = ((ToolStrip)Controls[0]).Items.Count - 3;// 画矩形按钮顺序索引号
            int Arrow_index = ((ToolStrip)Controls[0]).Items.Count - 2;// 画箭头按钮顺序索引号
            var RectangleButton = (ToolStripButton)((ToolStrip)Controls[0]).Items[Rectangle_index];
            var ArrowButton = (ToolStripButton)((ToolStrip)Controls[0]).Items[Arrow_index];
            if (isDrawingRectangle && e.Button == MouseButtons.Left)
            {
                if (rectangleEnd.X > this.Width)
                    rectangleEnd.X = this.Width;
                if (rectangleEnd.Y > this.Height)
                    rectangleEnd.Y = this.Height - 41;
                // 在截图上绘制矩形  
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    using (Pen pen = new Pen(Color.Red, 2)) // 设置矩形的颜色和宽度  
                    {
                        g.DrawRectangle(pen, GetRectangle(rectangleStart, rectangleEnd)); // 绘制矩形  
                    }
                }
                this.Invalidate(); // 刷新窗体以显示新绘制的矩形  
                isDrawingRectangle = false; // 结束绘制矩形状态
                RectangleButton.Checked = false;
                this.Cursor = isDrawingRectangle ? Cursors.Cross : Cursors.Default; // 根据状态更改光标 
                rectangleStart = new Point(0, 0);
            }
            else if (isDrawingArrow && e.Button == MouseButtons.Left)
            {
                if (arrowEnd.X > this.Width)
                    arrowEnd.X = this.Width;
                if (arrowEnd.Y > this.Height)
                    arrowEnd.Y = this.Height - 41;
                // 在截图上绘制箭头  
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    using (Pen pen = new Pen(Color.Red, 2)) // 设置箭头的颜色和宽度  
                    {
                        DrawArrow(g, arrowStart, arrowEnd, pen); // 绘制箭头  
                    }
                }
                this.Invalidate(); // 刷新窗体以显示新绘制的箭头  
                isDrawingArrow = false; // 结束绘制箭头状态  
                ArrowButton.Checked = false;
                this.Cursor = isDrawingArrow ? Cursors.Cross : Cursors.Default; // 根据状态更改光标 
                arrowStart = new Point(0, 0);
            }

        }

        /// <summary>
        ///  获取定义矩形的 Rectangle 对象 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns> 
        private Rectangle GetRectangle(Point start, Point end)
        {
            // 计算矩形的起点和终点坐标  
            var x = Math.Min(start.X, end.X);
            var y = Math.Min(start.Y, end.Y);
            var width = Math.Abs(start.X - end.X);
            var height = Math.Abs(start.Y - end.Y);
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// 设置涂鸦画笔的属性
        /// </summary>
        private void SelectPenSettings()
        {
            // 选择颜色  
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                penColor = colorDialog.Color; // 用户选择的颜色  
            }

            // 使用一个简单的输入框让用户输入笔宽  
            string input = Microsoft.VisualBasic.Interaction.InputBox("输入涂鸦笔的宽度（1 - 10）:", "选择笔宽", "2");
            float width = 0;
            // 尝试解析用户输入的笔宽  
            if (float.TryParse(input, out width) && width > 0 && width <= 10)
            {
                penWidth = width; // 更新笔宽  
            }
            else
            {
                MessageBox.Show("请输入有效的笔宽（1 到 10 之间）!", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 画箭头
        /// </summary>
        /// <param name="g"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pen"></param>
        private void DrawArrow(Graphics g, Point start, Point end, Pen pen)
        {
            // 绘制直线  
            g.DrawLine(pen, start, end);

            // 计算箭头的方向  
            var arrowLength = 50; // 箭头的长度  
            var arrowAngle = Math.PI / 6; // 箭头的角度（30度）  

            // 计算箭头的两个边  
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            var angle = Math.Atan2(dy, dx);

            Point arrowPoint1 = new Point(
                end.X - (int)(arrowLength * Math.Cos(angle - arrowAngle)),
                end.Y - (int)(arrowLength * Math.Sin(angle - arrowAngle)));

            Point arrowPoint2 = new Point(
                end.X - (int)(arrowLength * Math.Cos(angle + arrowAngle)),
                end.Y - (int)(arrowLength * Math.Sin(angle + arrowAngle)));

            // 绘制箭头的两个边  
            g.DrawLine(pen, end, arrowPoint1);
            g.DrawLine(pen, end, arrowPoint2);
        }




    }
}
