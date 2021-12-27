using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Tariffs;

namespace ATS.Users
{
    class User : IUser
    {
        public string Name { get; private set; }
        public string Number { get; private set; }
        public TariffPlanEnum UserTariffPlan { get; private set; }

        public User(string name, string number, TariffPlanEnum tariff)
        {
            Name = name;
            Number = number;
            UserTariffPlan = tariff;
        }
    }
}
