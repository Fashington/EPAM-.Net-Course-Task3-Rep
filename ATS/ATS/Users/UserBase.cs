using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Tariffs;
using ATS.BillingSystem;
using ATS.ATSEventArgs;

namespace ATS.Users
{
    class UserBase
    {
        private List<User> users = new List<User>();

        public void AddUser(string name, string number, TariffPlanEnum tariff)
        {
            users.Add(new User(name, number, tariff));
        }

        public event EventHandler<UserBaseEventArgs> RequestResponse;
        protected void OnResponse(object sender, UserBaseEventArgs args)
        {
            RequestResponse?.Invoke(sender, args);
        }
        public void TariffPlanRequestResponse(object sender, CallEventArgs args)
        {
            Console.Write("TariffPlanRequestResponse --> ");

            if (users.Count > 0)
            {
                IEnumerable<User> getUserTariff = from i in users
                                                  where i.Number == args.PubNumber
                                                  select i;
                UserBaseEventArgs userBaseArgs = new UserBaseEventArgs();
                userBaseArgs.Tariff = getUserTariff.ElementAt(0).UserTariffPlan;
                userBaseArgs.PubNumber = args.PubNumber;
                userBaseArgs.SubNumber = args.SubNumber;
                userBaseArgs.Duration = args.Duration;
                userBaseArgs.Date = args.Date;

                OnResponse(this, userBaseArgs);
            }
        }
    }
}
