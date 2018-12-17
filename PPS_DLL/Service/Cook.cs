using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Cook
    {
        public bool IsAvailable { get; set; }

        public Cook()
        {

        }

        public void Cooking(Recipe recipe) // cuisine la recette demande 
        {
            if (IsAvailable == true)
            {
                var time = recipe.TimePrepare; // temps pour le thread
                recipe.IsFinished = true;
            }
        }
    }
}
