﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class CookChief
    {
        public List<Order> Orders { get; set; }
        public List<Order> GroupOrders { get; set; }
        public List<Order> FinishedOrders { get; set; }
        public List<Cook> Cookers { get; set; }
        public List<Server> Servers { get; set; }
        public int nbrDoneRecipe { get; set; }

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
                        FinishedOrders.Add(Orders[i]); // on rajoute la commande parmis les commandes terminés
                        break;
                    }
                }
            }
        }

        public void CallServer()
        {
            while (true)
            {
                while (this.FinishedOrders != null) // quand des commandes terminées sont a servir
                {
                    Server goodServer = null;
                    foreach (Server server in this.Servers)
                    {
                        if (server.IsAvailable == true)
                        {
                            goodServer = server;
                            break;
                        }
                    }

                    if (goodServer != null) // on amene le plat de la commande
                    {
                        goodServer.Serve(FinishedOrders[0]);
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

        public void AddServer(Server s)
        {
            this.Servers.Add(s);
        }

        public void RemoveServer(Server s)
        {
            this.Servers.Remove(s);
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
