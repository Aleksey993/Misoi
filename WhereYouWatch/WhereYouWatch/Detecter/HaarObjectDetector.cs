﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using AForge.Imaging;
using System.Collections.Concurrent;

namespace WhereYouWatch.Detecter
{
    public enum ObjectDetectorSearchMode
    {
        Default = 0,
        Single,
        NoOverlap,
    }

    public enum ObjectDetectorScalingMode
    {
        GreaterToSmaller,
        SmallerToGreater,
    }

    public class HaarObjectDetector
    {

        private List<Rectangle> detectedObjects;
        private HaarClassifier classifier;
        private ObjectDetectorSearchMode searchMode;
        private ObjectDetectorScalingMode scalingMode;

        private Size minSize = new Size(15, 15);
        private Size maxSize = new Size(500, 500);
        private float factor = 1.2f;
        private int channel = RGB.R;

        private Rectangle[] lastObjects;
        private int steadyThreshold = 2;

        private int baseWidth;
        private int baseHeight;

        private int lastWidth;
        private int lastHeight;
        private float[] steps;


        public HaarObjectDetector(HaarCascade cascade, int minSize, ObjectDetectorSearchMode searchMode, float scaleFactor,
            ObjectDetectorScalingMode scalingMode)
        {
            this.classifier = new HaarClassifier(cascade);
            this.minSize = new Size(minSize, minSize);
            this.searchMode = searchMode;
            this.scalingMode = scalingMode;
            this.factor = scaleFactor;
            this.detectedObjects = new List<Rectangle>();

            this.baseWidth = cascade.Width;
            this.baseHeight = cascade.Height;
        }


        public bool UseParallelProcessing { get; set; }


        

        public int Steady { get; private set; }

        public Rectangle[] ProcessFrame(Bitmap frame)
        {
            return ProcessFrame(UnmanagedImage.FromManagedImage(frame));
        }

        public Rectangle[] ProcessFrame(UnmanagedImage image)
        {
            // Creates an integral image representation of the frame
            IntegralImage2 integralImage = IntegralImage2.FromBitmap(
                image, channel, classifier.Cascade.HasTiltedFeatures);

            // Creates a new list of detected objects.
            this.detectedObjects.Clear();

            int width = integralImage.Width;
            int height = integralImage.Height;

            // Update parameters only if different size
            if (steps == null || width != lastWidth || height != lastHeight)
                update(width, height);


            Rectangle window = Rectangle.Empty;

            // For each scaling step
            for (int i = 0; i < steps.Length; i++)
            {
                float scaling = steps[i];

                // Set the classifier window scale
                classifier.Scale = scaling;

                // Get the scaled window size
                window.Width = (int)(baseWidth * scaling);
                window.Height = (int)(baseHeight * scaling);

                // Check if the window is lesser than the minimum size
                if (window.Width < minSize.Width && window.Height < minSize.Height &&
                    window.Width > maxSize.Width && window.Height > maxSize.Height)
                {
                    // If we are searching in greater to smaller mode,
                    if (scalingMode == ObjectDetectorScalingMode.GreaterToSmaller)
                    {
                        goto EXIT; // it won't get bigger, so we should stop.
                    }
                    else
                    {
                        continue; // continue until it gets greater.
                    }
                }


                // Grab some scan loop parameters
                int xStep = window.Width >> 3;
                int yStep = window.Height >> 3;

                int xEnd = width - window.Width;
                int yEnd = height - window.Height;

                // Check if we should run in parallel or sequential
                if (!UseParallelProcessing)
                {
                    // Sequential mode. Scan the integral image searching
                    // for objects in the window without parallelization.

                    // For every pixel in the window column
                    for (int y = 0; y < yEnd; y += yStep)
                    {
                        window.Y = y;

                        // For every pixel in the window row
                        for (int x = 0; x < xEnd; x += xStep)
                        {
                            window.X = x;

                            if (searchMode == ObjectDetectorSearchMode.NoOverlap && overlaps(window))
                                continue; // We have already detected something here, moving along.

                            // Try to detect and object inside the window
                            if (classifier.Compute(integralImage, window))
                            {
                                // an object has been detected
                                detectedObjects.Add(window);

                                if (searchMode == ObjectDetectorSearchMode.Single)
                                    goto EXIT; // Stop on first object found
                            }
                        }
                    }
                }

                else
                {
                    // Parallel mode. Scan the integral image searching
                    // for objects in the window with parallelization.
                    ConcurrentBag<Rectangle> bag = new ConcurrentBag<Rectangle>();

                    int numSteps = (int)Math.Ceiling((double)yEnd / yStep);

                    // For each pixel in the window column
                    Parallel.For(0, numSteps, (j, options) =>
                    {
                        int y = j * yStep;

                        // Create a local window reference
                        Rectangle localWindow = window;

                        localWindow.Y = y;

                        // For each pixel in the window row
                        for (int x = 0; x < xEnd; x += xStep)
                        {
                            if (options.ShouldExitCurrentIteration) return;

                            localWindow.X = x;

                            // Try to detect and object inside the window
                            if (classifier.Compute(integralImage, localWindow))
                            {
                                // an object has been detected
                                bag.Add(localWindow);

                                if (searchMode == ObjectDetectorSearchMode.Single)
                                    options.Stop();
                            }
                        }
                    });

                    // If required, avoid adding overlapping objects at
                    // the expense of extra computation. Otherwise, only
                    // add objects to the detected objects collection.
                    if (searchMode == ObjectDetectorSearchMode.NoOverlap)
                    {
                        foreach (Rectangle obj in bag)
                            if (!overlaps(obj)) detectedObjects.Add(obj);
                    }
                    else if (searchMode == ObjectDetectorSearchMode.Single)
                    {
                        if (bag.TryPeek(out window))
                        {
                            detectedObjects.Add(window);
                            goto EXIT;
                        }
                    }
                    else
                    {
                        foreach (Rectangle obj in bag)
                            detectedObjects.Add(obj);
                    }
                }
            }


            EXIT:

            Rectangle[] objects = detectedObjects.ToArray();

            checkSteadiness(objects);
            lastObjects = objects;

            return objects; // Returns the array of detected objects.
        }

        private void update(int width, int height)
        {
            List<float> listSteps = new List<float>();

            // Set initial parameters according to scaling mode
            if (scalingMode == ObjectDetectorScalingMode.SmallerToGreater)
            {
                float start = 1f;
                float stop = Math.Min(width / (float)baseWidth, height / (float)baseHeight);
                float step = factor;

                for (float f = start; f < stop; f *= step)
                    listSteps.Add(f);
            }
            else
            {
                float start = Math.Min(width / (float)baseWidth, height / (float)baseHeight);
                float stop = 1f;
                float step = 1f / factor;

                for (float f = start; f > stop; f *= step)
                    listSteps.Add(f);
            }

            steps = listSteps.ToArray();

            lastWidth = width;
            lastHeight = height;
        }

        private void checkSteadiness(Rectangle[] rectangles)
        {
            if (lastObjects == null ||
                rectangles == null ||
                rectangles.Length == 0)
            {
                Steady = 0;
                return;
            }

            bool equals = true;
            foreach (Rectangle current in rectangles)
            {
                bool found = false;
                foreach (Rectangle last in lastObjects)
                {
                    if (current.IsEqual(last, steadyThreshold))
                    {
                        found = true;
                        continue;
                    }
                }

                if (!found)
                {
                    equals = false;
                    break;
                }
            }

            if (equals)
                Steady++;

            else
                Steady = 0;
        }

        private bool overlaps(Rectangle rect)
        {
            foreach (Rectangle r in detectedObjects)
            {
                if (rect.IntersectsWith(r))
                    return true;
            }
            return false;
        }


    }
}
