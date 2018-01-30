using System.Collections.Generic;

namespace C64CodeRelocator
{
    public class OpCode
    {
        public string m_code { get; private set; }
        private string m_name = "";
        private int m_numberOfBytes = 0;
        private string m_prefix = "";
        private string m_suffix = "";

        public OpCode(string code, string name, int numberOfBytes, string prefix, string suffix)
        {
            m_code = code;
            m_name = name;
            m_numberOfBytes = numberOfBytes;
            m_prefix = prefix;
            m_suffix = suffix;
        }

        public void GetCode(ref string line, ref int filePosition, byte[] fileStuff, int lineNumber, int pc)
        {

            if (m_numberOfBytes == 1)
            {
                line += "          " + m_name;
                filePosition += 1;
            }
            if (m_numberOfBytes == 2)
            {
                line += " " + fileStuff[filePosition + 1].ToString("X2");

                if (m_name.Contains("BCC") || m_name.Contains("BCS") ||
                    m_name.Contains("BEQ") || m_name.Contains("BMI") ||
                    m_name.Contains("BNE") || m_name.Contains("BPL") ||
                    m_name.Contains("BVC") || m_name.Contains("BVS"))
                {
                    sbyte s = unchecked((sbyte)fileStuff[filePosition + 1]);
                    s += 2;
                    line += "       " + m_name + " " + m_prefix + (pc + s).ToString("X4");
                }
                else
                {
                    line += "       " + m_name + " " + m_prefix + fileStuff[filePosition + 1].ToString("X2") + m_suffix;
                }

                filePosition += 2;
            }
            else if (m_numberOfBytes == 3)
            {
                line += " " + fileStuff[filePosition + 1].ToString("X2") + " " + fileStuff[filePosition + 2].ToString("X2");
                line += "    " + m_name + " " + m_prefix + fileStuff[filePosition + 2].ToString("X2") + fileStuff[filePosition + 1].ToString("X2") + m_suffix;
                filePosition += 3;
            }
        }
    }

    public static class PopulateOpCodeList
    {
        private static List<OpCode> m_OpCodes = new List<OpCode>();
        public static List<OpCode> GetOpCodes { get { return m_OpCodes; } }

        public static void Init()
        {
            m_OpCodes.Add(new OpCode("00", "BRK", 1, "", ""));
            m_OpCodes.Add(new OpCode("01", "ORA", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("02", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("03", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("04", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("05", "ORA", 2, "$", ""));
            m_OpCodes.Add(new OpCode("06", "ASL", 2, "$", ""));
            m_OpCodes.Add(new OpCode("07", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("08", "PHP", 1, "", ""));
            m_OpCodes.Add(new OpCode("09", "ORA", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("0A", "ASL", 1, "", ""));
            m_OpCodes.Add(new OpCode("0B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("0C", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("0D", "ORA", 3, "$", ""));
            m_OpCodes.Add(new OpCode("0E", "ASL", 3, "$", ""));
            m_OpCodes.Add(new OpCode("0F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("10", "BPL", 2, "$", ""));
            m_OpCodes.Add(new OpCode("11", "ORA", 2, "($", ",Y)"));
            m_OpCodes.Add(new OpCode("12", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("13", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("14", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("15", "ORA", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("16", "ASL", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("17", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("18", "CLC", 1, "", ""));
            m_OpCodes.Add(new OpCode("19", "ORA", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("1A", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("1B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("1C", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("1D", "ORA", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("1E", "ASL", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("1F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("20", "JSR", 3, "$", ""));
            m_OpCodes.Add(new OpCode("21", "AND", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("22", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("23", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("24", "BIT", 2, "$", ""));
            m_OpCodes.Add(new OpCode("25", "AND", 2, "$", ""));
            m_OpCodes.Add(new OpCode("26", "ROL", 2, "$", ""));
            m_OpCodes.Add(new OpCode("27", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("28", "PLP", 1, "", ""));
            m_OpCodes.Add(new OpCode("29", "AND", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("2A", "ROL", 1, "", ""));
            m_OpCodes.Add(new OpCode("2B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("2C", "BIT", 3, "$", ""));
            m_OpCodes.Add(new OpCode("2D", "AND", 3, "$", ""));
            m_OpCodes.Add(new OpCode("2E", "ROL", 3, "$", ""));
            m_OpCodes.Add(new OpCode("2F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("30", "BMI", 2, "$", ""));
            m_OpCodes.Add(new OpCode("31", "AND", 2, "($", "),Y"));
            m_OpCodes.Add(new OpCode("32", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("33", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("34", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("35", "AND", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("36", "ROL", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("37", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("38", "SEC", 1, "", ""));
            m_OpCodes.Add(new OpCode("39", "AND", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("3A", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("3B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("3C", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("3D", "AND", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("3E", "ROL", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("3F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("40", "RTI", 1, "", ""));
            m_OpCodes.Add(new OpCode("41", "EOR", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("42", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("43", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("44", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("45", "EOR", 2, "$", ""));
            m_OpCodes.Add(new OpCode("46", "LSR", 2, "$", ""));
            m_OpCodes.Add(new OpCode("47", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("48", "PHA", 1, "", ""));
            m_OpCodes.Add(new OpCode("49", "EOR", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("4A", "LSR", 1, "", ""));
            m_OpCodes.Add(new OpCode("4B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("4C", "JMP", 3, "$", ""));
            m_OpCodes.Add(new OpCode("4D", "EOR", 3, "$", ""));
            m_OpCodes.Add(new OpCode("4E", "LSR", 3, "$", ""));
            m_OpCodes.Add(new OpCode("4F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("50", "BVC", 2, "$", ""));
            m_OpCodes.Add(new OpCode("51", "EOR", 2, "($", "),Y"));
            m_OpCodes.Add(new OpCode("52", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("53", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("54", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("55", "EOR", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("56", "LSR", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("57", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("58", "CLI", 1, "", ""));
            m_OpCodes.Add(new OpCode("59", "EOR", 2, "$", ",Y"));
            m_OpCodes.Add(new OpCode("5A", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("5B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("5C", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("5D", "EOR", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("5E", "LSR", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("5F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("60", "RTS", 1, "", ""));
            m_OpCodes.Add(new OpCode("61", "ADC", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("62", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("63", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("64", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("65", "ADC", 2, "$", ""));
            m_OpCodes.Add(new OpCode("66", "ROR", 2, "$", ""));
            m_OpCodes.Add(new OpCode("67", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("68", "PLA", 1, "", ""));
            m_OpCodes.Add(new OpCode("69", "ADC", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("6A", "ROR", 1, "", ""));
            m_OpCodes.Add(new OpCode("6B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("6C", "JMP", 3, "($", ")"));
            m_OpCodes.Add(new OpCode("6D", "ADC", 3, "$", ""));
            m_OpCodes.Add(new OpCode("6E", "ROR", 3, "$", ""));
            m_OpCodes.Add(new OpCode("6F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("70", "BVS", 2, "$", ""));
            m_OpCodes.Add(new OpCode("71", "ADC", 2, "($", ",Y"));
            m_OpCodes.Add(new OpCode("72", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("73", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("74", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("75", "ADC", 2, "", ",X"));
            m_OpCodes.Add(new OpCode("76", "ROR", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("77", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("78", "SEI", 1, "", ""));
            m_OpCodes.Add(new OpCode("79", "ADC", 3, "", ""));
            m_OpCodes.Add(new OpCode("7A", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("7B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("7C", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("7D", "ADC", 3, "", ""));
            m_OpCodes.Add(new OpCode("7E", "ROR", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("7F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("80", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("81", "STA", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("82", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("83", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("84", "STY", 2, "$", ""));
            m_OpCodes.Add(new OpCode("85", "STA", 2, "$", ""));
            m_OpCodes.Add(new OpCode("86", "STX", 2, "$", ""));
            m_OpCodes.Add(new OpCode("87", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("88", "DEY", 1, "", ""));
            m_OpCodes.Add(new OpCode("89", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("8A", "TXA", 1, "", ""));
            m_OpCodes.Add(new OpCode("8B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("8C", "STY", 3, "$", ""));
            m_OpCodes.Add(new OpCode("8D", "STA", 3, "$", ""));
            m_OpCodes.Add(new OpCode("8E", "STX", 3, "$", ""));
            m_OpCodes.Add(new OpCode("8F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("90", "BCC", 2, "$", ""));
            m_OpCodes.Add(new OpCode("91", "STA", 2, "($", "),Y"));
            m_OpCodes.Add(new OpCode("92", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("93", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("94", "STY", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("95", "STA", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("96", "STX", 2, "$", ",Y"));
            m_OpCodes.Add(new OpCode("97", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("98", "TYA", 1, "", ""));
            m_OpCodes.Add(new OpCode("99", "STA", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("9A", "TXS", 1, "", ""));
            m_OpCodes.Add(new OpCode("9B", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("9C", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("9D", "STA", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("9E", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("9F", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("A0", "LDY", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("A1", "LDA", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("A2", "LDX", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("A3", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("A4", "LDY", 2, "$", ""));
            m_OpCodes.Add(new OpCode("A5", "LDA", 2, "$", ""));
            m_OpCodes.Add(new OpCode("A6", "LDX", 2, "$", ""));
            m_OpCodes.Add(new OpCode("A7", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("A8", "TAY", 1, "", ""));
            m_OpCodes.Add(new OpCode("A9", "LDA", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("AA", "TAX", 1, "", ""));
            m_OpCodes.Add(new OpCode("AB", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("AC", "LDY", 3, "$", ""));
            m_OpCodes.Add(new OpCode("AD", "LDA", 3, "$", ""));
            m_OpCodes.Add(new OpCode("AE", "LDX", 3, "$", ""));
            m_OpCodes.Add(new OpCode("AF", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("B0", "BCS", 2, "$", ""));
            m_OpCodes.Add(new OpCode("B1", "LDA", 2, "($", "),Y"));
            m_OpCodes.Add(new OpCode("B2", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("B3", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("B4", "LDY", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("B5", "LDA", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("B6", "LDX", 2, "$", ",Y"));
            m_OpCodes.Add(new OpCode("B7", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("B8", "CLV", 1, "", ""));
            m_OpCodes.Add(new OpCode("B9", "LDA", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("BA", "TSX", 1, "", ""));
            m_OpCodes.Add(new OpCode("BB", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("BC", "LDY", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("BD", "LDA", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("BE", "LDX", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("BF", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("C0", "CPY", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("C1", "CMP", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("C2", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("C3", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("C4", "CPY", 2, "$", ""));
            m_OpCodes.Add(new OpCode("C5", "CMP", 2, "$", ""));
            m_OpCodes.Add(new OpCode("C6", "DEC", 2, "$", ""));
            m_OpCodes.Add(new OpCode("C7", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("C8", "INY", 1, "", ""));
            m_OpCodes.Add(new OpCode("C9", "CMP", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("CA", "DEX", 1, "", ""));
            m_OpCodes.Add(new OpCode("CB", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("CC", "CPY", 3, "$", ""));
            m_OpCodes.Add(new OpCode("CD", "CMP", 3, "$", ""));
            m_OpCodes.Add(new OpCode("CE", "DEC", 3, "$", ""));
            m_OpCodes.Add(new OpCode("CF", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("D0", "BNE", 2, "$", ""));
            m_OpCodes.Add(new OpCode("D1", "CMP", 1, "($", "),Y"));
            m_OpCodes.Add(new OpCode("D2", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("D3", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("D4", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("D5", "CMP", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("D6", "DEC", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("D7", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("D8", "CLD", 1, "", ""));
            m_OpCodes.Add(new OpCode("D9", "CMP", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("DA", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("DB", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("DC", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("DD", "CMP", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("DE", "DEC", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("DF", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("E0", "CPX", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("E1", "SBC", 2, "($", ",X)"));
            m_OpCodes.Add(new OpCode("E2", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("E3", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("E4", "CPX", 2, "$", ""));
            m_OpCodes.Add(new OpCode("E5", "SBC", 2, "$", ""));
            m_OpCodes.Add(new OpCode("E6", "INC", 2, "$", ""));
            m_OpCodes.Add(new OpCode("E7", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("E8", "INX", 1, "", ""));
            m_OpCodes.Add(new OpCode("E9", "SBC", 2, "#$", ""));
            m_OpCodes.Add(new OpCode("EA", "NOP", 1, "", ""));
            m_OpCodes.Add(new OpCode("EB", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("EC", "CPX", 3, "$", ""));
            m_OpCodes.Add(new OpCode("ED", "SBC", 3, "$", ""));
            m_OpCodes.Add(new OpCode("EE", "INC", 3, "$", ""));
            m_OpCodes.Add(new OpCode("EF", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("F0", "BEQ", 2, "$", ""));
            m_OpCodes.Add(new OpCode("F1", "SBC", 2, "($", "),Y"));
            m_OpCodes.Add(new OpCode("F2", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("F3", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("F4", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("F5", "SBC", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("F6", "INC", 2, "$", ",X"));
            m_OpCodes.Add(new OpCode("F7", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("F8", "SED", 1, "", ""));
            m_OpCodes.Add(new OpCode("F9", "SBC", 3, "$", ",Y"));
            m_OpCodes.Add(new OpCode("FA", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("FB", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("FC", "???", 1, "", ""));
            m_OpCodes.Add(new OpCode("FD", "SBC", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("FE", "INC", 3, "$", ",X"));
            m_OpCodes.Add(new OpCode("FF", "???", 1, "", ""));
        }
    }
}