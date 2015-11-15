using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace WhereYouWatch.Detecter
{
    [Serializable]
    public class HaarCascade : ICloneable
    {
        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public HaarCascadeStage[] Stages { get; protected set; }

        public bool HasTiltedFeatures { get; protected set; }

        protected HaarCascade(int baseWidth, int baseHeight)
        {
            Width = baseWidth;
            Height = baseHeight;
        }

        public object Clone()
        {
            HaarCascadeStage[] newStages = new HaarCascadeStage[Stages.Length];
            for (int i = 0; i < newStages.Length; i++)
                newStages[i] = (HaarCascadeStage)Stages[i].Clone();

            HaarCascade r = new HaarCascade(Width, Height);
            r.HasTiltedFeatures = this.HasTiltedFeatures;
            r.Stages = newStages;

            return r;
        }

    }
}
