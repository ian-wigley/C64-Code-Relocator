using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64BinaryToAssemblyConverterTests
{
    public class TestAssemblyConverter : C64BinaryToAssemblyConverter.C64BinaryToAssemblyConverter
    {
        public Parser _parser;

        public TestAssemblyConverter()
        {
            DisAssemblyView = base.DisAssemblyView;
            _parser = base._parser;
        }

        public TextBox DisAssemblyView { get; set; }

        public void SetSelected(string value)
        {
            base.DisAssemblyView.Text = value;
            base.DisAssemblyView.SelectionStart = 0;
            base.DisAssemblyView.SelectionLength = base.DisAssemblyView.Text.Length;
        }

        public string GetSelected()
        {
            return base.DisAssemblyView.Text;
        }
    }

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
            byte[] bytes = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var start = 0x0814;
            var text = "";
            var target = "$00";
            var list = new List<string>();
            var textBox = new TextBox();

            foreach (var b in bytes)
            {
                text += start.ToString("X4") + "  " + b.ToString("X2") + "          BRK\r\n";
                start += 1;
            }

            PopulateOpCodeList.Init();
            var binaryToAssemblyConverter = new TestAssemblyConverter();
            binaryToAssemblyConverter._parser.ParseFileContent(bytes, textBox, 0, ref list);
            binaryToAssemblyConverter.SetSelected(text);
            binaryToAssemblyConverter.ConvertToDataBytesClick(null, null);
            var result = binaryToAssemblyConverter.GetSelected();

            var count = Enumerable.Range(0, result.Length - target.Length + 1)
                .Count(i => result.Substring(i, target.Length) == target);

            Assert.IsNotNull(count == bytes.Length);
        }
    }
}