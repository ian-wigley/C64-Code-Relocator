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
        private static readonly Font AddressFont = new Font("Microsoft Sans Serif", 8f);
        private static readonly Font HexdumpFont = new Font("Courier New", 8f);
        private readonly int _columnCount = 8;
        private byte[] _dataBuf;
        private int _displayLinesCount;
        private DisplayMode _displayMode;
        private TextBox _edit;
        private int _linesCount;
        private DisplayMode _realDisplayMode;
        private int _rowCount = 25;
        private VScrollBar _scrollBar;
        private int _scrollbarHeight;
        private int _scrollbarWidth;
        private int _startLine;
        private int _startAddress;

        public BytesView()
        {
            SuspendLayout();
            InitUI();
            ResumeLayout();
            _displayMode = DisplayMode.Hexdump;
            _realDisplayMode = DisplayMode.Hexdump;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private int CellToIndex(int column, int row)
        {
            return row * _columnCount + column;
        }

        private byte[] ComposeLineBuffer(int startingLine, int line)
        {
            var num = startingLine * _columnCount;
            var numArray = num + (line + 1) * _columnCount <= _dataBuf.Length
                ? new byte[_columnCount]
                : new byte[_dataBuf.Length % _columnCount];
            for (var column = 0; column < numArray.Length; ++column)
                numArray[column] = _dataBuf[num + CellToIndex(column, line)];
            return numArray;
        }

        private void DrawAddress(Graphics g, int startingLine, int line)
        {
            var addressFont = AddressFont;
            var s = ((startingLine + line) * _columnCount).ToString("X4",
                CultureInfo.InvariantCulture) + " " +
                    (_startAddress + ((startingLine + line) * _columnCount)).ToString("X4");
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
            // using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
            // {
            //     //g.FillRectangle(brush, new Rectangle(74, 5, 538, rowCount * 21));
            // }

            using (var pen = new Pen(SystemColors.ControlDark))
            {
                g.DrawRectangle(pen, new Rectangle(74, 5, 537, _rowCount * 21 - 1));
                g.DrawLine(pen, 474, 5, 474, 5 + _rowCount * 21 - 1);
            }
        }

        private static bool CharIsPrintable(char c)
        {
            var unicodeCategory = char.GetUnicodeCategory(c);
            return unicodeCategory != UnicodeCategory.Control;
        }

        private void DrawDump(Graphics g, byte[] lineBuffer, int line)
        {
            var stringBuilder = new StringBuilder(lineBuffer.Length);
            foreach (var t in lineBuffer)
            {
                var c = Convert.ToChar(t);
                stringBuilder.Append(CharIsPrintable(c) ? c : '.');
            }

            var hexdumpFont = HexdumpFont;
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
            var hexdumpFont = HexdumpFont;
            var stringBuilder = new StringBuilder(lineBuffer.Length * 3 + 1);
            for (var index = 0; index < lineBuffer.Length; ++index)
            {
                stringBuilder.Append(lineBuffer[index].ToString("X2", CultureInfo.InvariantCulture));
                stringBuilder.Append(" ");
                if (index == _columnCount / 2 - 1)
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
            _scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;
            _scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
            Size = new Size(612 + _scrollbarWidth + 2 + 3, 10 + _rowCount * 21);
            _scrollBar = new VScrollBar();
            _scrollBar.ValueChanged += ScrollChanged;
            _scrollBar.TabStop = true;
            _scrollBar.TabIndex = 0;
            _scrollBar.Dock = DockStyle.Right;
            _scrollBar.Visible = true; // false;
            _edit = new TextBox();
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
            Controls.Add(_scrollBar, 0, 0);
            // Controls.Add((Control)edit, 0, 0);
            MouseWheel += MouseWheelEvent;
        }

        private void InitState()
        {
            _linesCount = (_dataBuf.Length + _columnCount - 1) / _columnCount;
            _startLine = 0;
            if (_linesCount > _rowCount)
            {
                _displayLinesCount = _rowCount;
                _scrollBar.Hide();
                _scrollBar.Maximum = _linesCount - 1;
                _scrollBar.LargeChange = _rowCount;
                _scrollBar.Show();
                _scrollBar.Enabled = true;
            }
            else
            {
                _displayLinesCount = _linesCount;
                _scrollBar.Hide();
                _scrollBar.Maximum = _rowCount;
                _scrollBar.LargeChange = _rowCount;
                _scrollBar.Show();
                _scrollBar.Enabled = false;
            }

            _scrollBar.Select();
            Invalidate();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            _scrollBar.Select();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            switch (_realDisplayMode)
            {
                case DisplayMode.Hexdump:
                    SuspendLayout();
                    //edit.Hide();
                    _scrollBar.Show();
                    ResumeLayout();
                    DrawClient(graphics);
                    DrawLines(graphics, _startLine, _displayLinesCount);
                    break;
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            var num = (ClientSize.Height - 10) / 21;
            if (num < 0 || num == _rowCount)
                return;
            _rowCount = num;
            if (Dock == DockStyle.None)
                Size = new Size(612 + _scrollbarWidth + 2 + 3, 10 + _rowCount * 21);
            if (_scrollBar != null)
            {
                if (_linesCount > _rowCount)
                {
                    _scrollBar.Hide();
                    _scrollBar.Maximum = _linesCount - 1;
                    _scrollBar.LargeChange = _rowCount;
                    _scrollBar.Show();
                    _scrollBar.Enabled = true;
                    _scrollBar.Select();
                }
                else
                {
                    _scrollBar.Enabled = false;
                }
            }

            _displayLinesCount = _startLine + _rowCount < _linesCount
                ? _rowCount
                : _linesCount - _startLine;
        }

        private void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            if (_dataBuf == null) return;
            _scrollBar.Select();
        }   
        
        /// <summary>
        ///     Handles the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event on the
        ///     <see cref="T:System.ComponentModel.Design.BytesView" /> control's <see cref="T:System.Windows.Forms.ScrollBar" />.
        /// </summary>
        /// <param name="source">The source of the event. </param>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected virtual void ScrollChanged(object source, EventArgs e)
        {
            _startLine = _scrollBar.Value;
            Invalidate();
        }

        /// <summary>Sets the byte array to display in the viewer.</summary>
        /// <param name="bytes">The byte array to display. </param>
        /// <exception cref="T:System.ArgumentNullException">The specified byte array is <see langword="null" />. </exception>
        public void SetBytes(byte[] bytes, int startingAddress)
        {
            _startAddress = startingAddress;
            _dataBuf = null;
            _dataBuf = bytes ?? throw new ArgumentNullException(nameof(bytes));
            InitState();
            SetDisplayMode(_displayMode);
        }

        /// <summary>Sets the current display mode.</summary>
        /// <param name="mode">The display mode to set. </param>
        public virtual void SetDisplayMode(DisplayMode mode)
        {
            _displayMode = mode;
            _realDisplayMode = mode == DisplayMode.Auto ? DisplayMode.Hexdump : mode;
            switch (_realDisplayMode)
            {
                case DisplayMode.Hexdump:
                    SuspendLayout();
                    _edit.Hide();
                    if (_linesCount > _rowCount)
                    {
                        if (!_scrollBar.Visible)
                        {
                            _scrollBar.Show();
                            ResumeLayout();
                            _scrollBar.Invalidate();
                            _scrollBar.Select();
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