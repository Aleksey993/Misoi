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
        private static int[] ImageHistogram(Bitmap input)
        {

            int[] histogram = new int[256];

            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
            }

            for (int i = 0; i < input.Width; i++)
            {
                for (int j = 0; j < input.Height; j++)
                {
                    int red = input.GetPixel(i, j).R;
                    histogram[red]++;
                }
            }
            return histogram;
        }


        // вычисление порога для бинаризиации методом Отсу/Оцу
        private static int OtsuTreshold(Bitmap original)
        {

            int[] histogram = ImageHistogram(original);
            int total = original.Height * original.Width;

            float sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += i * histogram[i];
            }

            float sumB = 0;
            int wB = 0, wF = 0, threshold = 0;
            float varMax = 0, mB = 0, mF = 0, varBetween = 0;

            for (int i = 0; i < 256; i++)
            {
                wB += histogram[i];
                if (wB == 0) continue;
                wF = total - wB;

                if (wF == 0) break;

                sumB += (float)(i * histogram[i]);
                mB = sumB / wB;
                mF = (sum - sumB) / wF;

                varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = i;
                }
            }

            return threshold;
        }

        // бинаризиация методом Отсу/Оцу
        public static Bitmap OtsuBinarize(Bitmap original)
        {
            int red = 0,  newPixel = 0;
            int threshold = OtsuTreshold(original);
            Color newPixelColor;

            Bitmap binarized = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    red = original.GetPixel(i, j).R;
                    int alpha = original.GetPixel(i, j).A;
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
