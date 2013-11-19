using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQL.Domain
{
    public class User : Identifiable
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Active { get; set; }
        public string Version { get; set; }
    }
}
