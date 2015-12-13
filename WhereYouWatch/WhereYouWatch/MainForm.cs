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
using WhereYouWatch.Filter;
using MetriCam;

namespace WhereYouWatch
{
    public partial class MainForm : Form
    {
        private WebCam camera;
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        IFilter iFilter;
        Image newImage;
        private HaarDetector faceDetector = new HaarDetector("haarcascade_frontalface_alt.xml");
        private HaarDetector eyeDetector = new HaarDetector("haarcascade_eye.xml");
        private HaarDetector mouthDetector = new HaarDetector("F:/VisualRepo2/WhereYouWatch/WhereYouWatch/WhereYouWatch/bin/Debug/haarcascade_mouth.xml");
        //private HaarDetector noseDetector = new HaarDetector("haarcascade_nose.xml");
        private HaarDetector.DetectionParams faceParam = new HaarDetector.DetectionParams(10, 0, 3, 480, 1.1f, 0.7f, 0.2f, new Pen(Color.Red));
        private HaarDetector.DetectionParams eyeParam = new HaarDetector.DetectionParams(2, 0, 5, 8, 1.1f, 0.5f, 0.1f, new Pen(Color.Blue));

        private HaarDetector.DetectionParams noseParam = new HaarDetector.DetectionParams(1, 0, 3, 180, 1.1f, 0.7f, 0.1f, new Pen(Color.Gold));
        private HaarDetector.DetectionParams mouthParam = new HaarDetector.DetectionParams(1, 0, 3, 180, 1.1f, 0.7f, 0.1f, new Pen(Color.Green));

        public MainForm()
        {
            InitializeComponent();
            camera = new WebCam();
        }
        //Real-Time for debug
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker1.CancellationPending)
            {
                camera.Update();
                //pictureBox1.Image = camera.GetBitmap();
                //HaarDetector.DetectionParams eyeParam = new HaarDetector.DetectionParams(2, 1, 3, 480, 1.1f, 0.3f, 0.2f, new Pen(Color.Blue));
                Bitmap b = camera.GetBitmap();

                //HaarDetector.DResults mouthEye = mouthDetector.Detect(b, mouthParam);
                //HaarDetector.DResults nose = noseDetector.Detect(b, noseParam);
                HaarDetector.DetectionParams faceParam = new HaarDetector.DetectionParams(10, 0, 3, 480, 1.01f, 0.3f, 0.2f, null);
                HaarDetector.DetectionParams eyeParam = new HaarDetector.DetectionParams(2, 1, 3, 480, 1.1f, 0.3f, 0.2f, new Pen(Color.Blue));

                HaarDetector.DResults res = faceDetector.Detect(b, faceParam);
                Graphics imageFrame = Graphics.FromImage(b);
                if (res.DetectedOLocs != null) //We have face list
                {

                    Color color = Color.Red;
                    Rectangle face = res.DetectedOLocs[0]; //for one face
                    imageFrame.DrawRectangle(new Pen(color), face);

                    Bitmap faceFrame = b.Clone(face, b.PixelFormat);

                    //added middle Lines(X, Y)
                    IList<Point> eyesCenters = EyesDetection(face.X, face.Y, b, faceFrame, imageFrame);
                    Point mouthPoint = MouthDetection(face.X, face.Y, b, faceFrame, imageFrame);

                    //EyePair eyePair = GetEyePair(eyesCenters);

                    //added nose if needed

                    //coeficients Mathing

                    //this.sideLongTiltLabel.Text = MathService.AngleWithHorizont(eyesCenters[0], eyesCenters[1]).ToString();

                    //double dleft = MathService.LineLength(eyesCenters[0], mouthPoint);
                    //double dright = MathService.LineLength(eyesCenters[1], mouthPoint);

                    //double first = 1.257d;
                    //double second = 2;
                    //double dExp = (((dleft + dright) / first) / second);
                    //this.DExpLabel.Text = dExp.ToString();

                    //double dEyes = MathService.LineLength(eyesCenters[0], eyesCenters[1]);
                    //this.DEyesLabel.Text = dEyes.ToString();

                    //this.DKoefLabel.Text = (dExp / dEyes).ToString();

                    //drawing triangle

                    imageFrame.DrawLine(new Pen(Color.Black), new Point(eyesCenters[0].X, eyesCenters[0].Y), new Point(eyesCenters[1].X, eyesCenters[1].Y));
                    imageFrame.DrawLine(new Pen(Color.Black), new Point(eyesCenters[0].X, eyesCenters[0].Y), new Point(mouthPoint.X, mouthPoint.Y));
                    imageFrame.DrawLine(new Pen(Color.Black), new Point(eyesCenters[1].X, eyesCenters[1].Y), new Point(mouthPoint.X, mouthPoint.Y));
                }

                mainPicture.Image = b;


                pictureBox1.Image = b;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            HaarDetector.DetectionParams faceParam = new HaarDetector.DetectionParams(10, 0, 3, 480, 1.01f, 0.3f, 0.2f, null);
            HaarDetector.DetectionParams eyeParam = new HaarDetector.DetectionParams(2, 1, 3, 480, 1.1f, 0.3f, 0.2f, new Pen(Color.Blue));
            Bitmap b = ((Bitmap)mainPicture.Image);
            HaarDetector.DResults res = faceDetector.Detect(b, faceParam);
            Graphics imageFrame = Graphics.FromImage(b);
            if (res.DetectedOLocs != null) //We have face list
            {

                Color color = Color.Red;
                Rectangle face = res.DetectedOLocs[0]; //for one face
                imageFrame.DrawRectangle(new Pen(color), face);

                Bitmap faceFrame = b.Clone(face, b.PixelFormat);

                //added middle Lines(X, Y)
                IList<Point> eyesCenters = EyesDetection(face.X, face.Y, b, faceFrame, imageFrame);
                Point mouthPoint = MouthDetection(face.X, face.Y, b, faceFrame, imageFrame);

                //EyePair eyePair = GetEyePair(eyesCenters); //Double eye(left or right)

                //added nose if needed

                //coeficients Mathing

                if (eyesCenters.Count >= 2)
                {
                    this.sideLongTiltLabel.Text = MathService.AngleWithHorizont(eyesCenters[0], eyesCenters[1]).ToString();

                    double dleft = MathService.LineLength(eyesCenters[0], mouthPoint);
                    double dright = MathService.LineLength(eyesCenters[1], mouthPoint);

                    double first = 1.257d;
                    double second = 2;
                    double dExp = (((dleft + dright) / first) / second);
                    this.DExpLabel.Text = dExp.ToString();

                    double dEyes = MathService.LineLength(eyesCenters[0], eyesCenters[1]);
                    this.DEyesLabel.Text = dEyes.ToString();

                    this.DKoefLabel.Text = (dExp / dEyes).ToString();

                    //drawing triangle

                    imageFrame.DrawLine(new Pen(Color.Black), new Point(eyesCenters[0].X, eyesCenters[0].Y), new Point(eyesCenters[1].X, eyesCenters[1].Y));
                    imageFrame.DrawLine(new Pen(Color.Black), new Point(eyesCenters[0].X, eyesCenters[0].Y), new Point(mouthPoint.X, mouthPoint.Y));
                    imageFrame.DrawLine(new Pen(Color.Black), new Point(eyesCenters[1].X, eyesCenters[1].Y), new Point(mouthPoint.X, mouthPoint.Y));

                }
                else
                    this.sideLongTiltLabel.Text = "Warning Detected. Try again";
            }
            mainPicture.Image = b;
        }

        private IList<Point> EyesDetection(int X, int Y, Bitmap original, Bitmap faceFrame, Graphics imageFrame)
        {
            Color color = Color.Aqua;
            Rectangle eyesFrame = new Rectangle(X, Y, faceFrame.Width, Convert.ToInt32(faceFrame.Height * 0.6));

            imageFrame.DrawRectangle(new Pen(color,1), eyesFrame);
            HaarDetector.DetectionParams eyeParam = new HaarDetector.DetectionParams(2, 1, 3, 480, 1.1f, 0.3f, 0.2f, null);
            Bitmap eyesRegion = original.Clone(eyesFrame, original.PixelFormat);


            HaarDetector.DResults res = eyeDetector.Detect(eyesRegion, eyeParam);
            IList<Point> eyesCenters = new List<Point>();
            if (res.DetectedOLocs != null) //We have face list
            {
                int eyecount = 0;
                foreach (Rectangle eye in res.DetectedOLocs)
                {
                    if (eye.X == 0 && eye.Y == 0)
                    {
                        break;
                    }
                    eyecount++;
                    Rectangle eyeAbsolute = new Rectangle(X + eye.X, Y + eye.Y, eye.Width, eye.Height);

                    Bitmap image = eyesRegion.Clone(eye, original.PixelFormat);
                    Graphics graf = Graphics.FromImage(image);
                    Rectangle recImage = getEye(image);
                    graf.DrawEllipse(new Pen(Color.Blue), recImage);

                    if (eyecount == 1)
                    {
                        eye1.Image = image;
                    }
                    if (eyecount == 2)
                    {
                        eye2.Image = image;
                    }
                    if (eyecount == 3)
                    {
                        eye3.Image = image;
                    }
                    EyeDirection.Text = DirectionEyeX(eye, recImage);
                    labelDirectionEyeY.Text = DirectionEyeY(eye, recImage);
                    imageFrame.DrawRectangle(new Pen(Color.BlueViolet, 1), eyeAbsolute);
                    Point centerPoint = eyeAbsolute.Center();
                    eyesCenters.Add(centerPoint);

                    DrawPoint(centerPoint, imageFrame, Color.Fuchsia);
                }
            }
            return eyesCenters;
        }
        private String DirectionEyeX(Rectangle mainSquare, Rectangle eye)
        {
            string label = "";
            double dcenter = mainSquare.Width/2;
            if (eye.X > dcenter)
            {
                label = "left";
            }
            else
                label = "right";
            return label;
        }

        private String DirectionEyeY(Rectangle mainSquare, Rectangle eye)
        {
            string label = "";
            double dcenter = mainSquare.Height / 2;
            if (eye.Y < dcenter)
            {
                label = "up";
            }
            else
                label = "down";
            return label;
        }
        private void DrawPoint(Point centerPoint, Graphics imageFrame, Color color)
        {
            float x = centerPoint.X - 2;
            float y = centerPoint.Y - 2;
            float width = 2 * 2;
            float height = 2 * 2;
            imageFrame.DrawEllipse(new Pen(color, 2), x, y, width, height);
        }


        private Point MouthDetection(int X, int Y, Bitmap original, Bitmap faceFrame, Graphics imageFrame)
        {
            Color color = Color.Pink;
            int mouthFrameHeight = Convert.ToInt32(faceFrame.Height * 0.4);
            int mouthFrameY = Convert.ToInt32(Y+faceFrame.Height*0.6);
            Rectangle mouthFrame = new Rectangle(X, mouthFrameY, faceFrame.Width, mouthFrameHeight);
            imageFrame.DrawRectangle(new Pen(color, 1), mouthFrame);
            Bitmap mouthsRegion = original.Clone(mouthFrame, original.PixelFormat);

            HaarDetector.DResults mouths = mouthDetector.Detect(mouthsRegion, mouthParam);

            Point centerPoint = new Point();
            if (mouths.DetectedOLocs != null)
            {
                Rectangle mouth = mouths.DetectedOLocs[0];
                Rectangle mouthAbsolute = new Rectangle(X + mouth.X, mouth.Y + mouthFrameY, mouth.Width, mouth.Height);
                imageFrame.DrawRectangle(new Pen(Color.LightYellow, 1), mouthAbsolute);
                centerPoint = mouthAbsolute.Center();
                DrawPoint(centerPoint, imageFrame, Color.Yellow);
            }
            //if (centerPoint.X == 0 && centerPoint.Y == 0)
            //{
                mouthFrame = new Rectangle(Convert.ToInt32(X + faceFrame.Width*0.2), mouthFrameY, Convert.ToInt32(faceFrame.Width*0.6), mouthFrameHeight);
                mouthsRegion = original.Clone(mouthFrame, original.PixelFormat);
                int xValue = 0;
                int yValue = 0;
                int count = 0;
                for(int x=1;x< mouthsRegion.Width-1; x++)
                {
                    for(int y=1;y< mouthsRegion.Height-1; y++)
                    {
                        if ((mouthsRegion.GetPixel(x, y).R - mouthsRegion.GetPixel(x, y).G)>50 && (mouthsRegion.GetPixel(x, y).R-mouthsRegion.GetPixel(x, y).B)>20 && mouthsRegion.GetPixel(x, y).R>150)
                        {
                            xValue += x;
                            yValue += y;
                            count++;
                        }
                    }
                }
                if (count > 0)
                {
                DrawPoint(centerPoint, imageFrame, Color.Yellow);
                Point p=new Point(Convert.ToInt32(X *1.2)+ xValue / count, mouthFrameY + yValue / count);
                DrawPoint(p, imageFrame, Color.Green);
                if (centerPoint.X == 0 && centerPoint.Y == 0)
                    {
                    return p;
                    }
                }
            return centerPoint;
        }

        private EyePair GetEyePair(IList<Point> eyesCenters)
        {
            EyePair result = new EyePair();
            if (eyesCenters[0].X < eyesCenters[1].X)
            {
                result.LeftEye = eyesCenters[0];
                result.RightEye = eyesCenters[1];
            }
            else
            {
                result.LeftEye = eyesCenters[1];
                result.RightEye = eyesCenters[0];
            }
            return result;
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

        

        private Point detectEyeCenter(Bitmap image)
        {
            image = new OtsuBinarizationFilter().Filter(image);
            int sum = Int32.MaxValue;
            int x = 0;
            for (int i = 1; i < image.Width; i++)
            {
                int rowsum = 0;
                for (int j = 1; j < image.Height; j++)
                {
                    rowsum += image.GetPixel(i, j).B;
                }
                if (sum > rowsum)
                {
                    sum = rowsum;
                    x = i;
                }
            }
            int y = 0;
            int size = 0;
            int tmpsize = 0;
            for (int i = 1; i < image.Height; i++)
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

            return new Point(x, y + size / 2);
        }

        private void ConnectDisconnect_Click(object sender, EventArgs e)
        {
            if (!camera.IsConnected())
            {
                camera.Connect();
                ConnectDisconnect.Text = "&Disconnect";
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            camera.Disconnect();
            ConnectDisconnect.Text = "&Connect";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            iFilter = new RobertsFilter();
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = iFilter.Filter(mainBitmap);
        }

        private Rectangle getEye(Bitmap image)
        {
            image = FilterService.AddContrast(image, 3.0);
            iFilter = new GausFilter();
            image = iFilter.Filter(image);
            iFilter = new SimpleBinarizationFilter();
            image = iFilter.Filter(image);
            Graphics graf = Graphics.FromImage(image);
            int height = image.Height;
            int width = image.Width;
            int sum = 0;
            int linex = 0;
            int liney = 0;
            for (int x = 5; x < width - 5; x++)
            {
                int tmpsum = 0;
                for (int y = 5; y < height - 5; y++)
                {
                    if ((image).GetPixel(x, y).R < 10)
                    {
                        tmpsum += 1;
                    }
                }
                if (tmpsum > sum && tmpsum < height * 5 / 9)
                {
                    linex = x;
                    sum = tmpsum;
                }
            }
            int glassWidth = 0;
            int minValue = 5;
            for (int i = minValue; i < height - 5; i++)
            {
                int rightsize = 0;
                int leftsize = 0;
                if (image.GetPixel(linex, i).B < 10)
                {
                    int size = 0;
                    while (image.GetPixel(linex + size, i).B < 10)
                    {
                        rightsize++;
                        size++;
                        if (linex + size >= width)
                        {
                            break;
                        }
                    }
                    size = 0;
                    while (image.GetPixel(linex - size, i).B < 10)
                    {
                        leftsize++;
                        size++;
                        if (linex - size < 0)
                        {
                            break;
                        }
                    }
                }
                if (leftsize + rightsize > width / 5)
                {
                    minValue = i;
                }
            }
            for (int i = minValue; i < height - 5; i++)
            {
                int rightsize = 0;
                int leftsize = 0;
                if (image.GetPixel(linex, i).B < 10)
                {
                    int size = 0;
                    while (image.GetPixel(linex + size, i).B < 10)
                    {
                        rightsize++;
                        size++;
                        if (linex + size >= width)
                        {
                            break;
                        }
                    }
                    size = 0;
                    while (image.GetPixel(linex - size, i).B < 10)
                    {
                        leftsize++;
                        size++;
                        if (linex - size < 0)
                        {
                            break;
                        }
                    }
                }
                int tmpwidth;
                if (leftsize > rightsize)
                {
                    tmpwidth = rightsize;
                }
                else
                {
                    tmpwidth = leftsize;
                }

                if (tmpwidth > glassWidth)
                {
                    liney = i;
                    glassWidth = tmpwidth;
                }
            }
            Boolean isNeedChange = true;
            int count = 1;
            int r = 0;
            while (isNeedChange)
            {
                count++;
                int rightsize = 0;
                int leftsize = 0;
                int topsize = 0;
                int bottomsize = 0;
                int size = 0;
                while (image.GetPixel(linex + size, liney).B < 10)
                {
                    rightsize++;
                    size++;
                    if (linex + size >= width)
                    {
                        break;
                    }
                }
                size = 0;
                while (image.GetPixel(linex - size, liney).B < 10)
                {
                    leftsize++;
                    size++;
                    if (linex - size < 0)
                    {
                        break;
                    }
                }
                linex += (rightsize - leftsize) / 2;
                size = 0;
                while (image.GetPixel(linex, liney + size).B < 10)
                {
                    topsize++;
                    size++;
                    if (liney + size >= height)
                    {
                        break;
                    }
                }
                size = 0;
                while (image.GetPixel(linex, liney - size).B < 10)
                {
                    bottomsize++;
                    size++;
                    if (liney - size < 0)
                    {
                        break;
                    }
                }
                r = (leftsize + rightsize) / 2;
                if (bottomsize != topsize)
                {
                    liney += (topsize - bottomsize) / 2;
                }
                else
                {
                    isNeedChange = false;
                }
                if (count > 10)
                {
                    break;
                }
            }
            return new Rectangle(linex - r, liney - r, r * 2,r*2);
        }
    }
}
