using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class MedialFilter : IFilter
    {
        private const int SIZE = 5;

        public Bitmap Filter(Bitmap originalBitmap)
        {
            Bitmap resultBitmap = new Bitmap(originalBitmap);
            Color color;
            for (int i = SIZE; i < originalBitmap.Width - SIZE; i++)
            {
                for (int j = SIZE; j < originalBitmap.Height - SIZE; j++)
                {
                    List<int> redList = new List<int>();
                    List<int> greenList = new List<int>();
                    List<int> blueList = new List<int>();
                    int red = 0, green = 0, blue = 0;
                    for (int ii = -SIZE; ii < SIZE + 1; ii++)
                    {
                        for (int jj = -SIZE; jj < SIZE + 1; jj++)
                        {
                            color = originalBitmap.GetPixel(i + ii, j + jj);
                            redList.Add(color.R);
                            greenList.Add(color.G);
                            blueList.Add(color.B);
                        }
                    }
                    redList.Sort();
                    greenList.Sort();
                    blueList.Sort();
                    red = redList.ElementAt(SIZE);
                    green = greenList.ElementAt(SIZE);
                    blue = blueList.ElementAt(SIZE);
                    color = originalBitmap.GetPixel(i, j);
                    color = Color.FromArgb(color.A, FilterService.SetColor(red), FilterService.SetColor(green), FilterService.SetColor(blue));
                    resultBitmap.SetPixel(i, j, color);
                }
            }
            return resultBitmap;
        }
    }
}
