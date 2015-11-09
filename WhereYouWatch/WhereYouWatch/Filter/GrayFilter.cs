using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class GrayFilter : IFilter
    {
        public Bitmap Filter(Bitmap originalBitmap)
        {
            int alpha = 0, red = 0, green = 0, blue = 0, pixelLum = 0;
            Color newPixelColor;

            Bitmap lum = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int i = 0; i < originalBitmap.Width; i++)
            {
                for (int j = 0; j < originalBitmap.Height; j++)
                {
                    // Get pixels by R, G, B
                    alpha = originalBitmap.GetPixel(i, j).A;
                    red = originalBitmap.GetPixel(i, j).R;
                    green = originalBitmap.GetPixel(i, j).G;
                    blue = originalBitmap.GetPixel(i, j).B;
                    pixelLum = (int)(0.21 * red + 0.71 * green + 0.07 * blue);
                    // Return back to original format
                    newPixelColor = Color.FromArgb(alpha, pixelLum, pixelLum, pixelLum);
                    // Write pixels into image
                    lum.SetPixel(i, j, newPixelColor);
                }
            }
            return lum;
        }
    }
}
