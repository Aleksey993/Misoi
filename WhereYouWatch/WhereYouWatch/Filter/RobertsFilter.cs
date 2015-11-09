﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class RobertsFilter : IFilter
    {
        public Bitmap Filter(Bitmap originalBitmap)
        {
            Bitmap dst = new Bitmap(originalBitmap);
            for (int i = 0; i < originalBitmap.Width - 1; i++)
            {
                for (int j = 0; j < originalBitmap.Height - 1; j++)
                {
                    Color color00 = originalBitmap.GetPixel(i, j);
                    Color color11 = originalBitmap.GetPixel(i + 1, j + 1);
                    Color color10 = originalBitmap.GetPixel(i + 1, j);
                    Color color01 = originalBitmap.GetPixel(i, j + 1);
                    int red = (int)Math.Sqrt(Math.Pow(Math.Abs(color00.R - color11.R), 2) + Math.Pow(Math.Abs(color10.R - color01.R), 2));
                    int green = (int)Math.Sqrt(Math.Pow(Math.Abs(color00.G - color11.G), 2) + Math.Pow(Math.Abs(color10.G - color01.G), 2));
                    int blue = (int)Math.Sqrt(Math.Pow(Math.Abs(color00.B - color11.B), 2) + Math.Pow(Math.Abs(color10.B - color01.B), 2));
                    dst.SetPixel(i, j, Color.FromArgb(color00.A, FilterService.SetColor(red), FilterService.SetColor(green), FilterService.SetColor(blue)));
                }
            }
            return dst;
        }
    }
}
