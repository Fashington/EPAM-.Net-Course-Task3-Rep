using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Tariffs;
using ATS.Users;
using ATS.ATSEventArgs;

namespace ATS.BillingSystem
{
    class CallLogger
    {
        private List<Call> CallLog = new List<Call>();

        public event EventHandler<CallEventArgs> TariffPlanRequest;
        protected void OnTariffPlanRequest(object sender, CallEventArgs args)
        {
            TariffPlanRequest?.Invoke(sender, args);
        }
        public void AddRecord(object sender, CallEventArgs args)
        {
            Console.Write("CallLoger.AddRecord --> ");
            OnTariffPlanRequest(this, args);
        }

        public void NewRecord(object sender, UserBaseEventArgs args)
        {
            Console.Write("CallLoger.NewRecord --> ");

            CallLog.Add(new Call(args.PubNumber, args.SubNumber, CalculateCallCost(args.Duration, args.Tariff), args.Duration, args.Date));
        }

        private double CalculateCallCost(int duration, TariffPlanEnum tariff)
        {
            TariffPlan getCostProvider = new TariffPlan();

            return Math.Round((Math.Ceiling(Convert.ToDouble(duration) / 60.0) * getCostProvider.GetCost(tariff)), 2);
        }

        public void PrintLog(object sender, EventArgs args)
        {
            PrintLog(CallLog);
        }
        private void PrintLog(List<Call> list)
        {
            foreach (Call call in list)
            {
                Console.WriteLine($"| Pub: {call.PubNumber} | Sub: {call.SubNumber} | Cost: {call.Cost} BYN | Duration {call.DurationFormat} | Date: {call.Date} |");
            }
        }

        public void FilterByNumber(object sender, FilterLogEventArgs args)
        {
            var filteredList = CallLog.Where(i => i.PubNumber == args.number || i.SubNumber == args.number).ToList();

            PrintLog(filteredList);
        }

        public void FilterByDate(object sender, FilterLogEventArgs args)
        {
            DateTime date;
            if (args.day != null)
            {
                string dateString = $"{args.year}/{args.month}/{args.day}";
                date = Convert.ToDateTime(dateString);
            }
            else
            {
                date = args.date;
            }
            var filteredList = CallLog.Where(i => i.Date.Date == date.Date).ToList();

            PrintLog(filteredList);
        }

        public void FilterByCost(object sender, FilterLogEventArgs args)
        {
            var filteredList = CallLog.Where(i => i.Cost >= args.costLowerLimit && i.Cost <= args.costUpperLimit).ToList();

            PrintLog(filteredList);
        }
    }
}
