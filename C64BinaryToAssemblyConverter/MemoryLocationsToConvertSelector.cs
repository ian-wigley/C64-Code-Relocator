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
        private char[] startAddressValue;
        private char[] endAddressValue;
        private int startAddress;
        private int endAddress;


        public MemoryLocationsToConvertSelector(char[] startAddressValues, char[] endAddressValues)
        {
            InitializeComponent();

            startAddressValue = startAddressValues;
            endAddressValue = endAddressValues;
            
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

            startAddress = int.Parse(GetSelectedMemStartLocation, System.Globalization.NumberStyles.HexNumber);

            comboBox5.Text = endAddressValues[0].ToString();
            comboBox6.Text = endAddressValues[1].ToString();
            comboBox7.Text = endAddressValues[2].ToString();
            comboBox8.Text = endAddressValues[3].ToString();

            endAddress = int.Parse(GetSelectedMemEndLocation, System.Globalization.NumberStyles.HexNumber);
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
            InvalidStartAddress(one, comboBox1, startAddressValue[0].ToString());
        }

        private void SelectedStartIndexChangedTwo(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox2.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox2, startAddressValue[1].ToString());
        }

        private void SelectedStartIndexChangedThree(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox3.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox3, startAddressValue[2].ToString());
        }

        private void SelectedStartIndexChangedFour(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox4.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox4, startAddressValue[3].ToString());
        }

        private void InvalidStartAddress(int value, ComboBox comboBox, string startAddresses)
        {
            if (int.Parse(GetSelectedMemStartLocation, System.Globalization.NumberStyles.HexNumber) < startAddress)
            //if (value < int.Parse(startAddresses, System.Globalization.NumberStyles.HexNumber))
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
            InvalidEndAddress(one, comboBox5, endAddressValue[0].ToString());
        }

        private void SelectedEndIndexChangedTwo(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox6.Text, System.Globalization.NumberStyles.HexNumber, null, out var two);
            InvalidEndAddress(two, comboBox6, endAddressValue[1].ToString());
        }

        private void SelectedEndIndexChangedThree(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox7.Text, System.Globalization.NumberStyles.HexNumber, null, out var three);
            InvalidEndAddress(three, comboBox7, endAddressValue[2].ToString());
        }

        private void SelectedEndIndexChangedFour(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox8.Text, System.Globalization.NumberStyles.HexNumber, null, out var four);
            InvalidEndAddress(four, comboBox8, endAddressValue[3].ToString());
        }

        private void InvalidEndAddress(int value, ComboBox comboBox, string endAddresses)
        {
            if (int.Parse(GetSelectedMemEndLocation, System.Globalization.NumberStyles.HexNumber) > endAddress)
            //if (value > int.Parse(endAddresses, System.Globalization.NumberStyles.HexNumber))
            {
                comboBox.Text = endAddresses;
            }
        }


    }
}