using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.BillingSystem;
using ATS.ATSEventArgs;

namespace ATS
{
    class Station
    {
        private List<OngoingCall> OngoingCalls = new List<OngoingCall>();

        public event EventHandler<string> SendRequest;
        protected void OnRequestSend(object sender, string number)
        {
            SendRequest?.Invoke(sender, number);
        }
        public void FormRequest(object sender, string number)
        {
            Console.Write("Station.FormRequest --> ");
            OnRequestSend(sender, number);
        }

        public event EventHandler InitiateCall;
        protected void OnCallInitiation(object sender)
        {
            InitiateCall?.Invoke(sender, new EventArgs());
        }
        public void GetPortResponse(object sub, object pub)
        {
            Console.Write("Station.GetPortResponse --> ");
            Port publisher = (Port)pub;
            Port subscriber = (Port)sub;
            OngoingCalls.Add(new OngoingCall(publisher, subscriber, DateTime.Now));

            this.InitiateCall += subscriber.PortIncomingCall;
            OnCallInitiation(this);
            this.InitiateCall -= subscriber.PortIncomingCall;
        }

        public event EventHandler StartPortTimer;
        protected void OnTimerStart (object sender)
        {
            StartPortTimer?.Invoke(sender, new EventArgs());
        }
        public void EstablishConnection(object sender, EventArgs args)
        {
            Console.Write("Station.EstablishConnection --> ");

            foreach (OngoingCall call in OngoingCalls)
            {
                if (call.Sub == sender)
                {
                    StartPortTimer += call.Pub.StartTimer;
                    StartPortTimer += call.Sub.StartTimer;
                    OnTimerStart(this);
                    StartPortTimer -= call.Pub.StartTimer;
                    StartPortTimer -= call.Sub.StartTimer;
                }
            }
        }

        public event EventHandler RefreshCallState;
        protected void OnCallStateRefresh(object sender)
        {
            RefreshCallState?.Invoke(sender, new EventArgs());
        }
        public event EventHandler<CallEventArgs> LogCall;
        protected void OnCallLog(object sender, CallEventArgs args)
        {
            LogCall?.Invoke(sender, args);
        }
        public void EndConnection(object sender, int duration)
        {
            Console.Write("Station.EndConnection --> ");

            foreach (OngoingCall call in OngoingCalls)
            {
                if (sender == call.Pub)
                {
                    CallEventArgs args = new CallEventArgs();
                    args.PubNumber = call.Pub.Number;
                    args.SubNumber = call.Sub.Number;
                    args.Duration = duration;
                    args.Date = call.Date;

                    OnCallLog(this, args);

                    RefreshCallState += call.Pub.RefreshCallState;
                    OnCallStateRefresh(this);
                    RefreshCallState -= call.Pub.RefreshCallState;

                    OngoingCalls.Remove(call);
                    break;
                }
                else if (sender == call.Sub)
                {
                    CallEventArgs args = new CallEventArgs();
                    args.PubNumber = call.Pub.Number;
                    args.SubNumber = call.Sub.Number;
                    args.Duration = duration;
                    args.Date = call.Date;

                    OnCallLog(this, args);

                    RefreshCallState += call.Sub.RefreshCallState;
                    OnCallStateRefresh(this);
                    RefreshCallState -= call.Sub.RefreshCallState;

                    OngoingCalls.Remove(call);
                    break;
                }
            }
        }

        public event EventHandler GetLog;
        protected void OnGetLog(object sender)
        {
            GetLog?.Invoke(sender, new EventArgs());
        }
        public void PrintLogg()
        {
            Console.WriteLine("Station.PrintLog");
            OnGetLog(this);
        }

        public event EventHandler<FilterLogEventArgs> FilterByNumberEvent;
        protected void OnFilterByNumber(object sender, FilterLogEventArgs args)
        {
            FilterByNumberEvent?.Invoke(sender, args);
        }
        public void FilterByNumber(string number)
        {
            FilterLogEventArgs args = new FilterLogEventArgs();
            args.number = number;
            OnFilterByNumber(this, args);
        }

        public event EventHandler<FilterLogEventArgs> FilterByDateEvent;
        protected void OnFilterByDate(object sender, FilterLogEventArgs args)
        {
            FilterByDateEvent?.Invoke(sender, args);
        }
        public void FilterByDate(string year, string month, string day)
        {
            FilterLogEventArgs args = new FilterLogEventArgs();
            args.year = year;
            args.month = month;
            args.day = day;
            OnFilterByDate(this, args);
        }

        public void FilterByDate(DateTime date)
        {
            FilterLogEventArgs args = new FilterLogEventArgs();
            args.date = date;
            OnFilterByDate(this, args);
        }

        public event EventHandler<FilterLogEventArgs> FilterByCostEvent;
        protected void OnFilterByCost(object sender, FilterLogEventArgs args)
        {
            FilterByCostEvent?.Invoke(sender, args);
        }
        public void FilterByCost(double lowerLimit, double upperLimit)
        {
            FilterLogEventArgs args = new FilterLogEventArgs();
            args.costLowerLimit = lowerLimit;
            args.costUpperLimit = upperLimit;
        }
    }
}
