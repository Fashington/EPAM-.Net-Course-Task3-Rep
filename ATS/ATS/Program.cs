using System;
using System.Threading;
using System.Threading.Tasks;
using ATS.BillingSystem;
using ATS.Users;
using ATS.Tariffs;
using ATS.Person;

namespace ATS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creation

            Human[] users = new Human[4] { new Human(), new Human(), new Human(), new Human() };
            Phone[] phones = new Phone[4] { new Phone(), new Phone(), new Phone(), new Phone() };
            Port[] ports = new Port[4] { new Port(), new Port(), new Port(), new Port() };
            Station station = new Station();
            UserBase userBase = new UserBase();
            CallLogger logger = new CallLogger();

            // Binidng

            station.LogCall += logger.AddRecord;
            station.GetLog += logger.PrintLog;
            station.FilterByNumberEvent += logger.FilterByNumber;
            station.FilterByDateEvent += logger.FilterByDate;
            station.FilterByCostEvent += logger.FilterByCost;

            logger.TariffPlanRequest += userBase.TariffPlanRequestResponse;

            userBase.RequestResponse += logger.NewRecord;

            for (int i = 0; i < ports.Length; i++)
            {
                users[i].Answer += phones[i].PhoneAnswerCall;
                users[i].Reject += phones[i].PhoneEndCall;
                users[i].DialingNumber += phones[i].PhoneCall;

                phones[i].ChangePortState += ports[i].PortStateChange;
                phones[i].Call += ports[i].PortCall;
                phones[i].NotifyUser += users[i].IncomingCallDecision;
                phones[i].SendConformation += ports[i].PortConformationSend;
                phones[i].CallEnded += ports[i].PortEndCall;

                ports[i].Call += station.FormRequest;
                ports[i].StationRequestResponse += station.GetPortResponse;
                ports[i].RedirectCallToPhone += phones[i].CallIncome;
                ports[i].SendConformation += station.EstablishConnection;
                ports[i].EndCall += station.EndConnection;

                station.SendRequest += ports[i].GetStationRequest;

                ports[i].SetNumber($"1{i}");
                userBase.AddUser($"1{ i }", $"1{i}", TariffPlanEnum.DefaultTarifPlan);

                phones[i].PhoneAttach();
            }

            // Modelling calls

            // Trying to call the same number from different phones. Results can depend on was call rejected, or accepted.
            Console.WriteLine("\n10 >> 13\n");
            users[0].InitiateCall("13");
            Console.WriteLine("\n11 >> 13\n");
            users[1].InitiateCall("13");
            users[3].CallReject();
            if (ports[0].CallStatus == PortStates.CallState.Busy)
            {
                users[0].CallReject();
            }

            // Trying to call a number, that doesn`t exist.
            Console.WriteLine("\n\n10 >> 33\n");
            users[0].InitiateCall("33");

            //Trying to call to ourselvs
            Console.WriteLine("\n\n12 >> 12\n");
            users[2].InitiateCall("12");

            // Trying to call to a disconected port
            phones[2].PhoneDetach();
            Console.WriteLine("\n\n11 >> 12\n");
            users[1].InitiateCall("12");
            phones[2].PhoneAttach();

            Console.WriteLine("\n\nPrintin call loggs:\n");
            station.PrintLogg();

            Console.WriteLine("\nFilter call list by number 13:\n");
            station.FilterByNumber("13");

            Console.WriteLine("\nFiltering call list by date\n");
            station.FilterByDate("2021", "12", "27");
            Console.WriteLine("\nFiltering call list by date\n");
            station.FilterByDate(DateTime.Now);

            Console.WriteLine("\nFiltering call list by cost\n");
            station.FilterByCost(0.0, 0.56);

            // Unbinding

            station.LogCall -= logger.AddRecord;
            station.GetLog -= logger.PrintLog;
            station.FilterByNumberEvent -= logger.FilterByNumber;
            station.FilterByDateEvent -= logger.FilterByDate;
            station.FilterByCostEvent -= logger.FilterByCost;

            logger.TariffPlanRequest -= userBase.TariffPlanRequestResponse;

            userBase.RequestResponse -= logger.NewRecord;

            for (int i = 0; i < ports.Length; i++)
            {
                phones[i].PhoneDetach();

                users[i].Answer -= phones[i].PhoneAnswerCall;
                users[i].Reject -= phones[i].PhoneEndCall;
                users[i].DialingNumber -= phones[i].PhoneCall;

                phones[i].ChangePortState -= ports[i].PortStateChange;
                phones[i].Call -= ports[i].PortCall;
                phones[i].NotifyUser -= users[i].IncomingCallDecision;
                phones[i].SendConformation -= ports[i].PortConformationSend;
                phones[i].CallEnded -= ports[i].PortEndCall;

                ports[i].Call -= station.FormRequest;
                ports[i].StationRequestResponse -= station.GetPortResponse;
                ports[i].RedirectCallToPhone -= phones[i].CallIncome;
                ports[i].SendConformation -= station.EstablishConnection;
                ports[i].EndCall -= station.EndConnection;

                station.SendRequest -= ports[i].GetStationRequest;
            }
        }
    }
}
