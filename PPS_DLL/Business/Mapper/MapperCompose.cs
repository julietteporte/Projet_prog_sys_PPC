using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PPS_DLL.Business.Mapper
{
    public static class MapperCompose
    {
        public static Data.DAO.Compose Map(Compose value)
        {
            return new Data.DAO.Compose
            {
                Actions = value.Actions != null ? MapperActions.Map(value.Actions) : null,
                Scenario = value.Scenario != null ? MapperScenario.Map(value.Scenario) : null

            };
        }
        public static Compose Map(Data.DAO.Compose value)
        {
            return new Compose
            {
                Actions = value.Actions != null ? MapperActions.Map(value.Actions) : null,
                Scenario = value.Scenario != null ? MapperScenario.Map(value.Scenario) : null

            };
        }
        public static List<Compose> Map(List<Data.DAO.Compose> value)
        {
            return (from v in value select Map(v)).ToList();
        }
    }
}
