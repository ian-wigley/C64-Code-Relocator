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
        /// Selected End address Index Change event handler
        /// </summary>
        private void SelectedIndexChangedOne(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox5.Text, System.Globalization.NumberStyles.HexNumber, null, out var one);
            InvalidEndAddressOne(one, comboBox5, endAddress[0].ToString());
        }

        private void SelectedIndexChangedTwo(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox6.Text, System.Globalization.NumberStyles.HexNumber, null, out var two);
            InvalidEndAddressOne(two, comboBox6, endAddress[1].ToString());
        }

        private void SelectedIndexChangedThree(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox7.Text, System.Globalization.NumberStyles.HexNumber, null, out var three);
            InvalidEndAddressOne(three, comboBox7, endAddress[2].ToString());
        }

        private void SelectedIndexChangedFour(object sender, System.EventArgs e)
        {
            int.TryParse(comboBox8.Text, System.Globalization.NumberStyles.HexNumber, null, out var four);
            InvalidEndAddressOne(four, comboBox8, endAddress[3].ToString());
        }

        private void InvalidEndAddressOne(int one, ComboBox comboBox, string endAddresses)
        {
            if (one > int.Parse(endAddresses, System.Globalization.NumberStyles.HexNumber))
            {
                comboBox.Text = endAddresses;
            }
        }
    }
}