namespace WhereYouWatch.Detecter
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using AForge.Imaging;

    /// <summary>
    ///   Static tool functions for imaging.
    /// </summary>
    /// 
    /// <remarks>
    ///   <para>
    ///     References:
    ///     <list type="bullet">
    ///       <item><description>
    ///         P. D. Kovesi. MATLAB and Octave Functions for Computer Vision and Image Processing.
    ///         School of Computer Science and Software Engineering, The University of Western Australia.
    ///         Available in: <a href="http://www.csse.uwa.edu.au/~pk/Research/MatlabFns/Match/matchbycorrelation.m">
    ///         http://www.csse.uwa.edu.au/~pk/Research/MatlabFns/Match/matchbycorrelation.m </a>
    ///       </description></item>
    ///   </list></para>
    /// </remarks>
    /// 
    public static class Tools
    {

        private const double SQRT2 = 1.4142135623730951;



        /// <summary>
        ///   Compares two rectangles for equality, considering an acceptance threshold.
        /// </summary>
        public static bool IsEqual(this Rectangle objA, Rectangle objB, int threshold)
        {
            return (Math.Abs(objA.X - objB.X) < threshold) &&
                   (Math.Abs(objA.Y - objB.Y) < threshold) &&
                   (Math.Abs(objA.Width - objB.Width) < threshold) &&
                   (Math.Abs(objA.Height - objB.Height) < threshold);
        }

    }
}
