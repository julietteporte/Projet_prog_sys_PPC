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
        private IList<string> order;
        private int _presenceTime;
        public Recipe Recipe;

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

        public override void Wait()
        {

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

        public void Pay()
        {
            NbrSatisfiedCustomer++;
            var price = Recipe.Price;
            HotelMaster.Instance.Wallet = HotelMaster.Instance.Wallet + price;
        }

        public void OrderMenu()
        {
            // TODO implement here
        }

        public void Eat(int presenceTime)
        {
            _presenceTime = Strategy.GetPresenceTime(presenceTime);
            Console.WriteLine("Temps passé à manger : " + _presenceTime);
        }

        public Recipe GetRandomRecipe()
        {
            //Recipe AleaRecipe = new Recipe();
            //return AleaRecipe;
            return null;
        }
    }
}
