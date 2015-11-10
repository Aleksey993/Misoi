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
            var baseMatrix = new double[3, 3];
            baseMatrix[0, 2] = 1;
            baseMatrix[0, 1] = 2;
            baseMatrix[0, 0] = 1;
            baseMatrix[1, 2] = 2;
            baseMatrix[1, 1] = 4;
            baseMatrix[1, 0] = 2;
            baseMatrix[2, 2] = 1;
            baseMatrix[2, 1] = 2;
            baseMatrix[2, 0] = 1;
            return FilterService.LineAlgoritm(originalBitmap, baseMatrix, 16);
        }
    }
}
