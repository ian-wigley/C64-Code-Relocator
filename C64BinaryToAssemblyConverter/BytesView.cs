using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public class BytesView : TableLayoutPanel
    {
        private static readonly Font ADDRESS_FONT = new Font("Microsoft Sans Serif", 8f);
        private static readonly Font HEXDUMP_FONT = new Font("Courier New", 8f);
        private readonly int columnCount = 8;
        private byte[] dataBuf;
        private int displayLinesCount;
        private DisplayMode displayMode;
        private TextBox edit;
        private int linesCount;
        private DisplayMode realDisplayMode;
        private int rowCount = 25;
        private VScrollBar scrollBar;
        private int SCROLLBAR_HEIGHT;
        private int SCROLLBAR_WIDTH;
        private int startLine;
        private int startAddress;

        public BytesView()
        {
            SuspendLayout();
            InitUI();
            ResumeLayout();
            displayMode = DisplayMode.Hexdump;
            realDisplayMode = DisplayMode.Hexdump;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private int CellToIndex(int column, int row)
        {
            return row * columnCount + column;
        }

        private byte[] ComposeLineBuffer(int startingLine, int line)
        {
            var num = startingLine * columnCount;
            var numArray = num + (line + 1) * columnCount <= dataBuf.Length
                ? new byte[columnCount]
                : new byte[dataBuf.Length % columnCount];
            for (var column = 0; column < numArray.Length; ++column)
                numArray[column] = dataBuf[num + CellToIndex(column, line)];
            return numArray;
        }

        private void DrawAddress(Graphics g, int startingLine, int line)
        {
            var addressFont = ADDRESS_FONT;
            var s = ((startingLine + line) * columnCount).ToString("X4",
                CultureInfo.InvariantCulture) + " " +
                    (startAddress + ((startingLine + line) * columnCount)).ToString("X4");
            Brush brush = new SolidBrush(ForeColor);
            try
            {
                g.DrawString(s, addressFont, brush, 5f, 7 + line * 21);
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawClient(Graphics g)
        {
            using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
            {
                //g.FillRectangle(brush, new Rectangle(74, 5, 538, rowCount * 21));
            }

            using (var pen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(pen, new Rectangle(74, 5, 537, rowCount * 21 - 1));
                g.DrawLine(pen, 474, 5, 474, 5 + rowCount * 21 - 1);
            }
        }

        private static bool CharIsPrintable(char c)
        {
            var unicodeCategory = char.GetUnicodeCategory(c);
            return unicodeCategory != UnicodeCategory.Control ||
                   unicodeCategory == UnicodeCategory.Format ||
                   unicodeCategory == UnicodeCategory.LineSeparator ||
                   unicodeCategory == UnicodeCategory.ParagraphSeparator ||
                   unicodeCategory == UnicodeCategory.OtherNotAssigned;
        }

        private void DrawDump(Graphics g, byte[] lineBuffer, int line)
        {
            var stringBuilder = new StringBuilder(lineBuffer.Length);
            foreach (var t in lineBuffer)
            {
                var c = Convert.ToChar(t);
                stringBuilder.Append(CharIsPrintable(c) ? c : '.');
            }

            var hexdumpFont = HEXDUMP_FONT;
            var brush = new SolidBrush(ForeColor);
            try
            {
                g.DrawString(stringBuilder.ToString(), hexdumpFont, brush, 479f - 100, 7 + line * 21);
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawHex(Graphics g, byte[] lineBuffer, int line)
        {
            var hexdumpFont = HEXDUMP_FONT;
            var stringBuilder = new StringBuilder(lineBuffer.Length * 3 + 1);
            for (var index = 0; index < lineBuffer.Length; ++index)
            {
                stringBuilder.Append(lineBuffer[index].ToString("X2", CultureInfo.InvariantCulture));
                stringBuilder.Append(" ");
                if (index == columnCount / 2 - 1)
                    stringBuilder.Append(" ");
            }

            Brush brush = new SolidBrush(ForeColor);
            try
            {
                g.DrawString(stringBuilder.ToString(), hexdumpFont, brush, 76f, 7 + line * 21);
            }
            finally
            {
                brush?.Dispose();
            }
        }

        private void DrawLines(Graphics g, int startingLine, int linesCount)
        {
            for (var line = 0; line < linesCount; ++line)
            {
                var lineBuffer = ComposeLineBuffer(startingLine, line);
                DrawAddress(g, startingLine, line);
                DrawHex(g, lineBuffer, line);
                DrawDump(g, lineBuffer, line);
                DrawChar(g, lineBuffer, line);
            }
        }

        private void DrawChar(Graphics g, byte[] lineBuffers, int line)
        {
            if (lineBuffers.Length != 8) return;
            var bmp = CreateBitmapFromBytes(lineBuffers, 8, 8);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            const int scale = 2;
            g.DrawImage(bmp, new Rectangle(500, 7 + line * 21, 8 * scale, 8 * scale));
        }

        private void InitUI()
        {
            SCROLLBAR_HEIGHT = SystemInformation.HorizontalScrollBarHeight;
            SCROLLBAR_WIDTH = SystemInformation.VerticalScrollBarWidth;
            Size = new Size(612 + SCROLLBAR_WIDTH + 2 + 3, 10 + rowCount * 21);
            scrollBar = new VScrollBar();
            scrollBar.ValueChanged += ScrollChanged;
            scrollBar.TabStop = true;
            scrollBar.TabIndex = 0;
            scrollBar.Dock = DockStyle.Right;
            scrollBar.Visible = true; // false;
            edit = new TextBox();
            // edit.AutoSize = false;
            // edit.BorderStyle = BorderStyle.None;
            // edit.Multiline = true;
            // edit.ReadOnly = true;
            // edit.ScrollBars = ScrollBars.Both;
            // edit.AcceptsTab = true;
            // edit.AcceptsReturn = true;
            // edit.Dock = DockStyle.Fill;
            // edit.Margin = Padding.Empty;
            // edit.WordWrap = false;
            // edit.Visible = false;
            Controls.Add(scrollBar, 0, 0);
            // Controls.Add((Control)edit, 0, 0);
            MouseWheel += MouseWheelEvent;
        }

        private void InitState()
        {
            linesCount = (dataBuf.Length + columnCount - 1) / columnCount;
            startLine = 0;
            if (linesCount > rowCount)
            {
                displayLinesCount = rowCount;
                scrollBar.Hide();
                scrollBar.Maximum = linesCount - 1;
                scrollBar.LargeChange = rowCount;
                scrollBar.Show();
                scrollBar.Enabled = true;
            }
            else
            {
                displayLinesCount = linesCount;
                scrollBar.Hide();
                scrollBar.Maximum = rowCount;
                scrollBar.LargeChange = rowCount;
                scrollBar.Show();
                scrollBar.Enabled = false;
            }

            scrollBar.Select();
            Invalidate();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            scrollBar.Select();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            switch (realDisplayMode)
            {
                case DisplayMode.Hexdump:
                    SuspendLayout();
                    //edit.Hide();
                    scrollBar.Show();
                    ResumeLayout();
                    DrawClient(graphics);
                    DrawLines(graphics, startLine, displayLinesCount);
                    break;
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            var num = (ClientSize.Height - 10) / 21;
            if (num < 0 || num == rowCount)
                return;
            rowCount = num;
            if (Dock == DockStyle.None)
                Size = new Size(612 + SCROLLBAR_WIDTH + 2 + 3, 10 + rowCount * 21);
            if (scrollBar != null)
            {
                if (linesCount > rowCount)
                {
                    scrollBar.Hide();
                    scrollBar.Maximum = linesCount - 1;
                    scrollBar.LargeChange = rowCount;
                    scrollBar.Show();
                    scrollBar.Enabled = true;
                    scrollBar.Select();
                }
                else
                {
                    scrollBar.Enabled = false;
                }
            }

            displayLinesCount = startLine + rowCount < linesCount
                ? rowCount
                : linesCount - startLine;
        }

        private void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            if (dataBuf == null) return;
            scrollBar.Select();
        }   
        
        /// <summary>
        ///     Handles the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event on the
        ///     <see cref="T:System.ComponentModel.Design.BytesView" /> control's <see cref="T:System.Windows.Forms.ScrollBar" />.
        /// </summary>
        /// <param name="source">The source of the event. </param>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected virtual void ScrollChanged(object source, EventArgs e)
        {
            startLine = scrollBar.Value;
            Invalidate();
        }

        /// <summary>Sets the byte array to display in the viewer.</summary>
        /// <param name="bytes">The byte array to display. </param>
        /// <exception cref="T:System.ArgumentNullException">The specified byte array is <see langword="null" />. </exception>
        public void SetBytes(byte[] bytes, int startingAddress)
        {
            startAddress = startingAddress;
            dataBuf = null;
            dataBuf = bytes ?? throw new ArgumentNullException(nameof(bytes));
            InitState();
            SetDisplayMode(displayMode);
        }

        /// <summary>Sets the current display mode.</summary>
        /// <param name="mode">The display mode to set. </param>
        public virtual void SetDisplayMode(DisplayMode mode)
        {
            displayMode = mode;
            realDisplayMode = mode == DisplayMode.Auto ? DisplayMode.Hexdump : mode;
            switch (realDisplayMode)
            {
                case DisplayMode.Hexdump:
                    SuspendLayout();
                    edit.Hide();
                    if (linesCount > rowCount)
                    {
                        if (!scrollBar.Visible)
                        {
                            scrollBar.Show();
                            ResumeLayout();
                            scrollBar.Invalidate();
                            scrollBar.Select();
                            break;
                        }

                        ResumeLayout();
                        break;
                    }

                    ResumeLayout();
                    break;
            }
        }

        private static Bitmap CreateBitmapFromBytes(byte[] data, int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format1bppIndexed);

            // Lock the bitmap's bits
            var bmpData = bmp.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format1bppIndexed);

            var stride = bmpData.Stride;
            var ptr = bmpData.Scan0;

            var paddedData = new byte[stride * height];

            for (var y = 0; y < height; y++) paddedData[y * stride] = data[y];

            Marshal.Copy(paddedData, 0, ptr, paddedData.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
}