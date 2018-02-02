using System.Collections.Generic;
using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class MemorySelector : Form
    {
        private IList<object> memValues = new List<object>();
        private char[] m_startAdress;
        private char[] m_endAdress;

        public string GetSelectedMemStartLocation { get { return comboBox1.Text + comboBox2.Text + comboBox3.Text + comboBox4.Text; } }
        public string GetSelectedMemEndLocation { get { return comboBox5.Text + comboBox6.Text + comboBox7.Text + comboBox8.Text; } }

        public MemorySelector(char[] startAdress, char[] endAdress)
        {
            InitializeComponent();

            m_startAdress = startAdress;
            m_endAdress = endAdress;

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
        }

        private void InitialiseComboBoxes(ComboBox comboBox)
        {
            comboBox.BindingContext = new BindingContext();
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            comboBox.DataSource = memValues;
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}