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
        public float Wallet { get; set; }
        private static HotelMaster _instance = null;
        public Square ActualSquare { get; set; }
        public Table ActualTable { get; set; }
        public int NbrWelcomedCustomer;

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

        public override void Wait(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
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
                while (this.WaitingGroups != null) //quand des groupes attendent
                {
                    ChiefRank goodRankChief = null;
                    foreach (ChiefRank rankChief in this.ChievesRank) //recherche de chef de rang
                    {
                        if (rankChief.ActualSquare == HotelMaster.Instance().GetHomeSquare())
                        {
                            goodRankChief = rankChief;
                            break;
                        }
                    }
                    
                    if (goodRankChief != null) //on assigne des tables une fois le chef de rang trouvé
                    {
                        List<Customer> group = this.WaitingGroups[0];
                        this.Welcome(group); // on accueille le groupe
                        Table table = this.AssignTable(group); // on assigne
                        goodRankChief.PlaceCustomers(this.FindSquare(table), table, group); // on ordonne au chef de rang de placer les clients a la bonne table
                        this.WaitingGroups.Remove(this.WaitingGroups[0]);  //les clients n'attendent plus
                    }
                }
            }
        }

        public void GroupCustomers()
        {
            //on crée un groupe de client parmis ceux qui attendent de faocn aléatoire
            int nbrCustomers = this.WaitingCustomers.Count;
            int coef = this.SetRandomGroupNumber();
            int nbrCustomersPerGroup = nbrCustomers / coef;
            int nbrCustomersLeft = nbrCustomers % coef;
            List<Customer> customerGroup;
            Customer customer;

            for (int i = 0; i < coef; i++)
            {
                customerGroup = new List<Customer>();
                for (int j = 0; j < nbrCustomersPerGroup; j++)
                {
                    customer = this.WaitingCustomers[j];
                    this.WaitingCustomers.Remove(customer);
                    customerGroup.Add(customer);
                }
                this.WaitingGroups.Add(customerGroup);
            }

            if (nbrCustomersLeft != 0)
            {
                customerGroup = new List<Customer>();
                foreach (Customer _customer in this.WaitingCustomers)
                {
                    this.WaitingCustomers.Remove(_customer);
                    customerGroup.Add(_customer);
                }
                this.WaitingGroups.Add(customerGroup);
            }
        }

        private Table AssignTable(List<Customer> customers) // assignation d'une table a un groupe de client
        {
            List<Table> availableTables = new List<Table>();
            Table bestTable = null;
            foreach (Square square in this.Squares)  
            {
                foreach (Table table in square.ListTables) // Ajout de table prete
                {
                    if (table.IsAvailable && table.IsDressed)
                    {
                        availableTables.Add(table);
                    }
                }
            }

            bestTable = availableTables[0]; // recuperation de la premiere table disponible 

            foreach (Table table in availableTables) // optimisation de la table choisie en fonction du nombre de client dans le groupe
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

        private Square FindSquare(Table table) // Recuperation du carre appartenant a une table
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

        private int SetRandomGroupNumber() // création d'un groupe de client aleatoire depuis la liste des clients en attente
        {
            Random r = new Random();
            int rInt = r.Next(0, this.WaitingCustomers.Count);
            return rInt;
        }
        
        public void AddRankChief(ChiefRank rankChief)
        {
            this.ChievesRank.Add(rankChief);
        }
        
        public void RemoveRankChief(ChiefRank rankChief)
        {
            this.ChievesRank.Remove(rankChief);
        }
    
        public void AddWaitingCustomer(Customer c)
        {
            this.WaitingCustomers.Add(c);
        }

        public void RemoveWaitingCustomer(Customer c)
        {
            this.WaitingCustomers.Remove(c);
        }

        public void AddBuyingCustomer(Customer c)
        {
            this.BuyingCustomers.Add(c);
        }
        
        public void RemoveBuyingCustomer(Customer c)
        {
            this.BuyingCustomers.Remove(c);
        }
        
        public Square GetHomeSquare()
        {
            return this.Squares[0];
        }
    }
}
