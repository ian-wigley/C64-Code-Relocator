using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace C64BinaryToAssemblyConverterTests
{
    [TestClass]
    public class TestBinaryToAssemblyConverter
    {
        [TestMethod]
        public void InstantiateC64BinaryToAssemblyConverter()
        {
            var binaryToAssemblyConverter = new C64BinaryToAssemblyConverter.C64BinaryToAssemblyConverter();
            Assert.IsNotNull(binaryToAssemblyConverter);
        }

        [TestMethod]
        public void TestConvertToDataBytesClick()
        {
            var binaryToAssemblyConverter = new C64BinaryToAssemblyConverter.C64BinaryToAssemblyConverter();
            binaryToAssemblyConverter.ConvertToDataBytesClick(null, null);
            Assert.IsNotNull(binaryToAssemblyConverter);
        }

    }
}
