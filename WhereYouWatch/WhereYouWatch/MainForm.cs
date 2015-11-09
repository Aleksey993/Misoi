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
            Bitmap mainBitmap = (Bitmap)mainPicture.Image;
            Graphics g = mainPicture.CreateGraphics();
            g.Clear(SystemColors.ControlDarkDark);
            mainPicture.Image = FilterService.roberts(mainBitmap);
        }
    }
}
