using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class CookChief : People
    {
        public List<Order> Orders;
        public int nbrDoneRecipe;

        public CookChief()
        {
        }

        private static CookChief uniqueInstance;

        private void Singleton()
        {
            // TODO implement here
        }

        /// <summary>
        /// Implémentation People
        /// </summary>
        public override int Id
        {
            get { return Id; }
        }

        public override void Wait()
        {

        }


        /// <summary>
        /// @return
        /// </summary>
        public static CookChief getInstance()
        {
            // TODO implement here
            return null;
        }

        public void CancelDish()
        {
            // TODO implement here
        }

        public void RegroupOrder()
        {
            // TODO implement here
        }

        public void OrderCook()
        {
            // TODO implement here
        }
    }
}
