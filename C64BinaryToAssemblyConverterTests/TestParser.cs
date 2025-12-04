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
        public void TestParseFileContent()
        {
            var parser = InstantiateParser();
            var bytes = new byte[1024];
            var list = new List<string>();
            var textBox = new TextBox();
            var result = parser.ParseFileContent(bytes, textBox, 0, ref list);
            Assert.IsTrue(result.Length.Equals(0));
        }
    }
}
