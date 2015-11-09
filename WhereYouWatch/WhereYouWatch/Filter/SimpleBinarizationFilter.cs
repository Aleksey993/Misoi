﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class SimpleBinarizationFilter : IFilter
    {
        //метод простой бинаризации, пока не используется, в дальнейшем - с помощью него будет происходить бинар-я с заданным порогом
        public Bitmap Filter(Bitmap originalBitmap)
        {
            int treshold = 0; // порог  
            System.Drawing.Color color; // цвет - будет разбиваться на RGB
            int average = 0;  // средний цвет = яркость

            for (int i = 0; i < originalBitmap.Width; i++)
            {
                for (int j = 0; j < originalBitmap.Height; j++)
                {
                    color = originalBitmap.GetPixel(i, j);
                    average = (int)(color.R + color.G + color.B) / 3;
                    treshold = treshold + average;
                }
            }

            treshold = treshold / (originalBitmap.Width * originalBitmap.Height);

            Bitmap dst = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int i = 0; i < originalBitmap.Width; i++)
            {
                for (int j = 0; j < originalBitmap.Height; j++)
                {
                    color = originalBitmap.GetPixel(i, j);
                    average = (int)(color.R + color.G + color.B) / 3;
                    dst.SetPixel(i, j, average < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);
                }
            }
            return dst;
        }
    }
}
