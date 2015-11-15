using System;
using System.Drawing;
using System.Globalization;

namespace WhereYouWatch.DetectFace
{
    internal class Parser
    {

        // Sample: "20 20"
        public static Size ParseSize(string StringVal)
        {
            string[] Slices = StringVal.Trim().Split(' ');
            return new Size(Convert.ToInt32(Slices[0].Trim()), Convert.ToInt32(Slices[1].Trim()));
        }

        // Sample: "0.0337941907346249"
        public static float ParseSingle(string StringVal)
        {
            return (float.Parse(ReplaceDecimalSeperator(StringVal.Trim())));
        }

        // Sample: "1"
        public static int ParseInt(string StringVal)
        {
            return int.Parse(StringVal.Trim());
        }

        // Sample: "3 7 14 4 -1."
        public static HearCascade.FeatureRect ParseFeatureRect(string StringVal)
        {
            string[] Slices = StringVal.Trim().Split(' ');
            HearCascade.FeatureRect FR = new HearCascade.FeatureRect();
            FR.Rectangle = new Rectangle(Convert.ToInt32(Slices[0].Trim()), Convert.ToInt32(Slices[1].Trim()), Convert.ToInt32(Slices[2].Trim()), Convert.ToInt32(Slices[3].Trim()));

            string Weight = Slices[4];
            if (Weight.EndsWith("."))
            {
                Weight = Weight.Replace(".", "");
            }
            else
            {
                Weight = ReplaceDecimalSeperator(Weight);
            }
            FR.Weight = Convert.ToInt32(Weight.Trim());
            return FR;
        }

        public static string NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        public static string ReplaceDecimalSeperator(string Val)
        {
            return Val.Replace(".", NumberDecimalSeparator);
        }
    }

}
