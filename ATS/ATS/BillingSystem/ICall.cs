using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.BillingSystem
{
    interface ICall
    {
        public string PubNumber { get;}
        public string SubNumber { get;}
        public int Duration { get;}
        public DateTime Date { get;}
    }
}
