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
        private readonly string label = "label";
        private readonly string branch = "branch";
        private int labelCount = 0;
        private int branchCount = 0;
        private int startAddress = 0;

        private readonly List<string> code = new List<string>();
        private readonly List<string> passOne = new List<string>();
        private readonly List<string> passTwo = new List<string>();
        private readonly List<string> passThree = new List<string>();
        private readonly List<string> found = new List<string>();
        private readonly List<string> lineNumbers = new List<string>();
        private List<string> illegalOpcodes = new List<string>();

        private Dictionary<string, string[]> dataStatements = new Dictionary<string, string[]>();
        private readonly Dictionary<string, string> labelLoc = new Dictionary<string, string>();
        private readonly Dictionary<string, string> branchLoc = new Dictionary<string, string>();

        public C64BinaryToAssemblyConverter()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            generate.Enabled = false;
            leftWindowToolStripMenuItem.Enabled = false;
            rightWindowToolStripMenuItem.Enabled = false;
            PopulateOpCodeList.Init();
        }

        private void ParseFileContent(string fileName)
        {
            textBox1.Clear();
            var fileContent = File.ReadAllBytes(fileName);
            int filePosition = 0;
            var m_OpCodes = PopulateOpCodeList.GetOpCodes;
            while (filePosition < fileContent.Length)
            {
                int opCode = fileContent[filePosition];
                int lineNumber = startAddress + filePosition;
                lineNumbers.Add(lineNumber.ToString("X4"));
                string line = (startAddress + filePosition).ToString("X4");
                line += "  " + opCode.ToString("X2");
                int pc = startAddress + filePosition;
                foreach (OpCode oc in m_OpCodes)
                {
                    if (oc.Code == opCode.ToString("X2"))
                    {
                        oc.GetCode(ref line, ref filePosition, fileContent, lineNumber, pc, ref dataStatements, ref illegalOpcodes);
                    }
                }
                code.Add(line);
            }
            // Use a monospaced font
            textBox1.Font = new Font(FontFamily.GenericMonospace, textBox1.Font.Size);
            textBox1.Lines = code.ToArray();
            generate.Enabled = true;
            leftWindowToolStripMenuItem.Enabled = true;
        }

        private void AddLabels(
            int delta,
            string start, 
            string end, 
            bool replaceIllegalOpcodes, 
            Dictionary<string, string[]> bucket)
        {
            textBox2.Clear();
            ClearRightWindow();
            passThree.Add("                *=$" + start);
            var originalFileContent = code;
            InitialPass(delta, end, replaceIllegalOpcodes, bucket, originalFileContent);
            SecondPass(originalFileContent);
            FinalPass(originalFileContent);

            textBox2.Font = new Font(FontFamily.GenericMonospace, textBox2.Font.Size);
            textBox2.Lines = passThree.ToArray();
            rightWindowToolStripMenuItem.Enabled = true;
        }

        private void InitialPass(int delta, string end, bool replaceIllegalOpcodes, Dictionary<string, string[]> bucket, List<string> originalFileContent)
        {
            int count = 0;
            bool firstPass = true;
            int originalFileLength = code.Count;

            // First pass parses the content looking for branch & jump conditions
            while (firstPass)
            {
                //Split each line into an array
                var lineDetails = originalFileContent[count++].Split(' ');

                if (lineDetails.Length > 1)
                {
                    // Replace the Illegal Opcodes with data statement
                    if (replaceIllegalOpcodes && bucket.TryGetValue(lineDetails[0], out string[] dataValue))
                    {
                        foreach (string str in dataValue)
                        {
                            passOne.Add(str);
                        }
                    }
                    else
                    {
                        switch (lineDetails[2].ToUpper())
                        {
                            case "20": // JSR
                            case "4C": // JMP
                                if (!labelLoc.Keys.Contains(lineDetails[4] + lineDetails[3]))
                                {
                                    labelLoc.Add(lineDetails[4] + lineDetails[3], label + labelCount++.ToString());
                                }
                                passOne.Add(lineDetails[8] + " " + lineDetails[9]);
                                break;
                            case "90": // BCC
                            case "B0": // BCS
                            case "F0": // BEQ
                            case "30": // BMI
                            case "D0": // BNE
                            case "10": // BPL
                            case "50": // BVC
                            case "70": // BVS
                                if (!branchLoc.Keys.Contains(lineDetails[11].Replace("$", "")))
                                {
                                    branchLoc.Add(lineDetails[11].Replace("$", ""), branch + branchCount++.ToString());
                                }
                                passOne.Add(lineDetails[10] + " " + lineDetails[11]);
                                break;
                            default:
                                if (lineDetails[3] == "" && lineDetails[4] == "")
                                {
                                    passOne.Add(lineDetails[12]);
                                }
                                else if (lineDetails[3] != "" && lineDetails[4] == "")
                                {
                                    passOne.Add(lineDetails[10] + " " + lineDetails[11]);
                                }
                                else if (lineDetails[3] != "" && lineDetails[4] != "")
                                {
                                    passOne.Add(lineDetails[8] + " " + lineDetails[9]);
                                }
                                break;
                        }
                    }
                }
                if (count >= delta || count >= originalFileLength || lineDetails[0].ToLower().Contains(end.ToLower()))
                {
                    firstPass = false;
                }
            }
        }

        private void SecondPass(List<string> originalFileContent)
        {
            // Second pass iterates through first pass collection adding labels and branches into the code
            int counter = 0;
            for (int i = 0; i < passOne.Count; i++)
            {
                string assembly = passOne[counter++];
                foreach (KeyValuePair<String, String> memLocation in labelLoc)
                {
                    if (passOne[i].ToUpper().Contains(memLocation.Key))
                    //   if (originalFileContent[i].ToUpper().Contains(memLocation.Key))
                    {
                        var dets = assembly.Split(' ');
                        if (dets[0].Contains("JSR") || dets[0].Contains("JMP"))
                        {
                            assembly = dets[0] + " " + memLocation.Value;
                        }
                    }
                }
                foreach (KeyValuePair<String, String> memLocation in branchLoc)
                {
                    if (originalFileContent[i].ToUpper().Contains(memLocation.Key))
                    {
                        var dets = assembly.Split(' ');
                        if (dets[0].Contains("BNE") || dets[0].Contains("BEQ") || dets[0].Contains("BPL"))
                        {
                            assembly = dets[0] + " " + memLocation.Value;
                        }
                    }
                }
                passTwo.Add(assembly);
            }
        }

        private void FinalPass(List<string> originalFileContent)
        {
            // Add the labels to the front of the code
            int counter = 0;
            for (int i = 0; i < passOne.Count; i++)
            {
                var detail = originalFileContent[counter++].Split(' ');
                string label = "                ";
                foreach (var memLocation in from KeyValuePair<String, String> memLocation in labelLoc
                                            where detail[0].ToUpper().Contains(memLocation.Key)
                                            select memLocation)
                {
                    label = memLocation.Value + "          ";
                    // The memory address has been found add it another list
                    found.Add(memLocation.Key);
                }

                foreach (KeyValuePair<String, String> memLocation in branchLoc)
                {
                    if (detail[0].ToUpper().Contains(memLocation.Key))
                    {
                        label = memLocation.Value + "         ";
                    }
                }
                passThree.Add(label + passTwo[i]);
            }

            // Finally iterate through the found list & add references to the address not found
            foreach (KeyValuePair<String, String> memLocation in labelLoc)
            {
                if (!found.Contains(memLocation.Key))
                {
                    passThree.Add(memLocation.Value + " = $" + memLocation.Key);
                }
            }
        }

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
                    int.TryParse(ml.GetMemStartLocation, out startAddress);
                    ParseFileContent(openFileDialog.FileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Generate_Click(object sender, EventArgs e)
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
                    foreach (string str in code)
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

        private void ClearCollections()
        {
            ClearLeftWindow();
            ClearRightWindow();
        }

        private void ClearLeftWindow()
        {
            code.Clear();
        }

        private void ClearRightWindow()
        {
            passOne.Clear();
            passTwo.Clear();
            passThree.Clear();
            found.Clear();
            labelLoc.Clear();
            branchLoc.Clear();
        }

        private void LeftWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(code);
        }

        private void RightWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(passThree);
        }

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