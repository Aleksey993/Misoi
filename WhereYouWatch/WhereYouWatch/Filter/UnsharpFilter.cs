using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class UnsharpFilter : IFilter
    {

        private int SIZE = 7;

        public Bitmap Filter(Bitmap originalBitmap)
        {
            if (SIZE % 2 == 0)
            {
                SIZE++;
            }
            var gausList = new List<double>();
            int u = 0;
            for (int i = 0; i <= SIZE / 2 + 1; i++)
            {
                u += i;
            }
            u -= 1;
            for (int i = 0; i <= u; i++)
            {
                gausList.Add(1 / (Math.Sqrt(Math.PI * 2 * u)) * Math.Exp(-((double)(i * i)) / (2 * u)));
            }
            var w = new double[SIZE, SIZE];
            int k = 1;
            int c = SIZE / 2;
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
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    sum += w[i, j];
                }
            }
            sum = Math.Abs(sum);
            return FilterService.LineAlgoritm(originalBitmap, w, sum, 1);
        }
    }
}
