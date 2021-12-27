using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Tariffs
{
    class TariffPlan
    {
        public double GetCost(TariffPlanEnum tariff)
        {
            switch (tariff)
            {
                case TariffPlanEnum.DefaultTarifPlan:
                    return 0.56;
                    break;
                default:
                    return 0.56;
                    break;
            }
        }
    }
}
