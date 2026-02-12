using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace C64BinaryToAssemblyConverterTests
{
    [TestClass]
    public class TestAssemblyCreator
    {
        [TestMethod]
        public void TestAssemblyCreatorObject()
        {
            var assemblyCreator = new AssemblyCreator();
            Assert.IsNotNull(assemblyCreator);
        }
        
        [TestMethod]
        public void TestAssemblyCreatorInitialPass()
        {
            var assemblyCreator = new AssemblyCreator();
            assemblyCreator.InitialPass(0, 1, false, new Dictionary<string, string[]>());
            Assert.IsNull(assemblyCreator.Code);
            Assert.IsTrue(assemblyCreator.PassOne.Count == 0);
        }

        [TestMethod]
        public void TestAssemblyCreatorSecondPass()
        {
            var assemblyCreator = new AssemblyCreator();
            assemblyCreator.SecondPass();
        }
        
        [TestMethod]
        public void TestAssemblyCreatorFinalPass()
        {
            var assemblyCreator = new AssemblyCreator();
            var result = assemblyCreator.FinalPass(new List<string>(), 0, "0800");
            Assert.IsNotNull(result);
        }
    }
}
