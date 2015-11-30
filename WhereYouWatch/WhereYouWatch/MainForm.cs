using System;
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



namespace WhereYouWatch
{
    public partial class MainForm : Form
    {
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        Image newImage;

        private HaarDetector faceDetector = new HaarDetector("F:\\VisualRepo2\\WhereYouWatch\\WhereYouWatch\\WhereYouWatch\\haarcascade_frontalface_alt.xml");
        private HaarDetector eyeDetector = new HaarDetector("F:\\VisualRepo2\\WhereYouWatch\\WhereYouWatch\\WhereYouWatch\\haarcascade_eye.xml");


        public MainForm()
        {
            InitializeComponent();
        }


 
        private void Form1_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice();
         
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

        private void button1_Click_1(object sender, EventArgs e)  // кнопка "Бинаризировать"
        {
            try
            {
                if (!(mainPicture.Image.GetType() == typeof(Bitmap))) // мб эта проверка и не нужна?  а ещё нужна проверка на то, есть ли фотка вообще
                {
                    throw new InvalidCastException("Ошибка преобразования типа файла к Bitmap-у");
                }
                Bitmap grayscale = Binarization.ConverToGrayBitmap((Bitmap)mainPicture.Image);  // переводим изображение в серые тона
                // бинаризируем изображение по методу Отсу, используя изображение, переведённые в серые тона и 
                mainPicture.Image = Binarization.OtsuBinarize(grayscale);  // вывод на "дисплей" бинаризированного изображения            
            }
            catch (Exception ex)
            {
                Console.WriteLine("ОШИБКА: " + ex + "\n\n");
            }

        }

        private void LHFilterButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.LHFilter(mainBitmap);
        }

        private void addBrightButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.addBright(mainBitmap, 5);
        }

        private void removeBrightButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.addBright(mainBitmap, -5);
        }

        private void lineBoundsButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.lineBounds(mainBitmap);
        }

        private void clarityButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.clarity(mainBitmap);
        }

        private void addContrastButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.addContrast(mainBitmap,1.5);
        }

        private void removeContrastButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.addContrast(mainBitmap, 0.66);
        }

        private void gausButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.gaus(mainBitmap,7);
        }

        private void medialFilterButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.medial(mainBitmap, 5);
        }

        private void unsharpFilterButton_Click(object sender, EventArgs e)
        {
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.unsharp(mainBitmap, 7);
        }

        private void robertsFilterButton_Click(object sender, EventArgs e)
        {
            //Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            //Graphics g = mainPicture.CreateGraphics();
            //g.Clear(SystemColors.ControlDarkDark);
            //mainPicture.Image = FilterService.roberts(mainBitmap);
            //HaarCascade cascade = new HaarCascade("F:\\VisualRepo2\\WhereYouWatch\\WhereYouWatch\\WhereYouWatch\\haarcascade_frontalface_alt.xml");
            HaarDetector.DetectionParams faceParam = new HaarDetector.DetectionParams(10, 0, 3, 480, 1.01f, 0.3f, 0.2f, new Pen(Color.Red));
            HaarDetector.DetectionParams eyeParam = new HaarDetector.DetectionParams(2, 1, 3, 480, 1.1f, 0.3f, 0.2f, new Pen(Color.Blue));
            Bitmap b = ((Bitmap)mainPicture.Image);
            HaarDetector.DResults res =faceDetector.Detect(b, faceParam);
            if (res.DetectedOLocs != null)
            {
                foreach (Rectangle rec in res.DetectedOLocs)
                {
                    Bitmap eyeBitmap = b.Clone(rec, b.PixelFormat);
                    HarrisDetector hd = new HarrisDetector();
                    Bitmap gray = Binarization.ConverToGrayBitmap(eyeBitmap);
                    int[] input = new int[gray.Width*gray.Height];
                    for(int x=0;x< gray.Width; x++) {
                        for(int y = 0; y < gray.Height; y++)
                        {
                            int val = gray.GetPixel(x, y).R;          
                            input[y*gray.Width+x]= Convert.ToInt32(Convert.ToInt32(val) << 16 | Convert.ToInt32(val) << 8 | Convert.ToInt32(val));
                        }
                    }
                    hd.init(input, eyeBitmap.Width, eyeBitmap.Height, 0.04);
                    int[] res12= hd.process();
                    int i = 0;
                    i++;
                    //HaarDetector.DResults resEye = eyeDetector.Detect(eyeBitmap, eyeParam);
                    //if (resEye.DetectedOLocs != null)
                    //{
                    //    foreach (Rectangle recEye in resEye.DetectedOLocs)
                    //    {
                    //        if (recEye.X==0 && recEye.Y==0 && recEye.Width==0 && recEye.Height == 0)
                    //        {
                    //            break;
                    //        }
                    //        Point p = detectEyeCenter(eyeBitmap.Clone(recEye, eyeBitmap.PixelFormat));
                    //        Graphics G = Graphics.FromImage(b);
                    //        int startXEye = rec.X + recEye.X;
                    //        int startYEye = rec.Y + recEye.Y;
                    //        G.DrawRectangle(new Pen(Color.Blue), new Rectangle(startXEye, startYEye, recEye.Width, recEye.Height));
                    //        G.DrawLine(new Pen(Color.Green), startXEye + p.X - 5,startYEye+p.Y, startXEye + p.X + 5, startYEye + p.Y);
                    //        G.DrawLine(new Pen(Color.Green), startXEye + p.X, startYEye + p.Y - 5, startXEye + p.X, startYEye + p.Y + 5);
                    //        G.Dispose();
                    //    }
                    //}
                }
            }
            //res = eyeDetector.Detect(b, eyeParam);
            //int x=res.DetectedOLocs[0].X;
            //x = res.DetectedOLocs[0].Y;
            //x = res.DetectedOLocs[0].Width;
            //x = res.DetectedOLocs[0].Height;
            mainPicture.Image = b;
            int f = 1;
            f++;
        }

        private Point detectEyeCenter(Bitmap image)
        {
            Bitmap grayscale = Binarization.ConverToGrayBitmap(image);
            image = Binarization.OtsuBinarize(grayscale);
            int sum = Int32.MaxValue;
            int x = 0;
            for(int i=1;i<image.Width; i++)
            {
                int rowsum = 0;
                for(int j = 1; j < image.Height; j++)
                {
                    rowsum += image.GetPixel(i, j).B;
                }
                if (sum > rowsum)
                {
                    sum = rowsum;
                    x = i;
                }
            }
            int y=0;
            int size = 0;
            int tmpsize = 0;
            for(int i=1;i< image.Height; i++)
            {
                if (image.GetPixel(x, i).B == 0)
                {
                    tmpsize++;
                }
                else
                {
                    if (size < tmpsize)
                    {
                        size = tmpsize;
                        tmpsize = 0;
                        y = i - size;
                    }
                }
            }
            if (tmpsize > size)
            {
                size = tmpsize;
            }

            return new Point(x, y+size/2);
        }
    }
}
