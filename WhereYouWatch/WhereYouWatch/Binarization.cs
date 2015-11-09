using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WhereYouWatch
{
    public class Binarization
    {

        //метод простой бинаризации, пока не используется, в дальнейшем - с помощью него будет происходить бинар-я с заданным порогом
        public Bitmap SimpleBinarization(Bitmap src) 
        {
            int treshold = 0; // порог  
            System.Drawing.Color color; // цвет - будет разбиваться на RGB
            int average = 0;  // средний цвет = яркость

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    color = src.GetPixel(i, j);
                    average = (int)(color.R + color.G + color.B) / 3;
                    treshold = treshold + average;
                }
            }

            treshold = treshold / (src.Width * src.Height);

            Bitmap dst = new Bitmap(src.Width, src.Height);

            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    color = src.GetPixel(i, j);
                    average = (int)(color.R + color.G + color.B) / 3;
                    dst.SetPixel(i, j, average < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);
                }
            }
            return dst;
        } //SimpleBinarization


        // Перевод изображения в серый тон
        public static Bitmap ConverToGrayBitmap(Bitmap original)
        {
            int alpha = 0, red = 0 , green = 0 , blue = 0 , pixelLum = 0;
            Color newPixelColor;

            Bitmap lum = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    // Get pixels by R, G, B
                    alpha = original.GetPixel(i, j).A;
                    red = original.GetPixel(i, j).R;
                    green = original.GetPixel(i, j).G;
                    blue = original.GetPixel(i, j).B;
                    pixelLum = (int)(0.21 * red + 0.71 * green + 0.07 * blue);
                    // Return back to original format
                    newPixelColor = Color.FromArgb(alpha, pixelLum, pixelLum, pixelLum);
                    // Write pixels into image
                    lum.SetPixel(i, j, newPixelColor);
                }
            }
            return lum;
        }


        // Получение гистограммы изображения, переведённого в серые тона
        private static int[] ImageHistogram(Bitmap originalBitmap)
        {

            int[] histogram = new int[256];

            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
            }

            for (int i = 0; i < originalBitmap.Width; i++)
            {
                for (int j = 0; j < originalBitmap.Height; j++)
                {
                    int red = originalBitmap.GetPixel(i, j).R;
                    histogram[red]++;
                }
            }
            return histogram;
        }


        // вычисление порога для бинаризиации методом Отсу/Оцу
        private static int OtsuTreshold(Bitmap originalBitmap)
        {

            int[] histogram = ImageHistogram(originalBitmap);
            int totalPixels = originalBitmap.Height * originalBitmap.Width;

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

        // бинаризиация методом Отсу/Оцу
        public static Bitmap OtsuBinarize(Bitmap originalBitmap)
        {
            int red = 0,  newPixel = 0;
            int threshold = OtsuTreshold(originalBitmap);
            Color newPixelColor;

            Bitmap binarized = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int i = 0; i < originalBitmap.Width; i++)
            {
                for (int j = 0; j < originalBitmap.Height; j++)
                {
                    red = originalBitmap.GetPixel(i, j).R;
                    int alpha = originalBitmap.GetPixel(i, j).A;
                    if (red > threshold) { newPixel = 255; }
                    else { newPixel = 0; }
                    // Return back to original format
                    newPixelColor = Color.FromArgb(alpha, newPixel, newPixel, newPixel);
                    // Write pixels into image
                    binarized.SetPixel(i, j, newPixelColor);
                }
            }
            return binarized;
        }

    } // Binarization
}
