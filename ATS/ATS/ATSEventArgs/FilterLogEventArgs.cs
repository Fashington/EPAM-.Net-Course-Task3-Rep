using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.ATSEventArgs
{
    class FilterLogEventArgs
    {
        public double costLowerLimit { get; set; }
        public double costUpperLimit { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string number { get; set; }
        public DateTime date {get;set;}
    }
}
