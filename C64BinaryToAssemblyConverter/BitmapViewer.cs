using System.Drawing;
using System.Drawing.Imaging;

namespace C64BinaryToAssemblyConverter
{
    public class BitmapViewer
    {
        private readonly Color[] c64Palette =
        {
            Color.Black,
            Color.White,
            Color.FromArgb(136, 0, 0),
            Color.FromArgb(170, 255, 238),
            Color.FromArgb(204, 68, 204),
            Color.FromArgb(0, 204, 85),
            Color.FromArgb(0, 0, 170),
            Color.FromArgb(238, 238, 119),
            Color.FromArgb(221, 136, 85),
            Color.FromArgb(102, 68, 0),
            Color.FromArgb(255, 119, 119),
            Color.FromArgb(51, 51, 51),
            Color.FromArgb(119, 119, 119),
            Color.FromArgb(170, 255, 102),
            Color.FromArgb(0, 136, 255),
            Color.FromArgb(187, 187, 187)
        };

        public Color[] C64Colours { get { return c64Palette; } }

        public Bitmap ConvertMulticolorToBitmap(byte[] bitmapData, byte[] screenData, byte[] colorData,
            byte backgroundColorCode)
        {
            // Output image is 320x200 because multicolor pixels are double-wide
            Bitmap output = new Bitmap(320, 200, PixelFormat.Format32bppArgb);
            for (int charY = 0; charY < 25; charY++)
            {
                for (int charX = 0; charX < 40; charX++)
                {
                    int cellIndex = charY * 40 + charX;

                    // Extract the 4 possible colors for this specific 8x8 block
                    Color col00 = c64Palette[backgroundColorCode & 0x0F];
                    Color col01 = c64Palette[(screenData[cellIndex] >> 4) & 0x0F];
                    Color col10 = c64Palette[screenData[cellIndex] & 0x0F];
                    Color col11 = c64Palette[colorData[cellIndex] & 0x0F];

                    // Process the 8 rows of the character block
                    for (int row = 0; row < 8; row++)
                    {
                        int bitmapIndex = (cellIndex * 8) + row;
                        byte pixelByte = bitmapData[bitmapIndex];
                        int pixelY = (charY * 8) + row;

                        // Extract 4 pairs of bits from the byte (160 horizontal pixels total)
                        for (int bitPair = 0; bitPair < 4; bitPair++)
                        {
                            int shift = 6 - (bitPair * 2);
                            int colorBits = (pixelByte >> shift) & 0x03;

                            Color pixelColor = colorBits switch
                            {
                                0 => col00,
                                1 => col01,
                                2 => col10,
                                3 => col11,
                                _ => col00
                            };

                            // Map 1 multicolor pixel to 2 horizontal screen pixels
                            int pixelX = (charX * 8) + (bitPair * 2);
                            output.SetPixel(pixelX, pixelY, pixelColor);
                            output.SetPixel(pixelX + 1, pixelY, pixelColor);
                        }
                    }
                }
            }
            return output;
        }
    }
}