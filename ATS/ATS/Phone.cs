using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.PortStates;

namespace ATS
{
    class Phone
    {
        public event EventHandler<PortState> ChangePortState;
        protected void OnPortStateChange(object sender, PortState state)
        {
            ChangePortState?.Invoke(sender, state);
        }
        public void PhoneAttach()
        {
            OnPortStateChange(this, PortState.Online);
        }

        public void PhoneDetach()
        {
            OnPortStateChange(this, PortState.Offline);
        }

        public event EventHandler<string> Call;
        protected void OnCall(object sender, string number)
        {
            Call?.Invoke(sender, number);
        }
        public void PhoneCall(object sender, string number)
        {
            Console.Write("Phone.PhoneCall --> ");
            OnCall(this, number);
        }

        public event EventHandler NotifyUser;
        protected void OnUserNotification(object sender)
        {
            NotifyUser?.Invoke(sender, new EventArgs());
        }
        public void CallIncome(object sender, EventArgs args)
        {
            Console.Write("Phone.CallIncome --> ");
            OnUserNotification(this);
        }

        public event EventHandler SendConformation;
        protected void OnConformationSend(object sender)
        {
            SendConformation?.Invoke(sender, new EventArgs());
        }
        public void PhoneAnswerCall(object sender, EventArgs args)
        {
            Console.Write("Phone.PhoneAnswerCall --> ");
            OnConformationSend(this);
        }

        public event EventHandler CallEnded;
        protected void OnCallEnded(object sender)
        {
            CallEnded?.Invoke(sender, new EventArgs());
        }
        public void PhoneEndCall(object sender, EventArgs args)
        {
            Console.Write("Phone.PhoneEndCall --> ");
            OnCallEnded(this);
        }
    }
}
