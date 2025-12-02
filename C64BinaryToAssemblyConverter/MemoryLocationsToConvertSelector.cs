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
        
        public MemoryLocationsToConvertSelector(char[] startAddress, char[] endAddress)
        {
            InitializeComponent();

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

            comboBox1.Text = startAddress[0].ToString();
            comboBox2.Text = startAddress[1].ToString();
            comboBox3.Text = startAddress[2].ToString();
            comboBox4.Text = startAddress[3].ToString();

            comboBox5.Text = endAddress[0].ToString();
            comboBox6.Text = endAddress[1].ToString();
            comboBox7.Text = endAddress[2].ToString();
            comboBox8.Text = endAddress[3].ToString();
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
        ///
        /// </summary>
        private void Button1_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}