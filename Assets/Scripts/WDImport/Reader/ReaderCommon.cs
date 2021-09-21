using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WDViewer.Assets;

namespace WDViewer.Reader
{
    public class ReaderCommon
    {
        public static uint ASSET_PALETTE_COUNT = 256;

        public struct ImageSize
        {
            public ushort width;
            public ushort height;
        }

        public static Texture2D CreateImageRGB565(int width, int height, byte[] imageBytes)
        {
            var rgbaBytes = new byte[width * height * 4];
            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var origIndex = (y * width + x) * 2;
                    var destIndex = (y * width + x) * 4;
                    int combined = imageBytes[origIndex + 1] << 8 | imageBytes[origIndex];

                    var r = (combined >> 11) & 0x1f;
                    var g = (combined >> 5) & 0x3f;
                    var b = combined & 0x1f;

                    rgbaBytes[destIndex + 0] = (byte)((r * 527 + 23) >> 6);
                    rgbaBytes[destIndex + 1] = (byte)((g * 259 + 33) >> 6);
                    rgbaBytes[destIndex + 2] = (byte)((b * 527 + 23) >> 6);
                    rgbaBytes[destIndex + 3] = 255;
                }
            }
            var img = new Texture2D(width, height, TextureFormat.RGBA32, false);
            img.SetPixelData(rgbaBytes, 0);
            img.Apply();
            return img;
        }

        public static Texture2D CreateImageRGBA(int width, int height, byte[] imageBytes)
        {
            var img = new Texture2D(width, height, TextureFormat.RGBA32, false);
            img.SetPixelData(imageBytes, 0);
            img.Apply();
            return img;
        }

        public static Texture2D CreatePaletteImage(int width, int height, byte[] imageBytes, AssetPalette palette, bool transparent)
        {
            var colorBytes = new byte[width * height * 4];
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var paletteIndex = (int)imageBytes[x + y * width];
                    var color = palette.Palette[paletteIndex];
                    var isTransparent = transparent && paletteIndex == 0;
                    colorBytes[(x + y * width) * 4 + 0] = (byte)(isTransparent ? 0 : color.r);
                    colorBytes[(x + y * width) * 4 + 1] = (byte)(color.g);
                    colorBytes[(x + y * width) * 4 + 2] = (byte)(isTransparent ? 0 : color.b);
                    colorBytes[(x + y * width) * 4 + 3] = (byte)(isTransparent ? 0 : 255);

                    var isHalfTransparent = transparent && paletteIndex == palette.Palette.Length - 2;
                    var isQuarterTransparent = transparent && paletteIndex == palette.Palette.Length - 3;
                    if (isHalfTransparent)
                    {
                        colorBytes[(x + y * width) * 4 + 0] = 0;
                        colorBytes[(x + y * width) * 4 + 1] = 0;
                        colorBytes[(x + y * width) * 4 + 2] = 0;
                        colorBytes[(x + y * width) * 4 + 3] = 128;
                    }
                    if (isQuarterTransparent)
                    {
                        colorBytes[(x + y * width) * 4 + 0] = 0;
                        colorBytes[(x + y * width) * 4 + 1] = 0;
                        colorBytes[(x + y * width) * 4 + 2] = 0;
                        colorBytes[(x + y * width) * 4 + 3] = 96;
                    }
                }
            }

            var img = new Texture2D(width, height, TextureFormat.RGBA32, false);
            img.SetPixelData(colorBytes, 0);
            img.Apply();
            return img;
        }
    }
}
