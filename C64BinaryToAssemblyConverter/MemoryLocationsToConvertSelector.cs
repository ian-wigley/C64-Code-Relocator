using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public partial class MemoryLocationsToConvertSelector : Form
    {
        private readonly IList<object> memValues = new List<object>();
        private readonly int endAddress;
        private readonly char[] endAddressValue;
        private readonly int startAddress;
        private readonly char[] startAddressValue;
        private readonly Regex regex = new Regex(@"[0-9A-Fa-f]{1}");

        public MemoryLocationsToConvertSelector(char[] startAddressValues, char[] endAddressValues)
        {
            InitializeComponent();

            startAddressValue = startAddressValues;
            endAddressValue = endAddressValues;

            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            for (var i = 0; i < 16; i++) memValues.Add(new { Text = i.ToString("X1"), Value = i });

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

            startAddress = int.Parse(GetSelectedMemStartLocation, NumberStyles.HexNumber);

            comboBox5.Text = endAddressValues[0].ToString();
            comboBox6.Text = endAddressValues[1].ToString();
            comboBox7.Text = endAddressValues[2].ToString();
            comboBox8.Text = endAddressValues[3].ToString();

            endAddress = int.Parse(GetSelectedMemEndLocation, NumberStyles.HexNumber);
        }

        public string GetSelectedMemStartLocation => comboBox1.Text.ToUpper() + comboBox2.Text.ToUpper() + comboBox3.Text.ToUpper() + comboBox4.Text.ToUpper();
        public string GetSelectedMemEndLocation => comboBox5.Text.ToUpper() + comboBox6.Text.ToUpper() + comboBox7.Text.ToUpper() + comboBox8.Text.ToUpper();
        public bool GetConvertIllegalOpCodes => checkBox1.Checked;

        /// <summary>
        /// </summary>
        private void InitialiseComboBoxes(ComboBox comboBox)
        {
            comboBox.BindingContext = new BindingContext();
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = memValues;
        }

        /// <summary>
        ///     Button Click event handler
        /// </summary>
        private void ButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        ///     Selected Start address Index Change event handler
        /// </summary>
        private void SelectedStartIndexChangedOne(object sender, EventArgs e)
        {
            int.TryParse(comboBox1.Text, NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox1, startAddressValue[0].ToString());
        }

        private void SelectedStartIndexChangedTwo(object sender, EventArgs e)
        {
            int.TryParse(comboBox2.Text, NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox2, startAddressValue[1].ToString());
        }

        private void SelectedStartIndexChangedThree(object sender, EventArgs e)
        {
            int.TryParse(comboBox3.Text, NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox3, startAddressValue[2].ToString());
        }

        private void SelectedStartIndexChangedFour(object sender, EventArgs e)
        {
            int.TryParse(comboBox4.Text, NumberStyles.HexNumber, null, out var one);
            InvalidStartAddress(one, comboBox4, startAddressValue[3].ToString());
        }
        
        /// <summary>
        ///     Selected End address Index Change event handler
        /// </summary>
        private void SelectedEndIndexChangedOne(object sender, EventArgs e)
        {
            int.TryParse(comboBox5.Text, NumberStyles.HexNumber, null, out var one);
            InvalidEndAddress(one, comboBox5, endAddressValue[0].ToString());
        }

        private void SelectedEndIndexChangedTwo(object sender, EventArgs e)
        {
            int.TryParse(comboBox6.Text, NumberStyles.HexNumber, null, out var two);
            InvalidEndAddress(two, comboBox6, endAddressValue[1].ToString());
        }

        private void SelectedEndIndexChangedThree(object sender, EventArgs e)
        {
            int.TryParse(comboBox7.Text, NumberStyles.HexNumber, null, out var three);
            InvalidEndAddress(three, comboBox7, endAddressValue[2].ToString());
        }

        private void SelectedEndIndexChangedFour(object sender, EventArgs e)
        {
            int.TryParse(comboBox8.Text, NumberStyles.HexNumber, null, out var four);
            InvalidEndAddress(four, comboBox8, endAddressValue[3].ToString());
        }

        private void InvalidStartAddress(int value, ComboBox comboBox, string startAddresses)
        {
            if (int.Parse(GetSelectedMemStartLocation, NumberStyles.HexNumber) < startAddress || 
                int.Parse(GetSelectedMemStartLocation, NumberStyles.HexNumber) > endAddress)
                comboBox.Text = startAddresses;
        }

        private void InvalidEndAddress(int value, ComboBox comboBox, string endAddresses)
        {
            if (int.Parse(GetSelectedMemEndLocation, NumberStyles.HexNumber) > endAddress ||
                int.Parse(GetSelectedMemEndLocation, NumberStyles.HexNumber) < startAddress)
                comboBox.Text = endAddresses;
        }
        
        private void SelectedStartValueValidatingKeyPress(object sender, KeyPressEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo == null) return;
            if (regex.IsMatch(e.KeyChar.ToString())) {
                //comboBox1.Text = e.KeyChar.ToString().ToUpper();
                combo.Text = e.KeyChar.ToString().ToUpper();
            }
            e.Handled = true;
        }
    }
}