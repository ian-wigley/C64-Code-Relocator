using System;
using System.Collections.Generic;
using System.Linq;

namespace C64BinaryToAssemblyConverter
{
    public class AssemblyCreator
    {
        private readonly string label = "label";
        private readonly string branch = "branch";
        private int labelCount = 0;
        private int branchCount = 0;

        private List<string> code { get; set; } = new List<string>();
        public List<string> lineNumbers { get; private set; } = new List<string>();
        public List<string> illegalOpcodes { get; private set; } = new List<string>();
        public List<string> passOne { get; private set; } = new List<string>();
        public List<string> passTwo { get; private set; } = new List<string>();
        public List<string> passThree { get; private set; } = new List<string>();
        public List<string> found { get; private set; } = new List<string>();
        public Dictionary<string, string> labelLoc { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> branchLoc { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Initial Pass
        /// </summary>
        public void InitialPass(
            int delta, 
            string end, 
            bool replaceIllegalOpcodes, 
            Dictionary<string, string[]> replacedWithDataStatements, 
            List<string> originalFileContent
            )
        {
            int count = 0;
            bool firstPass = true;
            code = originalFileContent;
            int originalFileLength = code.Count;
            // First pass parses the content looking for branch & jump conditions
            while (firstPass)
            {
                // Split each line into an array
                var lineDetails = originalFileContent[count++].Split(' ');

                if (lineDetails.Length > 1)
                {
                    // Replace the Illegal Opcodes with data statement
                    if (replaceIllegalOpcodes && replacedWithDataStatements.TryGetValue(lineDetails[0], out string[] dataValue))
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
                if (count >= delta || count >= originalFileLength) // || lineDetails[0].ToLower().Contains(end.ToLower()))
                {
                    firstPass = false;
                }
            }
        }

        /// <summary>
        /// Second Pass
        /// </summary>
        public void SecondPass(List<string> originalFileContent)
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

        /// <summary>
        /// Final Pass
        /// </summary>
        public List<string> FinalPass(List<string> originalFileContent, string start)
        {
            passThree.Add("                *=$" + start);
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
            return passThree;
        }
    }
}
