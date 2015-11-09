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
        public static Bitmap LineAlgoritm (Bitmap originalBitmap, double[,] w, double k)
        {
            Bitmap dst = new Bitmap(originalBitmap);
            Color color;
            int xLength=w.GetLength(0)/2;
            int yLength = w.GetLength(1)/2;
            for (int i = xLength; i < originalBitmap.Width - xLength; i++)
            {
                for (int j = yLength; j < originalBitmap.Height - yLength; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -xLength; ii < xLength+1; ii++)
                    {
                        for (int jj = -yLength; jj < yLength + 1; jj++)
                        {
                            color = originalBitmap.GetPixel(i + ii, j + jj);
                            red += (int)(color.R * w[ii + xLength, jj + yLength]);
                            green += (int)(color.G * w[ii + xLength, jj + yLength]);
                            blue += (int)(color.B * w[ii + xLength, jj + yLength]);
                        }
                    }
                    color = originalBitmap.GetPixel(i, j);
                    red = (int)(red/k);
                    green = (int)(green / k); ;
                    blue = (int)(blue / k); ;
                    color = originalBitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.A, SetColor(red), SetColor(green), SetColor(blue));
                    dst.SetPixel(i, j, color);
                }
            }
            return dst;
        }

        public static Bitmap LineAlgoritm (Bitmap originalBitmap, double[,] w, double k, double a)
        {
            Bitmap dst = new Bitmap(originalBitmap);
            Color color;
            int xLength = w.GetLength(0) / 2;
            int yLength = w.GetLength(1) / 2;
            for (int i = xLength; i < originalBitmap.Width - xLength; i++)
            {
                for (int j = yLength; j < originalBitmap.Height - yLength; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -xLength; ii < xLength + 1; ii++)
                    {
                        for (int jj = -yLength; jj < yLength + 1; jj++)
                        {
                            color = originalBitmap.GetPixel(i + ii, j + jj);
                            red += (int)(color.R * w[ii + xLength, jj + yLength]);
                            green += (int)(color.G * w[ii + xLength, jj + yLength]);
                            blue += (int)(color.B * w[ii + xLength, jj + yLength]);
                        }
                    }
                    color = originalBitmap.GetPixel(i, j);
                    red = (int)((1+a) * color.R-a*red / k);
                    green = (int)((1 + a) * color.G - a * green / k);
                    blue = (int)((1 + a) * color.B - a * blue / k);
                    color = originalBitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.A, SetColor(red), SetColor(green), SetColor(blue));
                    dst.SetPixel(i, j, color);
                }
            }
            return dst;
        }

        public static Bitmap AddBright (Bitmap originalBitmap, int offset)
        {
            for (int i = 1; i < originalBitmap.Width - 1; i++)
            {
                for (int j = 1; j < originalBitmap.Height - 1; j++)
                {
                    var color = originalBitmap.GetPixel(i, j);
                    
                    color = Color.FromArgb(color.A, SetColorWithOffset(color.R, offset),
                                            SetColorWithOffset(color.G, offset),
                                            SetColorWithOffset(color.B, offset));
                    originalBitmap.SetPixel(i, j, color);
                }
            }
            return originalBitmap;
        }

        public static Bitmap AddContrast (Bitmap originalBitmap, double multiplier)
        {
            for (int i = 1; i < originalBitmap.Width - 1; i++)
            {
                for (int j = 1; j < originalBitmap.Height - 1; j++)
                {
                    var color = originalBitmap.GetPixel(i, j);

                    color = Color.FromArgb(color.A, SetColor(color.R, multiplier),
                                            SetColor(color.G, multiplier),
                                            SetColor(color.B, multiplier));
                    originalBitmap.SetPixel(i, j, color);
                }
            }
            return originalBitmap;
        }

        public static int SetColor (int color)
        {
            return SetColorWithOffset(color, 0);
        }

        private static int SetColor (int color, double multiplier)
        {
            var result = (int)(color * multiplier);
            return CorrectedSettingColor(result);
        }

        private static int SetColorWithOffset (int color, int offset)
        {
            var result = color + offset;
            return CorrectedSettingColor(result);
        }

        private static int CorrectedSettingColor (int color)
        {
            if (color < 0)
            {
                return 0;
            }
            else if (color > 255)
            {
                return 255;
            }
            else
            {
                return color;
            }
        }

        public static double[ , ] GausExpression (int size)
        {
            if (size % 2 == 0)
            {
                size++;
            }
            var gausList = new List<double>();
            int u = 0;
            for (int i = 0; i <= size / 2 + 1; i++)
            {
                u += i;
            }
            u -= 1;
            for (int i = 0; i <= u; i++)
            {
                gausList.Add(1 / (Math.Sqrt(Math.PI * 2 * u)) * Math.Exp(-((double)(i * i)) / (2 * u)));
            }
            var w = new double[size, size];
            int k = 1;
            int c = size / 2;
            w[c, c] = gausList.ElementAt(0);
            for (int i = 1; i <= c; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    w[c + i, c + j] = gausList.ElementAt(k);
                    w[c + i, c - j] = gausList.ElementAt(k);
                    w[c - i, c + j] = gausList.ElementAt(k);
                    w[c - i, c - j] = gausList.ElementAt(k);
                    w[c + j, c + i] = gausList.ElementAt(k);
                    w[c - j, c + i] = gausList.ElementAt(k);
                    w[c + j, c - i] = gausList.ElementAt(k);
                    w[c - j, c - i] = gausList.ElementAt(k);
                    k++;
                }
            }

            return w;
        }

        public static double SumGausArray (int size, double [,] gausArray)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sum += gausArray[i, j];
                }
            }
            return sum;
        }
    }
}
