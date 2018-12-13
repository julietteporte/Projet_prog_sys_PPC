using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class ChiefRank : People, IMobile
    {

        public Square AttributedSquare;
        public bool IsAvailable;
        public List<Order> Orders;

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

        public override void Wait()
        {

        }

        /// <summary>
        /// Implementation IMobile
        /// </summary>
        public Square ActualSquare { get; set; }
        public Table ActualTable { get; set; }
        public void Move(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
        }

        /// <summary>
        /// Method
        /// </summary>

        public void Dress(Table table)
        {
            if (table.IsCleaned == false)
            {
                Console.WriteLine("Table nettoyée");
                table.IsCleaned = true;
            }
            else if (table.IsDressed == false)
            {
                table.IsDressed = true;
            }
        }


        public void GiveMenu(Table table)
        {
            foreach (Customer customer in table.Customers)
            {
                customer.GetRandomRecipe();
                //code here
            }
            //wait
        }


        public Order TakeOrder(Customer customer)
        {
            Order order = new Order(customer.ActualTable);
            return order;
        }
    }
}
