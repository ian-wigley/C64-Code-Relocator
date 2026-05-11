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
            if (!(sender is ComboBox combo)) return;
            int.TryParse(combo.Tag.ToString(), out int index);
            InvalidStartAddress(combo, startAddressValue[index].ToString());
        }
        
        /// <summary>
        ///     Selected End address Index Change event handler
        /// </summary>
        private void SelectedEndIndexChangedOne(object sender, EventArgs e)
        {
            if (!(sender is ComboBox combo)) return;
            int.TryParse(combo.Tag.ToString(), out int index);
            InvalidEndAddress(combo, endAddressValue[index].ToString());
        }

        private void InvalidStartAddress(ComboBox comboBox, string startAddresses)
        {
            if (int.Parse(GetSelectedMemStartLocation, NumberStyles.HexNumber) < startAddress || 
                int.Parse(GetSelectedMemStartLocation, NumberStyles.HexNumber) > endAddress)
                comboBox.Text = startAddresses;
        }

        private void InvalidEndAddress(ComboBox comboBox, string endAddresses)
        {
            if (int.Parse(GetSelectedMemEndLocation, NumberStyles.HexNumber) > endAddress ||
                int.Parse(GetSelectedMemEndLocation, NumberStyles.HexNumber) < startAddress)
                comboBox.Text = endAddresses;
        }
        
        private void SelectedStartValueValidatingKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is ComboBox combo)) return;
            if (regex.IsMatch(e.KeyChar.ToString())) {
                combo.Text = e.KeyChar.ToString().ToUpper();
            }
            e.Handled = true;
        }
    }
}