namespace C64BinaryToAssemblyConverter
{
    public class SettingsCache
    {
        public SettingsCache(int numberOfBytesPerLine)
        {
            NumberOfBytesPerLine = numberOfBytesPerLine;
        }
        public int NumberOfBytesPerLine { get; private set; }
    }
}
