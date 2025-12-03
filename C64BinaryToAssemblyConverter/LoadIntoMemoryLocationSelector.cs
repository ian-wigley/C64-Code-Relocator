using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public partial class LoadIntoMemoryLocationSelector : Form
    {
        public string GetMemStartLocation => comboBox1.SelectedValue.ToString();

        public LoadIntoMemoryLocationSelector()
        {
            InitializeComponent();

            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] {
                new { Text = "0400", Value = 1024 },
                new { Text = "0800", Value = 2048 },
                new { Text = "0900", Value = 2304 },
                new { Text = "0A00", Value = 2566 },
                new { Text = "0C00", Value = 3072 },
                new { Text = "0D00", Value = 3328 },
                new { Text = "0E00", Value = 3584 },
                new { Text = "0F00", Value = 3840 },
                new { Text = "1000", Value = 4096 },
                new { Text = "2000", Value = 8192 },
                new { Text = "3000", Value = 12288 },
                new { Text = "4000", Value = 16384 },
                new { Text = "5000", Value = 20480 },
                new { Text = "6000", Value = 24576 },
                new { Text = "7000", Value = 28672 },
                new { Text = "8000", Value = 32768 },
                new { Text = "9000", Value = 36864 },
                new { Text = "A000", Value = 40960 },
                new { Text = "B000", Value = 45056 },
                new { Text = "C000", Value = 49152 },
                new { Text = "D000", Value = 53248 },
                new { Text = "E000", Value = 57344 },
                new { Text = "F000", Value = 61440 }
            };

            comboBox1.DataSource = items;
            comboBox1.SelectedIndex = 1;
        }

        /// <summary>
        ///
        /// </summary>
        private void Button1_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
