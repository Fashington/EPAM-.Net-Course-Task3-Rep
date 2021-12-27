using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Tariffs;

namespace ATS.ATSEventArgs
{
    class UserBaseEventArgs
    {
        public TariffPlanEnum Tariff = TariffPlanEnum.DefaultTarifPlan;
        public string PubNumber { get; set; }
        public string SubNumber { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
    }
}
