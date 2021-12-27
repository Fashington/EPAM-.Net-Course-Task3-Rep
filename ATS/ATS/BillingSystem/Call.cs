using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.BillingSystem
{
    class Call : ICall
    {
        public string PubNumber { get;  private set; }
        public string SubNumber { get; private set; }
        public double Cost { get; private set; }
        public int Duration { get; private set; }
        public string DurationFormat { get; private set; }
        public DateTime Date { get; private set; }

        public Call(string pub, string sub, double cost, int duration, DateTime date )
        {
            PubNumber = pub;
            SubNumber = sub;
            Duration = duration;
            Cost = cost;
            Date = date;

            TimeSpan time = TimeSpan.FromSeconds(duration);
            DurationFormat = $"{time.Hours}h {time.Minutes}m {time.Seconds}s";
        }
    }
}
