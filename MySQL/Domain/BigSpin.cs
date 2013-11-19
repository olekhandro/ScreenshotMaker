using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQL.Domain
{
    public class BigSpin:Identifiable
    {
        public string Theme { get; set; }
        public string SpinSubject { get; set;}
        public string SpinPost { get; set; }

        public BigSpin()
        {
            
        }
    }
}
