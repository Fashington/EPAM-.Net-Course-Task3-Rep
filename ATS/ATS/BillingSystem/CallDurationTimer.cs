using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ATS.BillingSystem
{
    class CallDurationTimer : ICallTimer
    {
        private Timer timer = new Timer(1000);
        public int CallDurationCount { get; private set; }

        public bool FakeTimerEnabled { get; private set; }
        public int FakeCallDurationCount = 0;

        public CallDurationTimer()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CallDurationCount++;
        }

        public void Start()
        {
            CallDurationCount = 0;
            timer.Enabled = true;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Enabled = false;
        }

        public bool IsEnabled()
        {
            return timer.Enabled;
        }

        public void FakeTimerStart()
        {
            FakeTimerEnabled = true;
        }

        public void FakeTimerStop()
        {
            Random rnd = new Random();
            this.FakeCallDurationCount = rnd.Next(50, 634);
            FakeTimerEnabled = false;
        }
    }
}
