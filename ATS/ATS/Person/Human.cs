using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Person
{
    class Human
    {
        public event EventHandler<string> DialingNumber;
        protected void OnNumberDialing(object sender, string number)
        {
            DialingNumber(sender, number);
        }
        public void InitiateCall(string number)
        {
            OnNumberDialing(this, number);
        }

        public void IncomingCallDecision(object sender, EventArgs args)
        {
            Console.Write("Human.IncomingCallDecision --> ");

            Random rnd = new Random();
            int value = rnd.Next(0, 2);

            if (value > 0)
            {
                CallAnswer();
            }
            else
            {
                CallReject();
            }
        }

        public event EventHandler Answer;
        protected void OnAnswer(object sender)
        {
            Console.Write("Human.CallAnswer --> ");
            Answer?.Invoke(sender, new EventArgs());
        }
        public void CallAnswer()
        {
            OnAnswer(this);
        }

        public event EventHandler Reject;
        protected void OnReject(object sender)
        {
            Reject?.Invoke(sender, new EventArgs());
        }
        public void CallReject()
        {
            Console.Write("Human.CallReject --> ");
            OnReject(this);
        }
    }
}
