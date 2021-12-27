using System;
using ATS.PortStates;
using ATS.BillingSystem;
using ATS.ATSEventArgs;

namespace ATS
{
    class Port  : IPort
    {
        CallDurationTimer timer = new CallDurationTimer();

        public PortState PortStatus { get; private set; }
        public CallState CallStatus { get; private set; }

        public string Number { get; private set; }

        public void SetNumber(string number)
        {
            this.Number = number;
        }

        public void PortStateChange(object sender, PortState state)
        {
            this.PortStatus = state;
            Console.WriteLine($"{this}|{PortStatus}");
        }

        public event EventHandler<string> Call;
        protected void OnCall(object sender, string number)
        {
            Call?.Invoke(sender, number);
        }
        public void PortCall(object sender, string number)
        {
            if (number != Number)
            {
                this.CallStatus = CallState.Busy;
                Console.Write("Port.PortCall --> ");
                OnCall(this, number);
            }
        }

        public event EventHandler<object> StationRequestResponse;
        protected void OnStationRequestResponse(object thisPort, object sender)
        {
            StationRequestResponse?.Invoke(thisPort, sender);
        }
        public void GetStationRequest(object sender, string number)
        {
            Console.Write("Port.GetStationRequest --> ");
            if (PortStatus == PortState.Online)
            {
                if (Number == number)
                {
                    if (CallStatus == CallState.Free)
                    {
                        OnStationRequestResponse(this, sender);
                    }
                }
            }
        }

        public event EventHandler RedirectCallToPhone;
        protected void OnCallToPhoneRedirection(object sender)
        {
            RedirectCallToPhone?.Invoke(sender, new EventArgs());
        }
        public void PortIncomingCall(object sender, EventArgs args)
        {
            this.CallStatus = CallState.Busy;
            Console.Write("Port.PortIncomingCall --> ");
            OnCallToPhoneRedirection(this);
        }

        public event EventHandler SendConformation;
        protected void OnConformationSend(object sender)
        {
            SendConformation?.Invoke(sender, new EventArgs());
        }
        public void PortConformationSend(object sender, EventArgs args)
        {
            Console.Write("Port.PortConformationSend --> ");
            OnConformationSend(this);
        }

        public void StartTimer(object sender, EventArgs args)
        {
            timer.FakeTimerStart();
        }

        public event EventHandler<int> EndCall;
        protected void OnCallEnded(object sender, int duration)
        {
            EndCall?.Invoke(sender, duration);
        }
        public void PortEndCall(object sender, EventArgs args)
        {
            if (timer.FakeTimerEnabled == true)
            {
                timer.FakeTimerStop();
            }

            CallStatus = CallState.Free;
            Console.Write("Port.PortEndCall --> ");
            OnCallEnded(this, timer.FakeCallDurationCount);
        }

        public void RefreshCallState(object sender, EventArgs args)
        {
            CallStatus = CallState.Free;
            Console.Write("Port.RefreshCallState\n");
        }
    }
}
