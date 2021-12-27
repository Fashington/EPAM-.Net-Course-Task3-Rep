using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Tariffs;

namespace ATS.ATSEventArgs
{
    public class CallEventArgs
    {
        public string PubNumber { get; set; }
        public string SubNumber { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
    }
}
