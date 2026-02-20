using System.Collections.Generic;
using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverterTests
{
    [TestClass]
    public class TestParser
    {
        private Parser InstantiateParser()
        {
            return new Parser();
        }
        
        [TestMethod]
        public void TestParserObject()
        {
            var parser = InstantiateParser();
            Assert.IsNotNull(parser);
        }
        
        [TestMethod]
        public void TestLoadBinaryData(){
            var parser = InstantiateParser();
            var result = parser.LoadBinaryData("");
            Assert.IsTrue(result.Length.Equals(0));
        }

        [TestMethod]
        public void TestParserReturnsEmptyListIfNoOpcodes()
        {
            var parser = InstantiateParser();
            var bytes = new byte[1024];
            var list = new List<string>();
            var textBox = new TextBox();
            var result = parser.ParseFileContent(bytes, textBox, 0, ref list);
            Assert.IsTrue(result.Length.Equals(0));
        }


        [TestMethod]
        public void TestParserReturnsBRK()
        {
            var expected = "BRK";
            PopulateOpCodeList.Init();
            var parser = InstantiateParser();
            var bytes = new byte[1024];
            var list = new List<string>();
            var textBox = new TextBox();
            var result = parser.ParseFileContent(bytes, textBox, 0, ref list);
            Assert.IsTrue(result.Length.Equals(bytes.Length));
            for (int i = 0; i < result.Length; i++)
            {
                Assert.IsTrue(result[i].Contains(expected));
            }
        }

        [TestMethod]
        public void TestParserBranchIfZeroFlagSet()
        {
            var expected = new[] { "0000  58          CLI", "0001  20 E4 FF    JSR $FFE4", "0004  F0 FB       BEQ $0001" };
            PopulateOpCodeList.Init();
            var parser = InstantiateParser();
            byte[] bytes = { 0x58, 0x20, 0xE4, 0xFF, 0xF0, 0xFB };
            var list = new List<string>();
            var textBox = new TextBox();
            var result = parser.ParseFileContent(bytes, textBox, 0, ref list);
            Assert.IsTrue(result.Length.Equals(expected.Length));
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
    }
}
