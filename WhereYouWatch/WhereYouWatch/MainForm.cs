﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using WhereYouWatch.Filter;
using System.Diagnostics;

namespace WhereYouWatch
{
    public partial class MainForm : Form
    {
        Detecter.HaarObjectDetector detector;


        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        IFilter iFilter;
        Image newImage;

        public MainForm()
        {
            InitializeComponent();
        }


 
        private void Form1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice();

            Detecter.HaarCascade cascade = new Detecter.FaceHaarCascade();
            detector = new Detecter.HaarObjectDetector(cascade, 30);

        }

        private void CheckFace_Click(object sender, EventArgs e)
        {
            detector.SearchMode = Detecter.ObjectDetectorSearchMode.NoOverlap;
            detector.ScalingMode = Detecter.ObjectDetectorScalingMode.SmallerToGreater;
            detector.ScalingFactor = 1.5f;
            detector.UseParallelProcessing = true;
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;

            Stopwatch sw = Stopwatch.StartNew();


            // Process frame to detect objects
            Rectangle[] objects = detector.ProcessFrame(mainBitmap);


            sw.Stop();


            if (objects.Length > 0)
            {
                Detecter.RectanglesMarker marker = new Detecter.RectanglesMarker(objects, Color.Fuchsia);
                mainPicture.Image = marker.Apply(mainBitmap);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += videoSource_NewFrame;
                videoSource.Start();
                while (newImage == null){}
                videoSource.Stop();
                newImage = null;
        }

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            newImage = (Bitmap)eventArgs.Frame.Clone();
            mainPicture.Image = newImage;
            realPicture.Image = newImage;
        }

        private void binarizationFilter_Click(object sender, EventArgs e)
        {
            iFilter = new OtsuBinarizationFilter();
            
            try
            {
                if (!(mainPicture.Image.GetType() == typeof(Bitmap))) // мб эта проверка и не нужна?  а ещё нужна проверка на то, есть ли фотка вообще
                {
                    throw new InvalidCastException("Ошибка преобразования типа файла к Bitmap-у");
                }
                // бинаризируем изображение по методу Отсу, используя изображение, переведённые в серые тона.
                mainPicture.Image = iFilter.Filter((Bitmap)mainPicture.Image);         
            }
            catch (Exception ex)
            {
                Console.WriteLine("ОШИБКА: " + ex + "\n\n");
            }

        }

        private void LHFilterButton_Click(object sender, EventArgs e)
        {
            iFilter = new LHFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private void addBrightButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.AddBright(mainBitmap, 5);
        }

        private void removeBrightButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.AddBright(mainBitmap, -5);
        }

        private void lineBoundsButton_Click(object sender, EventArgs e)
        {
            iFilter = new LineBoundFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private void clarityButton_Click(object sender, EventArgs e)
        {
            iFilter = new ClarityFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private void addContrastButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.AddContrast(mainBitmap,1.5);
        }

        private void removeContrastButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.AddContrast(mainBitmap, 0.66);
        }

        private void gausButton_Click(object sender, EventArgs e)
        {
            iFilter = new GausFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private void medialFilterButton_Click(object sender, EventArgs e)
        {
            iFilter = new MedialFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private void unsharpFilterButton_Click(object sender, EventArgs e)
        {
            iFilter = new UnsharpFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private void robertsFilterButton_Click(object sender, EventArgs e)
        {
            iFilter = new RobertsFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

       
    }
}
