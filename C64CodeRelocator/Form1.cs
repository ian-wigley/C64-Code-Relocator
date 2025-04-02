using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class C64BinaryToAssemblyConverter : Form
    {
        private byte[] data;
        private readonly AssemblyCreator assemblyCreator;
        private readonly Parser parser = new Parser();
        private List<string> lineNumbers = new List<string>();
        private List<string> illegalOpcodes = new List<string>();
        private Dictionary<string, string[]> dataStatements = new Dictionary<string, string[]>();

        public C64BinaryToAssemblyConverter()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            GenerateLabels.Enabled = false;
            leftWindowToolStripMenuItem.Enabled = false;
            rightWindowToolStripMenuItem.Enabled = false;
            PopulateOpCodeList.Init();
            assemblyCreator = new AssemblyCreator();
        }

        /// <summary>
        ///
        /// </summary>
        private void AddLabels(
            int delta,
            string start, 
            string end, 
            bool replaceIllegalOpcodes, 
            Dictionary<string, string[]> bucket)
        {
            textBox2.Clear();
            ClearRightWindow();
            textBox2.Font = new Font(FontFamily.GenericMonospace, textBox2.Font.Size);
            assemblyCreator.InitialPass(delta, end, replaceIllegalOpcodes, bucket, parser.Code);
            assemblyCreator.SecondPass(parser.Code);
            textBox2.Lines = assemblyCreator.FinalPass(parser.Code, start).ToArray();
            rightWindowToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        ///
        /// </summary>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open File",
                InitialDirectory = @"*.*",
                Filter = "All files (*.prg)|*.PRG|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ClearCollections();
                textBox1.Clear();
                MemoryLocation ml = new MemoryLocation();
                if (ml.ShowDialog() == DialogResult.OK)
                {
                    // Use a monospaced font
                    textBox1.Font = new Font(FontFamily.GenericMonospace, textBox1.Font.Size);
                    _ = int.TryParse(ml.GetMemStartLocation, out int startAddress);
                    data = parser.LoadBinaryData(openFileDialog.FileName);
                    textBox1.Lines = parser.ParseFileContent(data, textBox1, startAddress, ref lineNumbers); //, ref code);
                    GenerateLabels.Enabled = true;
                    leftWindowToolStripMenuItem.Enabled = true;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        ///
        /// </summary>
        private void GenerateLabelsClickEvent(object sender, EventArgs e)
        {
            char[] startAdress = new char[lineNumbers[0].Length];
            char[] endAdress = new char[lineNumbers[lineNumbers.Count - 1].Length];
            int firstOccurance = 0;
            int lastOccurrance = 0;

            int count = 0;
            foreach (char chr in lineNumbers[0])
            {
                startAdress[count++] = chr;
            }
            count = 0;
            foreach (char chr in lineNumbers[lineNumbers.Count - 1])
            {
                endAdress[count++] = chr;
            }

            MemorySelector ms = new MemorySelector(startAdress, endAdress);
            if (ms.ShowDialog() == DialogResult.OK)
            {
                int start = int.Parse(ms.GetSelectedMemStartLocation, System.Globalization.NumberStyles.HexNumber);
                int end = int.Parse(ms.GetSelectedMemEndLocation, System.Globalization.NumberStyles.HexNumber);
                int delta = end - start;
                bool firstIllegalOpcodeFound = false;
                Dictionary<string, string[]> replacedWithDataCollection = new Dictionary<string, string[]>();

                if (start <= end)
                {
                    //Check to see if illegal opcodes exist within the code selection
                    for (int i = start; i < end; i++)
                    {
                        if (illegalOpcodes.Contains(i.ToString("X4")))
                        {
                            if (i > firstOccurance && !firstIllegalOpcodeFound)
                            {
                                firstOccurance = i;
                                firstIllegalOpcodeFound = true;
                            }
                            if (i > lastOccurrance)
                            {
                                lastOccurrance = i;
                            }
                        }
                    }

                    var temp = lastOccurrance.ToString("X4");
                    int index = 0;
                    foreach (string str in parser.Code)
                    {
                        if (str.Contains(temp))
                        {
                            // nudge the last Occurance along to the next valid opCode
                            lastOccurrance = int.Parse(lineNumbers[++index], System.Globalization.NumberStyles.HexNumber);
                        }
                        index++;
                    }

                    for (int i = firstOccurance; i < lastOccurrance; i++)
                    {
                        // Replace the Illegal Opcodes with data statement
                        if (dataStatements.TryGetValue(i.ToString("X4"), out string[] dataValue))
                        {
                            replacedWithDataCollection.Add(i.ToString("X4"), dataValue);
                        }
                    }

                    DialogResult result = DialogResult.No;
                    if (firstIllegalOpcodeFound)
                    {
                        result = MessageBox.Show("Illegal Opcodes found within the selection from : " + firstOccurance.ToString("X4") + " to " + lastOccurrance.ToString("X4") + "\n"
                        + "Replace Illegal Opcodes with data statements ?", " ", MessageBoxButtons.YesNo);
                    }

                    bool convertToBytes = false;
                    if (result == DialogResult.Yes)
                    {
                        convertToBytes = true;
                    }
                    AddLabels(delta, ms.GetSelectedMemStartLocation, ms.GetSelectedMemEndLocation, convertToBytes, replacedWithDataCollection);
                }
                else
                {
                    MessageBox.Show("The selected end address exceeds the length of the bytes $" + lineNumbers[lineNumbers.Count - 1]);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void ClearCollections()
        {
            ClearLeftWindow();
            ClearRightWindow();
        }

        /// <summary>
        ///
        /// </summary>
        private void ClearLeftWindow()
        {
            parser.Code.Clear();
            parser.DataStatements.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        private void ClearRightWindow()
        {
            assemblyCreator.passOne.Clear();
            assemblyCreator.passTwo.Clear();
            assemblyCreator.passThree.Clear();
            assemblyCreator.found.Clear();
            assemblyCreator.labelLoc.Clear();
            assemblyCreator.branchLoc.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        private void LeftWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(parser.Code);
        }

        /// <summary>
        ///
        /// </summary>
        private void RightWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(assemblyCreator.passThree);
        }

        /// <summary>
        ///
        /// </summary>
        private void Save(List<string> collection)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save File",
                InitialDirectory = @"*.*",
                Filter = "All files (*.*)|*.*|All files (*.a)|*.a",
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