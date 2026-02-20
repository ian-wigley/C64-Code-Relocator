using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace C64BinaryToAssemblyConverterTests
{
    [TestClass]
    public class TestAssemblyCreator
    {

        readonly string branchLocation = "0002";
        readonly string[] linesOfCode = new[] {"0000  A0 00       LDY #$00",
                                        "0002  B9 05 D9    LDA $D905,Y",
                                        "0005  99 04 D9    STA $D904,Y",
                                        "0008  B9 DD D8    LDA $D8DD,Y",
                                        "000B  99 DC D8    STA $D8DC,Y",
                                        "000C  C8          INY",
                                        "000E  C0 11       CPY #$11",
                                        "0011  D0 EF       BNE $" };

        private AssemblyCreator InstantiatePopulatedAssemblyCreator()
        {
            linesOfCode[linesOfCode.Length - 1] = "0011  D0 EF       BNE $" + branchLocation;
            return new AssemblyCreator
            {
                Code = linesOfCode
            };
        }

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
        public void TestAssemblyCreatorInitialPassWithData()
        {
            var assemblyCreator = InstantiatePopulatedAssemblyCreator();
            assemblyCreator.InitialPass(0, linesOfCode.Length, false, new Dictionary<string, string[]>());
            var result = assemblyCreator.PassOne;
            Assert.IsTrue(result.Count.Equals(linesOfCode.Length));
            var branchResult = assemblyCreator.BranchLocations;
            Assert.IsTrue(branchResult.Keys.Count.Equals(1));
            Assert.IsTrue(branchResult.ContainsKey(branchLocation));
        }


        [TestMethod]
        public void TestAssemblyCreatorSecondPass()
        {
            var assemblyCreator = new AssemblyCreator();
            assemblyCreator.SecondPass();
            Assert.IsTrue(assemblyCreator.PassOne.Count.Equals(0));
            Assert.IsTrue(assemblyCreator.PassTwo.Count.Equals(0));
        }

        [TestMethod]
        public void TestAssemblyCreatorSecondPassWithData()
        {
            var assemblyCreator = InstantiatePopulatedAssemblyCreator();
            assemblyCreator.InitialPass(0, linesOfCode.Length, false, new Dictionary<string, string[]>());
            assemblyCreator.SecondPass();
            Assert.IsTrue(assemblyCreator.PassOne.Count.Equals(linesOfCode.Length));
            Assert.IsTrue(assemblyCreator.PassTwo.Count.Equals(linesOfCode.Length));
        }

        [TestMethod]
        public void TestAssemblyCreatorFinalPass()
        {
            var assemblyCreator = new AssemblyCreator();
            var result = assemblyCreator.FinalPass(new List<string>(), 0, "0800");
            Assert.IsNotNull(result);
        }

        /*
         TODO - There is a BUG with the Branching logic ...
         --------------------------------------------------
         branch1    LDY #$00             <= Wrong!
         branch1    LDA $D905,Y          <= Correct
                    STA $D904,Y
                    LDA $D8DD,Y
                    STA $D8DC,Y
                    INY
                    CPY #$11
                    BNE branch1
         */

        [TestMethod]
        public void TestAssemblyCreatorFinalPassWithData()
        {
            var assemblyCreator = InstantiatePopulatedAssemblyCreator();
            assemblyCreator.InitialPass(0, linesOfCode.Length, false, new Dictionary<string, string[]>());
            assemblyCreator.SecondPass();
            var result = assemblyCreator.FinalPass(new List<string>(linesOfCode), 0, "0000");
            Assert.IsNotNull(result);
        }
    }
}
