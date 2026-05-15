using System.Windows.Forms;

namespace C64BinaryToAssemblyConverter
{
    public partial class ConfigureSettings : Form
    {
        public ConfigureSettings(SettingsCache settingsCache)
        {
            InitializeComponent();
            textBox1.Text = settingsCache.Branch;
            textBox2.Text = settingsCache.Label;
            textBox3.Text = settingsCache.NumberOfBytesPerLine.ToString();
        }

        private void ButtonClick(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
