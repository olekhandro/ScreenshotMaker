using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ScreenshotMakerLibrary.Domain
{
    public class Message:Identifiable
    {
        private string _text = "";
        private DateTime _sendTime = DateTime.Now;

        public User Sender { get; set; }
        public User Receiver { get; set; }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public DateTime SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        public Message()
        {
        }
    }
}
