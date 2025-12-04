using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64BinaryToAssemblyConverterTests
{
    [TestClass]
    public class TestAssemblyCreator
    {
        [TestMethod]
        public void TestAssemblyCreatorObject()
        {
            AssemblyCreator assemblyCreator = new AssemblyCreator();
            Assert.IsNotNull(assemblyCreator);
        }
        
        public void TestLoadBinaryData()
        {
            AssemblyCreator assemblyCreator = new AssemblyCreator();
        }
    }
}
