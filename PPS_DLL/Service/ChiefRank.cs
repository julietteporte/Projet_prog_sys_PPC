using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class ChiefRank : People, IMobile
    {

        public Square AttributedSquare { get; set; }
        public bool IsAvailable { get; set; }
        public List<Order> Orders { get; set; }
        public Square ActualSquare { get; set; }
        public Table ActualTable { get; set; }

        public ChiefRank(Square attributedSquare)
        {
            this.AttributedSquare = attributedSquare;
            this.IsAvailable = true;
            this.Orders = new List<Order>();
        }

        /// <summary>
        /// Implementation People
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
        /// Implementation IMobile
        /// </summary>
        
        public void Move(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
        }

        /// <summary>
        /// Method
        /// </summary>

        public void Dress(Table table) // Nettoyage et mise des couverts 
        {
            if (table.IsCleaned == false)
            {
                table.IsCleaned = true;
            }
            else if (table.IsDressed == false)
            {
                table.IsDressed = true;
            }
        }


        public void GiveMenu(Table table) //Renvoit une recette aleatoire depuis le client 
        {
            foreach (Customer customer in table.Customers) 
            {
                customer.GetRandomRecipe(); // pour chaque client sur la table, on recupere une recette aleatoire
            }
            //wait
        }

        public void PlaceCustomers(Square square, Table table, List<Customer> customers) // Deplacement du client a sa table et on lui donne le menu
        {
            foreach (Customer customer in customers)
            {
                customer.Move(square, table);
                this.GiveMenu(table);
            }
        }


        public Order TakeOrder(Customer customer) // on crée une commande depuis un client
        {
            Order order = new Order(customer.ActualTable, customer, customer.Recipe);
            return order;
        }
    }
}
