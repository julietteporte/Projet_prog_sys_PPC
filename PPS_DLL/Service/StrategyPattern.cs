using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class StrategyPattern
    {
        public static void Main(String[] args)
        {
            //Préparation des stratégies
            IPresenceStrategy slowStrategy = new SlowStrategy();
            IPresenceStrategy fastStrategy = new FastStrategy();
            IPresenceStrategy normalStrategy = new NormalStrategy();

            Customer firstCustomer = new Customer(slowStrategy);
            firstCustomer.Strategy = fastStrategy;
            //firstCustomer.Eat(40);
            //firstCustomer.Pay();

        }
    }
}
