using Microsoft.VisualStudio.TestTools.UnitTesting;
using C64CodeRelocator;

namespace C64CodeRelocatorTests
{
    [TestClass]
    public class TestAssemblyCreator
    {
        [TestMethod]
        public void TestSomething()
        {
            AssemblyCreator assemblyCreator = new AssemblyCreator();
            assemblyCreator.InitialPass();
        }
    }
}
