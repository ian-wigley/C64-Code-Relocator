using System.Collections.Generic;
using System.Windows.Forms;

namespace C64CodeRelocator
{
    public partial class MemorySelector : Form
    {
        private IList<object> memValues = new List<object>();

        public string GetSelectedMemStartLocation { get { return comboBox1.Text + comboBox2.Text + comboBox3.Text + comboBox4.Text; } }
        public string GetSelectedMemEndLocation { get { return comboBox5.Text + comboBox6.Text + comboBox7.Text + comboBox8.Text; } }

        public MemorySelector()
        {
            InitializeComponent();

            comboBox1.BindingContext = new BindingContext();
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            comboBox2.BindingContext = new BindingContext();
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            comboBox3.BindingContext = new BindingContext();
            comboBox3.DisplayMember = "Text";
            comboBox3.ValueMember = "Value";
            comboBox4.BindingContext = new BindingContext();
            comboBox4.DisplayMember = "Text";
            comboBox4.ValueMember = "Value";
            comboBox5.BindingContext = new BindingContext();
            comboBox5.DisplayMember = "Text";
            comboBox5.ValueMember = "Value";
            comboBox6.BindingContext = new BindingContext();
            comboBox6.DisplayMember = "Text";
            comboBox6.ValueMember = "Value";
            comboBox7.BindingContext = new BindingContext();
            comboBox7.DisplayMember = "Text";
            comboBox7.ValueMember = "Value";
            comboBox8.BindingContext = new BindingContext();
            comboBox8.DisplayMember = "Text";
            comboBox8.ValueMember = "Value";

            for (int i = 0; i < 16; i++)
            {
                memValues.Add(new { Text = i.ToString("X1"), Value = i });
            }

            comboBox1.DataSource = memValues;
            comboBox2.DataSource = memValues;
            comboBox3.DataSource = memValues;
            comboBox4.DataSource = memValues;
            comboBox5.DataSource = memValues;
            comboBox6.DataSource = memValues;
            comboBox7.DataSource = memValues;
            comboBox8.DataSource = memValues;

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}