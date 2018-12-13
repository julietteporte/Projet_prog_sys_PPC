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

        public static HotelMaster Instance()
        {
            if (_instance == null)
            {
                _instance = new HotelMaster();
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
        /// Méthodes 
        /// </summary>

        public void Welcome(List<Customer> customers)
        {
            this.NbrWelcomedCustomer++;
        }

        public void Call()
        {
            while (true)
            {
                this.GroupCustomers();
                while (this.WaitingGroups != null) //while there are groups waiting outside the restaurant entrance
                {
                    ChiefRank goodRankChief = null;
                    foreach (ChiefRank rankChief in this.ChievesRank) //find a rankchief
                    {
                        if (rankChief.ActualSquare == HotelMaster.Instance().GetHomeSquare())
                        {
                            goodRankChief = rankChief;
                            break;
                        }
                    }

                    if (goodRankChief != null)
                    {
                        List<Customer> group = this.WaitingGroups[0];
                        this.Welcome(group); //say hello
                        Table table = this.AssignTable(group);
                        goodRankChief.PlaceCustomers(this.FindSquare(table), table, group); // place customers group to designed table
                        this.WaitingGroups.Remove(this.WaitingGroups[0]);  //customer groups aren't waiting anymore
                    }
                }
            }
        }

        public void GroupCustomers()
        {
            //divide a list of waiting customers into a random number of groups
            int nbrCustomers = this.WaitingCustomers.Count;
            int coef = this.SetRandomGroupNumber();
            int nbrCustomersPerGroup = nbrCustomers / coef;
            int nbrCustomersLeft = nbrCustomers % coef;
            List<Customer> customerGroup;
            Customer c;

            for (int i = 0; i < coef; i++)
            {
                customerGroup = new List<Customer>();
                for (int j = 0; j < nbrCustomersPerGroup; j++)
                {
                    c = this.WaitingCustomers[j];
                    this.WaitingCustomers.Remove(c);
                    customerGroup.Add(c);
                }
                this.WaitingGroups.Add(customerGroup);
            }

            if (nbrCustomersLeft != 0)
            {
                customerGroup = new List<Customer>();
                foreach (Customer customer in this.WaitingCustomers)
                {
                    this.WaitingCustomers.Remove(customer);
                    customerGroup.Add(customer);
                }
                this.WaitingGroups.Add(customerGroup);
            }
        }

        private Table AssignTable(List<Customer> customers)
        {
            List<Table> availableTables = new List<Table>();
            Table bestTable = null;
            foreach (Square square in this.Squares)
            {
                foreach (Table table in square.ListTables)
                {
                    if (table.IsAvailable && table.IsDressed)
                    {
                        availableTables.Add(table);
                    }
                }
            }

            bestTable = availableTables[0];

            foreach (Table table in availableTables)
            {
                if (table.Capacity >= customers.Count && table.Capacity < bestTable.Capacity)
                {
                    bestTable = table;
                }
            }

            bestTable.Customers = customers;
            bestTable.IsAvailable = false;
            return bestTable;
        }

        private Square FindSquare(Table table)
        {
            foreach (Square square in this.Squares)
            {
                foreach (Table t in square.ListTables)
                {
                    if (t == table) return square;
                }
            }

            return null;
        }

        private int SetRandomGroupNumber()
        {
            Random r = new Random();
            int rInt = r.Next(0, this.WaitingCustomers.Count);
            return rInt;
        }

        /// <summary>
        /// @param RankChief 
        /// @return
        /// </summary>
        public void AddRankChief(ChiefRank rankChief)
        {
            this.ChievesRank.Add(rankChief);
        }

        /// <summary>
        /// @param RankChief 
        /// @return
        /// </summary>
        public void RemoveRankChief(ChiefRank rankChief)
        {
            this.ChievesRank.Remove(rankChief);
        }

        /// <summary>
        /// @return
        /// </summary>
        public void AddWaitingCustomer(Customer c)
        {
            this.WaitingCustomers.Add(c);
        }

        /// <summary>
        /// @return
        /// </summary>
        public void RemoveWaitingCustomer(Customer c)
        {
            this.WaitingCustomers.Remove(c);
        }

        /// <summary>
        /// @return
        /// </summary>
        public void AddBuyingCustomer(Customer c)
        {
            this.BuyingCustomers.Add(c);
        }

        /// <summary>
        /// @return
        /// </summary>
        public void RemoveBuyingCustomer(Customer c)
        {
            this.BuyingCustomers.Remove(c);
        }

        /// <summary>
        /// @param Customer 
        /// @return
        /// </summary>
        public void CollectMoney(Customer customer)
        {
            foreach (Order order in customer.Orders)
            {
                foreach (Recipe recipe in order.Recipes)
                {
                    this.Wallet += recipe.Price;
                }
            }
        }

        public Square GetHomeSquare()
        {
            return this.Squares[0];
        }
    }
}
