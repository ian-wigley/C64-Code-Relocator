using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public class BytesView : TableLayoutPanel
    {
        private const int DEFAULT_COLUMN_COUNT = 16 /*0x10*/;
        private const int DEFAULT_ROW_COUNT = 25;
        private const int COLUMN_COUNT = 16 /*0x10*/;
        private const int BORDER_GAP = 2;
        private const int INSET_GAP = 3;
        private const int CELL_HEIGHT = 21;
        private const int CELL_WIDTH = 25;
        private const int CHAR_WIDTH = 8;
        private const int ADDRESS_WIDTH = 69;
        private const int HEX_WIDTH = 400;
        private const int DUMP_WIDTH = 128 /*0x80*/;
        private int SCROLLBAR_HEIGHT;
        private int SCROLLBAR_WIDTH;
        private const int HEX_DUMP_GAP = 5;
        private const int ADDRESS_START_X = 5;
        private const int CLIENT_START_Y = 5;
        private const int LINE_START_Y = 7;
        private const int HEX_START_X = 74;
        private const int DUMP_START_X = 479;
        private const int SCROLLBAR_START_X = 612;
        private static readonly Font ADDRESS_FONT = new Font("Microsoft Sans Serif", 8f);
        private static readonly Font HEXDUMP_FONT = new Font("Courier New", 8f);
        private VScrollBar scrollBar;
        private TextBox edit;
        private int columnCount = 16 /*0x10*/;
        private int rowCount = 25;
        private byte[] dataBuf;
        private int startLine;
        private int displayLinesCount;
        private int linesCount;
        private DisplayMode displayMode;
        private DisplayMode realDisplayMode;

        /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.BytesView" /> class.</summary>
        public BytesView()
        {
            this.SuspendLayout();
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.RowCount = 1;
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.InitUI();
            this.ResumeLayout();
            this.displayMode = DisplayMode.Hexdump;
            this.realDisplayMode = DisplayMode.Hexdump;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private static int AnalizeByteOrderMark(byte[] buffer, int index)
        {
            int c1_1 = (int)buffer[index] << 8 | (int)buffer[index + 1];
            int c1_2 = (int)buffer[index + 2] << 8 | (int)buffer[index + 3];
            return new int[13, 13]
            {
                {
                    1,
                    5,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    1,
                    1,
                    11,
                    1,
                    10,
                    4,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    2,
                    9,
                    5,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2,
                    2
                },
                {
                    3,
                    7,
                    3,
                    7,
                    3,
                    3,
                    3,
                    3,
                    3,
                    3,
                    3,
                    3,
                    3
                },
                {
                    14,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    6,
                    1,
                    1,
                    1,
                    1,
                    1,
                    3,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    8,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    2,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    13,
                    1,
                    1
                },
                {
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                },
                {
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    12
                },
                {
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1,
                    1
                }
            }[BytesView.GetEncodingIndex(c1_1), BytesView.GetEncodingIndex(c1_2)];
        }

        private int CellToIndex(int column, int row) => row * this.columnCount + column;

        private byte[] ComposeLineBuffer(int startLine, int line)
        {
            int num = startLine * this.columnCount;
            byte[] numArray = num + (line + 1) * this.columnCount <= this.dataBuf.Length
                ? new byte[this.columnCount]
                : new byte[this.dataBuf.Length % this.columnCount];
            for (int column = 0; column < numArray.Length; ++column)
                numArray[column] = this.dataBuf[num + this.CellToIndex(column, line)];
            return numArray;
        }

        private void DrawAddress(Graphics g, int startLine, int line)
        {
            Font addressFont = BytesView.ADDRESS_FONT;
            string s = ((startLine + line) * this.columnCount).ToString("X8",
                (IFormatProvider)CultureInfo.InvariantCulture);
            Brush brush = (Brush)new SolidBrush(this.ForeColor);
            try
            {
                g.DrawString(s, addressFont, brush, 5f, (float)(7 + line * 21));
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawClient(Graphics g)
        {
            using (Brush brush = (Brush)new SolidBrush(SystemColors.ControlLightLight))
                g.FillRectangle(brush, new Rectangle(74, 5, 538, this.rowCount * 21));
            using (Pen pen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(pen, new Rectangle(74, 5, 537, this.rowCount * 21 - 1));
                g.DrawLine(pen, 474, 5, 474, 5 + this.rowCount * 21 - 1);
            }
        }

        private static bool CharIsPrintable(char c)
        {
            UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
            return unicodeCategory != UnicodeCategory.Control || unicodeCategory == UnicodeCategory.Format ||
                   unicodeCategory == UnicodeCategory.LineSeparator ||
                   unicodeCategory == UnicodeCategory.ParagraphSeparator ||
                   unicodeCategory == UnicodeCategory.OtherNotAssigned;
        }

        private void DrawDump(Graphics g, byte[] lineBuffer, int line)
        {
            StringBuilder stringBuilder = new StringBuilder(lineBuffer.Length);
            for (int index = 0; index < lineBuffer.Length; ++index)
            {
                char c = Convert.ToChar(lineBuffer[index]);
                if (BytesView.CharIsPrintable(c))
                    stringBuilder.Append(c);
                else
                    stringBuilder.Append('.');
            }

            Font hexdumpFont = BytesView.HEXDUMP_FONT;
            Brush brush = (Brush)new SolidBrush(this.ForeColor);
            try
            {
                g.DrawString(stringBuilder.ToString(), hexdumpFont, brush, 479f, (float)(7 + line * 21));
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawHex(Graphics g, byte[] lineBuffer, int line)
        {
            Font hexdumpFont = BytesView.HEXDUMP_FONT;
            StringBuilder stringBuilder = new StringBuilder(lineBuffer.Length * 3 + 1);
            for (int index = 0; index < lineBuffer.Length; ++index)
            {
                stringBuilder.Append(lineBuffer[index].ToString("X2", (IFormatProvider)CultureInfo.InvariantCulture));
                stringBuilder.Append(" ");
                if (index == this.columnCount / 2 - 1)
                    stringBuilder.Append(" ");
            }

            Brush brush = (Brush)new SolidBrush(this.ForeColor);
            try
            {
                g.DrawString(stringBuilder.ToString(), hexdumpFont, brush, 76f, (float)(7 + line * 21));
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawLines(Graphics g, int startLine, int linesCount)
        {
            for (int line = 0; line < linesCount; ++line)
            {
                byte[] lineBuffer = this.ComposeLineBuffer(startLine, line);
                this.DrawAddress(g, startLine, line);
                this.DrawHex(g, lineBuffer, line);
                this.DrawDump(g, lineBuffer, line);
            }
        }

        private DisplayMode GetAutoDisplayMode()
        {
            int num1 = 0;
            int num2 = 0;
            if (this.dataBuf == null || this.dataBuf.Length >= 0 && this.dataBuf.Length < 8)
                return DisplayMode.Hexdump;
            switch (BytesView.AnalizeByteOrderMark(this.dataBuf, 0))
            {
                case 2:
                    return DisplayMode.Hexdump;
                case 3:
                    return DisplayMode.Unicode;
                case 4:
                case 5:
                    return DisplayMode.Hexdump;
                case 6:
                case 7:
                    return DisplayMode.Hexdump;
                case 8:
                case 9:
                    return DisplayMode.Hexdump;
                case 10:
                case 11:
                    return DisplayMode.Hexdump;
                case 12:
                    return DisplayMode.Hexdump;
                case 13:
                    return DisplayMode.Ansi;
                case 14:
                    return DisplayMode.Ansi;
                default:
                    int num3 = this.dataBuf.Length <= 1024 /*0x0400*/ ? this.dataBuf.Length / 2 : 512 /*0x0200*/;
                    for (int index = 0; index < num3; ++index)
                    {
                        char c = (char)this.dataBuf[index];
                        if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                            ++num1;
                    }

                    for (int byteIndex = 0; byteIndex < num3; byteIndex += 2)
                    {
                        char[] chars = new char[1];
                        Encoding.Unicode.GetChars(this.dataBuf, byteIndex, 2, chars, 0);
                        if (BytesView.CharIsPrintable(chars[0]))
                            ++num2;
                    }

                    if (num2 * 100 / (num3 / 2) > 80 /*0x50*/)
                        return DisplayMode.Unicode;
                    return num1 * 100 / num3 > 80 /*0x50*/ ? DisplayMode.Ansi : DisplayMode.Hexdump;
            }
        }

        /// <summary>Gets the bytes in the buffer.</summary>
        /// <returns>The unsigned byte array reference.</returns>
        public virtual byte[] GetBytes() => this.dataBuf;

        /// <summary>Gets the display mode for the control.</summary>
        /// <returns>The display mode that this control uses. The returned value is defined in <see cref="T:System.ComponentModel.Design.DisplayMode" />.</returns>
        public virtual DisplayMode GetDisplayMode() => this.displayMode;

        private static int GetEncodingIndex(int c1)
        {
            switch (c1)
            {
                case 0:
                    return 1;
                case 60:
                    return 6;
                case 63 /*0x3F*/:
                    return 8;
                case 15360:
                    return 5;
                case 15423:
                    return 9;
                case 16128:
                    return 7;
                case 19567:
                    return 11;
                case 30829:
                    return 10;
                case 42900:
                    return 12;
                case 61371:
                    return 4;
                case 65279:
                    return 2;
                case 65534:
                    return 3;
                default:
                    return 0;
            }
        }

        private void InitAnsi()
        {
            int length = this.dataBuf.Length;
            char[] lpWideCharStr = new char[length + 1];
//            int wideChar =
//                System.Design.NativeMethods.MultiByteToWideChar(0, 0, this.dataBuf, length, lpWideCharStr, length);
//            lpWideCharStr[wideChar] = char.MinValue;
//            for (int index = 0; index < wideChar; ++index)
            // {
            //     if (lpWideCharStr[index] == char.MinValue)
            //         lpWideCharStr[index] = '\v';
            // }
            //
            // this.edit.Text = new string(lpWideCharStr);
        }

        private void InitUnicode()
        {
            char[] chars = new char[this.dataBuf.Length / 2 + 1];
            Encoding.Unicode.GetChars(this.dataBuf, 0, this.dataBuf.Length, chars, 0);
            for (int index = 0; index < chars.Length; ++index)
            {
                if (chars[index] == char.MinValue)
                    chars[index] = '\v';
            }

            chars[chars.Length - 1] = char.MinValue;
            this.edit.Text = new string(chars);
        }

        private void InitUI()
        {
            this.SCROLLBAR_HEIGHT = SystemInformation.HorizontalScrollBarHeight;
            this.SCROLLBAR_WIDTH = SystemInformation.VerticalScrollBarWidth;
            this.Size = new Size(612 + this.SCROLLBAR_WIDTH + 2 + 3, 10 + this.rowCount * 21);
            this.scrollBar = new VScrollBar();
            this.scrollBar.ValueChanged += new EventHandler(this.ScrollChanged);
            this.scrollBar.TabStop = true;
            this.scrollBar.TabIndex = 0;
            this.scrollBar.Dock = DockStyle.Right;
            this.scrollBar.Visible = false;
            this.edit = new TextBox();
            this.edit.AutoSize = false;
            this.edit.BorderStyle = BorderStyle.None;
            this.edit.Multiline = true;
            this.edit.ReadOnly = true;
            this.edit.ScrollBars = ScrollBars.Both;
            this.edit.AcceptsTab = true;
            this.edit.AcceptsReturn = true;
            this.edit.Dock = DockStyle.Fill;
            this.edit.Margin = Padding.Empty;
            this.edit.WordWrap = false;
            this.edit.Visible = false;
            this.Controls.Add((Control)this.scrollBar, 0, 0);
            this.Controls.Add((Control)this.edit, 0, 0);
        }

        private void InitState()
        {
            this.linesCount = (this.dataBuf.Length + this.columnCount - 1) / this.columnCount;
            this.startLine = 0;
            if (this.linesCount > this.rowCount)
            {
                this.displayLinesCount = this.rowCount;
                this.scrollBar.Hide();
                this.scrollBar.Maximum = this.linesCount - 1;
                this.scrollBar.LargeChange = this.rowCount;
                this.scrollBar.Show();
                this.scrollBar.Enabled = true;
            }
            else
            {
                this.displayLinesCount = this.linesCount;
                this.scrollBar.Hide();
                this.scrollBar.Maximum = this.rowCount;
                this.scrollBar.LargeChange = this.rowCount;
                this.scrollBar.Show();
                this.scrollBar.Enabled = false;
            }

            this.scrollBar.Select();
            this.Invalidate();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e) => this.scrollBar.Select();

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            switch (this.realDisplayMode)
            {
                case DisplayMode.Hexdump:
                    this.SuspendLayout();
                    this.edit.Hide();
                    this.scrollBar.Show();
                    this.ResumeLayout();
                    this.DrawClient(graphics);
                    this.DrawLines(graphics, this.startLine, this.displayLinesCount);
                    break;
                case DisplayMode.Ansi:
                    this.edit.Invalidate();
                    break;
                case DisplayMode.Unicode:
                    this.edit.Invalidate();
                    break;
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            int num = (this.ClientSize.Height - 10) / 21;
            if (num < 0 || num == this.rowCount)
                return;
            this.rowCount = num;
            if (this.Dock == DockStyle.None)
                this.Size = new Size(612 + this.SCROLLBAR_WIDTH + 2 + 3, 10 + this.rowCount * 21);
            if (this.scrollBar != null)
            {
                if (this.linesCount > this.rowCount)
                {
                    this.scrollBar.Hide();
                    this.scrollBar.Maximum = this.linesCount - 1;
                    this.scrollBar.LargeChange = this.rowCount;
                    this.scrollBar.Show();
                    this.scrollBar.Enabled = true;
                    this.scrollBar.Select();
                }
                else
                    this.scrollBar.Enabled = false;
            }

            this.displayLinesCount = this.startLine + this.rowCount < this.linesCount
                ? this.rowCount
                : this.linesCount - this.startLine;
        }

        /// <summary>Writes the raw data from the data buffer to a file.</summary>
        /// <param name="path">The file path to save to. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file write failed. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access. </exception>
        public virtual void SaveToFile(string path)
        {
            if (this.dataBuf == null)
                return;
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            try
            {
                fileStream.Write(this.dataBuf, 0, this.dataBuf.Length);
                fileStream.Close();
            }
            catch
            {
                fileStream.Close();
                throw;
            }
        }

        /// <summary>Handles the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event on the <see cref="T:System.ComponentModel.Design.BytesView" /> control's <see cref="T:System.Windows.Forms.ScrollBar" />.</summary>
        /// <param name="source">The source of the event. </param>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected virtual void ScrollChanged(object source, EventArgs e)
        {
            this.startLine = this.scrollBar.Value;
            this.Invalidate();
        }

        /// <summary>Sets the byte array to display in the viewer.</summary>
        /// <param name="bytes">The byte array to display. </param>
        /// <exception cref="T:System.ArgumentNullException">The specified byte array is <see langword="null" />. </exception>
        public virtual void SetBytes(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            if (this.dataBuf != null)
                this.dataBuf = (byte[])null;
            this.dataBuf = bytes;
            this.InitState();
            this.SetDisplayMode(this.displayMode);
        }

        /// <summary>Sets the current display mode.</summary>
        /// <param name="mode">The display mode to set. </param>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified display mode is not from the <see cref="T:System.ComponentModel.Design.DisplayMode" /> enumeration. </exception>
        public virtual void SetDisplayMode(DisplayMode mode)
        {
            //this.displayMode = System.Windows.Forms.ClientUtils.IsEnumValid((Enum)mode, (int)mode, 1, 4)
            //    ? mode
           //     : throw new InvalidEnumArgumentException(nameof(mode), (int)mode, typeof(DisplayMode));
            
            this.displayMode = mode;
            this.realDisplayMode = mode == DisplayMode.Auto ? this.GetAutoDisplayMode() : mode;
            switch (this.realDisplayMode)
            {
                case DisplayMode.Hexdump:
                    this.SuspendLayout();
                    this.edit.Hide();
                    if (this.linesCount > this.rowCount)
                    {
                        if (!this.scrollBar.Visible)
                        {
                            this.scrollBar.Show();
                            this.ResumeLayout();
                            this.scrollBar.Invalidate();
                            this.scrollBar.Select();
                            break;
                        }

                        this.ResumeLayout();
                        break;
                    }

                    this.ResumeLayout();
                    break;
                case DisplayMode.Ansi:
                    this.InitAnsi();
                    this.SuspendLayout();
                    this.edit.Show();
                    this.scrollBar.Hide();
                    this.ResumeLayout();
                    this.Invalidate();
                    break;
                case DisplayMode.Unicode:
                    this.InitUnicode();
                    this.SuspendLayout();
                    this.edit.Show();
                    this.scrollBar.Hide();
                    this.ResumeLayout();
                    this.Invalidate();
                    break;
            }
        }

        /// <summary>Sets the file to display in the viewer.</summary>
        /// <param name="path">The file path to load from. </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />. </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
        /// <exception cref="T:System.IO.IOException">The file load failed. </exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access. </exception>
        public virtual void SetFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                int length = (int)fileStream.Length;
                byte[] numArray = new byte[length + 1];
                fileStream.Read(numArray, 0, length);
                this.SetBytes(numArray);
                fileStream.Close();
            }
            catch
            {
                fileStream.Close();
                throw;
            }
        }

        /// <summary>Sets the current line for the <see cref="F:System.ComponentModel.Design.DisplayMode.Hexdump" /> view.</summary>
        /// <param name="line">The current line to display from. </param>
        public virtual void SetStartLine(int line)
        {
            if (line < 0 || line >= this.linesCount || line > this.dataBuf.Length / this.columnCount)
                this.startLine = 0;
            else
                this.startLine = line;
        }
    }
}