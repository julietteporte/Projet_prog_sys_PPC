using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PPS_DLL.Business.Mapper
{
    public static class MapperRecipe
    {
        public static Data.DAO.Recipe Map(Recipe value)
        {
            return new Data.DAO.Recipe
            {
                Id = value.Id,
                Name = value.Name,
                PrepTime = value.PrepTime,
                Price = value.Price,
                Type = value.Type
            };
        }

        public static Recipe Map(Data.DAO.Recipe value)
        {
            return new Recipe
            {
                Id = value.Id,
                Name = value.Name,
                PrepTime = value.PrepTime,
                Price = value.Price,
                Type = value.Type
            };
        }
        public static List<Recipe> Map(List<Data.DAO.Recipe> value)
        {
            return (from v in value select Map(v)).ToList();
        }
    }
}
