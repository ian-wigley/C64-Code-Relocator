using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class MemoryLocation : Form
    {
        public string GetMemStartLocation { get { return comboBox1.SelectedValue.ToString(); } }

        public MemoryLocation()
        {
            InitializeComponent();
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";

            var items = new[] {
                new { Text = "$0400", Value = 1024 },
                new { Text = "$0800", Value = 2048 },
                new { Text = "$0900", Value = 2304 },
                new { Text = "$0a00", Value = 2566 },
                new { Text = "$1000", Value = 4096 },
                new { Text = "$2000", Value = 8192 },
                new { Text = "$8000", Value = 32768 }
            };

            comboBox1.DataSource = items;
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
