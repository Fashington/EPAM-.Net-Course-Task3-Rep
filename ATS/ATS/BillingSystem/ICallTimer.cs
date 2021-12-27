using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.BillingSystem
{
    interface ICallTimer
    {
        public int CallDurationCount { get; }

        public void Start();

        public void Stop();
    }
}
