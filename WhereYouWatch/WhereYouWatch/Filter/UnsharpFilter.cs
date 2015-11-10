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
            double sum = Math.Abs(FilterService.SumGausArray(SIZE, FilterService.GausExpression(SIZE)));
            return FilterService.LineAlgoritm(originalBitmap, FilterService.GausExpression(SIZE), sum, 1);
        }
    }
}
