using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class HotelMaster : People
    {
        public List<ChiefRank> ChievesRank { get; }
        public List<Customer> WaitingCustomers { get; }
        public List<List<Customer>> WaitingGroups { get; }
        public List<Customer> BuyingCustomers { get; }
        public List<Square> Squares { get; }
        public float Wallet;
        private static HotelMaster _instance = null;
        private int NbrWelcomedCustomer;

        /// <summary>
        /// Singleton
        /// </summary>
        private HotelMaster()
        {
            this.Wallet = 0;
            this.ChievesRank = new List<ChiefRank>();
            this.BuyingCustomers = new List<Customer>();
            this.WaitingCustomers = new List<Customer>();
            this.WaitingGroups = new List<List<Customer>>();
            this.Squares = new List<Square>
            {
                new Square(),
                new Square(),
                new Square()
            };
        }

        public static HotelMaster Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HotelMaster();
                }
                return _instance;
            }
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
        /// Méthodes 
        /// </summary>

        public void Welcome(List<Customer> customers)
        {
            this.NbrWelcomedCustomer++;
        }

        public void AssignTable(Customer customer)
        {

        }

        public void VerifyReservation(Customer customer)
        {

        }
    }
}
