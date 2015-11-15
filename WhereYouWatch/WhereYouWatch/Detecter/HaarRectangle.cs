using System;
using System.Globalization;

namespace WhereYouWatch.Detecter
{
    [Serializable]
    public class HaarRectangle : ICloneable
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int ScaledX { get; set; }
        public int ScaledY { get; set; }
        public int ScaledWidth { get; set; }
        public int ScaledHeight { get; set; }
        public float ScaledWeight { get; set; }
        public HaarRectangle(int[] values)
        {
            this.X = values[0];
            this.Y = values[1];
            this.Width = values[2];
            this.Height = values[3];
            this.Weight = values[4];
        }

        public HaarRectangle(int x, int y, int width, int height, float weight)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
        }

        private HaarRectangle()
        {
        }

        public int Area
        {
            get { return ScaledWidth * ScaledHeight; }
        }

        public void ScaleRectangle(float value)
        {
            ScaledX = (int)(X * value);
            ScaledY = (int)(Y * value);
            ScaledWidth = (int)(Width * value);
            ScaledHeight = (int)(Height * value);
        }

        public void ScaleWeight(float scale)
        {
            ScaledWeight = Weight * scale;
        }

        public object Clone()
        {
            HaarRectangle r = new HaarRectangle();
            r.Height = Height;
            r.ScaledHeight = ScaledHeight;
            r.ScaledWeight = ScaledWeight;
            r.ScaledWidth = ScaledWidth;
            r.ScaledX = ScaledX;
            r.ScaledY = ScaledY;
            r.Weight = Weight;
            r.Width = Width;
            r.X = X;
            r.Y = Y;

            return r;
        }

    }
}
