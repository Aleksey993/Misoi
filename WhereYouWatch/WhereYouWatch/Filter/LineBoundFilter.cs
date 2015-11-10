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
            var baseMatrix = new double[3, 3];
            baseMatrix[0, 2] = 0;
            baseMatrix[0, 1] = -1;
            baseMatrix[0, 0] = 0;
            baseMatrix[1, 2] = -1;
            baseMatrix[1, 1] = 5;
            baseMatrix[1, 0] = -1;
            baseMatrix[2, 2] = 0;
            baseMatrix[2, 1] = -1;
            baseMatrix[2, 0] = 0;
            return FilterService.LineAlgoritm(originalBitmap, baseMatrix, SIZE);
        }
    }
}
