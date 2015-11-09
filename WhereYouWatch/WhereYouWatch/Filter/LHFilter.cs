using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class LHFilter : IFilter
    {
        public Bitmap Filter(Bitmap originalBitmap)
        {
            var w = new double[3, 3];
            w[0, 2] = 1;
            w[0, 1] = 2;
            w[0, 0] = 1;
            w[1, 2] = 2;
            w[1, 1] = 4;
            w[1, 0] = 2;
            w[2, 2] = 1;
            w[2, 1] = 2;
            w[2, 0] = 1;
            return FilterService.LineAlgoritm(originalBitmap, w, 16);
        }
    }
}
