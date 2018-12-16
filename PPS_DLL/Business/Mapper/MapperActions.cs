using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PPS_DLL.Business.Mapper
{
    public static class MapperActions
    {
        public static Data.DAO.Actions Map(Actions value)
        {
            return new Data.DAO.Actions
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                Time = value.Time
            };
        }


        public static Actions Map(Data.DAO.Actions value)
        {
            return new Actions
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                People = value.People != null ? MapperPeople.Map(value.People) : null,
                Time = value.Time,

            };
        }
        public static List<Actions> Map(List<Data.DAO.Actions> value)
        {
            return (from v in value select Map(v)).ToList();
        }
    }
}
