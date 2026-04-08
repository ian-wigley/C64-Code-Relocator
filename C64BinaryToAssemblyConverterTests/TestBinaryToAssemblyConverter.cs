using C64BinaryToAssemblyConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverterTests
{
    public class TestAssemblyConverter : C64BinaryToAssemblyConverter.C64BinaryToAssemblyConverter
    {
        public TextBox DisAssemblyView { get; set; }
        public Parser _parser;

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

        public TestAssemblyConverter()
        {
            DisAssemblyView = base.DisAssemblyView;
            _parser = base._parser;
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
            PopulateOpCodeList.Init();
            byte[] bytes = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var list = new List<string>();
            var textBox = new TextBox();

            var binaryToAssemblyConverter = new TestAssemblyConverter();
            binaryToAssemblyConverter._parser.ParseFileContent(bytes, textBox, 0, ref list);

            //var text = "0814  00          BRK\r\n0815  00          BRK\r\n0816  00          BRK\r\n0817  00          BRK\r\n0818  00          BRK\r\n0819  00          BRK\r\n081A  00          BRK\r\n081B  00          BRK\r\n081C  00          BRK\r\n081D  00          BRK\r\n081E  00          BRK\r\n081F  00          BRK";

            var start = 0x0814;
            var text = "";
            foreach (byte b in bytes) {
                text += start.ToString("X4") + "  " + b.ToString("X2") + "          BRK\r\n";
                start += 1;
            }


            binaryToAssemblyConverter.SetSelected(text);
            binaryToAssemblyConverter.ConvertToDataBytesClick(null, null);
            var result = binaryToAssemblyConverter.GetSelected();

            string target = "$00";
            int count = Enumerable.Range(0, result.Length - target.Length + 1)
                .Count(i => result.Substring(i, target.Length) == target);

            Assert.IsNotNull(count == bytes.Length);
        }

    }
}
