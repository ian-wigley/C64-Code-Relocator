using System;
using System.Collections.Generic;
using System.Linq;

namespace C64BinaryToAssemblyConverter
{
    public class AssemblyCreator
    {
        private const string Label = "label";
        private const string Branch = "branch";
        private int _labelCount;
        private int _branchCount;

        // public List<string> LineNumbers { get; private set; } = new List<string>();
        // public List<string> IllegalOpcodes { get; private set; } = new List<string>();
        
        public List<string> Code { get; set; } = new List<string>();
        public List<string> PassOne { get; } = new List<string>();
        public List<string> PassTwo { get; } = new List<string>();
        public List<string> PassThree { get; } = new List<string>();
        public List<string> Found { get; } = new List<string>();
        public Dictionary<string, string> LabelLocations { get; } = new Dictionary<string, string>();
        public Dictionary<string, string> BranchLocations { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Initial Pass - parses the content looking for branch & jump conditions
        /// </summary>
        public void InitialPass(
            int delta, 
            string end, 
            bool replaceIllegalOpcodes, 
            Dictionary<string, string[]> replacedWithDataStatements, 
            List<string> originalFileContent
            )
        {
            var count = 0;
            var firstPass = true;
            Code = originalFileContent;
            var originalFileLength = Code.Count;

            while (firstPass)
            {
                // Split each line into an array
                var lineDetails = originalFileContent[count++].Split(' ');

                if (lineDetails.Length > 1)
                {
                    // Replace the Illegal Opcodes with data statement
                    if (replaceIllegalOpcodes && replacedWithDataStatements.TryGetValue(lineDetails[0], out string[] dataValue))
                    {
                        foreach (var str in dataValue)
                        {
                            PassOne.Add(str);
                        }
                    }
                    else
                    {
                        switch (lineDetails[2].ToUpper())
                        {
                            case "20": // JSR
                            case "4C": // JMP
                                if (!LabelLocations.Keys.Contains(lineDetails[4] + lineDetails[3]))
                                {
                                    LabelLocations.Add(lineDetails[4] + lineDetails[3], Label + _labelCount++.ToString());
                                }
                                PassOne.Add(lineDetails[8] + " " + lineDetails[9]);
                                break;
                            case "90": // BCC
                            case "B0": // BCS
                            case "F0": // BEQ
                            case "30": // BMI
                            case "D0": // BNE
                            case "10": // BPL
                            case "50": // BVC
                            case "70": // BVS
                                if (!BranchLocations.Keys.Contains(lineDetails[11].Replace("$", "")))
                                {
                                    BranchLocations.Add(lineDetails[11].Replace("$", ""), Branch + _branchCount++.ToString());
                                }
                                PassOne.Add(lineDetails[10] + " " + lineDetails[11]);
                                break;
                            default:
                                if (lineDetails[3] == "" && lineDetails[4] == "")
                                {
                                    PassOne.Add(lineDetails[12]);
                                }
                                else if (lineDetails[3] != "" && lineDetails[4] == "")
                                {
                                    PassOne.Add(lineDetails[10] + " " + lineDetails[11]);
                                }
                                else if (lineDetails[3] != "" && lineDetails[4] != "")
                                {
                                    PassOne.Add(lineDetails[8] + " " + lineDetails[9]);
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
        /// Second Pass - iterates through first pass collection adding labels and branches into the code
        /// </summary>
        public void SecondPass(List<string> originalFileContent)
        {
            var counter = 0;
            for (var i = 0; i < PassOne.Count; i++)
            {
                var assembly = PassOne[counter++];
                if (!PassOne[i].Contains("!byte $"))
                {
                    foreach (var memLocation in LabelLocations)
                    {
                        if (PassOne[i].ToUpper().Contains(memLocation.Key))
                        //   if (originalFileContent[i].ToUpper().Contains(memLocation.Key))
                        {
                            var dets = assembly.Split(' ');
                            if (dets[0].Contains("JSR") || dets[0].Contains("JMP"))
                            {
                                assembly = dets[0] + " " + memLocation.Value;
                            }
                        }
                    }
                    foreach (var memLocation in BranchLocations)
                    {
                        // if (originalFileContent[i].ToUpper().Contains(memLocation.Key))                        
                        // if (originalFileContent[innerCount++].ToUpper().Contains(memLocation.Key))
                        if(PassOne[i].ToUpper().Contains(memLocation.Key))
                        {
                            var dets = assembly.Split(' ');
                            if (dets[0].Contains("BNE") || dets[0].Contains("BEQ") || dets[0].Contains("BPL"))
                            {
                                assembly = dets[0] + " " + memLocation.Value;
                            }
                        }
                    }
                }
                PassTwo.Add(assembly);
            }
        }

        /// <summary>
        /// Final Pass - Method to add the labels to the front of the code
        /// </summary>
        public List<string> FinalPass(List<string> originalFileContent, string start)
        {
            PassThree.Add("                *=$" + start);
            var counter = 0;
            var i = 0;
            try
            {
                // The length of passOne can be longer if invalid opcodes have been found & converted to bytes 
                for (i = 0; i < PassOne.Count; i++)
                {
                    var label = "                ";
                    if (!PassOne[i].Contains("!byte $"))
                    {
                        var detail = originalFileContent[counter++].Split(' ');
                        foreach (var memLocation in from KeyValuePair<string, string> memLocation in LabelLocations
                                 where detail[0].ToUpper().Contains(memLocation.Key)
                                 select memLocation)
                        {
                            label = memLocation.Value + "          ";
                            // The memory address has been found add it another list
                            Found.Add(memLocation.Key);
                        }

                        foreach (var memLocation in BranchLocations.Where(memLocation =>
                                     detail[0].ToUpper().Contains(memLocation.Key)))
                        {
                            label = memLocation.Value + "         ";
                        }

                        // passThree.Add(label + passTwo[i]);
                    }
                    PassThree.Add(label + PassTwo[i]);
                    // else
                    // {
                    //     passThree.Add()
                    // }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var broken = ex.Message;
            }

            // Finally iterate through the found list & add references to the address not found
            foreach (var memLocation in LabelLocations.Where(memLocation => !Found.Contains(memLocation.Key)))
            {
                PassThree.Add(memLocation.Value + " = $" + memLocation.Key);
            }
            return PassThree;
        }
    }
}
