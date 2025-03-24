using System.Collections.Generic;
using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class MemorySelector : Form
    {
        private IList<object> memValues = new List<object>();
        public string GetSelectedMemStartLocation { get { return comboBox1.Text + comboBox2.Text + comboBox3.Text + comboBox4.Text; } }
        public string GetSelectedMemEndLocation { get { return comboBox5.Text + comboBox6.Text + comboBox7.Text + comboBox8.Text; } }

        public MemorySelector(char[] startAdress, char[] endAdress)
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

            comboBox1.Text = startAdress[0].ToString();
            comboBox2.Text = startAdress[1].ToString();
            comboBox3.Text = startAdress[2].ToString();
            comboBox4.Text = startAdress[3].ToString();

            comboBox5.Text = endAdress[0].ToString();
            comboBox6.Text = endAdress[1].ToString();
            comboBox7.Text = endAdress[2].ToString();
            comboBox8.Text = endAdress[3].ToString();
        }

        private void InitialiseComboBoxes(ComboBox comboBox)
        {
            comboBox.BindingContext = new BindingContext();
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = memValues;
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}