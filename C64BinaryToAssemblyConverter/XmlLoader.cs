using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace C64BinaryToAssemblyConverter
{
    public class XmlLoader
    {
        public bool Valid { set; get; }
        public SettingsCache SettingsCache { get; private set; }
        public bool SettingsLoaded { get; private set; }

        /// <summary>
        /// Method Loads and Parses the Settings XML file
        /// </summary>
        public void LoadSettings()
        {
            int numberOfBytesPerLine = 3;

            string settingsXML = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + "/" + "config.xml";
            XmlTextReader reader = new XmlTextReader(settingsXML);

            try
            {
                reader.MoveToContent();
                numberOfBytesPerLine = int.Parse(reader.GetAttribute("numberOfBytesPerLine"));
                SettingsLoaded = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error loading the VASM Config.xml file");
                SettingsLoaded = false;
                numberOfBytesPerLine = 8;
            }
            finally
            {
                reader.Close();
                SettingsCache = new SettingsCache(numberOfBytesPerLine);
            }
        }

        /// <summary>
        /// Get Data Type
        /// </summary>
        private void GetDataType(string data, out string dataSize)
        {
            if (data.Contains("."))
            {
                int index = data.IndexOf(".");
                dataSize = data.Substring(index + 1);
            }
            else { dataSize = "?"; }
        }
    }
}

