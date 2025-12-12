using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel.Design;

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

        public C64BinaryToAssemblyConverter()
        {
            InitializeComponent();
            byteviewer.SetDisplayMode(DisplayMode.Hexdump);
            MaximizeBox = false;
            MinimizeBox = false;
            GenerateLabels.Enabled = false;
            leftWindowToolStripMenuItem.Enabled = false;
            rightWindowToolStripMenuItem.Enabled = false;
            PopulateOpCodeList.Init();
            _assemblyCreator = new AssemblyCreator();
        }

        /// <summary>
        ///
        /// </summary>
        private void AddLabels(
            int delta,
            string start, 
            string end, 
            bool replaceIllegalOpcodes, 
            Dictionary<string, string[]> replacedWithDataStatements)
        {
            textBox2.Clear();
            ClearRightWindow();
            textBox2.Font = new Font(FontFamily.GenericMonospace, textBox2.Font.Size);
            _assemblyCreator.InitialPass(delta, end, replaceIllegalOpcodes, replacedWithDataStatements, _parser.Code);
            _assemblyCreator.SecondPass(_parser.Code);
            textBox2.Lines = _assemblyCreator.FinalPass(_parser.Code, start).ToArray();
            rightWindowToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        ///
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
            textBox1.Clear();
            var ml = new LoadIntoMemoryLocationSelector();
            if (ml.ShowDialog() != DialogResult.OK) return;
            // Use a monospaced font
            textBox1.Font = new Font(FontFamily.GenericMonospace, textBox1.Font.Size);
            _ = int.TryParse(ml.GetMemStartLocation, out var startAddress);
            _data = _parser.LoadBinaryData(openFileDialog.FileName);
            textBox1.Lines = _parser.ParseFileContent(_data, textBox1, startAddress, ref _lineNumbers);
            
            _dataStatements = _parser.DataStatements;
            _illegalOpcodes = _parser.IllegalOpCodes;            
            
            GenerateLabels.Enabled = true;
            leftWindowToolStripMenuItem.Enabled = true;
            byteviewer.SetFile(openFileDialog.FileName);
            FileLoaded.Text = openFileDialog.SafeFileName;
        }

        /// <summary>
        ///
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Generate Labels
        /// </summary>
        private void GenerateLabelsClickEvent(object sender, EventArgs e)
        {
            char[] startAddress = new char[_lineNumbers[0].Length];
            char[] endAddress = new char[_lineNumbers[_lineNumbers.Count - 1].Length];
            int firstOccurence = 0;
            int lastOccurrence = 0;

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

            var ms = new MemoryLocationsToConvertSelector(startAddress, endAddress);
            if (ms.ShowDialog() != DialogResult.OK) return;
            var start = int.Parse(ms.GetSelectedMemStartLocation, System.Globalization.NumberStyles.HexNumber);
            var end = int.Parse(ms.GetSelectedMemEndLocation, System.Globalization.NumberStyles.HexNumber);
            //var dataStatmentsRequired = ms.GetConvertIllegalOpCodes;
                
            var delta = end - start;
            var firstIllegalOpcodeFound = false;
            var replacedWithDataStatements = new Dictionary<string, string[]>();

            if (start <= end)
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
                int index = 0;
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

                var convertToBytes = false;
                if (result == DialogResult.Yes)
                {
                    convertToBytes = true;
                }
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
        ///
        /// </summary>
        private void LeftWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(_parser.Code);
        }

        /// <summary>
        ///
        /// </summary>
        private void RightWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(_assemblyCreator.PassThree);
        }

        /// <summary>
        ///
        /// </summary>
        private void Save(List<string> collection)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = @"Save File",
                InitialDirectory = @"*.*",
                Filter = @"All files (*.*)|*.*|All files (*.a)|*.a",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, collection);
            }
        }
    }
}