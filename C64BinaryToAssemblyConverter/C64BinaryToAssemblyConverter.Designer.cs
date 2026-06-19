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
            this.c64Bitmap = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.memoryView.SuspendLayout();
            this.bitmapViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c64Bitmap)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.bitmapViewer.Controls.Add(this.groupBox1);
            this.bitmapViewer.Controls.Add(this.c64Bitmap);
            this.bitmapViewer.Location = new System.Drawing.Point(4, 22);
            this.bitmapViewer.Name = "bitmapViewer";
            this.bitmapViewer.Padding = new System.Windows.Forms.Padding(3);
            this.bitmapViewer.Size = new System.Drawing.Size(752, 224);
            this.bitmapViewer.TabIndex = 1;
            this.bitmapViewer.Text = "Bitmap Viewer";
            this.bitmapViewer.UseVisualStyleBackColor = true;
            // 
            // c64Bitmap
            // 
            this.c64Bitmap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c64Bitmap.Location = new System.Drawing.Point(3, 3);
            this.c64Bitmap.Name = "c64Bitmap";
            this.c64Bitmap.Size = new System.Drawing.Size(746, 218);
            this.c64Bitmap.TabIndex = 0;
            this.c64Bitmap.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(487, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 140);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(91, 91);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 12;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Colour location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Screen location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Bitmap location";
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
            ((System.ComponentModel.ISupportInitialize)(this.c64Bitmap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private PictureBox c64Bitmap;
        private GroupBox groupBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}