﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class Form1 : Form
    {
        private string label = "label";
        private string branch = "branch";
        private int labelCount = 0;
        private int branchCount = 0;
        private int startAddress = 0;

        private List<string> code = new List<string>();
        private List<string> passOne = new List<string>();
        private List<string> passTwo = new List<string>();
        private List<string> passThree = new List<string>();
        private List<string> found = new List<string>();
        private List<string> lineNumbers = new List<string>();
        private Dictionary<string, string> labelLoc = new Dictionary<string, string>();
        private Dictionary<string, string> branchLoc = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            generate.Enabled = false;
            PopulateOpCodeList.Init();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            leftWindowToolStripMenuItem.Enabled = false;
            rightWindowToolStripMenuItem.Enabled = false;
        }

        private void ReadBin(string fileName)
        {
            textBox1.Clear();
            var fileStuff = File.ReadAllBytes(fileName);
            int filePosition = 0;
            int lineNumber = 0;
            int pc = 0;

            var m_OpCodes = PopulateOpCodeList.GetOpCodes;
            while (filePosition < fileStuff.Length)
            {
                int opCode = fileStuff[filePosition];

                lineNumber = startAddress + filePosition;
                lineNumbers.Add(lineNumber.ToString("X4"));
                string line = (startAddress + filePosition).ToString("X4");
                line += "  " + opCode.ToString("X2");
                pc = startAddress + filePosition;
                foreach (OpCode oc in m_OpCodes)
                {

                    if (oc.m_code == opCode.ToString("X2"))
                    {
                        oc.GetCode(ref line, ref filePosition, fileStuff, lineNumber, pc);
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


        private void AddLabels(string start, string end)
        {
            textBox2.Clear();
            ClearRightWindow();
            passThree.Add("                *=$" + start);
            var originalContent = code;
            bool firstPass = true;
            int count = 0;

            // First pass parses the content looking for branch & jump conditions
            while (firstPass)
            {
                //Split each line into an array
                var dets = originalContent[count++].Split(' ');

                if (dets.Length > 1)
                {
                    //dets[2] contains the OP code
                    switch (dets[2].ToUpper())
                    {
                        case "20": // JSR
                        case "4C": // JMP
                            if (!labelLoc.Keys.Contains(dets[4] + dets[3]))
                            {
                                labelLoc.Add(dets[4] + dets[3], label + labelCount++.ToString());
                            }
                            passOne.Add(dets[8] + " " + dets[9]);
                            break;
                        case "90": // BCC
                        case "B0": // BCS 
                        case "F0": // BEQ
                        case "30": // BMI 
                        case "D0": // BNE
                        case "10": // BPL 
                        case "50": // BVC
                        case "70": // BVS
                            if (!branchLoc.Keys.Contains(dets[11].Replace("$", "")))
                            {
                                branchLoc.Add(dets[11].Replace("$", ""), branch + branchCount++.ToString());
                            }
                            passOne.Add(dets[10] + " " + dets[11]);
                            break;
                        default:

                            if (dets[3] == "" && dets[4] == "")
                            {
                                passOne.Add(dets[12]);
                            }
                            else if (dets[3] != "" && dets[4] == "")
                            {
                                passOne.Add(dets[10] + " " + dets[11]);
                            }
                            else if (dets[3] != "" && dets[4] != "")
                            {
                                passOne.Add(dets[8] + " " + dets[9]);
                            }
                            break;
                    }
                }
                if (count >= int.Parse(end, System.Globalization.NumberStyles.HexNumber) || count >= originalContent.Count || dets[0].ToLower().Contains(end.ToLower()))
                {
                    firstPass = false;
                }
            }

            // Second pass iterates through first pass collection adding labels and branches into the code
            int counter = 0;
            for (int i = 0; i < passOne.Count; i++)
            {
                string label = "";
                string assembly = passOne[counter++];
                foreach (KeyValuePair<String, String> memLocation in labelLoc)
                {
                    if (originalContent[i].ToUpper().Contains(memLocation.Key))
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
                    if (originalContent[i].ToUpper().Contains(memLocation.Key))
                    {
                        var dets = assembly.Split(' ');
                        if (dets[0].Contains("BNE") || dets[0].Contains("BEQ") || dets[0].Contains("BPL"))
                        {
                            assembly = dets[0] + " " + memLocation.Value;
                        }
                    }
                }
                passTwo.Add(label + assembly);
            }

            // Add the labels to the front of the code
            counter = 0;
            for (int i = 0; i < passOne.Count; i++)
            {
                var dets = originalContent[counter++].Split(' ');
                string label = "                ";
                foreach (KeyValuePair<String, String> memLocation in labelLoc)
                {
                    if (dets[0].ToUpper().Contains(memLocation.Key))
                    {
                        label = memLocation.Value + "          ";
                        // The memory address has been found add it list of addresses found
                        found.Add(memLocation.Key);
                    }
                }

                foreach (KeyValuePair<String, String> memLocation in branchLoc)
                {
                    if (dets[0].ToUpper().Contains(memLocation.Key))
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

            textBox2.Font = new Font(FontFamily.GenericMonospace, textBox2.Font.Size);
            textBox2.Lines = passThree.ToArray();
            rightWindowToolStripMenuItem.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open File";
            openFileDialog.InitialDirectory = @"*.*";
            openFileDialog.Filter = "All files (*.prg)|*.PRG|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ClearCollections();
                textBox1.Clear();
                MemoryLocation ml = new MemoryLocation();
                if (ml.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(ml.GetMemStartLocation, out startAddress);
                    ReadBin(openFileDialog.FileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void generate_Click(object sender, EventArgs e)
        {
            char[] startAdress = new char[lineNumbers[0].Length];
            char[] endAdress = new char[lineNumbers[lineNumbers.Count - 1].Length];

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
                int end1 = int.Parse(ms.GetSelectedMemEndLocation, System.Globalization.NumberStyles.HexNumber);
                int end2 = int.Parse(lineNumbers[lineNumbers.Count - 1], System.Globalization.NumberStyles.HexNumber);

                if (end1 <= end2)
                {

                    AddLabels(ms.GetSelectedMemStartLocation, ms.GetSelectedMemEndLocation);
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

        private void leftWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(code);
        }

        private void rightWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(passThree);
        }

        private void Save(List<string> collection)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.InitialDirectory = @"*.*";
            saveFileDialog.Filter = "All files (*.*)|*.*|All files (*.a)|*.a";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, collection);
            }
        }
    }
}