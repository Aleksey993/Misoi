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
        public Bitmap LHFilter(Bitmap bmp)
        {
            byte[,,] byteArray = bitmapToByteRgbNaive(bmp);
            byte[,,] resultByteArray =(byte[,,])byteArray.Clone();
            for (int i=1;i< byteArray.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < byteArray.GetLength(0) - 1; j++)
                {
                    //resultByteArray[i,j,0]= (byteArray[i+1, j-1, 0]+ byteArray[i + 1, j - 1, 0]+
                    //    byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+
                    //    byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+
                    //    byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+
                    //    byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]+
                    //    byteArray[i + 1, j - 1, 0]+ byteArray[i + 1, j - 1, 0]) / 16
                }
            }
            return null;
        }

        public byte[,,] bitmapToByteRgbNaive(Bitmap bmp)
        {
            int width = bmp.Width,
                height = bmp.Height;
            byte[,,] res = new byte[3, height, width];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Color color = bmp.GetPixel(x, y);
                    res[0, y, x] = color.R;
                    res[1, y, x] = color.G;
                    res[2, y, x] = color.B;
                }
            }
            return res;
        }
    }
}
