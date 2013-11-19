using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQL.Domain
{
    public class WordPress:Identifiable
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Activated { get; set; }
        public string BlogUrl { get; set; }

        public WordPress()
        {
            
        }
    }
}
