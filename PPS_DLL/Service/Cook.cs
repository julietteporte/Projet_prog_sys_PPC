using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Cook
    {

        Recipe recipe = new Recipe();
        public int Id;
        public bool HaveToCook;
        public bool IsAvailable;

        public Cook()
        {

        }

        public void Cooking(Recipe recipe)
        {
            if (HaveToCook == true && IsAvailable == true)
            {
                var timeCook = recipe.TimePrepare;
                var name = recipe.RecipeName;
                recipe.IsFinished = true;
            }
        }
    }
}
