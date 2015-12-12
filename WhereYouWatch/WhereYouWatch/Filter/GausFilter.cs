using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereYouWatch.Filter
{
    class GausFilter : IFilter
    {
        private int SIZE = 3; 

        public Bitmap Filter(Bitmap originalBitmap)
        {
            return FilterService.LineAlgoritm(originalBitmap, FilterService.GausExpression(SIZE), 
                FilterService.SumGausArray(SIZE, FilterService.GausExpression(SIZE)));
        }
    }
}
