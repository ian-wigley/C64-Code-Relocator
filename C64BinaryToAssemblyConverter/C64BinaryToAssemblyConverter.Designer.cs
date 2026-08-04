using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    partial class C64BinaryToAssemblyConverter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C64BinaryToAssemblyConverter));
            this.DisAssemblyView = new System.Windows.Forms.TextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertSelectionToBytes = new System.Windows.Forms.ToolStripMenuItem();
            this.findText = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LeftWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RightWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportBytesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportBytesAsBinaryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportBytesAsTextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateLabels = new System.Windows.Forms.Button();
            this.AssemblyView = new System.Windows.Forms.TextBox();
            this.FileLoaded = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.memoryView = new System.Windows.Forms.TabPage();
            this.byteviewer = new BytesView();
            this.bitmapViewer = new System.Windows.Forms.TabPage();
            this.BitmapLocator = new System.Windows.Forms.GroupBox();
            this.BitmapColour = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ColourCombo = new System.Windows.Forms.ComboBox();
            this.ScreenCombo = new System.Windows.Forms.ComboBox();
            this.BitmapCombo = new System.Windows.Forms.ComboBox();
            this.ExportBitmap = new System.Windows.Forms.Button();
            this.DrawBitmap = new System.Windows.Forms.Button();
            this.ColourLocationLabel = new System.Windows.Forms.Label();
            this.ScreenLocationLabel = new System.Windows.Forms.Label();
            this.BitmapLocationLabel = new System.Windows.Forms.Label();
            this.C64Bitmap = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.memoryView.SuspendLayout();
            this.bitmapViewer.SuspendLayout();
            this.BitmapLocator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C64Bitmap)).BeginInit();
            this.SuspendLayout();
            // 
            // DisAssemblyView
            // 
            this.DisAssemblyView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisAssemblyView.ContextMenuStrip = this.contextMenu;
            this.DisAssemblyView.Location = new System.Drawing.Point(13, 38);
            this.DisAssemblyView.Multiline = true;
            this.DisAssemblyView.Name = "DisAssemblyView";
            this.DisAssemblyView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DisAssemblyView.Size = new System.Drawing.Size(362, 529);
            this.DisAssemblyView.TabIndex = 0;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertSelectionToBytes,
            this.findText});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(212, 48);
            // 
            // convertSelectionToBytes
            // 
            this.convertSelectionToBytes.Name = "convertSelectionToBytes";
            this.convertSelectionToBytes.Size = new System.Drawing.Size(211, 22);
            this.convertSelectionToBytes.Text = "Convert selection to Bytes";
            this.convertSelectionToBytes.Click += new System.EventHandler(this.ConvertToDataBytesClick);
            // 
            // findText
            // 
            this.findText.Name = "findText";
            this.findText.Size = new System.Drawing.Size(211, 22);
            this.findText.Text = "Find";
            this.findText.Click += new System.EventHandler(this.FindTextInTextBox);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.SaveAsMenuItem,
            this.ExportBytesMenuItem,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(138, 22);
            this.OpenMenuItem.Text = "Open";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LeftWindowMenuItem,
            this.RightWindowMenuItem});
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.Size = new System.Drawing.Size(138, 22);
            this.SaveAsMenuItem.Text = "Save As";
            // 
            // LeftWindowMenuItem
            // 
            this.LeftWindowMenuItem.Name = "LeftWindowMenuItem";
            this.LeftWindowMenuItem.Size = new System.Drawing.Size(149, 22);
            this.LeftWindowMenuItem.Text = "Left Window";
            this.LeftWindowMenuItem.Click += new System.EventHandler(this.LeftWindowToolStripMenuItem_Click);
            // 
            // RightWindowMenuItem
            // 
            this.RightWindowMenuItem.Name = "RightWindowMenuItem";
            this.RightWindowMenuItem.Size = new System.Drawing.Size(149, 22);
            this.RightWindowMenuItem.Text = "Right Window";
            this.RightWindowMenuItem.Click += new System.EventHandler(this.RightWindowToolStripMenuItem_Click);
            // 
            // ExportBytesMenuItem
            // 
            this.ExportBytesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportBytesAsBinaryMenuItem,
            this.ExportBytesAsTextMenuItem});
            this.ExportBytesMenuItem.Name = "ExportBytesMenuItem";
            this.ExportBytesMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ExportBytesMenuItem.Text = "Export Bytes";
            // 
            // ExportBytesAsBinaryMenuItem
            // 
            this.ExportBytesAsBinaryMenuItem.Name = "ExportBytesAsBinaryMenuItem";
            this.ExportBytesAsBinaryMenuItem.Size = new System.Drawing.Size(123, 22);
            this.ExportBytesAsBinaryMenuItem.Text = "As Binary";
            this.ExportBytesAsBinaryMenuItem.Click += new System.EventHandler(this.ExportBytesAsBinaryMenuItemClicked);
            // 
            // ExportBytesAsTextMenuItem
            // 
            this.ExportBytesAsTextMenuItem.Name = "ExportBytesAsTextMenuItem";
            this.ExportBytesAsTextMenuItem.Size = new System.Drawing.Size(123, 22);
            this.ExportBytesAsTextMenuItem.Text = "As Text";
            this.ExportBytesAsTextMenuItem.Click += new System.EventHandler(this.ExportBytesAsTextMenuItemClicked);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(138, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.Configure_Click);
            // 
            // GenerateLabels
            // 
            this.GenerateLabels.Location = new System.Drawing.Point(319, 575);
            this.GenerateLabels.Name = "GenerateLabels";
            this.GenerateLabels.Size = new System.Drawing.Size(145, 23);
            this.GenerateLabels.TabIndex = 2;
            this.GenerateLabels.Text = "Generate Labels";
            this.GenerateLabels.UseVisualStyleBackColor = true;
            this.GenerateLabels.Click += new System.EventHandler(this.GenerateLabelsClickEvent);
            // 
            // AssemblyView
            // 
            this.AssemblyView.Location = new System.Drawing.Point(382, 38);
            this.AssemblyView.Multiline = true;
            this.AssemblyView.Name = "AssemblyView";
            this.AssemblyView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AssemblyView.Size = new System.Drawing.Size(390, 529);
            this.AssemblyView.TabIndex = 3;
            // 
            // FileLoaded
            // 
            this.FileLoaded.AutoSize = true;
            this.FileLoaded.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FileLoaded.Location = new System.Drawing.Point(300, 5);
            this.FileLoaded.Name = "FileLoaded";
            this.FileLoaded.Size = new System.Drawing.Size(0, 13);
            this.FileLoaded.TabIndex = 9;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.memoryView);
            this.tabControl.Controls.Add(this.bitmapViewer);
            this.tabControl.Location = new System.Drawing.Point(12, 600);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 250);
            this.tabControl.TabIndex = 10;
            // 
            // memoryView
            // 
            this.memoryView.Controls.Add(this.byteviewer);
            this.memoryView.Location = new System.Drawing.Point(4, 22);
            this.memoryView.Name = "memoryView";
            this.memoryView.Padding = new System.Windows.Forms.Padding(3);
            this.memoryView.Size = new System.Drawing.Size(752, 224);
            this.memoryView.TabIndex = 0;
            this.memoryView.Text = "Memory View";
            this.memoryView.UseVisualStyleBackColor = true;
            // 
            // byteviewer
            // 
            this.byteviewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.byteviewer.AutoScroll = true;
            this.byteviewer.BackColor = System.Drawing.Color.Transparent;
            this.byteviewer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.byteviewer.ColumnCount = 1;
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.byteviewer.Location = new System.Drawing.Point(60, 10);
            this.byteviewer.Name = "byteviewer";
            this.byteviewer.RowCount = 1;
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.byteviewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.byteviewer.Size = new System.Drawing.Size(634, 178);
            this.byteviewer.TabIndex = 7;
            // 
            // bitmapViewer
            // 
            this.bitmapViewer.Controls.Add(this.BitmapLocator);
            this.bitmapViewer.Controls.Add(this.C64Bitmap);
            this.bitmapViewer.Location = new System.Drawing.Point(4, 22);
            this.bitmapViewer.Name = "bitmapViewer";
            this.bitmapViewer.Padding = new System.Windows.Forms.Padding(3);
            this.bitmapViewer.Size = new System.Drawing.Size(752, 224);
            this.bitmapViewer.TabIndex = 1;
            this.bitmapViewer.Text = "Bitmap Viewer";
            this.bitmapViewer.UseVisualStyleBackColor = true;
            // 
            // BitmapLocator
            // 
            this.BitmapLocator.Controls.Add(this.BitmapColour);
            this.BitmapLocator.Controls.Add(this.label1);
            this.BitmapLocator.Controls.Add(this.ColourCombo);
            this.BitmapLocator.Controls.Add(this.ScreenCombo);
            this.BitmapLocator.Controls.Add(this.BitmapCombo);
            this.BitmapLocator.Controls.Add(this.ExportBitmap);
            this.BitmapLocator.Controls.Add(this.DrawBitmap);
            this.BitmapLocator.Controls.Add(this.ColourLocationLabel);
            this.BitmapLocator.Controls.Add(this.ScreenLocationLabel);
            this.BitmapLocator.Controls.Add(this.BitmapLocationLabel);
            this.BitmapLocator.Location = new System.Drawing.Point(10, 20);
            this.BitmapLocator.Name = "BitmapLocator";
            this.BitmapLocator.Size = new System.Drawing.Size(200, 160);
            this.BitmapLocator.TabIndex = 7;
            this.BitmapLocator.TabStop = false;
            this.BitmapLocator.Text = "Bitmap locator";
            // 
            // BitmapColour
            // 
            this.BitmapColour.FormattingEnabled = true;
            this.BitmapColour.Location = new System.Drawing.Point(111, 25);
            this.BitmapColour.Name = "BitmapColour";
            this.BitmapColour.Size = new System.Drawing.Size(82, 21);
            this.BitmapColour.TabIndex = 19;
            this.BitmapColour.Text = "White";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Background Colour";
            // 
            // ColourCombo
            // 
            this.ColourCombo.FormattingEnabled = true;
            this.ColourCombo.Location = new System.Drawing.Point(93, 102);
            this.ColourCombo.Name = "ColourCombo";
            this.ColourCombo.Size = new System.Drawing.Size(100, 21);
            this.ColourCombo.TabIndex = 17;
            this.ColourCombo.Text = "4328";
            // 
            // ScreenCombo
            // 
            this.ScreenCombo.FormattingEnabled = true;
            this.ScreenCombo.Location = new System.Drawing.Point(93, 76);
            this.ScreenCombo.Name = "ScreenCombo";
            this.ScreenCombo.Size = new System.Drawing.Size(100, 21);
            this.ScreenCombo.TabIndex = 16;
            this.ScreenCombo.Text = "3F40";
            // 
            // BitmapCombo
            // 
            this.BitmapCombo.FormattingEnabled = true;
            this.BitmapCombo.Location = new System.Drawing.Point(93, 50);
            this.BitmapCombo.Name = "BitmapCombo";
            this.BitmapCombo.Size = new System.Drawing.Size(100, 21);
            this.BitmapCombo.TabIndex = 15;
            this.BitmapCombo.Text = "2000";
            // 
            // ExportBitmap
            // 
            this.ExportBitmap.Location = new System.Drawing.Point(102, 129);
            this.ExportBitmap.Name = "ExportBitmap";
            this.ExportBitmap.Size = new System.Drawing.Size(75, 23);
            this.ExportBitmap.TabIndex = 14;
            this.ExportBitmap.Text = "Export";
            this.ExportBitmap.UseVisualStyleBackColor = true;
            this.ExportBitmap.Click += new System.EventHandler(this.ExportBitmap_Click);
            // 
            // DrawBitmap
            // 
            this.DrawBitmap.Location = new System.Drawing.Point(21, 129);
            this.DrawBitmap.Name = "DrawBitmap";
            this.DrawBitmap.Size = new System.Drawing.Size(75, 23);
            this.DrawBitmap.TabIndex = 13;
            this.DrawBitmap.Text = "Draw";
            this.DrawBitmap.UseVisualStyleBackColor = true;
            this.DrawBitmap.Click += new System.EventHandler(this.DrawBitmapClick);
            // 
            // ColourLocationLabel
            // 
            this.ColourLocationLabel.AutoSize = true;
            this.ColourLocationLabel.Location = new System.Drawing.Point(7, 106);
            this.ColourLocationLabel.Name = "ColourLocationLabel";
            this.ColourLocationLabel.Size = new System.Drawing.Size(77, 13);
            this.ColourLocationLabel.TabIndex = 9;
            this.ColourLocationLabel.Text = "Colour location";
            // 
            // ScreenLocationLabel
            // 
            this.ScreenLocationLabel.AutoSize = true;
            this.ScreenLocationLabel.Location = new System.Drawing.Point(7, 79);
            this.ScreenLocationLabel.Name = "ScreenLocationLabel";
            this.ScreenLocationLabel.Size = new System.Drawing.Size(81, 13);
            this.ScreenLocationLabel.TabIndex = 8;
            this.ScreenLocationLabel.Text = "Screen location";
            // 
            // BitmapLocationLabel
            // 
            this.BitmapLocationLabel.AutoSize = true;
            this.BitmapLocationLabel.Location = new System.Drawing.Point(7, 55);
            this.BitmapLocationLabel.Name = "BitmapLocationLabel";
            this.BitmapLocationLabel.Size = new System.Drawing.Size(79, 13);
            this.BitmapLocationLabel.TabIndex = 7;
            this.BitmapLocationLabel.Text = "Bitmap location";
            // 
            // C64Bitmap
            // 
            this.C64Bitmap.Location = new System.Drawing.Point(240, 10);
            this.C64Bitmap.Name = "C64Bitmap";
            this.C64Bitmap.Size = new System.Drawing.Size(320, 220);
            this.C64Bitmap.TabIndex = 0;
            this.C64Bitmap.TabStop = false;
            // 
            // C64BinaryToAssemblyConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 853);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.FileLoaded);
            this.Controls.Add(this.GenerateLabels);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.AssemblyView);
            this.Controls.Add(this.DisAssemblyView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "C64BinaryToAssemblyConverter";
            this.Text = "C64 Binary To Assembly Converter";
            this.contextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.memoryView.ResumeLayout(false);
            this.bitmapViewer.ResumeLayout(false);
            this.BitmapLocator.ResumeLayout(false);
            this.BitmapLocator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C64Bitmap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        protected System.Windows.Forms.TextBox DisAssemblyView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.Button GenerateLabels;
        private System.Windows.Forms.TextBox AssemblyView;
        private System.Windows.Forms.ToolStripMenuItem LeftWindowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RightWindowMenuItem;
        private Label FileLoaded;
        private ToolStripMenuItem ExportBytesMenuItem;
        private ToolStripMenuItem ExportBytesAsBinaryMenuItem;
        private ToolStripMenuItem ExportBytesAsTextMenuItem;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem convertSelectionToBytes;
        private ToolStripMenuItem findText;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem configureToolStripMenuItem;
        private TabControl tabControl;
        private TabPage memoryView;
        private BytesView byteviewer;
        private TabPage bitmapViewer;
        private PictureBox C64Bitmap;
        private GroupBox BitmapLocator;
        private Label ColourLocationLabel;
        private Label ScreenLocationLabel;
        private Label BitmapLocationLabel;
        private Button DrawBitmap;
        private Button ExportBitmap;
        private ComboBox ColourCombo;
        private ComboBox ScreenCombo;
        private ComboBox BitmapCombo;
        private ComboBox BitmapColour;
        private Label label1;
        private ToolTip toolTip;
    }
}