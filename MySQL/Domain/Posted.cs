using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQL.Domain
{
    public class Posted:Identifiable
    {
        public string UserUrl { get; set; }
        public string BlogUrl { get; set; }
        public string KeyPhrase { get; set; }
        public long UserId { get; set; }
        public DateTime PostDate { get; set; }
        public string Theme { get; set; }
        public Posted()
        {
            
        }
    }
}
