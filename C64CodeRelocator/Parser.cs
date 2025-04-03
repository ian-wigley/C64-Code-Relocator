using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C64CodeRelocator
{
    public class Parser
    {
        public List<string> Code { get; set; } = new List<string>();
        public List<OpCode> CodeList { get; set; } = new List<OpCode>();
        private List<string> illegalOpcodes = new List<string>();
        private Dictionary<string, string[]> dataStatements = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> DataStatements { get { return dataStatements; } }

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
                MessageBox.Show("Error occurred whilst loading data ", exception.Message);
                return new byte[0];
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
            int filePosition = 0;
            var m_OpCodes = PopulateOpCodeList.GetOpCodes;

            while (filePosition < data.Length)
            {
                int opCode = data[filePosition];
                int lineNumber = startAddress + filePosition;
                lineNumbers.Add(lineNumber.ToString("X4"));
                string line = (startAddress + filePosition).ToString("X4");
                line += "  " + opCode.ToString("X2");
                int pc = startAddress + filePosition;
                foreach (OpCode oc in m_OpCodes)
                {
                    if (oc.Code == opCode.ToString("X2"))
                    {
                        oc.GetCode(ref line, ref filePosition, data, lineNumber, pc, ref dataStatements, ref illegalOpcodes);
                        CodeList.Add(oc);
                        break;
                    }
                }
                Code.Add(line);
            }

            return Code.ToArray();
        }
    }
}
