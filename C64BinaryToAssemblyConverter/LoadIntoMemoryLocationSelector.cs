using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;

namespace C64BinaryToAssemblyConverter
{
    public partial class LoadIntoMemoryLocationSelector : Form
    {
        public string GetMemStartLocation => StartAddressSelector.Text;

        public LoadIntoMemoryLocationSelector()
        {
            InitializeComponent();

            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            StartAddressSelector.DisplayMember = "Text";
            StartAddressSelector.ValueMember = "Value";

            var items = new[] {
                new { Text = "0400", Value = 1024 },
                new { Text = "0801", Value = 2049 },
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

            StartAddressSelector.DataSource = items;
            StartAddressSelector.SelectedIndex = 1;
        }

        /// <summary>
        ///
        /// </summary>
        private void Button1_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Validate the users Key Input
        /// </summary>
        private void ValidateKeyInput(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) { return; }
            char c = char.ToUpper(e.KeyChar);
            if (!Uri.IsHexDigit(c))
            {
                e.Handled = true;
                return;
            }
            e.KeyChar = c;
        }

        /// <summary>
        /// Validate Input when the user clicks OK
        /// </summary>
        private void ValidateInput(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Regex.IsMatch(StartAddressSelector.Text, @"\A[0-9A-F]{1,4}\z"))
            {
                MessageBox.Show(
                    "Enter a hexadecimal value (1–4 characters).",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
            }
        }
    }
}