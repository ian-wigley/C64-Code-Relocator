using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public class Parser
    {
        private List<OpCode> CodeList { get;} = new List<OpCode>();
        private List<string> _illegalOpcodes = new List<string>();
        private Dictionary<string, string[]> _dataStatements = new Dictionary<string, string[]>();
        public List<string> Code { get; set; } = new List<string>();
        public List<string> IllegalOpCodes => _illegalOpcodes;
        public Dictionary<string, string[]> DataStatements => _dataStatements;

        /// <summary>
        /// Load Binary Data
        /// </summary>
        public byte[] LoadBinaryData(string fileName)
        {
            try
            {
                return File.ReadAllBytes(fileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Error occurred whilst loading data ", exception.Message);
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Parse File Content
        /// </summary>
        public string[] ParseFileContent(
            byte[] data,
            TextBox textBox,
            int startAddress,
            ref List<string> lineNumbers
            )
        {
            textBox.Clear();
            var filePosition = 0;
            var opCodes = PopulateOpCodeList.GetOpCodes;
            if (opCodes.Count.Equals(0)) { return Array.Empty<string>();} 
            
            while (filePosition < data.Length)
            {
                int opCode = data[filePosition];
                var lineNumber = startAddress + filePosition;
                lineNumbers.Add(lineNumber.ToString("X4"));
                var line = (startAddress + filePosition).ToString("X4");
                line += "  " + opCode.ToString("X2");
                var pc = startAddress + filePosition;
                foreach (var oc in opCodes.Where(oc => oc.Code == opCode.ToString("X2")))
                {
                    oc.GetCode(ref line, ref filePosition, data, lineNumber, pc, ref _dataStatements, ref _illegalOpcodes);
                    CodeList.Add(oc);
                    break;
                }
                Code.Add(line);
            }

            return Code.ToArray();
        }
    }
}
