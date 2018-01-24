using System;
using System.Collections.Generic;
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

        private List<string> passOne = new List<string>();
        private List<string> passTwo = new List<string>();
        private List<string> passThree = new List<string>();
        private List<string> found = new List<string>();
        private Dictionary<string, string> labelLoc = new Dictionary<string, string>();
        private Dictionary<string, string> branchLoc = new Dictionary<string, string>();
        private string memStartLocation = "0900";
        private string memEndLocation = "0ad1";

        public Form1()
        {
            InitializeComponent();
            PopulateOpCodeList.Init();
            ReadBin();
            ReadFile();
        }

        private void ReadBin()
        {
            List<string> code = new List<string>();
            var fileStuff = File.ReadAllBytes("BIG-T");
            int filePosition = 0;
            int startAddress = 2304;
            int lineNumber = 0;
            int pc = 0;
            var m_OpCodes = PopulateOpCodeList.GetOpCodes;
            while (filePosition < fileStuff.Length)
            {
                int opCode = fileStuff[filePosition];
                lineNumber = startAddress + filePosition;
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
            textBox1.Lines = code.ToArray();
        }


        private void ReadFile()
        {
            var originalContent = File.ReadAllLines("Thun.s");
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
                        case "D0": // BNE
                        case "F0": // BEQ
                        case "10": // BPL
                            if (!branchLoc.Keys.Contains(dets[4] + dets[3]))
                            {
                                branchLoc.Add(dets[11].Replace("$", ""), branch + branchCount++.ToString());
                            }
                            int i = 0;
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
                if (dets[0].Contains("0ad1"))
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
            for (int i = 0; i < passOne.Count; i++)// String str in originalContent)
            {
                var dets = originalContent[counter++].Split(' ');
                string label = "                ";
                foreach (KeyValuePair<String, String> memLocation in labelLoc)
                {
                    if (dets[0].ToUpper().Contains(memLocation.Key))
                    {
                        label = memLocation.Value + "      ";
                        // The moemory address has been found add it another list
                        found.Add(memLocation.Key);
                    }
                }

                foreach (KeyValuePair<String, String> memLocation in branchLoc)
                {
                    if (dets[0].ToUpper().Contains(memLocation.Key))
                    {
                        label = memLocation.Value + "      ";
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

            File.WriteAllLines("new_assembly.s", passThree);

        }
    }
}