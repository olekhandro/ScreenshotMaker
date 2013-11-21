using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ScreenshotMakerLibrary.Domain
{
    public class Screenshot:Identifiable
    {
        public User User { get; set; }
        public Project Project { get; set; }

        public byte[] Data { get; set; }
        public DateTime CreationTime { get; set; }

        public Screenshot(Bitmap data)
        {
            var converter = new ImageConverter();
            Data = (byte[])converter.ConvertTo(data, typeof(byte[]));
        }

        protected Screenshot()
        {
        }
    }
}
