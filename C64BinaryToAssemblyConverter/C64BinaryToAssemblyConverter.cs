using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public partial class C64BinaryToAssemblyConverter : Form
    {
        private byte[] _data;
        private readonly AssemblyCreator _assemblyCreator;
        private readonly Parser _parser = new Parser();
        private List<string> _lineNumbers = new List<string>();
        private List<string> _illegalOpcodes = new List<string>();
        private Dictionary<string, string[]> _dataStatements = new Dictionary<string, string[]>();
        private char[] _startAddress;
        private char[] _endAddress;
        private int _userDefinedStartAddress;
        private const string _byteDefinition = "!byte $";
        private readonly Regex regex = new Regex(@"^(?:[0-9A-Fa-f]{4}\s+[0-9A-Fa-f]{2}(?:\s+[0-9A-Fa-f]{2})*\s*(?:[^\r\n]*)\r?\n?)+$");

        public C64BinaryToAssemblyConverter()
        {
            InitializeComponent();
            byteviewer.SetDisplayMode(DisplayMode.Hexdump);
            MaximizeBox = false;
            MinimizeBox = false;
            GenerateLabels.Enabled = false;
            LeftWindowMenuItem.Enabled = false;
            RightWindowMenuItem.Enabled = false;
            ExportBytesAsBinaryMenuItem.Enabled = false;
            ExportBytesAsTextMenuItem.Enabled = false;
            PopulateOpCodeList.Init();
            _assemblyCreator = new AssemblyCreator();
        }

        /// <summary>
        /// OpenToolStripMenuItem_Click
        /// </summary>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = @"Open File",
                InitialDirectory = @"*.*",
                Filter = @"All files (*.prg)|*.PRG|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            ClearCollections();

            var ml = new LoadIntoMemoryLocationSelector();
            if (ml.ShowDialog() != DialogResult.OK) return;
            // Use a monospaced font
            DisAssemblyView.Font = new Font(FontFamily.GenericMonospace, DisAssemblyView.Font.Size);
            if (!int.TryParse(ml.GetMemStartLocation, NumberStyles.HexNumber, null, out var startAddress)) return;
            _userDefinedStartAddress = startAddress;
            _data = _parser.LoadBinaryData(openFileDialog.FileName);

            DisAssemblyView.Lines = _parser.ParseFileContent(_data, DisAssemblyView, startAddress, ref _lineNumbers);

            _dataStatements = _parser.DataStatements;
            _illegalOpcodes = _parser.IllegalOpCodes;

            GenerateLabels.Enabled = true;
            LeftWindowMenuItem.Enabled = true;
            ExportBytesAsBinaryMenuItem.Enabled = true;
            ExportBytesAsTextMenuItem.Enabled = true;

            //byteviewer.SetFile(openFileDialog.FileName, startAddress);
            byteviewer.SetFile(openFileDialog.FileName);
            FileLoaded.Text = openFileDialog.SafeFileName;
            FileLoaded.Left = Width / 2 - FileLoaded.Size.Width / 2 - 10;

            ConfigureStartAndEndAddresses();
        }

        /// <summary>
        /// Add Labels
        /// </summary>
        private void AddLabels(
            int delta,
            string start,
            string end,
            bool replaceIllegalOpcodes,
            Dictionary<string, string[]> replacedWithDataStatements)
        {
            AssemblyView.Clear();
            ClearRightWindow();
            AssemblyView.Font = new Font(FontFamily.GenericMonospace, AssemblyView.Font.Size);
            _assemblyCreator.ResetLabelAndBranchCounts();
            _assemblyCreator.Code = DisAssemblyView.Lines;
            _assemblyCreator.InitialPass(delta, end, replaceIllegalOpcodes, replacedWithDataStatements);
            _assemblyCreator.SecondPass();
            AssemblyView.Lines = _assemblyCreator.FinalPass(_parser.Code, start).ToArray();
            RightWindowMenuItem.Enabled = true;
        }

        /// <summary>
        /// ExitToolStripMenuItem_Click
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// ConfigureStartAndEndAddresses
        /// </summary>
        private void ConfigureStartAndEndAddresses()
        {
            char[] startAddress = new char[_lineNumbers[0].Length];
            char[] endAddress = new char[_lineNumbers[_lineNumbers.Count - 1].Length];

            int count = 0;
            foreach (char chr in _lineNumbers[0])
            {
                startAddress[count++] = chr;
            }
            count = 0;
            foreach (char chr in _lineNumbers[_lineNumbers.Count - 1])
            {
                endAddress[count++] = chr;
            }
            _startAddress = startAddress;
            _endAddress = endAddress;
        }

        /// <summary>
        /// Generate Labels
        /// </summary>
        private void GenerateLabelsClickEvent(object sender, EventArgs e)
        {
            int firstOccurence = 0;
            int lastOccurrence = 0;

            var ms = new MemoryLocationsToConvertSelector(_startAddress, _endAddress);
            if (ms.ShowDialog() != DialogResult.OK) return;
            var start = int.Parse(ms.GetSelectedMemStartLocation, NumberStyles.HexNumber);
            var end = int.Parse(ms.GetSelectedMemEndLocation, NumberStyles.HexNumber);
            //var dataStatmentsRequired = ms.GetConvertIllegalOpCodes;

            var delta = end - start;
            var firstIllegalOpcodeFound = false;
            var replacedWithDataStatements = new Dictionary<string, string[]>();
            var lastLineNum = int.Parse(_lineNumbers[_lineNumbers.Count - 1], NumberStyles.HexNumber);

            if (start <= end && end <= lastLineNum)
            {
                //Check to see if illegal opcodes exist within the code selection
                for (var i = start; i < end; i++)
                {
                    if (_illegalOpcodes.Contains(i.ToString("X4")))
                    {
                        if (i > firstOccurence && !firstIllegalOpcodeFound)
                        {
                            firstOccurence = i;
                            firstIllegalOpcodeFound = true;
                        }
                        if (i > lastOccurrence)
                        {
                            lastOccurrence = i;
                        }
                    }
                }

                var temp = lastOccurrence.ToString("X4");
                var index = 0;
                foreach (string str in _parser.Code)
                {
                    if (str.Contains(temp))
                    {
                        // nudge the last Occurrence along to the next valid opCode
                        lastOccurrence = int.Parse(_lineNumbers[++index], System.Globalization.NumberStyles.HexNumber);
                    }
                    index++;
                }

                for (int i = firstOccurence; i < lastOccurrence; i++)
                {
                    // Replace the Illegal Opcodes with data statement
                    if (_dataStatements.TryGetValue(i.ToString("X4"), out string[] dataValue))
                    {
                        replacedWithDataStatements.Add(i.ToString("X4"), dataValue);
                    }
                }

                var result = DialogResult.No;
                if (firstIllegalOpcodeFound)
                {
                    result = MessageBox.Show(@"Illegal Opcodes found within the selection from : " + firstOccurence.ToString("X4") + @" to " + lastOccurrence.ToString("X4") + "\n"
                                             + @"Replace Illegal Opcodes with data statements ?", @" ", MessageBoxButtons.YesNo);
                }

                var convertToBytes = false || result == DialogResult.Yes;
                AddLabels(delta, ms.GetSelectedMemStartLocation, ms.GetSelectedMemEndLocation, convertToBytes, replacedWithDataStatements);
            }
            else
            {
                MessageBox.Show(@"The selected end address exceeds the length of the bytes $" + _lineNumbers[_lineNumbers.Count - 1]);
            }
        }

        /// <summary>
        /// Clear Collections
        /// </summary>
        private void ClearCollections()
        {
            ClearLeftWindow();
            ClearRightWindow();
            DisAssemblyView.Clear();
            _assemblyCreator.ResetLabelAndBranchCounts();
        }

        /// <summary>
        /// Clear Left Window
        /// </summary>
        private void ClearLeftWindow()
        {
            _parser.Code.Clear();
            _parser.DataStatements.Clear();
        }

        /// <summary>
        /// Clear Right Window
        /// </summary>
        private void ClearRightWindow()
        {
            _assemblyCreator.PassOne.Clear();
            _assemblyCreator.PassTwo.Clear();
            _assemblyCreator.PassThree.Clear();
            _assemblyCreator.Found.Clear();
            _assemblyCreator.LabelLocations.Clear();
            _assemblyCreator.BranchLocations.Clear();
        }

        /// <summary>
        /// LeftWindowToolStripMenuItem_Click
        /// </summary>
        private void LeftWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(_parser.Code, "ASM files(*.a) | *.asm");
        }

        /// <summary>
        /// RightWindowToolStripMenuItem_Click
        /// </summary>
        private void RightWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(_assemblyCreator.PassThree, "ASM files(*.a) | *.asm");
        }

        /// <summary>
        /// Save
        /// </summary>
        private void Save(List<string> collection, string filter)
        {
            var saveFileDialog = SaveFileDialogue(filter);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, collection);
            }
        }

        /// <summary>
        /// SaveFileDialogue
        /// </summary>
        private static SaveFileDialog SaveFileDialogue(string filter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = @"Save File",
                InitialDirectory = @"*.*",
                //Filter = @"All files (*.*)|*.*|All files (*.a)|*.a",
                Filter = filter,
                FilterIndex = 2,
                RestoreDirectory = true
            };
            return saveFileDialog;
        }

        /// <summary>
        /// ExportBytesAsBinaryMenuItemClicked
        /// </summary>
        private void ExportBytesAsBinaryMenuItemClicked(object sender, EventArgs e)
        {
            var ms = new MemoryLocationsToConvertSelector(_startAddress, _endAddress);
            if (ms.ShowDialog() != DialogResult.OK) return;
            var start = int.Parse(ms.GetSelectedMemStartLocation, System.Globalization.NumberStyles.HexNumber) - _userDefinedStartAddress;
            var end = int.Parse(ms.GetSelectedMemEndLocation, System.Globalization.NumberStyles.HexNumber) - _userDefinedStartAddress;
            var saveFileDialog = SaveFileDialogue("Binary files (*.bin)|*.bin");
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            //byte[] by = new byte[end-start];
            //if (_data.Length > 0 && end <= _data.Length)
            //{
            //    var count = 0;
            //    for (var i = start; i < end; i++)
            //    {
            //        by[count++] = _data[i];
            //    }
            //    File.WriteAllBytes(saveFileDialog.FileName, by);
            //}

            if (_data.Length > 0 && end <= _data.Length)
            {
                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    for (var i = start; i <= end; i++)
                    {
                        fileStream.WriteByte(_data[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Export Bytes As Text Menu Item Clicked
        /// </summary>
        private void ExportBytesAsTextMenuItemClicked(object sender, EventArgs e)
        {
            var ms = new MemoryLocationsToConvertSelector(_startAddress, _endAddress);
            if (ms.ShowDialog() != DialogResult.OK) return;
            var fileStart = int.Parse(ms.GetSelectedMemStartLocation, NumberStyles.HexNumber);
            var fileEnd = int.Parse(ms.GetSelectedMemEndLocation, NumberStyles.HexNumber);
            var start = (uint)(fileStart - _userDefinedStartAddress);
            var end = (uint)(fileEnd - _userDefinedStartAddress);

            var dataStatements = new List<string>
            {
                "*=$" + fileStart.ToString("x4"),
                "; start address:" + fileStart.ToString("x4") + "-" + fileEnd.ToString("x4")
            };

            if (_data.Length > 0 && end <= _data.Length)
            {
                ConstructByteValues(start, end, dataStatements);
                Save(dataStatements, "Text files (*.txt)|*.txt");
            }
        }

        /// <summary>
        /// ConstructByteValues
        /// </summary>
        private void ConstructByteValues(uint start, uint end, List<string> dataStatements)
        {
            //StringBuilder eightBytesBld = new StringBuilder();
            //eightBytesBld.Append("!byte $");
            var eightBytes = _byteDefinition;
            var byteCounter = 0;

            for (uint i = start; i <= end; i++)
            {
                if (byteCounter != 8)
                {
                    eightBytes += _data[i].ToString("X2") + ",$";
                    //eightBytesBld.Append(_data[i].ToString("X2") + ",$");
                    byteCounter++;
                }
                if (i == end || byteCounter == 8)
                {
                    //var newEightBytes = eightBytesBld.ToString();
                    //newEightBytes = newEightBytes.Remove(newEightBytes.LastIndexOf(","), 2);
                    //dataStatements.Add(newEightBytes);
                    eightBytes = eightBytes.Remove(eightBytes.LastIndexOf(","), 2);
                    dataStatements.Add(eightBytes + "\n");
                    eightBytes = _byteDefinition;
                    byteCounter = 0;
                }
            }
        }

        /// <summary>
        /// Method to Convert To Data Bytes Click
        /// </summary>
        private void ConvertToDataBytesClick(object sender, EventArgs e)
        {
            var codeList = _parser.CodeList;
            string selectedText = DisAssemblyView.SelectedText;
            if (CheckStartOfTheSelectionText(selectedText))
            {
                // Split the text selection up into seperate lines
                char[] delimiters = { '\r', '\n' };
                string[] splitSelectedText = selectedText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Split the line up into the seperate items
                var startText = splitSelectedText[0].Split(' ');
                List<string> dataStatements = new List<string> { "*=$" + startText[0] };

                for (int i = 0; i < splitSelectedText.Length; i++)
                {
                    startText = splitSelectedText[i].Split(' ');
                    var index = GetIndex(startText[0]);
                    if (CheckStartOfTheSelectionText(splitSelectedText[i]))
                    {
                        var opCode = codeList[index];
                        dataStatements.Add("!byte " + opCode.Bytes);
                        // Does the selected text length need increasing ?
                        if (opCode.LineLength != splitSelectedText[i].Length)
                        {
                            DisAssemblyView.SelectionLength += (opCode.LineLength - splitSelectedText[i].Length);
                        }
                    }
                }
                DisAssemblyView.SelectedText = string.Join("\r\n", dataStatements) + "\r\n";
            }
        }

        /// <summary>
        /// Method to check the Start of the selection text
        /// matches the expected format
        /// </summary>
        private bool CheckStartOfTheSelectionText(string selectedText)
        {
            return regex.IsMatch(selectedText);
        }

        /// <summary>
        /// Method to find the index utilising the Line number
        /// </summary>
        private int GetIndex(string startText)
        {
            return Enumerable.Range(0, _parser.CodeList.Count).FirstOrDefault(i => _parser.CodeList[i].LineNumber.StartsWith(startText));
        }

        /// <summary>
        ///  FindTextInTextBox
        /// </summary>
        private void FindTextInTextBox(object sender, EventArgs e)
        {
            ShowFindDialog();
        }

        /// <summary>
        ///  ShowFindDialog
        /// </summary>
        private void ShowFindDialog()
        {
            using (Form findForm = new Form())
            using (TextBox txtFind = new TextBox())
            using (Button btnFind = new Button())
            {
                findForm.Text = "Find";
                findForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                findForm.StartPosition = FormStartPosition.CenterParent;
                findForm.ClientSize = new Size(280, 80);
                findForm.MaximizeBox = false;
                findForm.MinimizeBox = false;

                txtFind.Location = new Point(10, 10);
                txtFind.Width = 260;

                btnFind.Text = "Find Next";
                btnFind.Location = new Point(190, 40);
                btnFind.DialogResult = DialogResult.OK;

                findForm.AcceptButton = btnFind;

                findForm.Controls.Add(txtFind);
                findForm.Controls.Add(btnFind);

                if (findForm.ShowDialog(this) == DialogResult.OK)
                {
                    FindText(txtFind.Text);
                }
            }
        }

        /// <summary>
        ///  FindText
        /// </summary>
        private void FindText(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) { return; }

            int startIndex = DisAssemblyView.SelectionStart + DisAssemblyView.SelectionLength;

            int index = DisAssemblyView.Text.IndexOf(
                searchText,
                startIndex,
                StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                DisAssemblyView.SelectionStart = index;
                DisAssemblyView.SelectionLength = searchText.Length;
                DisAssemblyView.ScrollToCaret();
                DisAssemblyView.Focus();
            }
            else
            {
                MessageBox.Show("Text not found.", "Find");
            }
        }

        /// <summary>
        ///  ProcessCmdKey
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                ShowFindDialog();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}