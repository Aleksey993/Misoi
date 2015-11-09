using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class LineBoundFilter : IFilter
    {
        private const int SIZE = 1;

        public Bitmap Filter(Bitmap originalBitmap)
        {
            var w = new double[3, 3];
            w[0, 2] = 0;
            w[0, 1] = -1;
            w[0, 0] = 0;
            w[1, 2] = -1;
            w[1, 1] = 5;
            w[1, 0] = -1;
            w[2, 2] = 0;
            w[2, 1] = -1;
            w[2, 0] = 0;
            return FilterService.LineAlgoritm(originalBitmap, w, SIZE);
        }
    }
}
