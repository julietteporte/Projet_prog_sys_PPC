using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Customer : People, IMobile
    {
        public IPresenceStrategy Strategy { get; set; }
        public bool IsReserved { get; set; }
        public int NbrSatisfiedCustomer { get; set; }
        public Table Table { get; set; }
        private int _presenceTime;
        public Recipe Recipe;
        public List<Order> Orders;

        public Customer(IPresenceStrategy strategy)
        {

        }

        /// <summary>
        /// Implémentation People
        /// </summary>
        public override int Id
        {
            get { return Id; }
        }

        public override void Wait(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
        }

        /// <summary>
        /// Implémentation IMobile
        /// </summary>
        public Square ActualSquare { get; set; }
        public Table ActualTable { get; set; }
        public void Move(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
        }


        /// <summary>
        /// Methodes 
        /// </summary>

        public void Pay() //on amene le client au maitre d'hotel et il paye sa commande
        {
            NbrSatisfiedCustomer++;
            var priceToPay = Recipe.Price;
            this.Move(HotelMaster.Instance().GetHomeSquare(), null);
            HotelMaster.Instance().Wallet = HotelMaster.Instance().Wallet + priceToPay;
        }

        public void Eat(int presenceTime) // le client mange en fonction du temps de sa strategie
        {
            _presenceTime = Strategy.GetPresenceTime(presenceTime);
        }

        public Recipe GetRandomRecipe() // retourne une recette aleatoire
        {
            
            return null;
        }
    }
}
