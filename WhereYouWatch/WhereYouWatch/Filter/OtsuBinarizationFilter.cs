using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class OtsuBinarizationFilter : IFilter
    {
        public Bitmap Filter(Bitmap originalBitmap)
        {
            IFilter grayFilter = new GrayFilter();
            Bitmap grayscale = grayFilter.Filter(originalBitmap);  // переводим изображение в серые тона

            int red = 0, newPixel = 0;
            int threshold = OtsuTreshold(grayscale);
            Color newPixelColor;

            Bitmap resultBitmap = new Bitmap(grayscale.Width, grayscale.Height);

            for (int i = 0; i < grayscale.Width; i++)
            {
                for (int j = 0; j < grayscale.Height; j++)
                {
                    red = grayscale.GetPixel(i, j).R;
                    int alpha = grayscale.GetPixel(i, j).A;
                    if (red > threshold) { newPixel = 255; }
                    else { newPixel = 0; }
                    // Return back to original format
                    newPixelColor = Color.FromArgb(alpha, newPixel, newPixel, newPixel);
                    // Write pixels into image
                    resultBitmap.SetPixel(i, j, newPixelColor);
                }
            }
            return resultBitmap;
        }


        // вычисление порога для бинаризиации методом Отсу/Оцу
        private static int OtsuTreshold(Bitmap originalBitmap)
        {

            var histogram = ImageHistogram(originalBitmap);
            var totalPixels = originalBitmap.Height * originalBitmap.Width;

            float averageLum = 0;
            for (int i = 0; i < 256; i++)
            {
                averageLum += i * histogram[i];
            }

            float averageLumBClass = 0;
            int wA = 0, wB = 0, threshold = 0;
            float maxDispersiya = 0, mA = 0, mB = 0, tempDispersiya = 0;

            for (int i = 0; i < 256; i++)
            {
                wA += histogram[i];
                if (wA == 0) continue;
                wB = totalPixels - wA;

                if (wB == 0) break;

                averageLumBClass += (float)(i * histogram[i]);
                mA = averageLumBClass / wA;
                mB = (averageLum - averageLumBClass) / wB;

                tempDispersiya = (float)wA * (float)wB * (mA - mB) * (mA - mB);

                if (tempDispersiya > maxDispersiya)
                {
                    maxDispersiya = tempDispersiya;
                    threshold = i;
                }
            }

            return threshold;
        }

        // Получение гистограммы изображения, переведённого в серые тона
        private static int[] ImageHistogram(Bitmap originalBitmap)
        {

            var histogram = new int[256];

            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
            }

            for (int i = 0; i < originalBitmap.Width; i++)
            {
                for (int j = 0; j < originalBitmap.Height; j++)
                {
                    var red = originalBitmap.GetPixel(i, j).R;
                    histogram[red]++;
                }
            }
            return histogram;
        }
    }
}
