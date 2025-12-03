using Microsoft.VisualStudio.TestTools.UnitTesting;
using C64BinaryToAssemblyConverter;

namespace C64CodeRelocatorTests
{
    [TestClass]
    public class TestParser
    {
        [TestMethod]
        public void TestParserObject()
        {
            Parser parser = new Parser();
            Assert.IsNotNull(parser);
        }
    }
}
