using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch
{
    class FilterService
    {
        public static Bitmap LHFilter(Bitmap src)
        {
            int[,] w = new int[3, 3];
            w[0, 2] = 1;
            w[0, 1] = 2;
            w[0, 0] = 1;
            w[1, 2] = 2;
            w[1, 1] = 4;
            w[1, 0] = 2;
            w[2, 2] = 1;
            w[2, 1] = 2;
            w[2, 0] = 1;
            return lineAlgoritm(src,w,16);
        }

        public static Bitmap clarity(Bitmap src)
        {
            int[,] w = new int[3, 3];
            w[0, 2] = -1;
            w[0, 1] = -1;
            w[0, 0] = -1;
            w[1, 2] = -1;
            w[1, 1] = 9;
            w[1, 0] = -1;
            w[2, 2] = -1;
            w[2, 1] = -1;
            w[2, 0] = -1;
            return lineAlgoritm(src, w);
        }

        public static Bitmap lineBounds(Bitmap src)
        {
            int[,] w = new int[3, 3];
            w[0, 2] = 0;
            w[0, 1] = -1;
            w[0, 0] = 0;
            w[1, 2] = -1;
            w[1, 1] = 5;
            w[1, 0] = -1;
            w[2, 2] = 0;
            w[2, 1] = -1;
            w[2, 0] = 0;
            return lineAlgoritm(src, w);
        }

        private static Bitmap lineAlgoritm(Bitmap src, int[,] w)
        {
            return lineAlgoritm(src, w, 1);
        }

        private static Bitmap lineAlgoritm(Bitmap src,int[,] w,int k)
        {
            Bitmap dst = new Bitmap(src);
            Color color;
            for (int i = 1; i < src.Width - 1; i++)
            {
                for (int j = 1; j < src.Height - 1; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -1; ii < 2; ii++)
                    {
                        for (int jj = -1; jj < 2; jj++)
                        {
                            color = src.GetPixel(i + ii, j + jj);
                            red += color.R * w[ii + 1, jj + 1];
                            green += color.G * w[ii + 1, jj + 1];
                            blue += color.B * w[ii + 1, jj + 1];
                        }
                    }
                    red /= k;
                    green /= k;
                    blue /= k;
                    color = src.GetPixel(i, j);
                    color = Color.FromArgb(color.A, setColor(red), setColor(green), setColor(blue));
                    dst.SetPixel(i, j, color);
                }
            }
            return dst;
        }

        public static Bitmap addBright(Bitmap src, int offset)
        {
            for (int i = 1; i < src.Width - 1; i++)
            {
                for (int j = 1; j < src.Height - 1; j++)
                {
                    Color color = src.GetPixel(i, j);
                    
                    color = Color.FromArgb(color.A, setColor(color.R, offset),
                        setColor(color.G, offset),
                        setColor(color.B, offset));
                    src.SetPixel(i, j, color);
                }
            }
            return src;
        }

        private static int setColor(int color)
        {
            return setColor(color, 0);
        }

        private static int setColor(int color,int offset)
        {
            int sum = color + offset;
            if (sum < 0)
            {
                return 0;
            }
            else if (sum > 255)
            {
                return 255;
            }
            else
            {
                return sum;
            }
        }

        
    }
}
