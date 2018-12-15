using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class CookChief
    {
        public List<Order> Orders;
        public List<Order> GroupOrders;
        public List<Order> FinishedOrders;
        public List<Cook> Cookers;
        public List<Cook> Servers;
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
        /// Methods
        /// </summary>


        public void RegroupOrder() //regroupe les commandes de la meme table dans une liste
        {
            for (int i = 0; i < this.Orders.Count; i++)
            {
                if (this.Orders[i].Table == this.Orders[i+1].Table)
                {
                    GroupOrders.Add(this.Orders[i]);
                }
            }
        }

        public void CallCook(Recipe recipe) // on demande a un cuisinier disponible de cuisiner une recette demandee
        {
            for (int i = 0; i < Orders.Count; i++) // on parcourt les commandes en attente
            {
                foreach (Cook cook in this.Cookers) // on cherche un cooker dispo
                {
                    if (cook.IsAvailable == true) // on lui ordonne de cuisiner
                    {
                        cook.Cooking(recipe);
                        cook.IsAvailable = false;
                        FinishedOrders.Add(Orders[i]); // on rajoute la commande parmi les commandes terminés
                        break;
                    }
                }
            }
        }

        public void CallServer()
        {
            while (true)
            {
                
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

        /*public void AddServer(Server s)
        {
            this.Servers.Add(s);
        }

        public void RemoveServer(Server s)
        {
            this.Servers.Remove(s);
        }*/

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
