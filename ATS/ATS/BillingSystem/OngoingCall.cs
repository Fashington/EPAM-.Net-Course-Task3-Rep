using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.BillingSystem
{
    class OngoingCall
    {
        public Port Pub { get; set; }
        public Port Sub { get; set; }
        public DateTime Date { get; set; }

        public OngoingCall(Port pub, Port sub, DateTime date)
        {
            Pub = pub;
            Sub = sub;
            Date = date;
        }
    }
}
