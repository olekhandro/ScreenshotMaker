using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using ScreenshotMakerLibrary.Domain;

namespace ScreenshotMakerLibrary
{
    public class ScreenshotManagerClass
    {
        private const int MINWAITINTERVALINMINUTES = 3;
        private const int MAXWAITINTERVALINMINUTES = 10;
        private static Thread WorkingThread { get; set; }

        public static User CurrentUser { get; set; }
        public static Project CurrentProject { get; set; }
        public static Bitmap LastSavedScreenshot { get; set;}
        public static MySQLBroker CurrentBroker { get; set; }

       

        public static event EventHandler OnScreenshotUploaded;

        public static void Work(User user, Project project, MySQLBroker broker)
        {
            if (WorkingThread!=null)
                WorkingThread.Abort();

            CurrentUser = user;
            CurrentProject = project;
            CurrentBroker = broker;
           
            WorkingThread = new Thread(new ThreadStart(StartWork));
            WorkingThread.Start();
        }

        public static void Stop()
        {
            WorkingThread.Abort();
        }

        private static void StartWork()
        {
            var random = new Random();
            while (true)
            {
                var minutes = random.Next(MINWAITINTERVALINMINUTES, MAXWAITINTERVALINMINUTES);
                Thread.Sleep(minutes * 60 * 100);
                var screenshot = GetScreenShot();
                var screenshotEntity = new Screenshot(screenshot);
                screenshotEntity.CreationTime = DateTime.UtcNow;
                screenshotEntity.Project = CurrentProject;
                screenshotEntity.User = CurrentUser;
                LastSavedScreenshot = screenshot;
                CurrentBroker.Save(screenshotEntity);
                OnScreenshotUploaded(null, EventArgs.Empty);
            } 
        }

        public static Bitmap GetScreenShot()
        {
            var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            }

            return bitmap;
        }
    }
}