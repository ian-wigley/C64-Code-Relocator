using System.Collections.Generic;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public partial class MemoryLocationsToConvertSelector : Form
    {
        private readonly IList<object> memValues = new List<object>();
        public string GetSelectedMemStartLocation => comboBox1.Text + comboBox2.Text + comboBox3.Text + comboBox4.Text;
        public string GetSelectedMemEndLocation => comboBox5.Text + comboBox6.Text + comboBox7.Text + comboBox8.Text;
        public bool GetConvertIllegalOpCodes => checkBox1.Checked;
        private char[] startAddress;
        private char[] endAddress;
        
        public MemoryLocationsToConvertSelector(char[] startAddressValues, char[] endAddressValues)
        {
            InitializeComponent();

            startAddress = startAddressValues;
            endAddress = endAddressValues;
            
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            for (int i = 0; i < 16; i++)
            {
                memValues.Add(new { Text = i.ToString("X1"), Value = i });
            }

            InitialiseComboBoxes(comboBox1);
            InitialiseComboBoxes(comboBox2);
            InitialiseComboBoxes(comboBox3);
            InitialiseComboBoxes(comboBox4);

            InitialiseComboBoxes(comboBox5);
            InitialiseComboBoxes(comboBox6);
            InitialiseComboBoxes(comboBox7);
            InitialiseComboBoxes(comboBox8);

            comboBox1.Text = startAddressValues[0].ToString();
            comboBox2.Text = startAddressValues[1].ToString();
            comboBox3.Text = startAddressValues[2].ToString();
            comboBox4.Text = startAddressValues[3].ToString();

            comboBox5.Text = endAddressValues[0].ToString();
            comboBox6.Text = endAddressValues[1].ToString();
            comboBox7.Text = endAddressValues[2].ToString();
            comboBox8.Text = endAddressValues[3].ToString();
        }

        /// <summary>
        ///
        /// </summary>
        private void InitialiseComboBoxes(ComboBox comboBox)
        {
            comboBox.BindingContext = new BindingContext();
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = memValues;
        }

        /// <summary>
        /// Button Click event handler
        /// </summary>
        private void ButtonClick(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Selected Start address Index Change event handler
        /// </summary>
        private void SelectedStartIndexChangedOne(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox1.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox1, startAddress[0].ToString());
        }

        private void SelectedStartIndexChangedTwo(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox2.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox2, startAddress[1].ToString());
        }

        private void SelectedStartIndexChangedThree(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox3.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox3, startAddress[2].ToString());
        }

        private void SelectedStartIndexChangedFour(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox4.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox4, startAddress[3].ToString());
        }

        private void InvalidStartAddress(int value, ComboBox comboBox, string startAddresses)
        {
            if (value < int.Parse(startAddresses, System.Globalization.NumberStyles.HexNumber))
            {
                comboBox.Text = startAddresses;
            }
        }

        /// <summary>
        /// Selected End address Index Change event handler
        /// </summary>
        private void SelectedEndIndexChangedOne(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox5.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidEndAddress(one, comboBox5, endAddress[0].ToString());
        }

        private void SelectedEndIndexChangedTwo(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox6.Text, System.Globalization.NumberStyles.HexNumber, null, out var two);
            InvalidEndAddress(two, comboBox6, endAddress[1].ToString());
        }

        private void SelectedEndIndexChangedThree(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox7.Text, System.Globalization.NumberStyles.HexNumber, null, out var three);
            InvalidEndAddress(three, comboBox7, endAddress[2].ToString());
        }

        private void SelectedEndIndexChangedFour(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox8.Text, System.Globalization.NumberStyles.HexNumber, null, out var four);
            InvalidEndAddress(four, comboBox8, endAddress[3].ToString());
        }

        private void InvalidEndAddress(int value, ComboBox comboBox, string endAddresses)
        {
            if (value > int.Parse(endAddresses, System.Globalization.NumberStyles.HexNumber))
            {
                comboBox.Text = endAddresses;
            }
        }


    }
}