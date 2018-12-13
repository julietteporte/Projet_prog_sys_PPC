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
        public List<Order> GroupOrders;
        public List<Cook> Cookers;
        public int nbrDoneRecipe;

        private CookChief()
        {

        }

        private static CookChief _instance = null;

        public static CookChief Instance()
        {
            if (_instance == null)
            {
                _instance = new CookChief();
            }

            return _instance;
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
        /// Methods
        /// </summary>

        
        public void RegroupOrder()
        {
            foreach (Order o in this.Orders) // on parcourt toutes les orders
            {
                
            }
        }

        public void CallCook(Recipe recipe)
        {
            while (true)
            {
                while (this.Orders != null) //quand on a des orders en attente
                {
                    Cook CookForOrder = null;
                    foreach (Cook cook in this.Cookers) // on cherche un cooker dispo
                    {
                        if (cook.IsAvailable == true)
                        {
                            CookForOrder = cook;
                            break;
                        }
                    }

                    if (CookForOrder != null)
                    {
                        CookForOrder.Cooking(recipe);
                        CookForOrder.IsAvailable = false;
                    }
                }
            }
        }

        public void AddCooker(Cook c)
        {
            this.Cookers.Add(c);
        }

        public void RemoveCooker(Cook c)
        {
            this.Cookers.Remove(c);
        }

        public void AddOrder(Order o)
        {
            this.Orders.Add(o);
        }

        public void RemoveOrder(Order o)
        {
            this.Orders.Remove(o);
        }
    }
}
