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
            double[,] w = new double[3, 3];
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
            double[,] w = new double[3, 3];
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
            double[,] w = new double[3, 3];
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

        public static Bitmap unsharp(Bitmap src, int size)
        {
            if (size % 2 == 0)
            {
                size++;
            }
            List<double> gausList = new List<double>();
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
            double[,] w = new double[size, size];
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
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sum += w[i, j];
                }
            }
            sum = Math.Abs(sum);
            return lineAlgoritm(src, w, sum,1);

        }

        public static Bitmap gaus(Bitmap src, int size)
        {
            if (size % 2 == 0)
            {
                size++;
            }
            List<double> gausList = new List<double>();
            int u = 0;
            for(int i = 0; i <= size/2+1; i++)
            {
                u += i;
            }
            u -= 1;
            for (int i = 0; i <= u; i++)
            {
                gausList.Add(1 / ( Math.Sqrt(Math.PI * 2*u)) * Math.Exp(-((double)(i * i)) / (2 * u)));
            }
            double[,] w = new double[size, size];
            int k = 1;
            int c = size/2;
            w[c,c]= gausList.ElementAt(0);
            for (int i = 1; i <= c; i++)
            {
                for(int j = 0; j <= i; j++)
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
            double sum = 0;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    sum += w[i, j];
                }
            }
            return lineAlgoritm(src, w, sum);

        }

        private static Bitmap lineAlgoritm(Bitmap src, double[,] w)
        {
            return lineAlgoritm(src, w, 1);
        }

        private static Bitmap lineAlgoritm(Bitmap src,double[,] w,double k)
        {
            Bitmap dst = new Bitmap(src);
            Color color;
            int xLength=w.GetLength(0)/2;
            int yLength = w.GetLength(1)/2;
            for (int i = xLength; i < src.Width - xLength; i++)
            {
                for (int j = yLength; j < src.Height - yLength; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -xLength; ii < xLength+1; ii++)
                    {
                        for (int jj = -yLength; jj < yLength + 1; jj++)
                        {
                            color = src.GetPixel(i + ii, j + jj);
                            red += (int)(color.R * w[ii + xLength, jj + yLength]);
                            green += (int)(color.G * w[ii + xLength, jj + yLength]);
                            blue += (int)(color.B * w[ii + xLength, jj + yLength]);
                        }
                    }
                    color = src.GetPixel(i, j);
                    red = (int)(red/k);
                    green = (int)(green / k); ;
                    blue = (int)(blue / k); ;
                    color = src.GetPixel(i, j);
                    color = Color.FromArgb(color.A, setColor(red), setColor(green), setColor(blue));
                    dst.SetPixel(i, j, color);
                }
            }
            return dst;
        }

        private static Bitmap lineAlgoritm(Bitmap src, double[,] w, double k,double a)
        {
            Bitmap dst = new Bitmap(src);
            Color color;
            int xLength = w.GetLength(0) / 2;
            int yLength = w.GetLength(1) / 2;
            for (int i = xLength; i < src.Width - xLength; i++)
            {
                for (int j = yLength; j < src.Height - yLength; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -xLength; ii < xLength + 1; ii++)
                    {
                        for (int jj = -yLength; jj < yLength + 1; jj++)
                        {
                            color = src.GetPixel(i + ii, j + jj);
                            red += (int)(color.R * w[ii + xLength, jj + yLength]);
                            green += (int)(color.G * w[ii + xLength, jj + yLength]);
                            blue += (int)(color.B * w[ii + xLength, jj + yLength]);
                        }
                    }
                    color = src.GetPixel(i, j);
                    red = (int)((1+a) * color.R-a*red / k);
                    green = (int)((1 + a) * color.G - a * green / k);
                    blue = (int)((1 + a) * color.B - a * blue / k);
                    color = src.GetPixel(i, j);
                    color = Color.FromArgb(color.A, setColor(red), setColor(green), setColor(blue));
                    dst.SetPixel(i, j, color);
                }
            }
            return dst;
        }

        public static Bitmap roberts(Bitmap src)
        {
            Bitmap dst = new Bitmap(src);
            for (int i = 0; i < src.Width - 1; i++)
            {
                for (int j = 0; j < src.Height - 1; j++)
                {
                    Color color00 = src.GetPixel(i, j);
                    Color color11 = src.GetPixel(i+1, j+1);
                    Color color10 = src.GetPixel(i+1, j);
                    Color color01 = src.GetPixel(i, j+1);
                    int red=(int)Math.Sqrt(Math.Pow(Math.Abs(color00.R - color11.R), 2) + Math.Pow(Math.Abs(color10.R - color01.R), 2));
                    int green = (int)Math.Sqrt(Math.Pow(Math.Abs(color00.G - color11.G), 2) + Math.Pow(Math.Abs(color10.G - color01.G), 2));
                    int blue = (int)Math.Sqrt(Math.Pow(Math.Abs(color00.B - color11.B), 2) + Math.Pow(Math.Abs(color10.B - color01.B), 2));
                    dst.SetPixel(i, j, Color.FromArgb(color00.A, setColor(red), setColor(green), setColor(blue)));
                }
            }
            return dst;
        }

        public static Bitmap medial(Bitmap src,int size)
        {
            Bitmap dst = new Bitmap(src);
            Color color;
            for (int i = size; i < src.Width - size; i++)
            {
                for (int j = size; j < src.Height - size; j++)
                {
                    List<int> redList = new List<int>();
                    List<int> greenList = new List<int>();
                    List<int> blueList = new List<int>();
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -size; ii < size + 1; ii++)
                    {
                        for (int jj = -size; jj < size + 1; jj++)
                        {
                            color = src.GetPixel(i + ii, j + jj);
                            redList.Add(color.R);
                            greenList.Add(color.G);
                            blueList.Add(color.B);
                        }
                    }
                    redList.Sort();
                    greenList.Sort();
                    blueList.Sort();
                    red = redList.ElementAt(size);
                    green = greenList.ElementAt(size);
                    blue = blueList.ElementAt(size);
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

        public static Bitmap addContrast(Bitmap src, double multiplier)
        {
            for (int i = 1; i < src.Width - 1; i++)
            {
                for (int j = 1; j < src.Height - 1; j++)
                {
                    Color color = src.GetPixel(i, j);

                    color = Color.FromArgb(color.A, setColor(color.R, multiplier),
                        setColor(color.G, multiplier),
                        setColor(color.B, multiplier));
                    src.SetPixel(i, j, color);
                }
            }
            return src;
        }


        private static int setColor(int color)
        {
            return setColor(color, 0);
        }

        private static int setColor(int color,double multiplier)
        {
            int result = (int)(color * multiplier);
            if (result < 0)
            {
                return 0;
            }
            else if (result > 255)
            {
                return 255;
            }
            else
            {
                return result;
            }
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
