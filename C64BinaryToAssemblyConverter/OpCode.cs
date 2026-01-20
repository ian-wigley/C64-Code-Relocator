using System.Collections.Generic;

namespace C64BinaryToAssemblyConverter
{
    public class OpCode
    {
        public string Code { get; private set; }
        private bool Illegal { get; set; }
        public int LineLength { get; private set; }
        public string LineNumber { get; private set; }
        public string Bytes { get; private set; }

        private readonly int _numberOfBytes = 0;
        private readonly string _name;
        private readonly string _prefix;
        private readonly string _suffix;

        /// <summary>
        /// Constructor
        /// </summary>
        public OpCode(
            string code,
            string name,
            int numberOfBytes,
            string prefix,
            string suffix,
            bool illegal)
        {
            Code = code;
            _name = name;
            _numberOfBytes = numberOfBytes;
            _prefix = prefix;
            _suffix = suffix;
            Illegal = illegal;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public OpCode(OpCode other)
        {
            Code = other.Code;
            _name = other._name;
            _numberOfBytes = other._numberOfBytes;
            _prefix = other._prefix;
            _suffix = other._suffix;
            Illegal = other.Illegal;
        }

        /// <summary>
        /// Get Code
        /// </summary>
        public void GetCode(
            ref string line,
            ref int filePosition,
            byte[] bytes,
            int lineNumber,
            int pc,
            ref Dictionary<string, string[]> dataStatements,
            ref List<string> illegalOpCodes
            )
        {
            LineNumber = lineNumber.ToString("X4");
            string[] temp;
            if (_numberOfBytes == 1)
            {
                if (Illegal)
                {
                    //Add the programme counter location to the list of illegal opcodes found
                    illegalOpCodes.Add(pc.ToString("X4"));
                }
                Bytes = "$" + Code;
                temp = new string[1];
                temp[0] = "!byte $" + Code;
                dataStatements.Add(pc.ToString("X4"), temp);
                line += "          " + _name;
                filePosition += 1;
            }
            if (_numberOfBytes == 2)
            {
                if (filePosition + 1 < bytes.Length)
                {

                    if (Illegal)
                    {
                        illegalOpCodes.Add(pc.ToString("X4"));
                    }
                    Bytes = "$" + Code + ", $" + bytes[filePosition + 1].ToString("X2");
                    temp = new string[2];
                    temp[0] = "!byte $" + Code;
                    temp[1] = "!byte $" + bytes[filePosition + 1].ToString("X2");
                    dataStatements.Add(pc.ToString("X4"), temp);
                    line += " " + bytes[filePosition + 1].ToString("X2");

                    if (_name.Contains("BCC") || _name.Contains("BCS") ||
                        _name.Contains("BEQ") || _name.Contains("BMI") ||
                        _name.Contains("BNE") || _name.Contains("BPL") ||
                        _name.Contains("BVC") || _name.Contains("BVS"))
                    {
                        sbyte s = unchecked((sbyte)bytes[filePosition + 1]);
                        s += 2;
                        line += "       " + _name + " " + _prefix + (pc + s).ToString("X4");
                    }
                    else
                    {
                        line += "       " + _name + " " + _prefix + bytes[filePosition + 1].ToString("X2") + _suffix;
                    }
                }
                filePosition += 2;
            }
            else if (_numberOfBytes == 3) {
                if (filePosition + 2 < bytes.Length)
                {
                    if (Illegal)
                    {
                        illegalOpCodes.Add(pc.ToString("X4"));
                    }
                    Bytes = "$" + Code + ", $" + bytes[filePosition + 1].ToString("X2") + ", $" + bytes[filePosition + 2].ToString("X2");
                    temp = new string[3];
                    temp[0] = "!byte $" + Code;
                    temp[1] = "!byte $" + bytes[filePosition + 1].ToString("X2");
                    temp[2] = "!byte $" + bytes[filePosition + 2].ToString("X2");
                    dataStatements.Add(pc.ToString("X4"), temp);

                    line += " " + bytes[filePosition + 1].ToString("X2") + " " + bytes[filePosition + 2].ToString("X2");
                    line += "    " + _name + " " + _prefix + bytes[filePosition + 2].ToString("X2") + bytes[filePosition + 1].ToString("X2") + _suffix;
                }
                filePosition += 3;
            }
            LineLength = line.Length;
        }
    }

    /// <summary>
    /// Populate OpCode List
    /// </summary>
    public static class PopulateOpCodeList
    {
        public static List<OpCode> GetOpCodes { get; } = new List<OpCode>();

        public static void Init()
        {
            GetOpCodes.Add(new OpCode("00", "BRK", 1, "", "", false));
            GetOpCodes.Add(new OpCode("01", "ORA", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("02", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("03", "SLO", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("04", "NOP", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("05", "ORA", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("06", "ASL", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("07", "SLO", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("08", "PHP", 1, "", "", false));
            GetOpCodes.Add(new OpCode("09", "ORA", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("0A", "ASL", 1, "", "", false));
            GetOpCodes.Add(new OpCode("0B", "ANC", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("0C", "NOP", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("0D", "ORA", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("0E", "ASL", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("0F", "SLO", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("10", "BPL", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("11", "ORA", 2, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("12", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("13", "SLO", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("14", "NOP", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("15", "ORA", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("16", "ASL", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("17", "SLO", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("18", "CLC", 1, "", "", false));
            GetOpCodes.Add(new OpCode("19", "ORA", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("1A", "NOP", 1, "", "", true));
            GetOpCodes.Add(new OpCode("1B", "SLO", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("1C", "NOP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("1D", "ORA", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("1E", "ASL", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("1F", "SLO", 3, "", ",X", true));
            GetOpCodes.Add(new OpCode("20", "JSR", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("21", "AND", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("22", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("23", "RLA", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("24", "BIT", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("25", "AND", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("26", "ROL", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("27", "RLA", 2, "", "", true));
            GetOpCodes.Add(new OpCode("28", "PLP", 1, "", "", false));
            GetOpCodes.Add(new OpCode("29", "AND", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("2A", "ROL", 1, "", "", false));
            GetOpCodes.Add(new OpCode("2B", "ANC", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("2C", "BIT", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("2D", "AND", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("2E", "ROL", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("2F", "RLA", 3, "", "", true));
            GetOpCodes.Add(new OpCode("30", "BMI", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("31", "AND", 2, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("32", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("33", "RLA", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("34", "NOP", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("35", "AND", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("36", "ROL", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("37", "RLA", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("38", "SEC", 1, "", "", false));
            GetOpCodes.Add(new OpCode("39", "AND", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("3A", "NOP", 1, "", "", true));
            GetOpCodes.Add(new OpCode("3B", "RLA", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("3C", "NOP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("3D", "AND", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("3E", "ROL", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("3F", "RLA", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("40", "RTI", 1, "", "", false));
            GetOpCodes.Add(new OpCode("41", "EOR", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("42", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("43", "SRE", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("44", "NOP", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("45", "EOR", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("46", "LSR", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("47", "SRE", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("48", "PHA", 1, "", "", false));
            GetOpCodes.Add(new OpCode("49", "EOR", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("4A", "LSR", 1, "", "", false));
            GetOpCodes.Add(new OpCode("4B", "ASR", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("4C", "JMP", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("4D", "EOR", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("4E", "LSR", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("4F", "SRE", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("50", "BVC", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("51", "EOR", 2, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("52", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("53", "SRE", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("54", "NOP", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("55", "EOR", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("56", "LSR", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("57", "SRE", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("58", "CLI", 1, "", "", false));
            GetOpCodes.Add(new OpCode("59", "EOR", 2, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("5A", "NOP", 1, "", "", true));
            GetOpCodes.Add(new OpCode("5B", "SRE", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("5C", "NOP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("5D", "EOR", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("5E", "LSR", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("5F", "SRE", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("60", "RTS", 1, "", "", false));
            GetOpCodes.Add(new OpCode("61", "ADC", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("62", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("63", "RRA", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("64", "NOP", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("65", "ADC", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("66", "ROR", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("67", "RRA", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("68", "PLA", 1, "", "", false));
            GetOpCodes.Add(new OpCode("69", "ADC", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("6A", "ROR", 1, "", "", false));
            GetOpCodes.Add(new OpCode("6B", "ARR", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("6C", "JMP", 3, "($", ")", false));
            GetOpCodes.Add(new OpCode("6D", "ADC", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("6E", "ROR", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("6F", "RRA", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("70", "BVS", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("71", "ADC", 2, "($", ",Y)", false));
            GetOpCodes.Add(new OpCode("72", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("73", "RRA", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("74", "NOP", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("75", "ADC", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("76", "ROR", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("77", "RRA", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("78", "SEI", 1, "", "", false));
            GetOpCodes.Add(new OpCode("79", "ADC", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("7A", "NOP", 1, "", "", true));
            GetOpCodes.Add(new OpCode("7B", "RRA", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("7C", "NOP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("7D", "ADC", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("7E", "ROR", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("7F", "RRA", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("80", "NOP", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("81", "STA", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("82", "NOP", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("83", "SAX", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("84", "STY", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("85", "STA", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("86", "STX", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("87", "SAX", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("88", "DEY", 1, "", "", false));
            GetOpCodes.Add(new OpCode("89", "NOP", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("8A", "TXA", 1, "", "", false));
            GetOpCodes.Add(new OpCode("8B", "ANE", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("8C", "STY", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("8D", "STA", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("8E", "STX", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("8F", "SAX", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("90", "BCC", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("91", "STA", 2, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("92", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("93", "SHA", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("94", "STY", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("95", "STA", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("96", "STX", 2, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("97", "SAX", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("98", "TYA", 1, "", "", false));
            GetOpCodes.Add(new OpCode("99", "STA", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("9A", "TXS", 1, "", "", false));
            GetOpCodes.Add(new OpCode("9B", "SHS", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("9C", "SHY", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("9D", "STA", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("9E", "SHX", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("9F", "SHA", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("A0", "LDY", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("A1", "LDA", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("A2", "LDX", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("A3", "LAX", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("A4", "LDY", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("A5", "LDA", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("A6", "LDX", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("A7", "LAX", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("A8", "TAY", 1, "", "", false));
            GetOpCodes.Add(new OpCode("A9", "LDA", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("AA", "TAX", 1, "", "", false));
            GetOpCodes.Add(new OpCode("AB", "LXA", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("AC", "LDY", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("AD", "LDA", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("AE", "LDX", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("AF", "LAX", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("B0", "BCS", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("B1", "LDA", 2, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("B2", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("B3", "LAX", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("B4", "LDY", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("B5", "LDA", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("B6", "LDX", 2, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("B7", "LAX", 2, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("B8", "CLV", 1, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("B9", "LDA", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("BA", "TSX", 1, "", "", false));
            GetOpCodes.Add(new OpCode("BB", "LAX", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("BC", "LDY", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("BD", "LDA", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("BE", "LDX", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("BF", "LAX", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("C0", "CPY", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("C1", "CMP", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("C2", "NOP", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("C3", "DCP", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("C4", "CPY", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("C5", "CMP", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("C6", "DEC", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("C7", "DCP", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("C8", "INY", 1, "", "", false));
            GetOpCodes.Add(new OpCode("C9", "CMP", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("CA", "DEX", 1, "", "", false));
            GetOpCodes.Add(new OpCode("CB", "SBX", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("CC", "CPY", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("CD", "CMP", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("CE", "DEC", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("CF", "DCP", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("D0", "BNE", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("D1", "CMP", 1, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("D2", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("D3", "DCP", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("D4", "NOP", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("D5", "CMP", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("D6", "DEC", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("D7", "DCP", 2, "", ",X", true));
            GetOpCodes.Add(new OpCode("D8", "CLD", 1, "", "", false));
            GetOpCodes.Add(new OpCode("D9", "CMP", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("DA", "NOP", 1, "", "", true));
            GetOpCodes.Add(new OpCode("DB", "DCP", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("DC", "NOP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("DD", "CMP", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("DE", "DEC", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("DF", "DCP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("E0", "CPX", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("E1", "SBC", 2, "($", ",X)", false));
            GetOpCodes.Add(new OpCode("E2", "NOP", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("E3", "ISB", 2, "($", ",X)", true));
            GetOpCodes.Add(new OpCode("E4", "CPX", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("E5", "SBC", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("E6", "INC", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("E7", "ISB", 2, "$", "", true));
            GetOpCodes.Add(new OpCode("E8", "INX", 1, "", "", false));
            GetOpCodes.Add(new OpCode("E9", "SBC", 2, "#$", "", false));
            GetOpCodes.Add(new OpCode("EA", "NOP", 1, "", "", false));
            GetOpCodes.Add(new OpCode("EB", "SBC", 2, "#$", "", true));
            GetOpCodes.Add(new OpCode("EC", "CPX", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("ED", "SBC", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("EE", "INC", 3, "$", "", false));
            GetOpCodes.Add(new OpCode("EF", "ISB", 3, "$", "", true));
            GetOpCodes.Add(new OpCode("F0", "BEQ", 2, "$", "", false));
            GetOpCodes.Add(new OpCode("F1", "SBC", 2, "($", "),Y", false));
            GetOpCodes.Add(new OpCode("F2", "JAM", 1, "", "", true));
            GetOpCodes.Add(new OpCode("F3", "ISB", 2, "($", "),Y", true));
            GetOpCodes.Add(new OpCode("F4", "NOP", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("F5", "SBC", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("F6", "INC", 2, "$", ",X", false));
            GetOpCodes.Add(new OpCode("F7", "ISB", 2, "$", ",X", true));
            GetOpCodes.Add(new OpCode("F8", "SED", 1, "", "", false));
            GetOpCodes.Add(new OpCode("F9", "SBC", 3, "$", ",Y", false));
            GetOpCodes.Add(new OpCode("FA", "NOP", 1, "", "", true));
            GetOpCodes.Add(new OpCode("FB", "ISB", 3, "$", ",Y", true));
            GetOpCodes.Add(new OpCode("FC", "NOP", 3, "$", ",X", true));
            GetOpCodes.Add(new OpCode("FD", "SBC", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("FE", "INC", 3, "$", ",X", false));
            GetOpCodes.Add(new OpCode("FF", "ISB", 3, "$", ",X", true));
        }
    }
}