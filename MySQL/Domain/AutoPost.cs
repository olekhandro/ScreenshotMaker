using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQL.Domain
{
    public class AutoPost : Identifiable
    {
        public long UserId { get; set; }
        public string TargetUrl { get; set; }
        public string KeyPhrase { get; set; }
        public string Theme { get; set; }
        public long Quantity { get; set; }
        public bool IsDone { get; set; }

        public AutoPost()
        {
            
        }
    }
}
