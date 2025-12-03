using Microsoft.VisualStudio.TestTools.UnitTesting;
using C64BinaryToAssemblyConverter;

namespace C64CodeRelocatorTests
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
    }
}
