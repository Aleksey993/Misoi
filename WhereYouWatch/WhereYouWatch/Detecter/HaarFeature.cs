using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace WhereYouWatch.Detecter
{
    [Serializable]
    public sealed class HaarFeature : ICloneable
    {

        public bool Tilted { get; set; }

        public HaarRectangle[] Rectangles { get; set; }

        public HaarFeature()
        {
            this.Rectangles = new HaarRectangle[2];
        }

        public HaarFeature(bool tilted, params int[][] rectangles)
        {
            this.Tilted = tilted;
            this.Rectangles = new HaarRectangle[rectangles.Length];
            for (int i = 0; i < rectangles.Length; i++)
                this.Rectangles[i] = new HaarRectangle(rectangles[i]);
        }

        public double GetSum(IntegralImage2 image, int x, int y)
        {
            double sum = 0.0;

            if (!Tilted)
            {
                // Compute the sum for a standard feature
                foreach (HaarRectangle rect in Rectangles)
                {
                    sum += image.GetSum(x + rect.ScaledX, y + rect.ScaledY,
                        rect.ScaledWidth, rect.ScaledHeight) * rect.ScaledWeight;
                }
            }
            else
            {
                // Compute the sum for a rotated feature
                foreach (HaarRectangle rect in Rectangles)
                {
                    sum += image.GetSumT(x + rect.ScaledX, y + rect.ScaledY,
                        rect.ScaledWidth, rect.ScaledHeight) * rect.ScaledWeight;
                }
            }

            return sum;
        }

        public void SetScaleAndWeight(float scale, float weight)
        {
            // manual loop unfolding

            if (Rectangles.Length == 2)
            {
                HaarRectangle a = Rectangles[0];
                HaarRectangle b = Rectangles[1];

                b.ScaleRectangle(scale);
                b.ScaleWeight(weight);

                a.ScaleRectangle(scale);
                a.ScaledWeight = -(b.Area * b.ScaledWeight) / a.Area;
            }
            else // rectangles.Length == 3
            {
                HaarRectangle a = Rectangles[0];
                HaarRectangle b = Rectangles[1];
                HaarRectangle c = Rectangles[2];

                c.ScaleRectangle(scale);
                c.ScaleWeight(weight);

                b.ScaleRectangle(scale);
                b.ScaleWeight(weight);

                a.ScaleRectangle(scale);
                a.ScaledWeight = -(b.Area * b.ScaledWeight
                    + c.Area * c.ScaledWeight) / (a.Area);
            }
        }

        public object Clone()
        {
            HaarRectangle[] newRectangles = new HaarRectangle[Rectangles.Length];
            for (int i = 0; i < newRectangles.Length; i++)
            {
                HaarRectangle rect = Rectangles[i];
                newRectangles[i] = new HaarRectangle(rect.X, rect.Y,
                    rect.Width, rect.Height, rect.Weight);
            }

            HaarFeature r = new HaarFeature();
            r.Rectangles = newRectangles;
            r.Tilted = Tilted;

            return r;
        }

    }
}
