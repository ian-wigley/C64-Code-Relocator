using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64BinaryToAssemblyConverterTests
{
    [TestClass]
    public class TestOpcode
    {
        [TestMethod]
        public void TestOpCodeObject()
        {
            var opCode = new OpCode("code", "name", 1, "prefix", "suffix", false);
            Assert.IsNotNull(opCode);
        }
    }
}
