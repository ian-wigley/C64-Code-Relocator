using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace C64BinaryToAssemblyConverter
{
    public class XmlLoader
    {
        public bool Valid { get; set; }
        public SettingsCache SettingsCache { get; private set; }
        public bool SettingsLoaded { get; private set; }

        /// <summary>
        ///     Method Loads and Parses the Settings XML file
        /// </summary>
        public void LoadSettings()
        {
            var numberOfBytesPerLine = 8;
            var label = "Label";
            var branch = "branch";

            var settingsXML = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) +
                              "/" + "config.xml";
            var reader = new XmlTextReader(settingsXML);

            try
            {
                reader.MoveToContent();
                numberOfBytesPerLine = int.Parse(reader.GetAttribute("numberOfBytesPerLine"));
                label = reader.GetAttribute("labelName");
                branch = reader.GetAttribute("branchName");
                SettingsLoaded = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error loading the Config.xml file");
                SettingsLoaded = false;
                numberOfBytesPerLine = 8;
            }
            finally
            {
                reader.Close();
                SettingsCache = new SettingsCache
                {
                    NumberOfBytesPerLine = numberOfBytesPerLine,
                    Label = label,
                    Branch = branch
                };
            }
        }

        /// <summary>
        ///     Get Data Type
        /// </summary>
        private void GetDataType(string data, out string dataSize)
        {
            if (data.Contains("."))
            {
                var index = data.IndexOf(".");
                dataSize = data.Substring(index + 1);
            }
            else
            {
                dataSize = "?";
            }
        }
    }
}