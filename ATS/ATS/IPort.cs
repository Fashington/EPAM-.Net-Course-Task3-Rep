using ATS.PortStates;

namespace ATS
{
    interface IPort
    {
        public PortState PortStatus { get;}
        public CallState CallStatus { get;}
        public string Number { get;}

        public void SetNumber(string number);
    }
}
