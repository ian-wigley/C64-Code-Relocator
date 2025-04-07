using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class MemoryLocation : Form
    {
        public string GetMemStartLocation { get { return comboBox1.SelectedValue.ToString(); } }

        public MemoryLocation()
        {
            InitializeComponent();

            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] {
                new { Text = "$0400", Value = 1024 },
                new { Text = "$0800", Value = 2048 },
                new { Text = "$0900", Value = 2304 },
                new { Text = "$0a00", Value = 2566 },
                new { Text = "$0c00", Value = 3072 },
                new { Text = "$0d00", Value = 3328 },
                new { Text = "$0e00", Value = 3584 },
                new { Text = "$0f00", Value = 3840 },
                new { Text = "$1000", Value = 4096 },
                new { Text = "$2000", Value = 8192 },
                new { Text = "$3000", Value = 12288 },
                new { Text = "$4000", Value = 16384 },
                new { Text = "$5000", Value = 20480 },
                new { Text = "$6000", Value = 24576 },
                new { Text = "$7000", Value = 28672 },
                new { Text = "$8000", Value = 32768 },
                new { Text = "$9000", Value = 36864 },
                new { Text = "$a000", Value = 40960 },
                new { Text = "$b000", Value = 45056 },
                new { Text = "$c000", Value = 49152 },
                new { Text = "$d000", Value = 53248 },
                new { Text = "$e000", Value = 57344 },
                new { Text = "$f000", Value = 61440 }
            };

            comboBox1.DataSource = items;
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
