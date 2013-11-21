using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenshotMakerLibrary
{
    public class ScreenshotManagerClass
    {
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